using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using elsaeedTea.data.Entities;
using elsaeedTea.service.Services.AuthenticationServices;
using elsaeedTea.service.Services.AuthenticationServices.Dtos;
using elsaeedTea.service.Services.EmailServices;
using elsaeedTea.service.Services.EmailServices.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace elsaeedTea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IJwtTokenService jwtTokenService, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
            _emailSender = emailSender;
        }

        [HttpPost("userRegister")]
        public async Task<IActionResult> UserRegister(registerDto registerDto)
        {
            var email = registerDto.Email;
            if (email.Contains(' '))
            {
                return BadRequest("The email format shouldn't have any spaces.");
            }
            // تحقق من وجود @ في البريد الإلكتروني
            if (!email.Contains('@'))
            {
                return BadRequest("The email format is invalid.");
            }

            // استخراج الجزء الذي بعد الـ "@" للتأكد أنه مكتوب بالكامل بحروف صغيرة
            var domain = email.Split('@').Last();  // النطاق بعد "@"

            if (domain != domain.ToLower())  // إذا كان النطاق يحتوي على أحرف كبيرة
            {
                return BadRequest("Invalid email format. The domain must be in lowercase.");
            }

            // التأكد من أن النطاق هو "@gmail.com" فقط
            if (!email.EndsWith("@gmail.com"))
            {
                return BadRequest("Only Gmail accounts are allowed.");
            }

            var user = new ApplicationUser
            {
                UserName = registerDto.Email.Split('@')[0],
                Email = registerDto.Email,
                FullName = registerDto.FullName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                // تعيين دور (اختياري)
                var role = "User";
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
                await _userManager.AddToRoleAsync(user, role);

                // إنشاء التوكن
                var token = _jwtTokenService.GenerateJwtToken(user);

                return Ok(new
                {
                    Message = "Registration successful",
                    Token = token,
                    UserId = user.Id,
                    FullName = user.FullName,
                    Email = user.Email
                });
            }
            if (result.Errors.Any(e => e.Code == "DuplicateUserName" || e.Code == "DuplicateEmail"))
            {
                return BadRequest("This email is already exist.");
            }
            else if (result.Errors.Any(e => e.Code == "PasswordTooWeak"))
            {
                return BadRequest("Password is too weak.");
            }
            else if (result.Errors.Any(e => e.Code == "InvalidEmail"))
            {
                return BadRequest("The email format is invalid.");
            }
            else
            {
                return BadRequest(new { Message = "Registration failed", Errors = result.Errors.Select(e => e.Description) });
            }

        }

        [Authorize]
        [HttpGet("getUserId")]
        public async Task<IActionResult> GetUserId()
        {
            // الحصول على التوكن من الهيدر
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { Message = "Token is missing" });
            }

            // فك تشفير التوكن لاستخراج الـ Claims
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // استخراج الـ email من التوكن
            var email = jwtToken?.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized(new { Message = "Email not found in token" });
            }

            // البحث عن المستخدم باستخدام الـ email
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Unauthorized(new { Message = "User not found" });
            }

            return Ok(new { UserId = user.Id });
        }














        [Authorize]
        [HttpGet("getFullName")]
        public async Task<IActionResult> GetFullName()
        {
            // الحصول على التوكن من الهيدر
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { Message = "Token is missing" });
            }

            // فك تشفير التوكن لاستخراج الـ Claims
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // استخراج الـ FullName من التوكن
            var fullName = jwtToken?.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value;

            if (string.IsNullOrEmpty(fullName))
            {
                return Unauthorized(new { Message = "FullName not found in token" });
            }

            return Ok(new { FullName = fullName });
        }









        //احتمال كبير منستخدمهاش 
        //[HttpPost("adminRegister")]
        //public async Task<IActionResult> AdminRegister(registerDto registerDto)
        //{
        //    var user = new ApplicationUser
        //    {
        //        UserName = registerDto.Email,
        //        Email = registerDto.Email,
        //        FullName = registerDto.FullName
        //    };

        //    var result = await _userManager.CreateAsync(user, registerDto.Password);

        //    if (result.Succeeded)
        //    {
        //        // تعيين دور (اختياري)
        //        var role = "Admin";
        //        if (!await _roleManager.RoleExistsAsync(role))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole(role));
        //        }
        //        await _userManager.AddToRoleAsync(user, role);

        //        // إنشاء التوكن
        //        var token = _jwtTokenService.GenerateJwtToken(user);

        //        return Ok(new
        //        {
        //            Message = "Registration successful",
        //            Token = token,
        //            UserId = user.Id,
        //            FullName = user.FullName,
        //            Email = user.Email
        //        });
        //    }

        //    return BadRequest(result.Errors.Select(e => e.Description));
        //}




        [HttpPost("login")]
        public async Task<IActionResult> Login(loginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid email or password" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // إنشاء التوكن
                var token = _jwtTokenService.GenerateJwtToken(user);

                return Ok(new
                {
                    Message = "Login successful",
                    Token = token,
                    UserId = user.Id,
                    FullName = user.FullName,
                    Email = user.Email
                });
            }

            return Unauthorized(new { Message = "Invalid email or password" });
        }







        [Authorize]
        [HttpGet("secure-data")]
        public IActionResult GetSecureData()
        {
            return Ok(new { message = "This is secured data" });
        }







        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(changePasswordDto.Email);
            if (user == null)
            {
                return BadRequest(new { Message = "User not found." });
            }

            // التحقق من كلمة المرور القديمة باستخدام CheckPasswordSignInAsync
            var passwordValid = await _signInManager.CheckPasswordSignInAsync(user, changePasswordDto.OldPassword, lockoutOnFailure: false);

            if (!passwordValid.Succeeded)
            {
                return BadRequest(new { Message = "Old password is incorrect." });
            }

            // إذا كانت كلمة المرور القديمة صحيحة، نتابع مع تغيير كلمة المرور الجديدة
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Password changed successfully." });
            }

            return BadRequest(new { Message = "Failed to change password", Errors = result.Errors.Select(e => e.Description) });
        }













        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // توليد الرمز (Token)
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // صلاحية الرمز (1 ساعة)
            var tokenExpirationTime = DateTime.UtcNow.AddHours(1);  // صلاحية لمدة ساعة

            // توليد الرابط الذي يحتوي على الـ Token
            //var resetLink = Url.Action("ResetPassword", "Auth", new { token = token, email = user.Email }, Request.Scheme);

            var resetLink = $"{Request.Scheme}://{Request.Host}/Auth/ResetPassword?token={token}&email={user.Email}";


            // إرسال البريد الإلكتروني
            await _emailSender.SendEmailAsync(user.Email, "Password Reset", $"Click on this link to reset your password: {resetLink}");

            return Ok(new { Message = "Password reset link has been sent." });
        }












        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // التحقق من صلاحية الرمز إذا كان قد انتهت صلاحياته
            var tokenExpirationTime = DateTime.UtcNow.AddHours(1);  // صلاحية لمدة ساعة
            if (DateTime.UtcNow > tokenExpirationTime)
            {
                return BadRequest("The reset link has expired.");
            }

            // إعادة تعيين كلمة المرور باستخدام الرمز
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            if (result.Succeeded)
            {
                return Ok(new { Message = "Password reset successfully." });
            }

            return BadRequest("Failed to reset password.");
        }









    }
}
