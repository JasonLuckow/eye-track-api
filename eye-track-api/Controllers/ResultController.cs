using eye_track_api.Models;
using eye_track_api.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eye_track_api.Controllers
{
    [Route("Result")]
    [ApiController]

    public class ResultController : ControllerBase
    {
        [HttpPost]
        [Route("post")]

        public IActionResult CreateResultGroup([FromBody][Required] Result result)
        {
            if (result.Question1 == null || result.Question2 == null || result.Question3 == null || 
                result.Question4 == null || result.Question5 == null || result.Question6 == null || 
                result.Question7 == null || result.Question8 == null || result.Question9 == null || 
                result.Question10 == null)
            {
                return Problem("could not process");
            }

            using (MySqlConnection conn = new MySqlConnection(Helpers.GetRDSConnectionString()))
            {
                conn.Open();

                MySqlCommand command = new MySqlCommand(@"INSERT INTO `eye-tracker-schema`.`TestResults` (Question1, Question2, Question3, Question4, Question5, Question6, Question7, Question8, Question9, Question10) 
                                                        VALUES (@Question1, @Question2, @Question3, @Question4, @Question5, @Question6, @Question7, @Question8, @Question9, @Question10)", conn);
                command.Parameters.AddWithValue("@Question1", result.Question1);
                command.Parameters.AddWithValue("@Question2", result.Question2);
                command.Parameters.AddWithValue("@Question3", result.Question3);
                command.Parameters.AddWithValue("@Question4", result.Question4);
                command.Parameters.AddWithValue("@Question5", result.Question5);
                command.Parameters.AddWithValue("@Question6", result.Question6);
                command.Parameters.AddWithValue("@Question7", result.Question7);
                command.Parameters.AddWithValue("@Question8", result.Question8);
                command.Parameters.AddWithValue("@Question9", result.Question9);
                command.Parameters.AddWithValue("@Question10", result.Question10);

                int rows = command.ExecuteNonQuery();

                if (rows == 0)
                {
                    return Problem("error creating");
                }
            }
            return Ok();
        }
    }
}



