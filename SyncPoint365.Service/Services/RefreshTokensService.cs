using AutoMapper;
using FluentValidation;
using SyncPoint365.Core.DTOs.RefreshTokens;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.Service.Services
{
    public class RefreshTokensService : BaseService<RefreshToken, RefreshTokenDTO, RefreshTokenAddDTO, RefreshTokenUpdateDTO>, IRefreshTokensService
    {
        private readonly IRefreshTokensRepository _repository;
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;

        public RefreshTokensService(IRefreshTokensRepository repository, IUsersRepository usersRepository, IMapper mapper, IValidator<RefreshTokenAddDTO> addValidator, IValidator<RefreshTokenUpdateDTO> updateValidator)
            : base(repository, mapper, addValidator, updateValidator)
        {
            _repository = repository;
            _userRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<RefreshTokenDTO?> GetRefreshTokenByUserIdAsync(int userId)
        {
            var refreshToken = await _repository.GetRefreshTokenByUserIdAsync(userId);
            return _mapper.Map<RefreshTokenDTO>(refreshToken);
        }

        public async Task AddRefreshTokenAsync(int userId, string refreshToken, DateTime? tokenExpiration)
        {
            var existingToken = await _repository.GetRefreshTokenByUserIdAsync(userId);
            if (existingToken == null)
            {
                var refreshTokenDto = new RefreshTokenAddDTO
                {
                    UserId = userId,
                    Token = refreshToken,
                    ExpirationDate = tokenExpiration ?? DateTime.UtcNow.AddDays(7)
                };
                var refreshTokenEntity = _mapper.Map<RefreshToken>(refreshTokenDto);
                await _repository.AddAsync(refreshTokenEntity);
                await _repository.SaveChangesAsync();
            }
            else
            {
                var refreshTokenUpdateDto = new RefreshTokenUpdateDTO
                {
                    Id = existingToken.Id,
                    UserId = userId,
                    Token = refreshToken,
                    ExpirationDate = tokenExpiration ?? DateTime.UtcNow.AddDays(7)
                };
                var refreshTokenEntity = _mapper.Map<RefreshToken>(refreshTokenUpdateDto);
                await _repository.AddAsync(refreshTokenEntity);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
