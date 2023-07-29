using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;


public class User
{
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string Password  { get; set; }
    public string Email     { get; set; }
    
    public string teamPosiion { get; set; }
    public string[] Skills { get; set; }

    public static string error { get; set; }
    public User()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
    }
    
    public User(string firstName, string lastName, string password, string email, string[] skills)
    {
        FirstName = firstName; 
        LastName = lastName; 
        Password = password;
        Email = email;
        Skills = skills;
    }
    
    
}


class Project
{

}

// LOG IN
/*

            FirebaseResponse result = client.Get(@"Users/" + safeEmail);
            User ResultUser = result.ResultAs<User>();

            User CurrentUser = new User()
            {
                Email = emailTextBox.Text,
                Password = passwordTextBox.Text
            };

            firstName = ResultUser.FirstName;
            lastName = ResultUser.LastName;

            if (User.isEqual(ResultUser, CurrentUser))
            {
                Hide();
                MainScreenViewController msvc = new MainScreenViewController();
                msvc.Closed += (s, args) => Close();
                msvc.Show();
            }
            else 
            {
                MessageBox.Show("Ooooops... Some error occured ;d");
            }
*/