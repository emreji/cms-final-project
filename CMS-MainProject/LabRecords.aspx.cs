using CMS_MainProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;

namespace CMS_MainProject
{
    public partial class LabRecords : System.Web.UI.Page
    {
        CalvinDb db = new CalvinDb();
        List<Patient> availablePatients = new List<Patient>();
        List<Staff> availableDoctors = new List<Staff>();

        protected void Page_Load(object sender, EventArgs e)
        {
            availablePatients = db.GetAllPatients();
            availableDoctors = db.Get_Doctors();

            foreach(Patient patient in availablePatients)
            {
                patient_dropdown.Items.Add(new ListItem(patient.First_name + " " + patient.Last_name, patient.Id.ToString()));
            }

            foreach(Staff doctor in availableDoctors)
            {
                doctor_dropdown.Items.Add(new ListItem(doctor._Stf_First_Name + " " + doctor._Stf_Last_Name, doctor._Stf_ID.ToString()));
            }

        }

        protected void btn_add_lab_patient_Click(object sender, EventArgs e)
        {
            LabRecord record = new LabRecord();
          
            int selectedPatientId = Convert.ToInt32(patient_dropdown.SelectedValue);
            int selectedDoctorId = Convert.ToInt32(doctor_dropdown.SelectedValue);
            Patient selectedPatient = findPatientFromList(availablePatients, selectedPatientId);
            Staff referredDoctor = findDoctorFromList(availableDoctors, selectedDoctorId);

            if (selectedPatient != null && referredDoctor != null)
            {
                record.Id = 9;
                record.Patient = selectedPatient;
                record.Test_date = DateTime.Now;
                record.Test_name = txt_test_name.Text;
                record.Doctor = referredDoctor;
                db.AddPatientLabRecord(record);
            }
        }

        private Patient findPatientFromList(List<Patient> patients, int id)
        {
            Patient patient = null;

            foreach(Patient p in patients)
            {
                if (p.Id == id)
                {
                    patient = p;
                }
            }

            return patient;
        }

        private Staff findDoctorFromList(List<Staff> doctors, int id)
        {
            Staff doctor = null;

            foreach (Staff d in doctors)
            {
                if (d._Stf_ID == id)
                {
                    doctor = d;
                }
            }

            return doctor;
        }

        protected void btn_update_patient_Click(object sender, EventArgs e)
        {

        }

        protected void btn_delete_patient_Click(object sender, EventArgs e)
        {

        }

        protected void btn_display_patients_Click(object sender, EventArgs e)
        {
            try
            {
                List<LabRecord> labRecordList =  db.DisplayLabRecordList();
                foreach(LabRecord record in labRecordList)
                {
                    patients_lab_list.InnerHtml += "<li>" + record.Patient.First_name + " " + record.Patient.Last_name + "</li><li>" +
                                                    record.Test_name + "</li><li>" + record.Test_date.ToString() + "</li>";
                }

            } catch(OracleException ex)
            {
                lbl_err_message.Text = ex.Message;
            }
        }
    }
}