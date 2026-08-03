using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;
using RentalCar.DomainLayer.DTO;

namespace RentalCar.Controllers
{
    [Route("api/[controller]")]  // api/setup
    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly RentalCarDbContext _context;
        private readonly IUser _IUser;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<SetupController> _logger;

        public SetupController(
            RentalCarDbContext context,
            IUser IUser,
            RoleManager<IdentityRole> roleManager,
            ILogger<SetupController> logger
        )
        {
            _logger = logger;
            _context = context;
            _IUser = IUser;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }
       
		[Route("AddAdmin")]
        [HttpPost]
        public async Task<IActionResult> AddAdmin([FromBody] RegisterDTO  ToAdd)
        {
            var res =await _IUser.Add(ToAdd);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            // Check if the role exist
            var roleExist = await _roleManager.RoleExistsAsync(name);

            if(!roleExist) // checks on the role exist status
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(name));

                // We need to check if the role has been added successfully
                if(roleResult.Succeeded)
                {
                    _logger.LogInformation($"The Role {name} has been added successfully");
                    return Ok(new {
                        result = $"The role {name} has been added successfully"
                    });
                } else {
                     _logger.LogInformation($"The Role {name} has not been added");
                    return BadRequest(new {
                        error = $"The role {name} has not been added"
                    });
                }
                
            }

            return BadRequest(new {error = "Role already exist"});
        }

       
    }
}