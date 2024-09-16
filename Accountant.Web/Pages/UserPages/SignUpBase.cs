using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Accountant.Web.Pages.UserPages
{
    public class SignUpBase : ComponentBase
    {
        [Inject]
        public IUserServices UserServices { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Inject]
        public IJSRuntime JS { get; set; }

        public UserDto? NewUser { get; set; }
        public int userid { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Email { get; set; }
        public string? ImageURL { get; set; }
        public bool IsDisabled { get; set; }

        public string? ErrorMessage { get; set; }


        protected async void AddNewUser_Click()
        {
            try
            {
                if (Password == ConfirmPassword)
                {

                    if (Username != null && Password != null && Email != null)
                    {
                        IsDisabled = true;

                        NewUser = new UserDto
                        {
                            Email = Email,
                            Password = Password,
                            UserName = Username,
                            ImgURL = ImageURL
                        };
                        if (await UserServices.SignUp(NewUser) != null)
                        {
                            await JS.InvokeAsync<UserDto>("alert", "You Signup With success ! ");

                            navigationManager.NavigateTo($"/UserMainPage/{Username}/{Password}", true);
                        }

                        else
                        {
                            await JS.InvokeVoidAsync("alert", "Something went wrong while signup you ! ");
                            IsDisabled = false;
                            StateHasChanged();
                        }
                    }

                    else
                    {
                        await JS.InvokeAsync<UserDto>("alert", " You should fill Username , Password And Email field !");
                    }
                }

                else
                {
                    await JS.InvokeAsync<UserDto>("alert", "Your password and confirm should be match exactly !");
                }
            }

            catch (Exception ex)
            {
                IsDisabled = false;
                StateHasChanged();
                await JS.InvokeVoidAsync("alert", "Something went wrong while signup you maybe your username taken before ! ");
                ErrorMessage = ex.Message;
            }

        }

    }
}



