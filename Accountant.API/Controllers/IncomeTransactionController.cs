using Accountant.API.Entities;
using Accountant.API.Repository.Interfaces;
using Accountant.Model.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Accountant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class IncomeTransactionController : ControllerBase
    {
        private readonly IIncomeTransactionRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public IncomeTransactionController(IIncomeTransactionRepository repository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ICollection<IncomeTransactionDto>>> GetAllUsersTransactions()
        {
            try
            {
                var transactions = await _repository.GetAllIncomes();
                if (transactions != null)
                {
                    return Ok(transactions);
                }
                return StatusCode(StatusCodes.Status404NotFound);
            }

            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("{Userid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ICollection<IncomeTransactionDto>>> GetUserTransactions(int Userid)
        {
            try
            {
                var Users = await _userRepository.UserExists(Userid);
                if (Users != false)
                {

                    var UserTransaction = await _repository.IncomeTransactions(Userid);

                    if (!ModelState.IsValid)
                    {
                        return NotFound();
                    }

                    return Ok(UserTransaction);
                }

                return NotFound();
            }

            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("{Userid}/{Transactionid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IncomeTransactionDto>> GetTransaction(int Userid, int Transactionid)
        {
            try
            {
                var Users = await _userRepository.UserExists(Userid);
                if (Users != false)
                {
                    var TransactionFind = await _repository.IncomeTransaction(Userid, Transactionid);
                    if (TransactionFind != null)
                    {
                        return Ok(TransactionFind);
                    }

                    return NotFound();
                }
                return BadRequest();
            }

            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public async Task<ActionResult<IncomeTransactionDto>> AddIncomeTransaction([FromBody] AddTransactionsStandardDto TransactionStd)
        {
            try
            {
                if (TransactionStd == null)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var TransactionMap = _mapper.Map<IncomeTransaction>(TransactionStd);
                    TransactionMap.User = await _userRepository.GetUserById(TransactionStd.Userid);
                    var AddTransaction = await _repository.AddIncomeTransaction(TransactionMap);

                    if (AddTransaction != null)
                    {
                        var AgainMap = _mapper.Map<IncomeTransactionDto>(AddTransaction);
                        return Ok(AgainMap);
                    }

                    else
                    {
                        return BadRequest(ModelState);
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }
        }


        [HttpPatch("{UserId:int}")]
        public async Task<ActionResult<IncomeTransactionDto>> UpdateTransaction(int UserId,
            [FromBody] IncomeTransactionDto transactionDto)
        {
            try
            {
                var FindTransaction = await _repository.IncomeExists((int)transactionDto.Id);

                if (!FindTransaction)
                {
                    return NotFound();
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        var TransactionMap = _mapper.Map<IncomeTransaction>(transactionDto);
                        TransactionMap.User = await _userRepository.GetUserById(UserId);

                        var UpdateTransaction = await _repository.UpdateIncomeTransactioin(TransactionMap);

                        if (UpdateTransaction != null) 
                        {
                            return Ok(transactionDto);
                        }

                        return BadRequest(ModelState);
                    }
                }
            }

            catch (Exception)
            {
                //Log Exception
                throw;
            }
        }


        [HttpDelete("{Transactionid}/{Userid}")]
        public async Task<ActionResult<IncomeTransactionDto>> DeleteIncomeTransaction(int Transactionid, int Userid)
        {
            if (await _repository.IncomeExists(Transactionid))
            {
                var transaction = await _repository.IncomeTransaction(Userid, Transactionid);

                if (transaction == null)
                {
                    return NotFound(ModelState);
                }
                else
                {
                    await _repository.DeleteIncomeTransaction(transaction);
                    var transactionMap = _mapper.Map<IncomeTransactionDto>(transaction);
                    return Ok(transactionMap);
                }
            }

            return BadRequest(ModelState);
        }


        [HttpDelete("{Userid}")]
        public async Task<ActionResult<IncomeTransactionDto>> DeleteIncomeTransactions(int Userid)
        {
            if (!await (_repository.DeleteIncomeTransactions(Userid)))
            {
                return BadRequest(ModelState);
            }


            return Ok("SuccessFuly !");

        }

    }
}  // Class
