namespace Messages.Models;

public class Message
{
    public int Id { get; set; }
    
    public string Content { get; set; }
    
    public string User { get; set; }

    public string DateTime { get; set; }
}