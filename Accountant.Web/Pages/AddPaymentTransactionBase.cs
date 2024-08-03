using Accountant.Model.Dto;
using Accountant.Web.Services;
using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.ComponentModel;

namespace Accountant.Web.Pages
{
    public class AddPaymentTransactionBase : ComponentBase
    {
        [Parameter]
        public int userid { get; set; }

        [Parameter]
        public string username { get; set; }

        [Parameter]
        public string password { get; set; }

        [Inject]
        public IPaymentServices PaymentServices { get; set; }

        [Inject]
        public IJSRuntime JS { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public AddTransactionsStandardDto newTransaction { get; set; }

        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionTime { get; set; } = DateTime.Now;
        public string? Descriptions { get; set; }

        public string ErorMessage { get; set; }

        public async Task AddPayment_Click()
        {
            try
            {

                 newTransaction = new AddTransactionsStandardDto
                {
                    Userid = userid,
                    Amount = Amount,
                    Descriptions = Descriptions,
                    TransactionTime = TransactionTime
                };

                if (newTransaction != null)
                {
                   var addpayment =  await PaymentServices.AddTransaction(newTransaction);
                
                    if(addpayment != null) 
                    {
                        JS.InvokeVoidAsync("alert", "Your Transaction Add Compeletely !");
                        NavigationManager.NavigateTo($"/UserMainPage/{username}/{password}");
                    }
                    else
                    {
                        JS.InvokeVoidAsync("alert", "Your Transaction Isn't Complete ! ");
                    }
                }

                else
                {
                    ErorMessage = " Please fill Amount and Date  !";
                }

               
            }

            catch (Exception ex)
            {
                ErorMessage = ex.Message;
                ex.GetBaseException();
            }
        }
    } //Class
}

