using DataAccess;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignupController : ControllerBase
    {

        private readonly ISqlDataAccess _db;
        private readonly object _configuration;

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

                var result = await _db.ExecuteScalarAsync<ATTSignup, string>(sql, signup);

                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error", Error = ex.Message });
            }    
        }



        [HttpGet("GetSignup")]
        public async Task<IActionResult> GetSignup([FromQuery] int? id)
        {
            try
            {
                string sql = @"SELECT signup.cfn_get_signup(@Id);";    

                var parameters = new { Id = id };

                var result = await _db.LoadDataRefCursor<dynamic, dynamic>(sql, parameters);

                return result.Count() ==0 ? NotFound():Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error", Error = ex.Message });
            }
        }






    }
}
