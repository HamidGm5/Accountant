using Accountant.API.Entities;
using Accountant.API.Repository.Interfaces;
using Accountant.Model.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;

namespace Accountant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentTransactionController : ControllerBase
    {
        private readonly IPaymentTransactionRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PaymentTransactionController(IPaymentTransactionRepository repository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ICollection<PaymentTransactionDto>>> GetAllTransaction()
        {
            try
            {
                var transactions = await _repository.GetAllPaymentTransactions();
                if (transactions != null)
                {
                    return Ok(transactions);
                }
                return BadRequest();
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
        public async Task<ActionResult<ICollection<PaymentTransactionDto>>> GetUserTransactions(int Userid) // Mapper Check
        {
            try
            {
                var Users = await _userRepository.UserExists(Userid);
                if (Users != false)
                {

                    var UserTransaction = await _repository.PaymentTransactions(Userid);

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
        public async Task<ActionResult<PaymentTransactionDto>> GetTransaction(int Userid, int Transactionid)
        {
            try
            {
                var Users = await _userRepository.UserExists(Userid);
                if (Users != false)
                {
                    var TransactionFind = await _repository.PaymentTransaction(Userid, Transactionid);
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
        public async Task<ActionResult<PaymentTransactionDto>> AddPaymentTransaction([FromBody] AddTransactionsStandardDto paymentsStandard)                                             // From Query
        {
            try
            {
                if (paymentsStandard == null)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var TransactionMap = _mapper.Map<PaymentTransaction>(paymentsStandard);
                    TransactionMap.User = await _userRepository.GetUserById(paymentsStandard.Userid);

                    var AddTransaction = await _repository.AddPaymentTransaction(TransactionMap);

                    if (AddTransaction != null)
                    {
                        var TransactionAgainMap = _mapper.Map<PaymentTransactionDto>(paymentsStandard);
                        return Ok(TransactionAgainMap);
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

        [HttpPost("{Count:int}" , Name ="AddMultiPayments")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<bool>> AddMultiPayments([FromBody]AddTransactionsStandardDto transaction , int Count)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(await _userRepository.UserExists(transaction.Userid))
                    {
                        var TransactionMap = _mapper.Map<PaymentTransaction>(transaction);
                        TransactionMap.User = await _userRepository.GetUserById(transaction.Userid);

                        List<PaymentTransaction> Payments = new List<PaymentTransaction>();
                        Payments.AddRange(Enumerable.Repeat(TransactionMap, Count));
                        
                        if(await _repository.AddMultiPayments(Payments))        // add just one transaction ! (in here and too for income)
                        {
                            return Ok("Successfully");
                        }
                    }
                    return NotFound();
                }
                return BadRequest(ModelState);
            }
            catch
            {
                throw;
            }
        }

        [HttpPatch("{userid:int}")]
        public async Task<ActionResult<PaymentTransactionDto>> UpdateTransaction(int userid,
            [FromBody] PaymentTransactionDto transactionDto)
        {
            try
            {
                var transactionFind = await _repository.PaymentTransaction(userid, (int)transactionDto.Id);         //Look Like (Ok)

                if (transactionFind == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

                else
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        var TransactionMap = _mapper.Map<PaymentTransaction>(transactionDto);
                        TransactionMap.User = await _userRepository.GetUserById(userid);

                        var updateTransaction = await _repository.UpdatePaymentTransaction(TransactionMap);

                        if (updateTransaction != null)
                        {
                            return Ok(transactionDto);
                        }
                        else
                        {
                            return BadRequest(ModelState);
                        }
                    }
                }
            }

            catch (Exception)
            {
                //Log Exception
                throw;
            }
        }

        [HttpDelete("{Userid:int}/{Transactionid:int}")]
        public async Task<ActionResult<PaymentTransactionDto>> DeletePayTransaction(int Userid, int Transactionid)
        {
            try
            {
                var transaction = await _repository.PaymentTransaction(Userid, Transactionid);

                if (transaction == null)
                {
                    return NotFound(ModelState);
                }
                else
                {
                    await _repository.DeletePaymentTransaction(transaction);
                    var transactionMap = _mapper.Map<PaymentTransactionDto>(transaction);
                    return Ok(transactionMap);
                }

                return BadRequest(ModelState);
            }

            catch (Exception)
            {

                throw;
            }

        }


        [HttpDelete("{Userid}")]
        public async Task<ActionResult<PaymentTransactionDto>> DeletePayTransaction(int Userid)
        {
            if (await (_repository.DeletePaymentTransactions(Userid)))
            {
                return BadRequest(ModelState);
            }

            else
            {
                return Ok("SuccessFuly !");
            }

        }

    }// Class
}
