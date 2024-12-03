using AutoMapper;
using FluentValidation;
using SyncPoint365.Core.DTOs.Users;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;
using System.Security.Cryptography;
using System.Text;

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

            CreatePasswordHashAndSalt(dto.Password, out var passwordHash, out var passwordSalt);

            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;

            await Repository.AddAsync(entity, cancellationToken);
            await Repository.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<UserDTO>> GetUsersListAsync(CancellationToken cancellationToken = default)
        {
            var users = await _repository.GetUsersListAsync();

            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<bool> UpdateUserStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await _repository.GetByUserIdAsync(id, cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found!");
            }

            user.isActive = !user.isActive;
            await _repository.UpdateUserStatusAsync(user, cancellationToken);

            return user.isActive;
        }

        private void CreatePasswordHashAndSalt(string password, out string passwordHash, out string passwordSalt)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            passwordSalt = Convert.ToBase64String(saltBytes);

            using (var hmac = new HMACSHA512(saltBytes))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                passwordHash = Convert.ToBase64String(hashBytes);
            }
        }

        public async Task<UserDTO> ValidateUserAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            var user = await _repository.GetUserByEmailAsync(email, cancellationToken);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password!");
            }

            if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new UnauthorizedAccessException("Invalid email or password!");
            }

            return _mapper.Map<UserDTO>(user);
        }

        public bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            using (var hmac = new HMACSHA512(Convert.FromBase64String(storedSalt)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return storedHash == Convert.ToBase64String(hash);
            }
        }
    }
}