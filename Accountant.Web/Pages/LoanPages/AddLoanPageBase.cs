using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Accountant.Web.Pages.LoanPages
{
    public class AddLoanPageBase : ComponentBase
    {
        [Parameter]
        public int UserID { get; set; }
        [Parameter]
        public string Username { get; set; }
        [Parameter]
        public string Password { get; set; }

        [Inject]
        public ILoanServices loanServices { get; set; }
        [Inject]
        public IJSRuntime js { get; set; }
        [Inject]
        public NavigationManager navigate { get; set; }

        public double Amount { get; set; }
        public int Period { get; set; }
        public float Percentage { get; set; }
        public string? Description { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;

        public string ErrorMessage { get; set; }

        public async void SubmitNewLoan_Click()
        {
            try
            {
                if (Amount != 0 && Period != 0)
                {
                    LoanDto newLoan = new LoanDto()
                    {
                        Userid = UserID,
                        Description = Description,
                        LoanAmount = Amount,
                        Percentage = Percentage,
                        PeriodPerMonth = Period,
                        StartTime = StartTime
                    };

                    var Response = await loanServices.AddNewLoan(newLoan);
                    if (Response)
                    {
                        await js.InvokeVoidAsync("alert", "Your loan add successfully");
                        navigate.NavigateTo($"/Loans/{UserID}/{Username}/{Password}");
                        StateHasChanged();
                    }
                    else
                    {
                        await js.InvokeVoidAsync("alert", "somthing went wrong try again !");
                    }

                }
                else
                {
                    await js.InvokeVoidAsync("alert", "You should fill the essential field !");
                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
