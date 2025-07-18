using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Accountant.Web.Pages.AdminPages
{
    public class AdminControlPageBase : ComponentBase
    {
        [Parameter]
        public string AdminSpec { get; set; }
        [Parameter]
        public string Password { get; set; }

        [Inject]
        public IAdminServices AdminServices { get; set; }
        [Inject]
        public IJSRuntime js { get; set; }


        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
    }
}
