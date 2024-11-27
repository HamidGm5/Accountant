using Accountant.API.Entities;
using Accountant.API.Repository.Interfaces;
using Accountant.Model.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Accountant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AdminController(IAdminRepository repository,
                                IUserRepository userRepository,
                                IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ICollection<UserDto>>> GetUsers()
        {
            var users = await _userRepository.GetAllUser();
            return Ok(users);
        }

        [HttpGet("{AdminID:int}", Name = "GetAdminByID")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<AdminDto>> GetAdminByID(int AdminID)
        {
            try
            {
                var Admin = await _repository.GetAdminById(AdminID);
                if (Admin == new Admin())
                {
                    return NotFound();
                }
                return Ok(Admin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{UserSpec}", Name = "GetUserBySpec")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<UserDto>> GetUserBySpec(string UserSpec)
        {
            try
            {
                var user = await _repository.GetUserByUsernameOrEmail(UserSpec);
                if (user == new User())
                    return NotFound();

                var UserMap = _mapper.Map<UserDto>(user);
                return Ok(UserMap);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{AdminSpec}/{password}", Name = "LoginAdminByAlias")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<AdminDto>> LoginAdminByAlias(string AdminSpec, string password)
        {
            try
            {
                var admin = await _repository.loginAdmin(AdminSpec, password);
                if (admin == new Admin())
                    return NotFound();
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{Email}/{password}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<bool>> UpdateUserPassword(string Email, string password)
        {
            try
            {
                var update = await _repository.UpdateUserPassword(Email, password);
                if (update)
                    return Ok(true);
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<bool>> DeleteUser(string Email)
        {
            try
            {
                var delete = await _repository.DeleteUser(Email);
                if (delete)
                    return Ok("User Deleted !");
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}