using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eye_track_api.Models
{
    public class TestGroup
    {
        [JsonProperty(PropertyName = "testId")]
        public int TestId { get; }

        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "DOB")]
        public string DOB { get; set; }

        [JsonProperty(PropertyName = "SEX")]
        public string SEX { get; set; }

        [JsonProperty(PropertyName = "DisorderDisability")]
        public string DisorderDisability { get; set; }

        [JsonProperty(PropertyName = "Hand")]
        public string Hand { get; set; }

        [JsonProperty(PropertyName = "Glasses")]
        public string Glasses { get; set; }

        public TestGroup(int testId, string firstName, string lastName, string dob, string sex, string disabilityDisorder, string hand, string glasses)
        {
            TestId = testId;
            FirstName = firstName;
            LastName = lastName;
            DOB = dob;
            SEX = sex;
            DisorderDisability = disabilityDisorder;
            Hand = hand;
            Glasses = glasses;

        }

        public static TestGroup FromTestId(int testId)
        {

            using (MySqlConnection conn = new MySqlConnection(Helpers.GetRDSConnectionString()))
            {

                conn.Open();

                string queryString = "SELECT * FROM `eye-tracker-schema`.`TestGroup` WHERE testID = @param1";
                MySqlCommand command = new MySqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@param1", testId);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        return new TestGroup(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4),
                            reader.GetString(5),
                            reader.GetString(6),
                            reader.GetString(7)
                        );
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
