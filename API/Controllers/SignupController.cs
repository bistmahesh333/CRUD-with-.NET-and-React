using API.DataAccess;
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

                //var changesdata = new
                //{
                //    id = signup.id.ToString(),
                //    username = signup.username.ToString(),
                //    password = signup.password.ToString(),
                //    fullname = signup.fullname.ToString(),
                //    dob = signup.dob.ToString(),
                //    email = signup.email.ToString()
                //};

                var result = await _db.ExecuteScalarAsync<ATTSignup, string>(sql, signup);

                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error", Error = ex.Message });
            }    
        }


        //[HttpPost("AddEditSignup")]
        //public async Task<IActionResult> AddEditSignup([FromBody] ATTSignup signup)
        //{
        //    try
        //    {
        //        string sql = @"SELECT signup.cfn_add_edit_signup(
        //                        @Id, @Username, @Password, @FullName, @Dob, @Email)";

        //        var result = await _db.ExecuteScalarAsync<ATTSignup, string>(sql, signup);

        //        return Ok(new { Message = result });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "Error", Error = ex.Message });
        //    }
        //}



        [HttpGet("GetSignup")]
        public async Task<IActionResult> GetSignup([FromQuery] int? id)
        {
            try
            {
                string sql = @"SELECT signup.cfn_get_signup(@Id);";

                var parameters = new { Id = id };

                var result = await _db.LoadDataRefCursor<dynamic, dynamic>(sql, parameters);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error", Error = ex.Message });
            }
        }

 
        //public async Task<IActionResult> GetSignup([FromQuery] int? id)
        //{
        //    try
        //    {
        //        await using var conn = new NpgsqlConnection();
        //        await conn.OpenAsync();

        //        await using var cmd = conn.CreateCommand();
        //        cmd.CommandText = "SELECT signup.cfn_get_signup(@Id)";
        //        cmd.Parameters.AddWithValue("Id", (object?)id ?? DBNull.Value);

        //        // First, get the cursor name
        //        var cursorName = (string)await cmd.ExecuteScalarAsync();

        //        // Now fetch data from the cursor
        //        cmd.CommandText = $"FETCH ALL FROM \"{cursorName}\";";
        //        cmd.Parameters.Clear();

        //        var result = new List<Dictionary<string, object>>();

        //        await using (var reader = await cmd.ExecuteReaderAsync())
        //        {
        //            while (await reader.ReadAsync())
        //            {
        //                var row = new Dictionary<string, object>();
        //                for (int i = 0; i < reader.FieldCount; i++)
        //                    row[reader.GetName(i)] = reader.GetValue(i);
        //                result.Add(row);
        //            }
        //        }

        //        // Close the cursor
        //        cmd.CommandText = $"CLOSE \"{cursorName}\";";
        //        await cmd.ExecuteNonQueryAsync();

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "Error", Error = ex.Message });
        //    }
        //}






        //public async Task<List<LanguageATT>> GetLanguage(string? langId = null)
        //{
        //    try
        //    {
        //        string SP = @"SELECT ""librarymanagement"".fn_get_language(@p_lang_cd);";
        //        List<LanguageATT> lang = (await _db.LoadDataRefCursor<LanguageATT, dynamic>(SP, new { p_lang_cd = langId })).ToList();
        //        return lang;
        //    }
        //    catch (Exception e)
        //    {

        //        _logger.LogError("MasterDLL: List : Error:{0}", e.Message);
        //        throw new Exception("Error while fetching Language List");
        //    }
        //}




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
