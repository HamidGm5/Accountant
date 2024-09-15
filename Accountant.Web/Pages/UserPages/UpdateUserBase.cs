using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Microsoft.AspNetCore.Components;

namespace Accountant.Web.Pages.UserPages
{
    public class UpdateUserBase : ComponentBase
    {
        [Parameter]
        public string Username { get; set; }

        [Parameter]
        public string Password { get; set; }

        [Inject]
        public IUserServices UserServices { get; set; }

        public UserDto User { get; set; }


        public int userid { get; set; } = 0;
        public string username { get; set; } = "";
        public string password { get; set; } = "";
        public string confirmPassword { get; set; } = "";
        public string email { get; set; } = "";
        public string? imgUrl { get; set; }

        public string? ErrorMessage { get; set; }


        protected async override Task OnParametersSetAsync()
        {
            try
            {
                User = await UserServices.Login(Username, Password);

                username = User.UserName;
                password = User.Password;
                email = User.Email;
                confirmPassword = User.Password;
                imgUrl = User.ImgURL;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected void UpdateUser_Click()
        {
            if (User != null)
            {
                if (confirmPassword == password)
                {
                    userid = (int)User.Id;
                    var newuser = new UserDto
                    {
                        Id = userid,
                        UserName = username,
                        Password = password,
                        Email = email,
                        ImgURL = imgUrl
                    };

                    UserServices.UpdateUser(newuser);
                }

                else
                {
                    ErrorMessage = "Your Password And Confirm Password Should Be Exactly Match !";
                }
            }

            else
            {
                ErrorMessage = "Your Account is Not Found !";
            }
        }

    }// Class
}
