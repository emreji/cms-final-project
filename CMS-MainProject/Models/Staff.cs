using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_MainProject.Models
{
    public class Staff
    {
        private int Stf_ID;
        private string Stf_Number;
        private string Stf_First_Name;
        private string Stf_Last_Name;
        private string Stf_Phone_Number;
        private string Stf_Address;
        private string Stf_City;
        private string Stf_State;
        private string Stf_Email;
        private string Stf_Designation;


        //PROPERTIES
        public int _Stf_ID
        {
            get { return this.Stf_ID; }
            set { this.Stf_ID = value; }
        }


        public string _Stf_Number
        {
            get { return this.Stf_Number; }
            set { this.Stf_Number = value; }
        }


        public string _Stf_First_Name
        {
            get { return this.Stf_First_Name; }
            set { this.Stf_First_Name = value; }
        }


        public string _Stf_Last_Name
        {
            get { return this.Stf_Last_Name; }
            set { this.Stf_Last_Name = value; }
        }


        public string _Stf_Phone_Number
        {
            get { return this.Stf_Phone_Number; }
            set { this.Stf_Phone_Number = value; }
        }


        public string _Stf_Address
        {
            get { return this.Stf_Address; }
            set { this.Stf_Address = value; }
        }


        public string _Stf_Email
        {
            get { return this.Stf_Email; }
            set { this.Stf_Email = value; }
        }


        public string _Stf_Designation
        {
            get { return this.Stf_Designation; }
            set { this.Stf_Designation = value; }
        }

        public string Stf_City1 { get => Stf_City; set => Stf_City = value; }
        public string Stf_State1 { get => Stf_State; set => Stf_State = value; }
    }
}