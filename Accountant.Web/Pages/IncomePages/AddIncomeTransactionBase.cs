using Accountant.Model.Dto;
using Accountant.Web.Services;
using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Accountant.Web.Pages.IncomePages
{
    public class AddIncomeTransactionBase : ComponentBase
    {
        [Parameter]
        public int Userid { get; set; }

        [Parameter]
        public string username { get; set; }

        [Parameter]
        public string password { get; set; }

        [Inject]
        public IIncomeServices IncomeServices { get; set; }

        [Inject]
        public IUserServices UserServices { get; set; }

        [Inject]
        public IJSRuntime JS { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public int? Id { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionTime { get; set; } = DateTime.Now;
        public string? Descriptions { get; set; }
        public int Count { get; set; } = 1;

        public string? ErrorMessage { get; set; }

        public async Task AddIncome_Click()
        {
            try
            {
                var newTransaction = new AddTransactionsStandardDto
                {
                    Userid = Userid,
                    Amount = Amount,
                    Descriptions = Descriptions,
                    TransactionTime = TransactionTime
                };

                if (newTransaction != null)
                {
                    if (Count == 1)
                    {
                        var addtransaction = await IncomeServices.AddTransaction(newTransaction);

                        if (addtransaction != null)
                        {
                            await JS.InvokeVoidAsync("alert", "Your Transaction Add Compeletely !");
                            NavigationManager.NavigateTo($"/UserMainPage/{username}/{password}");
                        }

                        else
                        {
                           await JS.InvokeVoidAsync("alert", "Your Transaction Isn't Complete ! ");
                        }
                    }
                    else if (Count > 1)
                    {
                        bool addtransaction = await IncomeServices.AddMultiIncomeTransaction(newTransaction, Count);

                        if (addtransaction)
                        {
                           await JS.InvokeVoidAsync("alert", "Your Transaction Add Compeletely !");
                            NavigationManager.NavigateTo($"/UserMainPage/{username}/{password}");
                        }
                        else
                        {
                            await JS.InvokeVoidAsync("alert", "Your Transaction Isn't Complete ! ");
                        }
                    }
                    else
                    {
                        await JS.InvokeVoidAsync("alert", "Count shouldn't be less than 1 !");
                    }
                }

                else
                {
                    ErrorMessage = " Please fill Amount and Date  !";
                }
            }

            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }// Class
}
