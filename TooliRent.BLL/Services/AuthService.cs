using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using TooliRent.BLL.Services.Interfaces;
using TooliRentClassLibrary.Models.DTO;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwt _jwt;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        public AuthService(IJwt jwt, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _jwt = jwt;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<RegisterDtoResponse?> RegisterUserAsync(RegisterDtoRequest registerDtoRequest)
        {
            var CreateUser = new ApplicationUser
            {
                UserName = registerDtoRequest.UserName,
                Email = registerDtoRequest.Email,
                PhoneNumber = registerDtoRequest.PhoneNumber,
            };
           
            //Create user
            var result = await _userManager.CreateAsync(CreateUser, registerDtoRequest.Password);

            if(result == null || !result.Succeeded)
            {
                throw new Exception("Registration failed");
            }

            var user = await _userManager.FindByEmailAsync(registerDtoRequest.Email);

            if(user == null)
            {
                throw new Exception("User not found after registration");
            }

            //Assign role to user 
            await _userManager.AddToRoleAsync(user, "User");
        
            //Map and return the created user details
            return _mapper.Map<RegisterDtoResponse>(user);
        }

        public async Task<LoginDtoRespond?> LoginAsync(LoginDtoRequest loginDtoRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginDtoRequest.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            if(user.LockoutEnd != null && user.LockoutEnd > DateTime.UtcNow && user.LockoutEnabled == true)
            {
                throw new UnauthorizedAccessException("User account is locked. Please try again later.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDtoRequest.Password, false);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwt.GenerateToken(user.Id, roles);
            var refreshToken = Guid.NewGuid().ToString("N"); // Generate a new refresh token 

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // Set refresh token expiry time
            await _userManager.UpdateAsync(user); // Update user with new refresh token

            var response = _mapper.Map<LoginDtoRespond>(user);

            response.Token = token;
            response.RefreshToken = refreshToken;

            return response;

        }

        public async Task<TokenRefreshResponseDto?> RefreshToken(TokenRefreshRequestDto tokenRefreshRequest)
        {
            var user = await _userManager.FindByEmailAsync(tokenRefreshRequest.Email);

            if (user == null || user.RefreshToken != tokenRefreshRequest.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid refresh token or token has expired");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var newToken = _jwt.GenerateToken(user.Id, roles);
            var newRefreshToken = Guid.NewGuid().ToString("N"); // Generate a new refresh token
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // Set new refresh token expiry time
            await _userManager.UpdateAsync(user); // Update user with new refresh token

            return new TokenRefreshResponseDto
            {
                AccessToken = newToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30) // Assuming the access token expires in 30 minutes
            };
        }
    }
}
