using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SyncPoint365.Core.DTOs.Users;
using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Enums;
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
        private readonly IConfiguration _configuration;
        public UsersService(IUsersRepository repository, IMapper mapper, IValidator<UserAddDTO> addValidator, IValidator<UserUpdateDTO> updateValidator, IConfiguration configuration) : base(repository, mapper, addValidator, updateValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public override async Task AddAsync([FromForm] UserAddDTO dto, CancellationToken cancellationToken = default)
        {
            await AddValidator.ValidateAndThrowAsync(dto, cancellationToken);

            var entity = Mapper.Map<User>(dto);

            entity.IsActive = true;

            entity.PasswordSalt = Cryptography.GenerateSalt(); ;
            entity.PasswordHash = Cryptography.GenerateHash(dto.Password, entity.PasswordSalt);

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                entity.ImagePath = await HandleImageUpload(dto.ImageFile, entity.Id);
            }

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

        public async Task<IPagedList<UserDTO>> GetUsersPagedListAsync(bool? isActive, string? query = null, int? roleId = null, string? loggedUserRole = null, string? orderBy = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(loggedUserRole))
                throw new Exception("Logged user role not provided!");

            if (loggedUserRole == Role.Administrator.ToString())
                roleId = (int)Role.Employee;

            var usersList = await _repository.GetUsersPagedListAsync(isActive, query, roleId, orderBy, page, pageSize, cancellationToken);

            var dtos = Mapper.Map<List<UserDTO>>(usersList);

            return new PagedList<UserDTO>(usersList, dtos);
        }

        public override async Task UpdateAsync([FromForm] UserUpdateDTO dto, CancellationToken cancellationToken = default)
        {
            await UpdateValidator.ValidateAndThrowAsync(dto, cancellationToken);

            var entity = await _repository.GetByIdAsync(dto.Id, cancellationToken);
            if (entity == null)
            {
                throw new KeyNotFoundException();
            }

            Mapper.Map(dto, entity);
            string? newImagePath = null;

            try
            {
                if (dto.IsImageDeleted)
                {
                    DeleteImage(entity.ImagePath);
                    entity.ImagePath = null;
                }
                else if (dto.ImageFile != null && dto.ImageFile.Length > 0)
                {
                    DeleteImage(entity.ImagePath);

                    newImagePath = await HandleImageUpload(dto.ImageFile, entity.Id);
                    entity.ImagePath = newImagePath;
                }
                _repository.Update(entity);
                await _repository.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                if (!string.IsNullOrEmpty(newImagePath))
                {
                    DeleteImage(newImagePath);
                }

                throw;
            }
        }

        private void DeleteImage(string? imagePath)
        {
            var uploadsDirectory = Path.Combine(_configuration["FileSettings:RootDirectory"]!, _configuration["FileSettings:UploadsDirectory"]!);

            if (!string.IsNullOrEmpty(imagePath))
            {
                var oldFilePath = Path.Combine(uploadsDirectory, imagePath);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }
        }

        public async Task<bool> ChangeUserStatusAsync(int id, int loggedUserId, CancellationToken cancellationToken = default)
        {
            var user = await _repository.GetByIdAsync(id, cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found!");
            }
            if (loggedUserId == id)
            {
                throw new Exception("The currently logged in user cannot deactivate himself!");
            }

            user.IsActive = !user.IsActive;
            _repository.Update(user);
            await _repository.SaveChangesAsync(cancellationToken);

            return user.IsActive;
        }

        public async Task<bool> ChangePasswordAsync(int id, string password, CancellationToken cancellationToken = default)
        {
            var user = await _repository.GetByIdAsync(id, cancellationToken);
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

        public override async Task<UserDTO?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            var userDTO = _mapper.Map<UserDTO>(user);

            if (!string.IsNullOrEmpty(user.ImagePath))
            {
                var filePath = Path.Combine(_configuration["FileSettings:RootDirectory"]!, user.ImagePath);
                if (File.Exists(filePath))
                {
                    var fileBytes = File.ReadAllBytesAsync(filePath, cancellationToken);
                    userDTO.ImageContent = Convert.ToBase64String(await fileBytes);
                }
            }

            return userDTO;
        }

        private async Task<string> HandleImageUpload(IFormFile? imageFile, int userId)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return string.Empty;
            }

            var extension = Path.GetExtension(imageFile.FileName).ToLower();
            var allowedExtension = _configuration.GetSection("FileSettings:AllowedExtensions").Get<List<string>>();

            if (allowedExtension == null || !allowedExtension.Contains(extension))
            {
                throw new Exception("Unsupported file format!");
            }

            if (!imageFile.ContentType.StartsWith("image/"))
            {
                throw new Exception("Invalid file type!");
            }

            var filePath = Path.Combine(_configuration["FileSettings:RootDirectory"]!, _configuration["FileSettings:UploadsDirectory"]!, $"{userId}{extension}");

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return Path.Combine(_configuration["FileSettings:UploadsDirectory"]!, $"{userId}{extension}");
        }
    }
}
