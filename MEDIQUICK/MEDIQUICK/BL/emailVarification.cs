// EmailVerificationService.cs
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailVerificationService
{
    private readonly ApplicationDbContext _context;
    private readonly ISendGridClient _sendGridClient;
    private readonly IConfiguration _configuration;

    public EmailVerificationService(ApplicationDbContext context, ISendGridClient sendGridClient, IConfiguration configuration)
    {
        _context = context;
        _sendGridClient = sendGridClient;
        _configuration = configuration;
    }

    public async Task<bool> SendVerificationEmailAsync(string email)
    {
        var token = GenerateVerificationToken();
        var verificationRecord = new EmailVerification
        {
            Email = email,
            Token = token,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };

        _context.EmailVerifications.Add(verificationRecord);
        await _context.SaveChangesAsync();

        var msg = new SendGridMessage()
        {
            From = new EmailAddress(_configuration["SendGrid:FromEmail"], "Your App Name"),
            Subject = "אימות כתובת אימייל",
            PlainTextContent = $"לחץ על הקישור הבא כדי לאמת את כתובת האימייל שלך: {_configuration["App:Url"]}/verify?token={token}"
        };
        msg.AddTo(new EmailAddress(email));

        var response = await _sendGridClient.SendEmailAsync(msg);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> VerifyEmailAsync(string token)
    {
        var verification = await _context.EmailVerifications
            .FirstOrDefaultAsync(v => v.Token == token && v.ExpiresAt > DateTime.UtcNow);

        if (verification == null)
            return false;

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == verification.Email);
        if (user != null)
        {
            user.EmailConfirmed = true;
            _context.EmailVerifications.Remove(verification);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    private string GenerateVerificationToken() => Guid.NewGuid().ToString();
}

// ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<EmailVerification> EmailVerifications { get; set; }
}

// EmailVerification.cs
public class EmailVerification
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
}

// User.cs
public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    // Add other user properties as needed
}

// Startup.cs (partial)
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

    services.AddSingleton<ISendGridClient>(new SendGridClient(Configuration["SendGrid:ApiKey"]));
    services.AddScoped<EmailVerificationService>();

    // ... other service configurations
}

// EmailVerificationController.cs
[ApiController]
[Route("[controller]")]
public class EmailVerificationController : ControllerBase
{
    private readonly EmailVerificationService _verificationService;

    public EmailVerificationController(EmailVerificationService verificationService)
    {
        _verificationService = verificationService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendVerificationEmail([FromBody] string email)
    {
        if (!IsValidEmail(email))
            return BadRequest("Invalid email address");

        var result = await _verificationService.SendVerificationEmailAsync(email);
        if (result)
            return Ok("Verification email sent successfully");
        else
            return StatusCode(500, "Failed to send verification email");
    }

    [HttpGet("verify")]
    public async Task<IActionResult> VerifyEmail([FromQuery] string token)
    {
        var result = await _verificationService.VerifyEmailAsync(token);
        if (result)
            return Ok("Email verified successfully");
        else
            return BadRequest("Invalid or expired verification token");
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}