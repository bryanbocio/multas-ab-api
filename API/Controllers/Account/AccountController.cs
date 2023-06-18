using API.DTOs.Identity;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Agent;
using Core.Entities.Driver;
using Core.Entities.Identity;
using Core.Enums;
using Core.Interfaces.Services;
using Core.Interfaces.Tokens;
using Core.Interfaces.UnitOfWork;
using Core.Specification;
using Infrastructure.Data.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Account
{
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokeService;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
       


        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 ITokenService tokenService,
                                 IUserService userService,
                                 IUnitOfWork unitOfWork)
                                 
        {
            _unitOfWork= unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokeService = tokenService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {

            var user = await _userManager.FindUserByEmailFormClaimsPrincipal(User);

            return new UserDto
            {
                Email = user.Email,
                IdentityUserId = user.IdentityAppUser,
                Token = await _tokeService.CreateToken(user),
            };

        }

        
        [HttpGet("emailexist")]
        public async Task<ActionResult<bool>> CheckEmailExistAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                Email = user.Email,
                IdentityUserId = user.IdentityAppUser,
                Token =await _tokeService.CreateToken(user),
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if (CheckEmailExistAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" } });
            }

            var driver = await _unitOfWork.Repository<Driver>().GetEntityWithSpecification(new DriverSpecification(registerDto.IdentityUserId));

            if(driver == null)
            {
                var newDriver = UtilDriver.BuildDriverObject(registerDto.IdentityUserId, registerDto.Name, registerDto.LastName, registerDto.PhoneNumber);
                _unitOfWork.Repository<Driver>().Add(newDriver);
                var isSaved = await _unitOfWork.Complete();

                if (isSaved <= 0) return BadRequest(new ApiResponse(400, "Problems creating a new account"));
            }

            var user = new AppUser
            {
                IdentityAppUser = registerDto.IdentityUserId,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };


            var result = await _userService.CreateUserAsync(user,registerDto.Password,registerDto.Role);

            if (!result) return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                Email = user.Email,
                IdentityUserId = user.IdentityAppUser,
                Token = await _tokeService.CreateToken(user),
            };
        }

        [Authorize(Roles="ADMIN")]
        [HttpPost("registerbyadmin")]
        public async Task<ActionResult<UserDto>> RegisterUserByAdmin(RegisterDto registerDto)
        {
            if (CheckEmailExistAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" } });
            }

            var driver = await _unitOfWork.Repository<Driver>().GetEntityWithSpecification(new DriverSpecification(registerDto.IdentityUserId));

            if (driver == null)
            {
                var newDriver = UtilDriver.BuildDriverObject(registerDto.IdentityUserId, registerDto.Name, registerDto.LastName, registerDto.PhoneNumber);
                _unitOfWork.Repository<Driver>().Add(newDriver);
                var isSaved = await _unitOfWork.Complete();

                if (isSaved <= 0) return BadRequest(new ApiResponse(400, "Problems creating a new account"));
            }

            if (Roles.AGENT.ToString().ToUpper().Equals(registerDto.Role.ToUpper()))
            {
                var agent = await _unitOfWork.Repository<Agent>().GetEntityWithSpecification(new AgentSpecification(registerDto.IdentityUserId));
                if(agent == null)
                {
                    var newAgent = UtilsAgent.BuildAgentObject(registerDto.IdentityUserId, registerDto.Name, registerDto.LastName, registerDto.PhoneNumber);
                    _unitOfWork.Repository<Agent>().Add(newAgent);
                    var isSaved = await _unitOfWork.Complete();
                    if (isSaved <= 0) return BadRequest(new ApiResponse(400, "Problems creating a new account"));
                }
            }

            var user = new AppUser
            {
                IdentityAppUser = registerDto.IdentityUserId,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };


            var result = await _userService.CreateUserAsync(user, registerDto.Password, registerDto.Role);

            if (!result) return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                Email = user.Email,
                IdentityUserId = user.IdentityAppUser,
                Token = await _tokeService.CreateToken(user),
            };
        }

    }
}
