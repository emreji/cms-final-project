using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using CMS_MainProject.Utils;

namespace CMS_MainProject.Models
{
    public class CalvinDb
    {
        private static string host = "calvin.humber.ca";
        private static string username = OracleLogin.username;
        private static string password = OracleLogin.password;
        private static string port = "1521";
        private static string sid = "grok";
        private static string connectionString = OracleConnString(host, port, sid, username, password);
        private OracleConnection conn = new OracleConnection(connectionString);
        private OracleCommand cmd;
        private string _message;
        private OracleDataReader reader;

        public string Message
        {
            set { _message = value; }
            get { return _message; }
        }
        
        
        public static string OracleConnString(string host, string port, string servicename, string user, string pass)
        {
            return String.Format(
              "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})" +
              "(PORT={1}))(CONNECT_DATA=(SERVICE_NAME={2})));User Id={3};Password={4};",
              host,
              port,
              servicename,
              user,
              pass);
        }
        
        public Staff GetStaffById(int staff_id)
        {
            Staff staff = new Staff();

            OracleConnection connection = new OracleConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM STAFFS WHERE STAFF_ID = :staff_id";

            OracleCommand command = new OracleCommand(query, connection);
            command.Parameters.Add(new OracleParameter("staff_id", staff_id));
            OracleDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                staff._Stf_ID = Convert.ToInt32(reader[TableColumnMappings.Staff_id_index]);
                staff._Stf_Number = reader[TableColumnMappings.Staff_number_index].ToString();
                staff._Stf_First_Name = reader[TableColumnMappings.Staff_first_name_index].ToString();
                staff._Stf_Last_Name = reader[TableColumnMappings.Staff_last_name_index].ToString();
                staff._Stf_Phone_Number = reader[TableColumnMappings.Staff_phone_number_index].ToString();
                staff._Stf_Address = reader[TableColumnMappings.Staff_address_index].ToString();
                staff.Stf_City1 = reader[TableColumnMappings.Staff_city_index].ToString();
                staff.Stf_State1 = reader[TableColumnMappings.Staff_state_index].ToString();
                staff._Stf_Email = reader[TableColumnMappings.Staff_email_index].ToString();
                staff._Stf_Designation = reader[TableColumnMappings.Staff_designation_index].ToString();
            }

            connection.Close();
            return staff;
        }

        public Patient GetPatientById(int patient_id)
        {
            Patient patient = new Patient();

            OracleConnection connection = new OracleConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM patients WHERE PATIENT_ID = :patient_id";
            OracleCommand command = new OracleCommand(query, connection);
            command.Parameters.Add(new OracleParameter("patient_id", patient_id));
            OracleDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                patient.Id = Convert.ToInt32(reader[TableColumnMappings.Patient_id_index]);
                patient.First_name = reader[TableColumnMappings.Patient_first_name_index].ToString();
                patient.Last_name = reader[TableColumnMappings.Patient_last_name_index].ToString();
                patient.Email = reader[TableColumnMappings.Patient_email_index].ToString();
                patient.Phone_number = reader[TableColumnMappings.Patient_phone_index].ToString();
                //patient.Address = reader["ADDRESSLINE1"].ToString();
                //patient.City = reader[TableColumnMappings.Patient_city_index].ToString();
                //patient.State = reader[TableColumnMappings.Patient_state_index].ToString();
                //patient.Zipcode = reader[TableColumnMappings.Patient_zipcode_index].ToString();
                patient.Healthcard = Convert.ToInt64(reader[TableColumnMappings.Patient_health_card_num_index]);
                int doctor_id = Convert.ToInt32(reader[TableColumnMappings.Patient_doctor_id_index]);
                patient.Doctor = GetStaffById(doctor_id);
            }

            connection.Close();

            return patient;
        }

        public List<Staff> Get_Doctors()
        {
            List<Staff> doctorList = new List<Staff>();
            

            string query;

            query = "SELECT STAFF_ID, FIRST_NAME, LAST_NAME FROM STAFFS WHERE DESIGNATION = 'doctor'";


            OracleConnection connection = new OracleConnection(connectionString);
            connection.Open();
            OracleCommand command = new OracleCommand(query, connection);
            OracleDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Staff doctor = new Staff();
                doctor._Stf_ID = Convert.ToInt32(reader["STAFF_ID"]);
                doctor._Stf_First_Name = reader["FIRST_NAME"].ToString();
                doctor._Stf_Last_Name = reader["LAST_NAME"].ToString();

                doctorList.Add(doctor);
            }

            connection.Close();

            return doctorList;
        }

        //CREATE
        public void Add(Patient patient)
        {
            string command = "INSERT INTO       PATIENTS " +
                                                  "(PATIENT_ID      ," +
                                                  " FIRST_NAME      ," +
                                                  " LAST_NAME       ," +
                                                  " EMAIL           ," +
                                                  " PHONE_NUMBER    ," +
                                                  " ADDRESS         ," +
                                                  " CITY            ," +
                                                  " STATE           ," +
                                                  " ZIP_CODE        ," +
                                                  " HEALTH_CARD_ID  ," +
                                                  " DOCTOR'S_ID     )" +

                             "VALUES" +
                                                  "(  :P_PATIENT_ID      ," +
                                                    " :P_F_NAME          ," +
                                                    " :P_L_NAME          ," +
                                                    " :P_EMAIL           ," +
                                                    " :P_PHONE_NUMBER    ," +
                                                    " :P_ADDRESS         ," +
                                                    " :P_CITY            ," +
                                                    " :P_STATE           ," +
                                                    " :P_ZIP_CODE        ," +
                                                    " :P_HEALTH_CARD_ID  ," +
                                                    " :P_DOCTOR'S_ID     )";
            int rows = 0;

            conn.Open();

            cmd = new OracleCommand(command, conn);
            cmd.Parameters.Add(new OracleParameter("P_PATIENT_ID", patient.Id));
            cmd.Parameters.Add(new OracleParameter("P_F_NAME", patient.First_name));
            cmd.Parameters.Add(new OracleParameter("P_L_NAME", patient.Last_name));
            cmd.Parameters.Add(new OracleParameter("P_EMAIL", patient.Email));
            cmd.Parameters.Add(new OracleParameter("P_PHONE_NUMBER", patient.Phone_number));
            cmd.Parameters.Add(new OracleParameter("P_ADDRESS", patient.Address));
            cmd.Parameters.Add(new OracleParameter("P_CITY", patient.City));
            cmd.Parameters.Add(new OracleParameter("P_STATE", patient.State));
            cmd.Parameters.Add(new OracleParameter("P_ZIP_CODE", patient.Zipcode));
            cmd.Parameters.Add(new OracleParameter("P_HEALTH_CARD_ID", patient.Healthcard));
            cmd.Parameters.Add(new OracleParameter("P_DOCTOR'S_ID", patient.Doctor._Stf_ID));

            rows = cmd.ExecuteNonQuery();

            conn.Close();

            _message = rows.ToString() + " rows added.";
        }

        //UPDATE
        public void Update(Patient patient)
        {
            string command = "UPDATE          PATIENTS" +
                             "SET               FIRST_NAME      = :P_F_NAME          ," +
                                              " LAST_NAME       = :P_L_NAME          ," +
                                              " EMAIL           = :P_EMAIL           ," +
                                              " PHONE_NUMBER    = :P_PHONE_NUMBER    ," +
                                              " ADDRESS         = :P_ADDRESS         ," +
                                              " CITY            = :P_CITY            ," +
                                              " STATE           = :P_STATE           ," +
                                              " ZIP_CODE        = :P_ZIP_CODE        ," +
                                              " HEALTH_CARD_ID  = :P_HEALTH_CARD_ID  ," +
                                              " DOCTOR'S_ID     = :P_DOCTOR'S_ID     ," +

                             "WHERE           PATIENT_ID      = :P_PATIENT_ID ";
            int rows = 0;

            conn.Open();

            cmd = new OracleCommand(command, conn);
            cmd.Parameters.Add(new OracleParameter("P_F_NAME", patient.First_name));
            cmd.Parameters.Add(new OracleParameter("P_L_NAME", patient.Last_name));
            cmd.Parameters.Add(new OracleParameter("P_EMAIL", patient.Email));
            cmd.Parameters.Add(new OracleParameter("P_PHONE_NUMBER", patient.Phone_number));
            cmd.Parameters.Add(new OracleParameter("P_ADDRESS", patient.Address));
            cmd.Parameters.Add(new OracleParameter("P_CITY", patient.City));
            cmd.Parameters.Add(new OracleParameter("P_STATE", patient.State));
            cmd.Parameters.Add(new OracleParameter("P_ZIP_CODE", patient.Zipcode));
            cmd.Parameters.Add(new OracleParameter("P_HEALTH_CARD_ID", patient.Healthcard));
            cmd.Parameters.Add(new OracleParameter("P_DOCTOR'S_ID", patient.Doctor._Stf_ID));
            cmd.Parameters.Add(new OracleParameter("P_PATIENT_ID", patient.Id));

            rows = cmd.ExecuteNonQuery();

            conn.Close();

            _message = rows.ToString() + " rows updated.";
        }


        //DELETE
        public void Delete(Patient patient)
        {
            string command = "DELETE " +
                             "FROM          PATIENTS " +
                             "WHERE         PATIENT_ID = :P_PATIENT_ID";

            int rows = 0;

            conn.Open();

            OracleCommand cmd = new OracleCommand(command, conn);
            cmd.Parameters.Add(new OracleParameter("PATIENT_ID", patient.Id));

            rows = cmd.ExecuteNonQuery();

            conn.Close();

            _message = rows.ToString() + " rows deleted.";

        }

        //READ
        public List<Patient> Read()
        {
            conn.Open();

            string query = "SELECT             FIRST_NAME " +
                                "AS                 PATIENT_FIRST_NAME ," +
                                                   "LAST_NAME " +
                                "AS                 PATIENT_LAST_NAME ," +
                                                   "PHONE_NUMBER " +
                                "AS                 PATIENT_PHONE_NUM " +
                                "FROM               PATIENTS";


            List<Patient> patientslist = new List<Patient>();

            OracleCommand cmd = new OracleCommand(query, conn);

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Patient patient = new Patient();
                {
                    patient.First_name = reader["PATIENT_FIRST_NAME"].ToString();
                    patient.Last_name = reader["PATIENT_LAST_NAME"].ToString();
                    patient.Phone_number = reader["PATIENT_PHONE_NUM"].ToString();
                };

                patientslist.Add(patient);
            }

            conn.Close();

            return patientslist;

        }

        //LabRecords

        public List<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();

            string query = "SELECT * from PATIENTS";

            OracleConnection connection = new OracleConnection(connectionString);
            connection.Open();

            OracleCommand cmd = new OracleCommand(query, connection);
            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Patient patient = new Patient();
                patient.Id = Convert.ToInt32(reader[TableColumnMappings.Patient_id_index]);
                patient.First_name = reader[TableColumnMappings.Patient_first_name_index].ToString();
                patient.Last_name = reader[TableColumnMappings.Patient_last_name_index].ToString();
                patients.Add(patient);
            }

            connection.Close();

            return patients;
        }

        public List<LabRecord> DisplayLabRecordList()
        {
            List<LabRecord> labRecordList = new List<LabRecord>();

            string query = "SELECT * from LABORATORY";

            OracleConnection connection = new OracleConnection(connectionString);
            connection.Open();

            OracleCommand cmd = new OracleCommand(query, connection);
            OracleDataReader reader = cmd.ExecuteReader();

            LabRecord labRecord = new LabRecord();

            while (reader.Read())
            {
                int patient_id = Convert.ToInt32(reader[TableColumnMappings.Lab_patient_index]);
                labRecord.Patient = GetPatientById(patient_id);
                labRecord.Test_name = reader[TableColumnMappings.Lab_test_name_index].ToString();
                labRecord.Test_date = Convert.ToDateTime(reader[TableColumnMappings.Lab_test_date_index]);
                int doctor_id = Convert.ToInt32(reader[TableColumnMappings.Lab_doctor_index]);
                labRecord.Doctor = GetStaffById(doctor_id);

                labRecordList.Add(labRecord);
            }

            connection.Close();

            return labRecordList;
        }

        public void AddPatientLabRecord(LabRecord record) 
        {
            string query = "INSERT INTO laboratory VALUES(:id, :patient_id, :test_name, :test_date, :doctor_referred)";

            OracleConnection connection = new OracleConnection(connectionString);
            connection.Open();
            OracleCommand cmd = new OracleCommand(query, connection);

            cmd.Parameters.Add(new OracleParameter("id", record.Id));
            cmd.Parameters.Add(new OracleParameter("patient_id", record.Patient.Id));
            cmd.Parameters.Add(new OracleParameter("test_name", record.Test_name));
            cmd.Parameters.Add(new OracleParameter("test_date", record.Test_date));
            cmd.Parameters.Add(new OracleParameter("doctor_referred", record.Doctor._Stf_ID));

            int rows = cmd.ExecuteNonQuery();
            Message = Convert.ToString(rows) + " rows added to database";

            cmd.Dispose();
            conn.Close();
        }
    }
}