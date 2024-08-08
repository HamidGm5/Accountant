using Accountant.API.Business;
using Accountant.API.Entities;
using Accountant.API.Repository.Interfaces;
using Accountant.Model.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Accountant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : Controller
    {
        private readonly ILoanRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IinstallmentRepository _installmentRepository;
        private readonly InstallmentBusiness _installmentBusiness;
        private readonly LoanBusiness _business;
        private readonly IMapper _mapper;

        public LoanController(ILoanRepository repository,
                              IMapper mapper,
                              IUserRepository userRepository,
                              IinstallmentRepository installmentRepository,
                              InstallmentBusiness installmentBusiness,
                              LoanBusiness business
                               )
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _installmentRepository = installmentRepository;
            _installmentBusiness = installmentBusiness;
            _business = business;
        }

        [HttpGet(Name = "GetAllLoans")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ICollection<Loan>>> GetLoans()
        {
            try
            {
                var Loans = await _repository.GetLoans();
                if (Loans == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Loans);
                }
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Userid:int}", Name = "GetUserLoans")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ICollection<Loan>>> GetUserLoans(int Userid)
        {
            try
            {
                var userLoans = await _repository.GetUserLoan(Userid);
                if (userLoans == null)
                {
                    return NotFound("Not Found anything with this user ID");
                }
                else
                {
                    return Ok(userLoans);
                }
            }

            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{UserID:int}/{LoanID:int}", Name = "GetLoanByID")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Loan>> GetLoanByID(int UserID, int LoanID)
        {
            try
            {
                var UserLoan = await _repository.GetLoanByID(UserID, LoanID);
                if (UserLoan == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(UserLoan);
                }
            }

            catch
            {
                return BadRequest();
            }
        }

        [HttpPost(Name = "AddNewLoan")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<bool>> AddNewLoan([FromBody] LoanDto newLoan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var LoanMap = _mapper.Map<Loan>(newLoan);
                    LoanMap.user = await _userRepository.GetUserById(newLoan.Userid);

                    if (LoanMap.user == null)
                    {
                        return NotFound();
                    }

                    LoanMap.RecursiveAmount = await _business.RecursiveAmount(newLoan);
                    var Installments = await _installmentBusiness.AddInstallments(newLoan);
                    LoanMap.Intallments = Installments;
                    LoanMap.EndTime = Installments.LastOrDefault().PayTime;

                    var addLoan = await _repository.AddLoan(LoanMap);

                    if (addLoan)
                    {
                        return Ok("successfully");
                    }
                    else
                    {
                        return StatusCode(500);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{LoanID:int}", Name = "UpdateLoan")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Loan>> UpdateLoan(int LoanID, [FromBody] LoanDto newLoan)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                else
                {
                    var LoanMap = _mapper.Map<Loan>(newLoan);

                    var Installments = await _installmentBusiness.UpdateInstallment(newLoan);
                    await _installmentRepository.RemoveInstallments(LoanID);    // must remove installments because in Update For Loan the previous installment been remaine

                    LoanMap.Intallments = Installments;

                    if (Installments == null)
                    {
                        return NotFound();
                    }

                    LoanMap.EndTime = Installments.LastOrDefault().PayTime;
                    LoanMap.RecursiveAmount = await _business.RecursiveAmount(newLoan);
                    LoanMap.ID = LoanID;
                    bool Response = await _repository.UpdateLoan(LoanMap);

                    if (!Response)
                    {
                        return StatusCode(500);
                    }
                    else
                    {
                        return Ok("Successfully");
                    }

                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{UserID:int}/{LoanID:int}", Name = "DeleteUserLoan")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Loan>> DeleteUserLoan(int UserID, int LoanID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var DeleteLoan = await _repository.DeleteLoan(LoanID, UserID);
                    if (DeleteLoan)
                        return Ok("Successfully");
                    else
                        return BadRequest();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{UserID:int}", Name = "DeleteUserLoans")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Loan>> DeleteUserLoans(int UserID)
        {
            try
            {
                var DeleteLoan = await _repository.DeleteAllLoan(UserID);

                if (DeleteLoan)
                    return Ok("successfuly");
                else
                    return NotFound();

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
