using Messages.Models;
using Microsoft.AspNetCore.Mvc;

namespace Messages.Controllers;

[ApiController]
[Route("[controller]")]
public class MessagesController : ControllerBase
{
    private readonly ILogger<MessagesController> _logger;

    public static int IdCounter = 0;

    public static List<Message> Messages = new();

    private DbService _dbService;
    

    public MessagesController(ILogger<MessagesController> logger, DbService dbService)
    {
        this._logger = logger;
        _dbService = dbService;
    }
    
    [HttpGet(Name = "GetAllMessages")]
    public IEnumerable<Message> GetAllMessages()
    {
        _logger.Log(LogLevel.Information, "Someone requested get all messages");
        return Messages;
    } 
    
    [HttpGet("{id}", Name = "GetMessageById")]
    public IActionResult GetMessageById(int id)
    {
        var message = Messages.FirstOrDefault(m => m.Id == id);

        if (message == null)
        {
            return NotFound("Message not found");
        }

        return Ok(message);
    } 
    
    [HttpPut("{id}", Name = "EditMessage")]
    public IActionResult EditMessage(int id, Message editedMessage)
    {
        var message = Messages.FirstOrDefault(m => m.Id == id);

        if (message == null)
        {
            return NotFound("Message not found");
        }

        message.Content = editedMessage.Content;
        message.User = editedMessage.User;
        message.DateTime = editedMessage.DateTime;

        return Ok(message);
    } 
    
    [HttpPost(Name = "CreateNewMessage")]
    public IActionResult CreateNewMessage(Message message)
    {
        this._dbService.saveMessage(message);
        
        if (message.User == null)
        {
            return BadRequest("You need to specify user");
        }

        message.Id += IdCounter;
        
        Messages.Add(message);

        return Created("User successfully created", message);
    } 
}