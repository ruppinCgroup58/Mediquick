//// התקנת ספריית SendGrid באמצעות NuGet
//// Install-Package SendGrid

//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SendGrid;
//using SendGrid.Helpers.Mail;
//using MEDIQUICK.Data;
//using MEDIQUICK.Models;
//using MEDIQUICK.ViewModels;
//using MEDIQUICK.BL;

//namespace MEDIQUICK.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly UserManager<User> _userManager;
//        private readonly ApplicationDbContext _context;
//        private const string SendGridApiKey = "YOUR_SENDGRID_API_KEY";

//        public AccountController(UserManager<User> userManager, ApplicationDbContext context)
//        {
//            _userManager = userManager;
//            _context = context;
//        }

//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Register(UserRegisterViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = new User { UserName = model.Email, Email = model.Email };
//                var result = await _userManager.CreateAsync(user, model.Password);

//                if (result.Succeeded)
//                {
//                    var code = new Random().Next(100000, 999999).ToString();
//                    await SendVerificationEmailAsync(model.Email, code);

//                    //_context.VerificationCodes.Add(new VerificationCode { Email = model.Email, Code = code });
//                    //await _context.SaveChangesAsync();

//                    return RedirectToAction("VerifyEmail", new { email = model.Email });
//                }
//                AddErrors(result);
//            }

//            return View(model);
//        }

//        public IActionResult VerifyEmail(string email)
//        {
//            return View(new VerifyEmailViewModel { Email = email });
//        }

//        [HttpPost]
//        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var verificationCode = await _context.VerificationCodes
//                    .FirstOrDefaultAsync(vc => vc.Email == model.Email && vc.Code == model.Code);

//                if (verificationCode != null)
//                {
//                    _context.VerificationCodes.Remove(verificationCode);
//                    await _context.SaveChangesAsync();

//                    var user = await _userManager.FindByEmailAsync(model.Email);
//                    if (user != null)
//                    {
//                        user.EmailConfirmed = true;
//                        await _userManager.UpdateAsync(user);
//                    }

//                    return RedirectToAction("Index", "Home");
//                }

//                ModelState.AddModelError(string.Empty, "Invalid verification code.");
//            }

//            return View(model);
//        }

//        private async Task SendVerificationEmailAsync(string email, string code)
//        {
//            var client = new SendGridClient(SendGridApiKey);
//            var from = new EmailAddress("your-email@example.com", "Your Name");
//            var subject = "Email Verification Code";
//            var to = new EmailAddress(email);
//            var plainTextContent = $"Your verification code is {code}";
//            var htmlContent = $"<strong>Your verification code is {code}</strong>";
//            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
//            var response = await client.SendEmailAsync(msg);
//        }

//        private void AddErrors(IdentityResult result)
//        {
//            foreach (var error in result.Errors)
//            {
//                ModelState.AddModelError(string.Empty, error.Description);
//            }
//        }
//    }
//}
