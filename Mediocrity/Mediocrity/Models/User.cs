using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;


internal class User
{
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string Password  { get; set; }
    public string Email     { get; set; }
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
    
    public static bool isEqual(User user1, User user2)
    {
        if (user1 == null || user2 == null) { return false; }

        if (user1.Email != user2.Email)
        {
            error = "Email does not exist";
            return false;
        } else if (user1.Password != user2.Password)
        {
            error = "Wrong password to that email";
            return false;   
        }
        return true;
    }
}


class Project
{

}
