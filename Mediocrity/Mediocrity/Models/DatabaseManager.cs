using System;
using System.Collections.Generic;
using System.Linq;
using FirebaseAdmin.Messaging;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Mediocrity.Models;

class DatabaseManager
{
    
    //Function to establish connection with DB; Returning a client instance and needs to be assigned to it (IFirebaseClient client = DatabaseManager.establishDataBaseConnection())
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

    
    // Return safe email to use as a key inside a DB
    public static string safeEmail(string email)
    { 
        return email.Replace('.', '_');
    }

    
    // Checks if user with that email already exists.
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

    // Function for logging the user in, checks if email and password combination exists in DB. Stops if doesExist == true.
    public static bool isEqual(User user1, User user2)
    {
        if (user1 == null || user2 == null) { return false; }

        if (user1.Email != user2.Email)
        {
            return false;
        } else if (user1.Password != user2.Password)
        {
            return false;   
        }
        return true;
    }
    
    
    // Creates a new user with parameters as in constructor
    public static bool createNewUser(string userFirstName,string userLastName, string userPassword, string userEmail, string[] userStack)
    {

        if (DatabaseManager.doesExist(userEmail))
        {
            return false;
        }

        IFirebaseClient client = DatabaseManager.establishDataBaseConnection();
        
        User user = new User(userFirstName, userLastName, userPassword, userEmail, userStack);

        string safeEmail = DatabaseManager.safeEmail(userEmail);

        Console.WriteLine("Trying to set a value");

        SetResponse response = client.Set(@"Users/" + safeEmail, user);
        
        Console.WriteLine("Finished setting a value");

        return true;
    }
    
    
    // Function to add user into the project instance in DB once they connected 
    public static void userJoinedProject(string userEmail, string projectID)
    {       
        IFirebaseClient client = DatabaseManager.establishDataBaseConnection();

        string path = @"Projects/" + projectID + "/Members/";
        
        FirebaseResponse result = client.Get(path);
        string[] userData = result.ResultAs<string[]>();
        
        string[] tempArray = userData;

        tempArray = DatabaseManager.AddElement(tempArray, userEmail);
        Console.WriteLine(tempArray.Length);
        
        SetResponse response = client.Set(@"Projects/" + projectID + "/Members/" , tempArray);
    }
    
    
    // Function to delete a user from the project instance in DB once they decided to leave
    public static void userLeftProject(string userEmail, string projectID)
    {
        IFirebaseClient client = DatabaseManager.establishDataBaseConnection();

        string path = @"Projects/" + projectID + "/Members/";
        
        FirebaseResponse result = client.Get(path);
        string[] userData = result.ResultAs<string[]>();
        
        string[] tempArray = userData;

        tempArray = DatabaseManager.RemoveElement(tempArray, userEmail);
        
        SetResponse response = client.Set(@"Projects/" + projectID + "/Members/" , tempArray);
    }
    
    
    
    // Checks if project with the same id exists
    public static bool doesProjectExist(string projectID)
    {
        bool returnResult = false;
        IFirebaseClient client = DatabaseManager.establishDataBaseConnection();
       
        string path = "Projects/";
        
        FirebaseResponse result = client.Get(path);
        Dictionary<string, Project> projectData = result.ResultAs<Dictionary<string, Project>>();

        foreach (var sameProject in projectData)
        {
            string getsame = sameProject.Value.ProjectID;
            if (projectID == getsame)
            {
                returnResult = true;
                Console.WriteLine("Project name already taken...");
            }
        }
        
        return returnResult;
    }
    
    
    
    // Creates a new project with parameters as in constructor. Stops if doesProjectExist == true.

    public static bool createNewProject(string title, string description, string[] stack, int participantsNumber, int price, string contactInfo)
    {
        
        Project project = new Project(title, description, stack, participantsNumber, price, contactInfo);

        if (DatabaseManager.doesProjectExist(project.ProjectID))
        {
            Console.WriteLine("Project name already exists. Please try to change it");
            return false;
        }


        IFirebaseClient client = DatabaseManager.establishDataBaseConnection();

        string projectID = project.ProjectID;

        Console.WriteLine("Trying to set a value");

        SetResponse response = client.Set(@"Projects/" + projectID, project);
        
        Console.WriteLine("Finished setting a value");
        return true;
    }
    
    
    // Adds an element into array
    private static string[] AddElement(string[] inputArray, string adding)
    {
        string[] newArray = new string[inputArray.Length + 1];
        for(int i = 0; i < inputArray.Length; i++)
        {
            newArray[i] = inputArray[i];
        }
        newArray[inputArray.Length] = adding;
        return newArray;
    }
    
    // Removes an element from the array by value
    
    private static string[] RemoveElement(string[] removeFrom, string removeElement)
    {
        int id = 0;
        for(int i = 0; i < removeFrom.Length; i++)
        {
            if (removeFrom[i] == removeElement)
                id = i;
        }   

        string[] newArray = new string[removeFrom.Length - 1];

        int j = 0;
        for (int i = 0; i < removeFrom.Length; i++)
        {
            if (id == i)
                continue;
            newArray[j] = removeFrom[i];
            j++;
        }
        return newArray;
    }

}