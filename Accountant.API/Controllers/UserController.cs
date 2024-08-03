using Accountant.API.Entities;
using Accountant.API.Repository.Interfaces;
using Accountant.Model.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Accountant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<UserDto>> GetAllUser()
        {
            var users = await _repository.GetAllUser();
            return Ok(users);
        }

        [HttpGet("{username}/{password}")]
        public async Task<ActionResult<UserDto>> LoginUser(string username, string password)
        {
            var user = await _repository.Login(username, password);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> SignUpUser([FromBody] UserDto user)
        {
            var Signuser = await _repository.GetByUserName(user.UserName);

            if (Signuser != null)
            {
                return BadRequest("This Username taken in past !");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                else
                {
                    var newuser = _mapper.Map<User>(user);

                    var SignUser = await _repository.SignUp(newuser);

                    if (SignUser != null)
                    {
                        var usermap = _mapper.Map<UserDto>(newuser);
                        return Ok(usermap);
                    }

                    else
                    {
                        return BadRequest(ModelState);
                    }
                }
            }
        }


        [HttpPut("{userid}")]
        public async Task<ActionResult<UserDto>> UpdateUser(int userid, [FromBody] UserDto userdto)
        {
            try
            {
                if (userdto == null)
                {
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var userMap = _mapper.Map<User>(userdto);

                if (!await _repository.UpdateUser(userid, userMap))
                {
                    ModelState.AddModelError("", "Somthings Went Wrong While Updating !");
                    return StatusCode(500, ModelState);
                }
                return Ok("Succefuly !");
            }

            catch (Exception)
            {
                //Log Excepsion
                throw;
            }

        }

        [HttpDelete("{userid}")]
        public async Task<ActionResult<UserDto>> DeleteUser(int userid)
        {
            var delUser = await _repository.UserExists(userid);
            if (!delUser)
            {
                return NotFound();
            }
            var finduser = await _repository.GetUserById(userid);
            if (finduser == null)
            {
                return BadRequest("Somthing went Wrong While deleting !");
            }
            _repository.DeleteUser(userid);

            return Ok("SuccessFully !");
        }

    } //Class
}
