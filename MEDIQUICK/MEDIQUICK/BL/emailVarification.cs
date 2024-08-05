// התקנת ספריית SendGrid באמצעות NuGet
// Install-Package SendGrid

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Google.Cloud.AIPlatform.V1;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using YourNamespace.;
using YourNamespace.Models;
using YourNamespace.ViewModels;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    private const string SendGridApiKey = "YOUR_SENDGRID_API_KEY";

    public AccountController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var code = new Random().Next(100000, 999999).ToString();
                await SendVerificationEmailAsync(model.Email, code);

                _context.VerificationCodes.Add(new VerificationCode { Email = model.Email, Code = code });
                await _context.SaveChangesAsync();

                return RedirectToAction("VerifyEmail", new { email = model.Email });
            }
            AddErrors(result);
        }

        return View(model);
    }

    public IActionResult VerifyEmail(string email)
    {
        return View(new VerifyEmailViewModel { Email = email });
    }

    [HttpPost]
    public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
    {
        if (ModelState.IsValid)
        {
            var verificationCode = await _context.VerificationCodes
                .FirstOrDefaultAsync(vc => vc.Email == model.Email && vc.Code == model.Code);

            if (verificationCode != null)
            {
                _context.VerificationCodes.Remove(verificationCode);
                await _context.SaveChangesAsync();

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid verification code.");
        }

        return View(model);
    }

    private async Task SendVerificationEmailAsync(string email, string code)
    {
        var client = new SendGridClient(SendGridApiKey);
        var from = new EmailAddress("your-email@example.com", "Your Name");
        var subject = "Email Verification Code";
        var to = new EmailAddress(email);
        var plainTextContent = $"Your verification code is {code}";
        var htmlContent = $"<strong>Your verification code is {code}</strong>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}

// מודל VerifyEmailViewModel
namespace YourNamespace.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Code { get; set; }
    }
}

// תצוגה VerifyEmail
@model YourNamespace.ViewModels.VerifyEmailViewModel

<h2> Verify Email</h2>

<form asp-action="VerifyEmail">
    <div class= "form-group" >
        < label asp -for= "Email" ></ label >
        < input asp -for= "Email" class= "form-control" readonly />
    </ div >
    < div class= "form-group" >
        < label asp -for= "Code" ></ label >
        < input asp -for= "Code" class= "form-control" />
    </ div >
    < button type = "submit" class= "btn btn-primary" > Verify </ button >
</ form >

// מודל UserRegisterViewModel
namespace YourNamespace.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

// מחלקת VerificationCode
namespace YourNamespace.Models
{
    public class VerificationCode
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
    }
}

// הוספת DbSet ל-ApplicationDbContext
public class ApplicationDbContext : DbContext
{
    public DbSet<VerificationCode> VerificationCodes { get; set; }
}
