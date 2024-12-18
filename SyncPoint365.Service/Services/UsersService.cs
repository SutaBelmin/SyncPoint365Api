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


            await Repository.AddAsync(entity, cancellationToken);
            await Repository.SaveChangesAsync(cancellationToken);
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

        public async Task<IPagedList<UserDTO>> GetUsersPagedListAsync(bool? isActive, string? query = null, int? roleId = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, string? orderBy = null, CancellationToken cancellationToken = default)
        {
            var usersList = await _repository.GetUsersPagedListAsync(isActive, query, roleId, page, pageSize, orderBy, cancellationToken);
            var users = usersList.ToList();

            var dtos = Mapper.Map<List<UserDTO>>(users);
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

            Repository.Update(entity);
            await Repository.SaveChangesAsync(cancellationToken);
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

    }
}
