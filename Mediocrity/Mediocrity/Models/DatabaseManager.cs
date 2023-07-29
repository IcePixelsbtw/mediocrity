using System;
using System.Collections.Generic;
using System.Linq;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

class DatabaseManager
{
    public static IFirebaseClient establishDataBaseConnection()
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "uQErdNqESLoti6IGnqTqBxDyxTmBpur6RTWU4cvj",
            BasePath = "https://mediocrity-af691-default-rtdb.europe-west1.firebasedatabase.app"
        };

        IFirebaseClient client = new FirebaseClient(config);

        return client;
    }

    public static string safeEmail(string email)
    { 
        return email.Replace('.', '_');
    }

    public static bool doesExist(string email)
    {
        bool returnResult = false;
        IFirebaseClient client = DatabaseManager.establishDataBaseConnection();
       
        string safeEmail = DatabaseManager.safeEmail(email);
        string path = "Users/";
        
        FirebaseResponse result = client.Get(path);
        Dictionary<string, User> userData = result.ResultAs<Dictionary<string, User>>();

        foreach (var sameEmail in userData)
        {
            string getsame = sameEmail.Value.Email;
            if (email == getsame)
            {
                returnResult = true;
                Console.WriteLine("Email already taken...");
            }
        }
        
        return returnResult;
        
    }

    public static void createNewUser(string userFirstName,string userLastName, string userPassword, string userEmail, string[] userStack)
    {

        if (DatabaseManager.doesExist(userEmail))
        {
            return;
        }

        IFirebaseClient client = DatabaseManager.establishDataBaseConnection();
        
        User user = new User(userFirstName, userLastName, userPassword, userEmail, userStack);

        string safeEmail = DatabaseManager.safeEmail(userEmail);

        Console.WriteLine("Trying to set a value");

        SetResponse response = client.Set(@"Users/" + safeEmail, user);
        
        Console.WriteLine("Finished setting a value");
    }

}