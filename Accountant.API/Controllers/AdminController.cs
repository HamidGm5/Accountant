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

        [HttpGet("{id:int}", Name = "GetAdminByID")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<AdminDto>> GetAdminByID(int ID)
        {
            try
            {
                var Admin = await _repository.GetAdminById(ID);
                if (Admin == null)
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

        [HttpGet("{spec}", Name = "GetUserBySpec")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<UserDto>> GetUserBySpec(string spec)
        {
            try
            {
                var user = await _repository.GetUserByUsernameOrEmail(spec);
                if (user == null)
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
                if (admin == null)
                    return NotFound();
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{spec}/{password}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<bool>> UpdateUserPassword(string spec, string password)
        {
            try
            {
                var update = await _repository.UpdateUserPassword(spec, password);
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