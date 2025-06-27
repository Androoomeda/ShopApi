using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShopApi.Data;
using ShopApi.Entities;

namespace ShopApi.Controllers;

// [ApiController]
// [Route("api/[controller]")]
// public class AuthController : ControllerBase
// {
//   private readonly UserManager<ShopUser> userManager;
//   private readonly IConfiguration configuration;
//   private readonly ShopContext context;

//   public AuthController(UserManager<ShopUser> userManager, IConfiguration configuration, ShopContext context)
//   {
//     this.userManager = userManager;
//     this.configuration = configuration;
//     this.context = context;
//   }

//   [HttpPost("register")]
//   public async Task<IActionResult> Register([FromBody] RegisterModel model)
//   {
//     var user = new ShopUser { UserName = model.Email, Email = model.Email };
//     var result = await userManager.CreateAsync(user, model.Password);


//     if (result.Succeeded)
//     {
//       var cart = new Cart { UserId = user.Id };
//       context.Carts.Add(cart);
//       await context.SaveChangesAsync();
//       return Ok("Пользователь успешно зарегистрирован");
//     }
//     else
//       return BadRequest(result.Errors);
//   }

//   [HttpPost("login")]
//   public async Task<IActionResult> Login([FromBody] LoginModel model)
//   {
//     var user = await userManager.FindByEmailAsync(model.Email);

//     if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
//     {
//       var authClaims = new List<Claim>
//       {
//         new Claim(ClaimTypes.Name, user.UserName),
//         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//       };

//       var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

//       var token = new JwtSecurityToken(
//         issuer: configuration["Jwt:Issuer"],
//         audience: configuration["Jwt:Issuer"],
//         expires: DateTime.Now.AddHours(5),
//         claims: authClaims,
//         signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
//       );

//       return Ok(new
//       {
//         token = new JwtSecurityTokenHandler().WriteToken(token),
//         expiration = token.ValidTo
//       });
//     }
//     return Unauthorized();
//   }
// }

// public class RegisterModel
// {
//   public string Email { get; set; }
//   public string Password { get; set; }
// }

// public class LoginModel
// {
//   public string Email { get; set; }
//   public string Password { get; set; }
// }
