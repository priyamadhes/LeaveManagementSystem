using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LmsWebApi.Models.BindingModel;
using Microsoft.AspNetCore.Identity;
using LmsWebApi.Data.Entities;
using LmsWebApi.Models.DTO;
using LmsWebApi.Models;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using LmsWebApi.Enums;
using LmsWebApi.Models;

namespace LmsWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;

        // For login we will use microsfot identity servcies, here will inject 2 services
        /* Injection of two services -- .net core comes with built in we inject instance to controlleres */
        private readonly UserManager<Appuser> _userManager; //For user who register

        private readonly SignInManager<Appuser> _signInManager; //For user who signin

        private readonly JWTConfig _jWTConfig;

        //here we use Dependency Injection in controller 

        //here we wil use dependency injection to get insatnce of jwtconfig
        public UserController(ILogger<UserController> logger, UserManager<Appuser> userManager, SignInManager<Appuser> signManager, IOptions<JWTConfig> jwtConfig)
        {
            _userManager = userManager;
            _signInManager = signManager;
            _logger = logger;
            _jWTConfig = jwtConfig.Value;
        }

        [HttpPost("RegisterUser")]
        //From body -- information will be in request body and  other model request will come from url
        public async Task<object> RegisterUser([FromBody] AddUpdateRegisterUserBindingModel model)
        {
            try
            {
                var user = new Appuser() { FullName = model.FullName, Email = model.Email, UserName = model.Email, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return await Task.FromResult("User has been Registered");
                }
                return await Task.FromResult(string.Join(",", result.Errors.Select(x => x.Description).ToArray()));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllUser")]
        public async Task<object> GetAllUser()
        {
            try
            {
                //We use DTO(Data Transfer Object) as its not good practice to send entity model so used DTO.

                var users = _userManager.Users.Select(x => new UserDTO(x.FullName, x.Email, x.UserName, x.DateCreated));
                //return await Task.FromResult(users);
                return await Task.FromResult(new ResponseModel(ResponseCode.Ok, "", users));
            }
            catch (Exception ex)
            {
                //return await Task.FromResult(ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }

        [HttpPost("Login")]
        public async Task<object> Login([FromBody] LoginBindingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        var appUser = await _userManager.FindByEmailAsync(model.Email);
                        var user = new UserDTO(appUser.FullName, appUser.Email, appUser.UserName, appUser.DateCreated);

                        user.Token = GenerateToken(appUser);

                        //return await Task.FromResult(user);
                        return await Task.FromResult(new ResponseModel(ResponseCode.Ok, "", user));
                    }
                }

                //return await Task.FromResult("Invalid Email or Password");
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Invalid Email or Password", null));
            }
            catch (Exception ex)
            {
                // return await Task.FromResult(ex.Message);
                return await Task.FromResult(new ResponseModel(ResponseCode.Error,ex.Message,null));
            }

        }

        private string GenerateToken(Appuser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId, user.Id),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(12),//add eky to sign token with algorithm 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jWTConfig.Audience,
                Issuer = _jWTConfig.Issuer
            };

            //create token
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
    
}
