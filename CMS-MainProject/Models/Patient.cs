using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_MainProject.Models
{
    public class Patient
    {
        private int id;
        private string first_name;
        private string last_name;
        private string email;
        private string phone_number;
        private string address;
        private string city;
        private string state;
        private string zipcode;
        private long healthcard;
        private Staff doctor;

        public int Id { get => id; set => id = value; }
        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public string Email { get => email; set => email = value; }
        public string Phone_number { get => phone_number; set => phone_number = value; }
        public string Address { get => address; set => address = value; }
        public string City { get => city; set => city = value; }
        public string State { get => state; set => state = value; }
        public string Zipcode { get => zipcode; set => zipcode = value; }
        public long Healthcard { get => healthcard; set => healthcard = value; }
        public Staff Doctor { get => doctor; set => doctor = value; }
    }
}