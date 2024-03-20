

using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Options;
using Billing_API_NET8.Controllers.Authorization;
using Billing_API_NET8.Controllers.Helpers;
using Billing_API_Net8.Entities;
using Billing_API_NET8.Entities;
using Billing_API_Net8.Helpers;
using Billing_API_NET8.Helpers;
using Billing_API_NET8.Models;

namespace Billing_API_NET8.Controllers.Services
{
    public interface IUserService
    {
        SystemCurrentUserDto GetCurrentSystemRolePermission(string systemUserName);
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        SystemUser Register(SystemUserForRegisterDto systemUserForRegisterDto);
        SystemUser ChangePassword(SystemUserForLoginDto systemUserForLoginDto);
        IEnumerable<SystemUser> GetAll();
        SystemUser GetById(int id);
    }

    public class UserService : IUserService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public UserService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public SystemCurrentUserDto GetCurrentSystemRolePermission(string systemUserName)
        {
            SystemUser systemUserId = _context.SystemUser.FirstOrDefault(c => c.Username == systemUserName);
            SystemCurrentUserDto sysusrdto = new SystemCurrentUserDto();
            sysusrdto.CurrentUser = systemUserId;
            return sysusrdto;
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.SystemUser.SingleOrDefault(x => x.Username == model.Username);

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful so generate jwt token
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }

        //todo
        public SystemUser Register(SystemUserForRegisterDto systemUserForRegisterDto)
        {
            var user = _context.SystemUser.SingleOrDefault(x => x.Username == systemUserForRegisterDto.Username);

            if (user != null)
                throw new AppException("Username invalid");



            var systemUserToCreate = new SystemUser
            {
                Username = systemUserForRegisterDto.Username,
                Email = systemUserForRegisterDto.Email,
                RegistrationTemporalCode = "TEST",
                Role = Role.User,
                PasswordHash = BCryptNet.HashPassword(systemUserForRegisterDto.Password)

            };

            _context.SystemUser.Add(systemUserToCreate);
            _context.SaveChanges();
            return systemUserToCreate;
        }

        //todo
        public SystemUser ChangePassword(SystemUserForLoginDto systemUserForLoginDto)
        {
            var user = _context.SystemUser.SingleOrDefault(x => x.Username == systemUserForLoginDto.Username);
            if (user == null)
                throw new AppException("Username or password is incorrect");



            user.PasswordHash = BCryptNet.HashPassword(systemUserForLoginDto.Password);
            _context.SaveChanges();
            return user;
        }

        public IEnumerable<SystemUser> GetAll()
        {
            return _context.SystemUser;
        }

        public SystemUser GetById(int id)
        {
            var user = _context.SystemUser.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
