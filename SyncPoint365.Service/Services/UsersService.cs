using AutoMapper;
using SyncPoint365.Core.DTOs.Users;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.Service.Services
{
    public class UsersService : BaseService<User, UserDTO, UserAddDTO>, IUsersService
    {
        private readonly IUsersRepository _repository;

        public UsersService(IUsersRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
        }

        public override async Task AddAsync(UserAddDTO dto, CancellationToken cancellationToken = default)
        {
            var entity = Mapper.Map<User>(dto);

            //entity.PasswordSalt = Cryptography.GenerateSalt();
            //entity.PasswordHash = Cryptography.GenerateHash(dto.Password, entity.PasswordSalt);

            await Repository.AddAsync(entity, cancellationToken);
            await Repository.SaveChangesAsync(cancellationToken);
        }
    }
}
