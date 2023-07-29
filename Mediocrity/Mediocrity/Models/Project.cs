namespace Mediocrity.Models;

public class Project
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string[] Stack  { get; set; }
    public string ParticipantsNumber  { get; set; }
    public string[] Participants {get; set;}
    public int Price  { get; set; }
    public string ContactInfo { get; set; }

    public Project()
    {
        Title = string.Empty;
        Description = string.Empty;
        ParticipantsNumber  = string.Empty;
        Price  = 0;
        ContactInfo = string.Empty;
    }

    public Project(string title, string description, string[] stack, string participantsNumber, int price,
        string contactInfo)
    {
        Title = title;
        Description = description;
        Stack = stack;
        ParticipantsNumber = participantsNumber;
        Price = price;
        ContactInfo = contactInfo;
    }
    
    
}
