using PimApi.Config;
using PimApi.Exceptions;
using PimApi.Repositories.Interfaces;
using PimApi.Services.Interfaces;
using PimModels.DTO;
using PimModels.Models;
using PimModels.RequestModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PimApi.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly ApiConfig _apiConfig;

    public AuthenticationService(IUserRepository userRepository, IOptions<ApiConfig> config)
    {
        _userRepository = userRepository;
        _apiConfig = config.Value;
    }

    public async  Task<User?> GetCurrentUserAsync(int userId) => await _userRepository.GetUserByIdAsync(userId);

    public async Task<LoginUserDTO?> LoginAsync(LoginUser loginUser)
    {
        var user = await _userRepository.GetUserByUsernameAsync(loginUser.Username);
        if (user is null) throw new KeyNotFoundException("Nie ma takiego usera");
        if (user.Password != GetMD5(loginUser.Password)) throw new ArgumentException("Złe hasło");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_apiConfig.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Name", user.Username.ToString()),
                new Claim("Email", user.Email.ToString()),
                new Claim("Id", user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new LoginUserDTO() 
        { 
            Token = tokenHandler.WriteToken(token), 
            ExpirationDate = ((DateTimeOffset)token.ValidTo).ToUnixTimeSeconds(),
            UserId = user.Id,
        };
    }

    public async Task RegisterAsync(RegisterUser registerUser)
    {
        var user = await _userRepository.GetUserByUsernameAsync(registerUser.Username);
        if (user is not null) throw new KeyAlreadyExistsException("User with this username already exists.");
        user = await _userRepository.GetUserByEmailAsync(registerUser.Email);
        if (user is not null) throw new KeyAlreadyExistsException("User with this E-mail already exists.");
        if (registerUser.Password != registerUser.Password2) throw new ArgumentException("Passwords are not the same.");

        user = new();
        user.Username = registerUser.Username;
        user.Email = registerUser.Email;
        user.Password = GetMD5(registerUser.Password);

        await _userRepository.CreateAsync(user);
    }

    //out of interface

    private string GetMD5(string str)
    {
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(str);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++) sb.Append(hashBytes[i].ToString("X2"));

            return sb.ToString();
        }
    }
}
