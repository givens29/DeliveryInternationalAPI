using BackEnd_DeliveryInternational.Configurations;
using BackEnd_DeliveryInternational.Data;
using BackEnd_DeliveryInternational.Dtos;
using BackEnd_DeliveryInternational.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEnd_DeliveryInternational.Services
{
    public interface IUserService
    {
        Task Register(RegisterUserDto model);
        Task<string> Login(LoginUserDto model);
        Task<ProfileUserDto> Profile(string email);
        Task<User> EditProfile(string email, EditUserProfileDto model);
        Task<bool> Logout(string id);
    }
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly JwtBearerTokenSettings _bearerTokenSettings;

        public UserService(DataContext context, IOptions<JwtBearerTokenSettings> jwtTokenOptions)
        {
            _context = context;
            _bearerTokenSettings = jwtTokenOptions.Value;
        }

        public async Task<User> EditProfile(string email, EditUserProfileDto model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            user.FullName = model.FullName;
            user.Address = model.Address;
            user.BirthDate = model.BirthDate;
            user.Gender = model.Gender;
            user.PhoneNumber = model.PhoneNumber;

            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<ProfileUserDto> Profile(string email)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            var prof = new ProfileUserDto
            {
                id = user.id,
                FullName = user.FullName,
                Email = user.Email,
                Address = user.Address,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber
            };
            return prof;
        }

        public async Task<string> Login(LoginUserDto model)
        {
            var user = await ValidateUser(model);
            if (user == null)
            {
                throw new InvalidOperationException("Login failed");
            }
            string token = GenerateToken(user);
            var storeToken = new StorageUsersTokens
            {
                email = user.Email,
                Token = token
            };
            _context.StorageUsersTokens.Add(storeToken);
            await _context.SaveChangesAsync();

            return token;
        }



        public async Task Register(RegisterUserDto model)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("User with the same email already exists");
            }

            var identityUser = new User
            {
                FullName = model.FullName,
                Password = model.Password,
                Email = model.Email,
                Address = model.Address,
                BirthDate = model.BirthDate,
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber
            };

            _context.Users.Add(identityUser);

            await _context.SaveChangesAsync();

        }
        private async Task<User> ValidateUser(LoginUserDto credentials)
        {
            var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == credentials.Email && u.Password == credentials.Password);

            return user;
        }
        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_bearerTokenSettings.SecretKey);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Authentication, user.id.ToString())

                }),
                Expires = DateTime.UtcNow.AddSeconds(_bearerTokenSettings.ExpiryTimeInSeconds),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _bearerTokenSettings.Audience,
                Issuer = _bearerTokenSettings.Issuer,
            };

            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> Logout(string email)
        {
            var existingUser = await _context.StorageUsersTokens.FirstOrDefaultAsync(u => u.email == email);
            if (existingUser != null)
            {
                _context.StorageUsersTokens.Remove(existingUser);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

    }
}
