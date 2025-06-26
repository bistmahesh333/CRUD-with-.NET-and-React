using API.DataAccess;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignupController : ControllerBase
    {

        private readonly ISqlDataAccess _db;

        public SignupController(ISqlDataAccess db)
        {
            _db = db;
        }

        [HttpPost("AddEditSignup")]
        public async Task<IActionResult> AddEditSignup([FromBody] ATTSignup signup)
        {
            try
            {
                string sql = @"SELECT signup.cfn_add_edit_signup(
                                @Id, @Username, @Password, @FullName, @Dob, @Email)";

                var changesdata = new
                {
                    id = signup.id.ToString(),
                    username = signup.username.ToString(),
                    password = signup.password.ToString(),
                    fullname = signup.fullname.ToString(),
                    dob = signup.dob.ToString(),
                    email = signup.email.ToString()
                };

                var result = await _db.ExecuteScalarAsync<object, string>(sql, changesdata);

                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error", Error = ex.Message });
            }    
        }

        //this api is just to practice
        [HttpGet("run-practice")]
        public IActionResult RunPracticeCode()
        {
            Practice.SayHello(); // This runs your logic
            //return Ok("Practice logic executed.");
            return Ok(Practice.SayHello());
        }

        [HttpGet("run-add")]
        public IActionResult RunAddPractice()
        {
            //int result = Practice.Add(3, 5); // You can change 3 and 5 to any numbers you like
            return Ok(Practice.Add(3, 5)); // Returns 8 in this example
        }

        [HttpGet("run-anime-list")]
        public IActionResult RunAnimeList()
        {
            return Ok(GetAnimeList());
        }

        [NonAction] // prevents it from being treated as an API route
        public List<string> GetAnimeList()
        {
            return new List<string>
            {
                "Naruto Uzumaki",
                "Monkey D. Luffy",
                "Goku",
                "Saitama",
                "Levi Ackerman"
            };
        }



    }
}
