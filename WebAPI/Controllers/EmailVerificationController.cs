using Business.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailVerificationController : ControllerBase
    {
        private readonly EmailVerificationService _verificationService;

        public EmailVerificationController(EmailVerificationService verificationService)
        {
            _verificationService = verificationService;
        }

        [HttpGet("email-verify/{token}")]
        public IActionResult VerifyEmail(string token)
        {
            var isValid = _verificationService.VerifyToken(token);

            if (isValid)
            {
                return Ok("E-posta adresiniz başarıyla doğrulandı.");
            }
            else
            {
                return BadRequest("Geçersiz veya süresi dolmuş token.");
            }
        }
    }
}
