using Billing_API_NET8.Helpers;
using Billing_API_NET8.Controllers.Authorization;
using Billing_API_NET8.Controllers.Services;
using Billing_API_NET8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Billing_API_Net8.Entities;

    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private IUserService _userService;
        private readonly DataContext context;

        public AuthenticationController(DataContext context, IUserService userService)
        {
            this.context = context;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // only admins can access other user records
            var currentUser = (SystemUser)HttpContext.Items["User"];
            if (id != currentUser.Id && currentUser.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var user = _userService.GetById(id);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SystemUserForRegisterDto systemUserForRegisterDto)
        {
            // validate request
            systemUserForRegisterDto.Username = systemUserForRegisterDto.Username.ToLower();

            var user = await context.SystemUser.FirstOrDefaultAsync(cs => cs.Id == systemUserForRegisterDto.Id);
            if (user != null)
                return BadRequest("Username already exist");

            var createdUser = _userService.Register(systemUserForRegisterDto);
            return StatusCode(201);
        }
    }
