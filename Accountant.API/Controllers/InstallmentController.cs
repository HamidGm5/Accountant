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
        private readonly InstallmentBusiness _business;

        public InstallmentController(IinstallmentRepository repository,
                                     ILoanRepository loanRepository,
                                     InstallmentBusiness business)
        {
            _repository = repository;
            _loanRepository = loanRepository;
            _business = business;
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
                return BadRequest();
            }
        }

        //[HttpPost("{loan}", Name = "AddNewInstallment")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        //public async Task<ActionResult<Installment>> AddNewInstallment(LoanDto loan)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (await _loanRepository.ExistLoan(loan.ID))
        //            {
        //                foreach (var item in await _business.AddInstallments(loan))
        //                {
        //                    var addInstallment = await _repository.AddNewInstallments(item);

        //                    if (!addInstallment)
        //                        return BadRequest();
        //                }

        //                return Ok("successfully");
        //            }
        //            else
        //            {
        //                return NotFound();
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(ModelState);
        //        }
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpPut(Name = "UpdateInstallments")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        //public async Task<ActionResult<ICollection<Installment>>> UpdateInstallments(LoanDto loan)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var installmentsUpdate = await _business.UpdateInstallment(loan);

        //            if (installmentsUpdate != null)
        //            {
        //                var UpdateResponse = await _repository
        //                return Ok("successfully");
        //            }
        //            else
        //            {
        //                return NotFound();
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(ModelState);
        //        }
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

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
                return BadRequest();
            }
        }

        [HttpDelete("{LoanID:int}", Name = "DeleteInstallmentsByLoanID")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<bool>> DeleteInstallmentsByLoanID(int LoanID)
        {
            try
            {
                var Delete = await _repository.RemoveInstallments(LoanID);
                if (Delete)
                    return Ok("successfully");
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
