using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberTypeController : ControllerBase
    {

        private readonly ISqlDataAccess _db;
        private readonly object _configuration;

        public MemberTypeController(ISqlDataAccess db)
        {
            _db = db;
        }


        [HttpGet("GetMemberTypes")]
        public async Task<IActionResult> GetMemberTypes(int? memberTypeId = null)
        {
            try
            {
                string sql = "SELECT member.fn_get_member_type(@p_member_type_id);";

                var parameters = new { p_member_type_id = memberTypeId };

                var result = await _db.LoadDataRefCursor<dynamic, dynamic>(sql, parameters);

                return  Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error", Error = ex.Message });
            }
        }


    }
}
