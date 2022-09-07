using BackSearch.DAL;
using BackSearchWebApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BackSearchWebApi.Controllers
{
    [ApiController]
    [Route("/user")]
    public class UserController : Controller
    {
        private UserRepository urRepo;
        public UserController()
        {
            urRepo = new UserRepository(new DataContext());
        }
        // POST: UserController/signUp
        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto userData)
        {
            if (urRepo.GetAll().FirstOrDefault(u => u.Mail == userData.Mail) == null)
            {
                User user = new User()
                {
                    Name = userData.Name,
                    Mail = userData.Mail,
                    Surname = userData.Surname,
                    Password = userData.Password,
                    Role = "user"
                };

                await urRepo.InsertAsync(user);
                return Ok(user);
            }
            return BadRequest("You are already registered.");

        }
        // POST: UserController/signIn
        [HttpGet("signIn")]
        public async Task<IActionResult> SignIn([FromQuery] LogInDto userData)
        {
            User ur = urRepo.GetAll().FirstOrDefault(u => u.Mail == userData.Mail && u.Password == userData.Password);
            if (ur != null)
            {
                return Ok(ur);
            }
            return BadRequest("Wrong e-mail or password.");

        }
    }


}
