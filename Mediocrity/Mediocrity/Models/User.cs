using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;


public class User
{
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string Password  { get; set; }
    public string Email     { get; set; }
    
    public string TeamPosition { get; set; }
    
    public string workExperience { get; set; }
   
    public string englishLevel { get; set; }
    public string[] Skills { get; set; }

    public string CurrentProject { get; set; }
    
    
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