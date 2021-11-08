using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eye_track_api.Models
{
    public class CsvRecord
    {
        [JsonProperty(PropertyName = "csvID")]
        public int CsvID { get; }

        [JsonProperty(PropertyName = "csvName")]
        public string CsvName { get; set; }

        [JsonProperty(PropertyName = "Question1")]
        public string Question1 { get; set; }

        [JsonProperty(PropertyName = "Question2")]
        public string Question2 { get; set; }

        [JsonProperty(PropertyName = "Question3")]
        public string Question3 { get; set; }

        [JsonProperty(PropertyName = "Question4")]
        public string Question4 { get; set; }

        [JsonProperty(PropertyName = "Question5")]
        public string Question5 { get; set; }

        [JsonProperty(PropertyName = "Question6")]
        public string Question6 { get; set; }

        [JsonProperty(PropertyName = "Question7")]
        public string Question7 { get; set; }

        [JsonProperty(PropertyName = "Question8")]
        public string Question8 { get; set; }

        [JsonProperty(PropertyName = "Question9")]
        public string Question9 { get; set; }

        [JsonProperty(PropertyName = "Question10")]
        public string Question10 { get; set; }

        public CsvRecord(int csvID, string csvName, string question1, string question2, string question3,
            string question4, string question5, string question6, string question7, string question8,
            string question9, string question10)
        {
            CsvID = csvID;
            CsvName = csvName;
            Question1 = question1;
            Question2 = question2;
            Question3 = question3;
            Question4 = question4;
            Question5 = question5;
            Question6 = question6;
            Question7 = question7;
            Question8 = question8;
            Question9 = question9;
            Question10 = question10;
        }
    }
}
