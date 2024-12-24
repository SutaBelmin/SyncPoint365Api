using AutoMapper;
using FluentValidation;
using SyncPoint365.Core.DTOs.Users;
using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;
using SyncPoint365.Service.Helpers;
using X.PagedList;

namespace SyncPoint365.Service.Services
{
    public class UsersService : BaseService<User, UserDTO, UserAddDTO, UserUpdateDTO>, IUsersService
    {
        private readonly IUsersRepository _repository;
        protected readonly IMapper _mapper;

        public UsersService(IUsersRepository repository, IMapper mapper, IValidator<UserAddDTO> addValidator, IValidator<UserUpdateDTO> updateValidator) : base(repository, mapper, addValidator, updateValidator)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task AddAsync(UserAddDTO dto, CancellationToken cancellationToken = default)
        {
            await AddValidator.ValidateAndThrowAsync(dto, cancellationToken);

            var entity = Mapper.Map<User>(dto);

            entity.PasswordSalt = Cryptography.GenerateSalt(); ;
            entity.PasswordHash = Cryptography.GenerateHash(dto.Password, entity.PasswordSalt);


            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<UserDTO>> GetUsersListAsync(CancellationToken cancellationToken = default)
        {
            var users = await _repository.GetUsersListAsync();

            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public Task<bool> EmailExists(string email)
        {
            return _repository.EmailExists(email);
        }

        public async Task<IPagedList<UserDTO>> GetUsersPagedListAsync(bool? isActive, string? query = null, int? roleId = null, string? orderBy = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            var usersList = await _repository.GetUsersPagedListAsync(isActive, query, roleId, orderBy, page, pageSize, cancellationToken);

            var dtos = Mapper.Map<List<UserDTO>>(usersList);

            return new PagedList<UserDTO>(usersList, dtos);
        }

        public override async Task UpdateAsync(UserUpdateDTO dto, CancellationToken cancellationToken = default)
        {
            await UpdateValidator.ValidateAndThrowAsync(dto, cancellationToken);

            var entity = await _repository.GetByIdAsync(dto.Id, cancellationToken);
            if (entity == null)
            {
                throw new KeyNotFoundException();
            }

            Mapper.Map(dto, entity);

            _repository.Update(entity);
            await _repository.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> ChangeUserStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await _repository.GetByUserIdAsync(id, cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found!");
            }

            user.IsActive = !user.IsActive;
            _repository.Update(user);
            await _repository.SaveChangesAsync(cancellationToken);

            return user.IsActive;
        }
        public async Task<bool> ChangePasswordAsync(int id, string password, CancellationToken cancellationToken = default)
        {
            var user = await _repository.GetByUserIdAsync(id, cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found!");
            }

            user.PasswordSalt = Cryptography.GenerateSalt();
            user.PasswordHash = Cryptography.GenerateHash(password, user.PasswordSalt);

            _repository.Update(user, cancellationToken);

            await _repository.SaveChangesAsync(cancellationToken);

            return true;
        }
        public async Task<string> UploadProfilePictureAsync(FileUploadRequest request, CancellationToken cancellationToken = default)
        {
            if (request.File == null || request.File.Length == 0)
            {
                throw new Exception("Invalid file!");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(request.File.FileName);

            if (!allowedExtensions.Contains(extension.ToLower()))
            {
                throw new Exception("Unsupported file format!");
            }

            if (!request.File.ContentType.StartsWith("image/"))
            {
                throw new Exception("Invalid file type!");
            }

            var user = await _repository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found!");
            }

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";
            var relativePath = Path.Combine("uploads", uniqueFileName);
            var filePath = Path.Combine("wwwroot", relativePath);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream, cancellationToken);
            }

            user.ImagePath = relativePath;
            _repository.Update(user);
            await _repository.SaveChangesAsync(cancellationToken);

            return relativePath;
        }
    }
}
