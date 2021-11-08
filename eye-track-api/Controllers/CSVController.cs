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
    [Route("CsvRecord")]
    [ApiController]

    public class CSVController : ControllerBase
    {
        [HttpPost]
        [Route("post")]

        public IActionResult CreateCSVGroup([FromBody][Required] CsvRecord csvRecord)
        {
            if (csvRecord.CsvName == string.Empty || csvRecord.Question1 == null || csvRecord.Question2 == null ||
                csvRecord.Question3 == null || csvRecord.Question4 == null || csvRecord.Question5 == null ||
                csvRecord.Question6 == null || csvRecord.Question7 == null || csvRecord.Question8 == null ||
                csvRecord.Question9 == null || csvRecord.Question10 == null)
            {
                return Problem("could not process");
            }

            using (MySqlConnection conn = new MySqlConnection(Helpers.GetRDSConnectionString()))
            {
                conn.Open();

                MySqlCommand command = new MySqlCommand(@"INSERT INTO `eye-tracker-schema`.`XYCoordinates` (csvName, Question1, Question2, Question3, Question4, Question5, Question6, Question7, Question8, Question9, Question10) 
                                                        VALUES (@csvName, @Question1, @Question2, @Question3, @Question4, @Question5, @Question6, @Question7, @Question8, @Question9, @Question10)", conn);
                command.Parameters.AddWithValue("@csvName", csvRecord.CsvName);
                command.Parameters.AddWithValue("@Question1", csvRecord.Question1);
                command.Parameters.AddWithValue("@Question2", csvRecord.Question2);
                command.Parameters.AddWithValue("@Question3", csvRecord.Question3);
                command.Parameters.AddWithValue("@Question4", csvRecord.Question4);
                command.Parameters.AddWithValue("@Question5", csvRecord.Question5);
                command.Parameters.AddWithValue("@Question6", csvRecord.Question6);
                command.Parameters.AddWithValue("@Question7", csvRecord.Question7);
                command.Parameters.AddWithValue("@Question8", csvRecord.Question8);
                command.Parameters.AddWithValue("@Question9", csvRecord.Question9);
                command.Parameters.AddWithValue("@Question10", csvRecord.Question10);

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
