namespace Mediocrity.Models;

public class Project
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string[] Stack  { get; set; }
    public int ParticipantsNumber  { get; set; }
    public string[] Members {get; set;}
    public int Price  { get; set; } 
    public string ContactInfo { get; set; }
    public string ProjectID { get; set; }

    public Project()
    {
        Title = string.Empty;
        Description = string.Empty;
        ParticipantsNumber = 0;
        Price = 0;
        ContactInfo = string.Empty;
        ProjectID = string.Empty;
        Members = new string[]{ };
    }

    public Project(string title, string description, string[] stack, int participantsNumber, int price,
        string contactInfo)
    {
        Title = title;
        Description = description;
        Stack = stack;
        ParticipantsNumber = participantsNumber;
        Price = price;
        ContactInfo = contactInfo;
        ProjectID = title +  "_"  + participantsNumber.ToString() + "_"  + price.ToString() + "_" + contactInfo;
        Members = new string[]{" "};
    }
}
