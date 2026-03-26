using Microsoft.AspNetCore.Mvc;
using Rick_BackendTest_MidLevel.Model;
using System.Data.SqlClient;

namespace Rick_BackendTest_MidLevel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyOfficeACPDController : ControllerBase
    {
        private readonly string _connectionString;

        public MyOfficeACPDController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var list = new List<MyOfficeACPD>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM MyOffice_ACPD";

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(MapToModel(reader));
                }
            }

            return Ok(list);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            MyOfficeACPD data = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM MyOffice_ACPD WHERE ACPD_SID = @id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    data = MapToModel(reader);
                }
            }

            if (data == null)
                return NotFound();

            return Ok(data);
        }


        [HttpPost]
        public IActionResult Create(MyOfficeACPD model)
        {
            string newId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO MyOffice_ACPD
                (
                    ACPD_SID, ACPD_Cname, ACPD_Ename, ACPD_Sname, ACPD_Email,
                    ACPD_Status, ACPD_Stop, ACPD_StopMemo,
                    ACPD_LoginID, ACPD_LoginPWD, ACPD_Memo
                )
                VALUES
                (
                    @SID, @Cname, @Ename, @Sname, @Email,
                    @Status, @Stop, @StopMemo,
                    @LoginID, @LoginPWD, @Memo
                )";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@SID", newId);
                cmd.Parameters.AddWithValue("@Cname", (object?)model.ACPD_Cname ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Ename", (object?)model.ACPD_Ename ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Sname", (object?)model.ACPD_Sname ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object?)model.ACPD_Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", (object?)model.ACPD_Status ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Stop", (object?)model.ACPD_Stop ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@StopMemo", (object?)model.ACPD_StopMemo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LoginID", (object?)model.ACPD_LoginID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LoginPWD", (object?)model.ACPD_LoginPWD ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Memo", (object?)model.ACPD_Memo ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            model.ACPD_SID = newId;

            return CreatedAtAction(nameof(GetById), new { id = newId }, model);
        }


        [HttpPut("{id}")]
        public IActionResult Update(string id, MyOfficeACPD model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"UPDATE MyOffice_ACPD
                SET 
                    ACPD_Cname = @Cname,
                    ACPD_Ename = @Ename,
                    ACPD_Sname = @Sname,
                    ACPD_Email = @Email,
                    ACPD_Status = @Status,
                    ACPD_Stop = @Stop,
                    ACPD_StopMemo = @StopMemo,
                    ACPD_LoginID = @LoginID,
                    ACPD_LoginPWD = @LoginPWD,
                    ACPD_Memo = @Memo,
                    ACPD_UPDDateTime = GETDATE()
                WHERE ACPD_SID = @SID";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@SID", id);
                cmd.Parameters.AddWithValue("@Cname", (object?)model.ACPD_Cname ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Ename", (object?)model.ACPD_Ename ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Sname", (object?)model.ACPD_Sname ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object?)model.ACPD_Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", (object?)model.ACPD_Status ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Stop", (object?)model.ACPD_Stop ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@StopMemo", (object?)model.ACPD_StopMemo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LoginID", (object?)model.ACPD_LoginID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LoginPWD", (object?)model.ACPD_LoginPWD ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Memo", (object?)model.ACPD_Memo ?? DBNull.Value);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                    return NotFound();
            }

            return Ok(model);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM MyOffice_ACPD WHERE ACPD_SID = @id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                    return NotFound();
            }

            return NoContent();
        }

        private MyOfficeACPD MapToModel(SqlDataReader reader)
        {
            return new MyOfficeACPD
            {
                ACPD_SID = reader["ACPD_SID"]?.ToString(),
                ACPD_Cname = reader["ACPD_Cname"]?.ToString(),
                ACPD_Ename = reader["ACPD_Ename"]?.ToString(),
                ACPD_Sname = reader["ACPD_Sname"]?.ToString(),
                ACPD_Email = reader["ACPD_Email"]?.ToString(),
                ACPD_Status = reader["ACPD_Status"] == DBNull.Value ? null : (byte?)reader["ACPD_Status"],
                ACPD_Stop = reader["ACPD_Stop"] == DBNull.Value ? null : (bool?)reader["ACPD_Stop"],
                ACPD_StopMemo = reader["ACPD_StopMemo"]?.ToString(),
                ACPD_LoginID = reader["ACPD_LoginID"]?.ToString(),
                ACPD_LoginPWD = reader["ACPD_LoginPWD"]?.ToString(),
                ACPD_Memo = reader["ACPD_Memo"]?.ToString()
            };
        }
    }
}