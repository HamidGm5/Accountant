using Accountant.API.Business;
using Accountant.API.Entities;
using Accountant.API.Repository.Interfaces;
using Accountant.Model.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Accountant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstallmentController : Controller
    {
        private readonly IinstallmentRepository _repository;
        private readonly ILoanRepository _loanRepository;

        public InstallmentController(IinstallmentRepository repository,
                                     ILoanRepository loanRepository)
        {
            _repository = repository;
            _loanRepository = loanRepository;

        }

        [HttpGet(Name = "GetInstallments")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ICollection<Installment>>> GetInstallments()
        {
            try
            {
                var Installments = await _repository.GetInstallments();
                return Ok(Installments);
            }
            catch
            {
                //Log Exception
                return BadRequest();
            }
        }

        [HttpGet("{LoanID:int}", Name = "GetInstallmentsByLoanID")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ICollection<Installment>>> GetLoanInstallmentsByLoanID(int LoanID)
        {
            try
            {
                var Installments = await _repository.GetInstallmentsByLoanID(LoanID);
                if (Installments == null)
                {
                    return NotFound();
                }
                return Ok(Installments);
            }
            catch
            {
                //Log Exception
                return BadRequest();
            }
        }

        [HttpGet("{LoanID:int}/{InstallmentID:int}", Name = "GetInstallmentByID")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Installment>> GetIstallmentByID(int LoanID, int InstallmentID)
        {
            try
            {
                var Installment = await _repository.GetInstallmentByID(InstallmentID, LoanID);
                if (Installment == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Installment);
                }
            }
            catch
            {
                //Log Exception
                return BadRequest();
            }
        }

        [HttpPatch("{InstallmentID:int}", Name = "PayInstallment")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<Installment>> PayInstallment(int InstallmentID)
        {
            try
            {
                if (InstallmentID == 0)
                {
                    return BadRequest();
                }
                else
                {
                    var Update = await _repository.UpdatePayInstallment(InstallmentID);

                    if (Update)
                        return Ok("successfuly");
                    else
                        return NotFound();
                }
            }
            catch
            {
                //Log Exception
                return BadRequest();
            }
        }

        //[HttpDelete("{LoanID:int}", Name = "DeleteInstallmentsByLoanID")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        //public async Task<ActionResult<bool>> DeleteInstallmentsByLoanID(int LoanID)
        //{
        //    try
        //    {
        //        var Delete = await _repository.RemoveInstallments(LoanID);
        //        if (Delete)
        //            return Ok("successfully");
        //        else
        //            return NotFound();
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
