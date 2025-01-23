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

        public async Task SaveRefreshTokenAsync(RefreshToken refreshToken)
        {
            await _repository.SaveRefreshTokenAsync(refreshToken);
        }

        public Task<(bool isValid, User user)> ValidateAccessTokenAsync(string accessToken)
        {
            throw new NotImplementedException();
        }
    }
}
