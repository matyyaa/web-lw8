using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Lab8.Auth.Requests;
using Lab8.Auth.Responses;
using Lab8.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Lab8.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserController(UserManager<User> userManager, IMapper mapper, RoleManager<IdentityRole<Guid>> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
    {
        var user = this._mapper.Map<User>(request);
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) return BadRequest();
        return Ok();
    }
    
    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] UserRegisterRequest request)
    {
        var roleExists = await _roleManager.RoleExistsAsync(Roles.Admin);
        if (!roleExists)
        {
            var role = new IdentityRole<Guid>(Roles.Admin);
            await _roleManager.CreateAsync(role);
        }
        var user = this._mapper.Map<User>(request);
        user.Role = Roles.Admin;
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) return BadRequest();
        await _userManager.AddToRoleAsync(user, Roles.Admin);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password)) return BadRequest();
        var token = await GenerateJwtTokenAsync(user);
        return Ok(new LoginResponse() {Token = token});
    }
    
    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserInfoResponse>> GetMyInformation()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        var response = _mapper.Map<UserInfoResponse>(user);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("user-method")]
    public ActionResult<string> UserMethod()
    {
        return Ok("Hello, everyone");
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("admin-method")]
    public ActionResult<string> AdminMethod()
    {
        return Ok("Hello, admins");
    }


    private async Task<string> GenerateJwtTokenAsync(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        foreach(var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JWT:JwtLifeTimeMin")),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = _configuration["JWT:ValidAudience"],
            Issuer = _configuration["JWT:ValidIssuer"]
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}