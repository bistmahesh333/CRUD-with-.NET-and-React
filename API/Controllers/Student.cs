using API.Models;
using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Student : ControllerBase
    {

        private readonly ISqlDataAccess _db;
        private readonly object _configuration;

        public Student(ISqlDataAccess db)
        {
            _db = db;
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudentDetail([FromBody] studentAtt std)
        {
            try
            {
                string sql = @"SELECT student.cfn_add_student_detail(
                                @Id, @Name, @Address)";


                var result = await _db.ExecuteScalarAsync<studentAtt, string>(sql, std);

                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error", Error = ex.Message });
            }
        }

        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                string sql = @"SELECT student.cfn_get_all_students();";

                var result = await _db.LoadDataRefCursor<dynamic, dynamic>(sql, new { });

                return result.Count() == 0 ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error", Error = ex.Message });
            }
        }

        [HttpPost("EditStudent")]
        public async Task<IActionResult> EditStudent([FromBody] studentAtt std)
        {
            try
            {
                string sql = @"SELECT student.cfn_edit_student_detail(@Id, @Name, @Address)";

                var result = await _db.ExecuteScalarAsync<studentAtt, int>(sql, std);

                return Ok(new { Message = $"Student with ID {result} updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error", Error = ex.Message });
            }
        }




    }
}
