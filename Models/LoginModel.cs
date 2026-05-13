namespace Chapeau.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginModel()
        {

        }

        public LoginModel(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }
    }
}
