
using eye_track_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eye_track_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Hello : ControllerBase
    {
        //get
        [HttpGet]
        [Route("get")]
        public string Get()
        {
            return "returned GET";
        }

        //external
        [HttpGet]
        [Route("external")]
        public string External()
        {
            try
            {
                HttpClient client = new HttpClient();
                var requestTask = client.GetStringAsync("http://www.google.com");
                requestTask.Wait();
                return "external connectivity PASS";
            }
            catch (Exception e)
            {
                return "external connectivity FAIL";
            }
        }

        //database
        [HttpGet]
        [Route("database")]
        public string Database()
        {
            try
            {
                string queryString = "SELECT * FROM `eye-tracker-schema`.`Account`";
                string connectionString = Helpers.GetRDSConnectionString();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(queryString, connection);
                    connection.Open();
                    var result = command.ExecuteScalar();
                    connection.Close();
                    return string.Format("db connection ok, {0} users in database", result);
                }
            }
            catch (Exception e)
            {
                return "db connection error: " + e.ToString();
            }
        }

    }
}
