using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Accountant.Web.Pages.InstallmentPages
{
    public class DisplayInstallmentsBase : ComponentBase
    {
        [Parameter]
        public ICollection<InstallmentDto> Installments { get; set; }
        [Parameter]
        public int UserID { get; set; }
        [Parameter]
        public string Username { get; set; }
        [Parameter]
        public string Password { get; set; }
        [Parameter]
        public int LoanID { get; set; }

        [Inject]
        public IinstallmentServices services { get; set; }
        [Inject]
        public IPaymentServices Paymentservices { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public IJSRuntime JS { get; set; }

        public async Task PayInstallment(int ID)
        {
            var Response = await services.UpdatePay(ID);

            AddTransactionsStandardDto transaction = new AddTransactionsStandardDto()
            {
                Amount = Installments.FirstOrDefault().Amount,
                Descriptions = "Pay for Loan",
                TransactionTime = DateTime.Now,
                Userid = UserID
            };

            var TransactionResponse = await Paymentservices.AddTransaction(transaction);

            if (Response && TransactionResponse != null)
            {
                StateHasChanged();
                await JS.InvokeVoidAsync("alert", "Your installment paid successfully !");
                Navigation.NavigateTo(Navigation.Uri, true);
            }
            else
            {
                await JS.InvokeVoidAsync("alert", "something get wrong !");
            }
        }
    }


}
