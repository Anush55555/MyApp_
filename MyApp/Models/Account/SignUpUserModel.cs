namespace MyApp.Models.Account
{
    public class SignUpUserModel
    {
        private List<SignUpUser> _signUpUsers = new List<SignUpUser>();

        public IEnumerable<SignUpUser> signUpUsers
        {
            get { return _signUpUsers; }
        }

        public void AddUser(SignUpUser signUpUser)
        {
            _signUpUsers.Add(signUpUser);
        }
    }
}
