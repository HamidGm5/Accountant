using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;

namespace Accountant.Web.Pages.InstallmentPages
{
    public class InstallmentsBase : ComponentBase
    {
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
        public NavigationManager navigate { get; set; }

        public ICollection<InstallmentDto> Installments { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Installments = await services.GetInstallments(LoanID);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
