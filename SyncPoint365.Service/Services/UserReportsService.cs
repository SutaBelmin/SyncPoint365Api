using AutoMapper;
using SyncPoint365.Core.DTOs.UserReports;
using SyncPoint365.Core.DTOs.Users;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.Service.Services
{
    public class UserReportsService : IUserReportsService
    {
        protected readonly IUsersRepository _usersRepository;
        protected readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UserReportsService(IUsersRepository usersRepository, IUsersService usersService, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _usersService = usersService;
            _mapper = mapper;
        }
        public async Task<UserReportDTO> GenerateUserReportAsync(int userId)
        {
            var user = await _usersRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");
            var userDto = _mapper.Map<UserDTO>(user);
            var userReportDto = new UserReportDTO
            {
                Title = $"Report for {user.FirstName} {user.LastName}",
                UserId = user.Id,
                User = userDto
            };

            Console.WriteLine($"Generated UserReportDTO: {System.Text.Json.JsonSerializer.Serialize(userReportDto)}");

            return userReportDto;
        }
    }
}
