using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_MainProject.Models
{
    public class LabRecord
    {
        private int id;
        private Patient patient;
        private string test_name;
        private DateTime test_date;
        private Staff doctor;

        public int Id { get => id; set => id = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public string Test_name { get => test_name; set => test_name = value; }
        public DateTime Test_date { get => test_date; set => test_date = value; }
        public Staff Doctor { get => doctor; set => doctor = value; }
        
    }
}