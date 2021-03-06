using eye_track_api.Models;
using eye_track_api.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
/*using MySqlConnector;*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eye_track_api.Controllers
{
	[Route("TestGroup")]
	[ApiController]
	public class TestGroupController : ControllerBase
	{
		[HttpGet]
		[Route("get")]
		public IActionResult GetTestGroup([FromQuery][Required] string type, [FromQuery][Required] string identifier)
		{
			TestGroup testGroup = null;

			if (type.ToLower().Equals("id"))
			{
				int testId;

				if (int.TryParse(identifier, out testId))
				{
					testGroup = Models.TestGroup.FromTestId(testId);
				}
				else
				{
					return Problem("identifier must be an integer when type is id");
				}
			}
			/*else if (type.ToLower().Equals("email"))
			{
				testGroup = Models.User.FromEmail(identifier);
			}*/
			else
			{
				return Problem("invalid type");
			}

			if (testGroup == null)
			{
				return NotFound("not found");
			}
			else
			{
				return Ok(JsonConvert.SerializeObject(testGroup, Formatting.Indented));
			}
		}

		[HttpPost]
		[Route("post")]
		public IActionResult CreateTestGroup([FromBody][Required] TestGroup testGroup)
		{
			if (testGroup.FirstName == string.Empty || testGroup.LastName == null || testGroup.DOB == string.Empty || testGroup.SEX == null || testGroup.DisorderDisability == string.Empty)
			{
				return Problem("could not process");
			}

			using (MySqlConnection conn = new MySqlConnection(Helpers.GetRDSConnectionString()))
			{
				conn.Open();
				
				MySqlCommand command = new MySqlCommand(@"INSERT INTO `eye-tracker-schema`.`TestGroup` (FirstName, LastName, DOB, SEX, DisorderDisability, Hand, Glasses) VALUES (@FirstName, @LastName, @DOB, @SEX, @DisorderDisability, @Hand, @Glasses)", conn);
				command.Parameters.AddWithValue("@FirstName", testGroup.FirstName);
				command.Parameters.AddWithValue("@LastName", testGroup.LastName);
				command.Parameters.AddWithValue("@DOB", testGroup.DOB);
				command.Parameters.AddWithValue("@SEX", testGroup.SEX);
				command.Parameters.AddWithValue("@DisorderDisability", testGroup.DisorderDisability);
				command.Parameters.AddWithValue("@Hand", testGroup.Hand);
				command.Parameters.AddWithValue("@Glasses", testGroup.Glasses);

				int rows = command.ExecuteNonQuery();

				if (rows == 0)
				{
					return Problem("error creating");
				}

			}

			return Ok();
		}

		[HttpPatch("patch/{id}")]
		public IActionResult UpdateTestGroup([FromRoute] int id, [FromBody] Dictionary <string, string> patch)
		{
			string tableName = "TestGroup";
			string columnName = "testID";

			using (MySqlConnection conn = new MySqlConnection(Helpers.GetRDSConnectionString()))
			{
				conn.Open();

				MySqlCommand command = QueryBuilder.UpdateBuilder(patch, tableName, columnName, id);
				command.Connection = conn;
				int rows = command.ExecuteNonQuery();

				if (rows == 0)
				{
					return Problem("error creating");
                }
                else
                {
					return Ok();
                }
			}
		}
	}

}
