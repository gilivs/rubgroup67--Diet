using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using FinalProject.Models;

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;
    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        string pStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(pStr);
        con.Open();
        return con;
    }


    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

        return cmd;
    }
    public int insert(Patient p)
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlCommand cmd1;
        SqlCommand cmd2;

        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String pStr = BuildInsertPatientCommand(p);      // helper method to build the insert string

        cmd = CreateCommand(pStr, con);             // create the command
        int numAffected = 0;

        try
        {
            int numPatient = Convert.ToInt16(cmd.ExecuteScalar());// execute the command

            int numAffected1 = 0;
            DateTime time = DateTime.Now;             // Use current time.
            string format = "dd/MM/yyyy";   // Use this format.
            string dateFormat = time.ToString(format);
            double bmi = p.Weight / (p.Height * p.Height);
            bmi = Math.Round(bmi, 4);
            String cStr2 = BuildInsertBMI(numPatient, dateFormat, p, bmi);
            cmd2 = CreateCommand(cStr2, con);
            numAffected1 = cmd2.ExecuteNonQuery();// execute the command




            for (int i = 0; i < p.Sensitivities.Count; i++)
            {
                String cStr1 = BuildInsertSensitivities(p.Sensitivities[i], numPatient);
                cmd1 = CreateCommand(cStr1, con);
                numAffected = cmd1.ExecuteNonQuery();
            }

            return numAffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }


        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }




    private String BuildInsertPatientCommand(Patient p)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}' ,'{2}', '{3}', {4}, '{5}', '{6}','{7}',{8}, {9}, '{10}', {11},'{12}','{13}','{14}',{15} )", p.Id, p.FirstName, p.LastName, p.DateOfBirth, p.Age, p.ContactName, p.ContactPhone, p.ContactRelation, p.Height, p.Weight, p.Diseases, p.IdTexture,/*p.NumSensitivity.ToString(),*/ p.Classification, p.Image, p.Comments, p.Active);
        String prefix = "INSERT INTO Patients (idPatient, firstNamePatient, lastNamePatient, dateOfBirthPatient, age, nameEmergencyContact, phoneEmergencyContact, relationContact, height, weight, diseases, idTexture, classificationPatient, image, additionalComments, active) OUTPUT INSERTED.numPatient ";
        //sb.AppendFormat("Values('{0}', '{1}' ,'{2}', '{3}', {4}, '{5}', '{6}','{7}',{8}, {9}, '{10}', {11},'{12}','{13}','{14}' )", p.Id, p.FirstName, p.LastName, p.DateOfBirth.ToString(), p.Age.ToString(), p.ContactName, p.ContactPhone, p.ContactRelation, p.Height.ToString(), p.Weight.ToString(), p.Diseases, p.IdTexture.ToString(),/*p.NumSensitivity.ToString(),*/ p.Classification, p.Image, p.Comments);
        //String prefix = "INSERT INTO Patients (idPatient, firstNamePatient, lastNamePatient, dateOfBirthPatient, age, nameEmergencyContact, phoneEmergencyContact, relationContact, height, weight, diseases, idTexture, classificationPatient, image, additionalComments) OUTPUT INSERTED.numPatient ";

        command = prefix + sb.ToString();

        return command;
    }

    private String BuildInsertBMI(int numPatient, string dateFormat, Patient p, double bmi)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat(" Values({0},'{1}',{2},{3},{4})", numPatient, dateFormat, p.Weight, p.Height, bmi.ToString());
        String prefix = "INSERT INTO Bmi (numPatient, date, weight, height, bmi) OUTPUT INSERTED.idBmi ";
        //String prefix = "INSERT INTO Bmi (numPatient, date, weight, height, bmi) ";


        command = prefix + sb.ToString();

        return command;
    }



    private String BuildInsertSensitivities(int NumSensitivity, int numpatient)
    {

        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values({0},{1} )", NumSensitivity.ToString(), numpatient.ToString());
        String prefix = "INSERT INTO Sensitivity_Patient (numSensitivity, numPatient) ";
        command = prefix + sb.ToString();

        return command;
    }

    private String BuildInsertProductCommand(Product p)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', {1} ,'{2}', {3}, {4})", p.NameProduct, p.IdCategory, p.MainGroup, p.Calories, p.Weight);
        String prefix = "INSERT INTO Products ( nameProduct, idCategory, MainFoodGroups, calories, weight) ";

        command = prefix + sb.ToString();

        return command;
    }
    public List<Sensitivities> ReadSen(string conString, string tableName)
    {

        SqlConnection con = null;
        List<Sensitivities> ls = new List<Sensitivities>();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName;
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Sensitivities s = new Sensitivities();
                s.NumSensitivity = Convert.ToInt32(dr["numSensitivity"]);
                s.NameSensitivity = (string)dr["nameSensitivity"];
                ls.Add(s);
            }
            return ls;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public List<Patient> GetPatients()
    {
        SqlConnection con;
        List<Patient> lo = new List<Patient>();

        try
        {
            con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            String selectSTR = "SELECT numPatient, idPatient, firstNamePatient, lastNamePatient, tf.kind0fTextureFood FROM patients  pt inner join TextureFood  tf on tf.idTexture = pt.idTexture where pt.active=1 ";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ReadFromDataBase("dietDBConnectionString", "Sensitivity_Patient");

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
                List<int> sen = new List<int>();
                Patient patient = new Patient();
                //pizza.Id = Convert.ToInt16(dr["id"]);
                //pizza.Name = Convert.ToString(dr["name"]);
                patient.NumPatient = Convert.ToInt16(dr["numPatient"]);
                patient.Id = Convert.ToString(dr["idPatient"]);
                patient.FirstName = Convert.ToString(dr["firstNamePatient"]);
                patient.LastName = Convert.ToString(dr["lastNamePatient"]);
                //patient.DateOfBirth = Convert.ToString(dr["dateOfBirthPatient"]);
                //patient.Age = Convert.ToDouble(dr["Age"]);
                //patient.ContactName = Convert.ToString(dr["nameEmergencyContact"]);
                //patient.ContactPhone = Convert.ToString(dr["phoneEmergencyContact"]);
                //patient.ContactRelation = Convert.ToString(dr["phoneEmergencyContact"]);
                //patient.Height= Convert.ToDouble(dr["height"]);
                //patient.Weight = Convert.ToDouble(dr["weight"]);
                //patient.Diseases = Convert.ToString(dr["diseases"]);
                patient.Kind0fTextureFood = Convert.ToString(dr["kind0fTextureFood"]);
                //patient.NumSensitivity = Convert.ToInt32(dr["numSensitivity"]);
                //patient.Classification = Convert.ToString(dr["classificationPatient"]);
                //patient.Image = Convert.ToString(dr["image"]);
                //patient.Comments = Convert.ToString(dr["additionalComments"]);
                foreach (DataRow drow in dt.Rows)
                {
                    if (Convert.ToInt16(drow["numPatient"]) == patient.NumPatient)
                    {
                        sen.Add(Convert.ToInt16(drow["numSensitivity"]));
                    }

                }
                patient.Sensitivities = sen;
                lo.Add(patient);
            }
            return lo;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }


        }

    }

    public List<Patient> GetPatientsforSmart()
    {
        SqlConnection con;
        List<Patient> lo = new List<Patient>();

        try
        {
            con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            String selectSTR = "SELECT numPatient, idPatient, firstNamePatient, lastNamePatient, tf.kind0fTextureFood, height, weight FROM patients  pt inner join TextureFood  tf on tf.idTexture = pt.idTexture where pt.active=1 ";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ReadFromDataBase("dietDBConnectionString", "Sensitivity_Patient");

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
                List<int> sen = new List<int>();
                Patient patient = new Patient();
                //pizza.Id = Convert.ToInt16(dr["id"]);
                //pizza.Name = Convert.ToString(dr["name"]);
                patient.NumPatient = Convert.ToInt16(dr["numPatient"]);
                patient.Id = Convert.ToString(dr["idPatient"]);
                patient.FirstName = Convert.ToString(dr["firstNamePatient"]);
                patient.LastName = Convert.ToString(dr["lastNamePatient"]);
                //patient.DateOfBirth = Convert.ToString(dr["dateOfBirthPatient"]);
                //patient.Age = Convert.ToDouble(dr["Age"]);
                //patient.ContactName = Convert.ToString(dr["nameEmergencyContact"]);
                //patient.ContactPhone = Convert.ToString(dr["phoneEmergencyContact"]);
                //patient.ContactRelation = Convert.ToString(dr["phoneEmergencyContact"]);
                patient.Height = (float)Convert.ToDouble(dr["height"]);
                patient.Weight = (float)Convert.ToDouble(dr["weight"]);
                //patient.Diseases = Convert.ToString(dr["diseases"]);
                patient.Kind0fTextureFood = Convert.ToString(dr["kind0fTextureFood"]);
                //patient.NumSensitivity = Convert.ToInt32(dr["numSensitivity"]);
                //patient.Classification = Convert.ToString(dr["classificationPatient"]);
                //patient.Image = Convert.ToString(dr["image"]);
                //patient.Comments = Convert.ToString(dr["additionalComments"]);
                foreach (DataRow drow in dt.Rows)
                {
                    if (Convert.ToInt16(drow["numPatient"]) == patient.NumPatient)
                    {
                        sen.Add(Convert.ToInt16(drow["numSensitivity"]));
                    }

                }
                patient.Sensitivities = sen;
                lo.Add(patient);
            }
            return lo;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }


        }

    }




    public List<Patient> GetPatients(string searchTXT)
    {
        SqlConnection con;
        List<Patient> lo = new List<Patient>();

        try
        {
            con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            String selectSTR = "SELECT numPatient, idPatient, firstNamePatient, lastNamePatient, tf.kind0fTextureFood FROM patients  pt inner join TextureFood  tf on tf.idTexture = pt.idTexture where pt.active=1 and " +
                "(idPatient like '%" + searchTXT + "%' or firstNamePatient like '%" + searchTXT + "%' or lastNamePatient like '%" + searchTXT + "%' or tf.kind0fTextureFood like '%" + searchTXT + "%' )";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            ReadFromDataBase("dietDBConnectionString", "Sensitivity_Patient");

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
                List<int> sen = new List<int>();
                Patient patient = new Patient();
                patient.NumPatient = Convert.ToInt16(dr["numPatient"]);
                patient.Id = Convert.ToString(dr["idPatient"]);
                patient.FirstName = Convert.ToString(dr["firstNamePatient"]);
                patient.LastName = Convert.ToString(dr["lastNamePatient"]);
                patient.Kind0fTextureFood = Convert.ToString(dr["kind0fTextureFood"]);
                foreach (DataRow drow in dt.Rows)
                {
                    if (Convert.ToInt16(drow["numPatient"]) == patient.NumPatient)
                    {
                        sen.Add(Convert.ToInt16(drow["numSensitivity"]));
                    }

                }
                patient.Sensitivities = sen;
                lo.Add(patient);
            }
            return lo;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }


        }

    }

    public DBservices ReadFromDataBase(string conString, string tableName)
    {

        SqlConnection con = null;

        try
        {
            con = connect(conString); // open the connection to the database/

            String selectStr = "SELECT * FROM " + tableName; // create the select that will be used by the adapter to select data from the DB

            SqlDataAdapter da = new SqlDataAdapter(selectStr, con); // create the data adapter

            DataSet ds = new DataSet(); // create a DataSet and give it a name (not mandatory) as defualt it will be the same name as the DB
            da.Fill(ds);                        // Fill the datatable (in the dataset), using the Select command

            DataTable dt = ds.Tables[0];

            // add the datatable and the dataa adapter to the dbS helper class in order to be able to save it to a Session Object
            this.dt = dt;
            this.da = da;

            return this;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }
    public Patient GetPatient(int NumPatient)
    {

        SqlConnection con = null;
        Patient patient = new Patient();

        try
        {
            con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM patients WHERE numPatient='" + NumPatient + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            ReadFromDataBase("dietDBConnectionString", "Sensitivity_Patient");

            while (dr.Read())
            {   // Read till the end of the data into a row
                if (NumPatient == Convert.ToInt32(dr["numPatient"]))
                {
                    List<int> list = new List<int>();

                    patient.NumPatient = Convert.ToInt32(dr["numPatient"]);
                    patient.Id = Convert.ToString(dr["idPatient"]);
                    patient.FirstName = Convert.ToString(dr["firstNamePatient"]);
                    patient.LastName = Convert.ToString(dr["lastNamePatient"]);
                    patient.DateOfBirth = Convert.ToString(dr["dateOfBirthPatient"]);
                    patient.Age = (float)Convert.ToDouble(dr["Age"]);
                    patient.ContactName = Convert.ToString(dr["nameEmergencyContact"]);
                    patient.ContactPhone = Convert.ToString(dr["phoneEmergencyContact"]);
                    patient.ContactRelation = Convert.ToString(dr["relationContact"]);
                    patient.Height = (float)Convert.ToDouble(dr["height"]);
                    patient.Weight = (float)Convert.ToDouble(dr["weight"]);
                    patient.Diseases = Convert.ToString(dr["diseases"]);
                    patient.IdTexture = Convert.ToInt32(dr["idTexture"]);
                    

                    //patient.NumSensitivity = Convert.ToInt32(dr["numSensitivity"]);

                    patient.Classification = Convert.ToString(dr["classificationPatient"]);

                    patient.Image = Convert.ToString(dr["image"]);
                    patient.Comments = Convert.ToString(dr["additionalComments"]);


                    //person.Hobiesarr = GetHobbToPerson(person.Id);
                    //person.HobbiesName = GetHobbToPerson(p.PersonId, "ConnectionStringPersonTbl", "PersonToHobbies");
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (Convert.ToInt16(drow["numPatient"]) == patient.NumPatient)
                        {
                            list.Add(Convert.ToInt16(drow["numSensitivity"]));
                        }

                    }
                    patient.Sensitivities = list;

                    break;

                }
            }
            if (patient == null)
            {
                return null;
            }
            else
            {
                return patient;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }

    }


    public int Update(Patient p)
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlCommand cmd1;
        SqlCommand cmd2;


        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdatePatient(p);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command
        DeleteSensetivityToPatient(p);

        int numAffected1 = 0;
        DateTime time = DateTime.Now;             // Use current time.
        string format = "dd/MM/yyyy";   // Use this format.
        string dateFormat = time.ToString(format);
        double bmi = p.Weight / (p.Height * p.Height);
        bmi = Math.Round(bmi, 4);
        String cStr2 = BuildInsertBMI(p.NumPatient, dateFormat, p, bmi);
        cmd2 = CreateCommand(cStr2, con);
        numAffected1 = cmd2.ExecuteNonQuery();// execute the command

        for (int i = 0; i < p.Sensitivities.Count; i++)
        {
            String cStr1 = BuildInsertSensitivities(p.Sensitivities[i], p.NumPatient);
            cmd1 = CreateCommand(cStr1, con);
            int numAffected = cmd1.ExecuteNonQuery();
        }
        try
        {
            int numAffected = cmd.ExecuteNonQuery(); // execute the comm

            return numAffected;


        }
        catch (Exception ex)
        {

            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }



    private string BuildUpdatePatient(Patient p)
    {
        string cmdStr = "UPDATE Patients SET firstNamePatient ='" + p.FirstName + "',lastNamePatient ='" + p.LastName + "' ,nameEmergencyContact ='" + p.ContactName + "' , phoneEmergencyContact='" + p.ContactPhone + "', relationContact='" + p.ContactRelation + "',height='" + p.Height + "', weight='" + p.Weight + "',diseases='" + p.Diseases + "',idTexture='" + p.IdTexture + "', classificationPatient='" + p.Classification + "', image='" + p.Image + "', additionalComments='" + p.Comments + "' WHERE numPatient ='" + p.NumPatient + "'";
        // helper method to build the insert string

        return cmdStr;

    }


    public int DeleteSensetivityToPatient(Patient p)
    {

        SqlConnection con;
        SqlCommand cmd;


        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildDeleteSensetivityPatient(p);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command


        try
        {
            int numAffected = cmd.ExecuteNonQuery(); // execute the comm

            return numAffected;


        }
        catch (Exception ex)
        {

            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }


    private string BuildDeleteSensetivityPatient(Patient p)
    {
        string cmdStr = "DELETE  FROM [bgroup67_test2].[dbo].[Sensitivity_Patient] WHERE numPatient =" + p.NumPatient;
        return cmdStr;
    }


    public int insertLabT(LaboratoryTests LabT)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String pStr = BuildInsertLabTCommand(LabT);      // helper method to build the insert string

        cmd = CreateCommand(pStr, con);             // create the command

        int numAffected = 0;

        try
        {
            //int idTest = Convert.ToInt16(cmd.ExecuteScalar());

            numAffected = cmd.ExecuteNonQuery(); // execute the command    

            return numAffected;

        }

        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------
    // Build the Insert command String
    //--------------------------------------------------------------------
    private String BuildInsertLabTCommand(LaboratoryTests lt)
    {

        String command;

        StringBuilder sb = new StringBuilder();

        sb.AppendFormat("Values( {0}, '{1}' , {2}, {3}, {4}, {5}, {6}, {7} )", lt.NumPatient, lt.DateLab, lt.Albumin, lt.Lymphocytes, lt.Cholesterol, lt.Crp, lt.ProteinTotal, lt.ActiveLab);
        String prefix = "INSERT INTO LaboratoryTests (numPatient, dateLab, albumin, lymphocytes, cholesterol, crp, proteinTotal, activeLab) OUTPUT INSERTED.idTest ";
        command = prefix + sb.ToString();

        return command;
    }

    public int insertProduct(Product p)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String pStr = BuildInsertProductCommand(p);      // helper method to build the insert string

        cmd = CreateCommand(pStr, con);             // create the command


        int numAffected = 0;

        try
        {
            //int idTest = Convert.ToInt16(cmd.ExecuteScalar());

            numAffected = cmd.ExecuteNonQuery(); // execute the command    

            return numAffected;

        }

        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    public int PutLt(LaboratoryTests lt)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildUpdateLt(lt);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            //int idTest = Convert.ToInt16(cmd.ExecuteScalar());

            int numAffected = cmd.ExecuteNonQuery(); // execute the comm

            return numAffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private string BuildUpdateLt(LaboratoryTests lt)
    {
        string cmdStr = "UPDATE LaboratoryTests SET NumPatient='" + lt.NumPatient + "' ,Albumin='" + lt.Albumin + "' ,Lymphocytes='" + lt.Lymphocytes + "' ,Cholesterol='" + lt.Cholesterol + "' ,Crp='" + lt.Crp + "' ,ProteinTotal='" + lt.ProteinTotal + "' WHERE IdTest=" + lt.IdTest;
        return cmdStr;
    }


    public List<LaboratoryTests> GetLaboratoryTests()
    {
        SqlConnection con;
        List<LaboratoryTests> laboratoryTestList = new List<LaboratoryTests>();

        try
        {
            con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            String selectSTR = "SELECT idTest, numPatient, dateLab FROM LaboratoryTests where activeLab=1 ";

            //String selectSTR = "SELECT * FROM LaboratoryTests ";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
                LaboratoryTests laboratoryTest = new LaboratoryTests();

                laboratoryTest.IdTest = Convert.ToInt16(dr["idTest"]);
                laboratoryTest.NumPatient = Convert.ToInt16(dr["numPatient"]);
                laboratoryTest.DateLab = Convert.ToString(dr["dateLab"]);
                //laboratoryTest.Albumin = (float)Convert.ToDouble(dr["albumin"]);
                //laboratoryTest.Lymphocytes = (float)Convert.ToDouble(dr["lymphocytes"]);
                //laboratoryTest.Cholesterol = (float)Convert.ToDouble(dr["cholesterol"]);
                //laboratoryTest.Crp = (float)Convert.ToDouble(dr["crp"]);
                //laboratoryTest.ProteinTotal = (float)Convert.ToDouble(dr["proteinTotal"]);

                laboratoryTestList.Add(laboratoryTest);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        return laboratoryTestList;
    }

    public LaboratoryTests GetlabTest(int IdTest)
    {

        SqlConnection con = null;
        LaboratoryTests labTest = new LaboratoryTests();

        try
        {
            con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM LaboratoryTests WHERE idTest='" + IdTest + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                if (IdTest == Convert.ToInt16(dr["idTest"]))
                {
                    List<LaboratoryTests> list = new List<LaboratoryTests>();

                    labTest.IdTest = Convert.ToInt16(dr["idTest"]);
                    labTest.NumPatient = Convert.ToInt32(dr["numPatient"]);
                    labTest.DateLab = Convert.ToString(dr["dateLab"]);
                    labTest.Albumin = (float)Convert.ToDouble(dr["albumin"]);
                    labTest.Lymphocytes = (float)Convert.ToDouble(dr["lymphocytes"]);
                    labTest.Cholesterol = (float)Convert.ToDouble(dr["cholesterol"]);
                    labTest.Crp = (float)Convert.ToDouble(dr["crp"]);
                    labTest.ProteinTotal = (float)Convert.ToDouble(dr["proteinTotal"]);

                    list.Add(labTest);

                }
            }
            if (labTest == null)
            {
                return null;
            }
            else
            {
                return labTest;
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }




    public List<Category> ReadCategory(string conString, string tableName)
    {

        SqlConnection con = null;
        List<Category> ls = new List<Category>();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file



            String selectSTR = "SELECT * FROM " + tableName;


            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end


            while (dr.Read())
            {   // Read till the end of the data into a row
                Category c = new Category();
                c.IdCategory = Convert.ToInt32(dr["idCategory"]);
                c.CategoryName = (string)dr["categoryName"];
                ls.Add(c);
            }
            return ls;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public List<BMI> GetBMI(string conString, string tableName, int NumPatient)
    {

        SqlConnection con = null;
        List<BMI> lb = new List<BMI>();

        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName + " WHERE numPatient='" + NumPatient + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            //ReadFromDataBase(conString, "PersonHobbies_new");
            while (dr.Read())
            {   // Read till the end of the data into a row
                if (NumPatient == Convert.ToInt16(dr["numPatient"]))
                {
                    BMI b = new BMI();


                    b.IdBMI = Convert.ToInt16(dr["idBmi"]);
                    b.NumPatient = Convert.ToInt16(dr["numPatient"]);
                    b.Date = (string)dr["date"];
                    b.Height = (float)Convert.ToDouble(dr["height"]);
                    b.Weight = (float)Convert.ToDouble(dr["weight"]);
                    b.Bmi = (float)Convert.ToDouble(dr["bmi"]);

                    lb.Add(b);
                }

            }

            return lb;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }

    }

    public List<MealType> GetMT(string conString, string tableName)
    {

        SqlConnection con = null;
        List<MealType> lMT = new List<MealType>();

        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName;
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            //ReadFromDataBase(conString, "PersonHobbies_new");
            while (dr.Read())
            {   // Read till the end of the data into a row
                MealType mt = new MealType();


                mt.IdMealType = Convert.ToInt16(dr["idMealType"]);
                mt.NameMealType = (string)dr["nameMealType"];

                lMT.Add(mt);
            }



            return lMT;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }

    }

    public List<LaboratoryTests> GetLab(string conString, string tableName, int NumPatient)
    {

        SqlConnection con = null;
        List<LaboratoryTests> labTestList = new List<LaboratoryTests>();

        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName + " WHERE numPatient='" + NumPatient + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            //ReadFromDataBase(conString, "PersonHobbies_new");
            while (dr.Read())
            {   // Read till the end of the data into a row
                LaboratoryTests labTest = new LaboratoryTests();


                labTest.IdTest = Convert.ToInt16(dr["idTest"]);
                labTest.NumPatient = Convert.ToInt32(dr["numPatient"]);
                labTest.DateLab = Convert.ToString(dr["dateLab"]);
                labTest.Albumin = (float)Convert.ToDouble(dr["albumin"]);
                labTest.Lymphocytes = (float)Convert.ToDouble(dr["lymphocytes"]);
                labTest.Cholesterol = (float)Convert.ToDouble(dr["cholesterol"]);
                labTest.Crp = (float)Convert.ToDouble(dr["crp"]);
                labTest.ProteinTotal = (float)Convert.ToDouble(dr["proteinTotal"]);

                labTestList.Add(labTest);

            }

            return labTestList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }

    }


    public List<LabRange> GetLabRange(string conString, string tableName)
    {

        SqlConnection con = null;
        List<LabRange> labR = new List<LabRange>();

        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName;
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                LabRange lab = new LabRange();


                lab.NumParameter = Convert.ToInt16(dr["numParameter"]);
                lab.Name = Convert.ToString(dr["name"]);
                lab.MaxP = (float)Convert.ToDouble(dr["maxP"]);
                lab.MinP = (float)Convert.ToDouble(dr["minP"]);
                lab.Unit = Convert.ToString(dr["unit"]);
                lab.GoodMax = (float)Convert.ToDouble(dr["good_max"]);
                lab.GoodMin = (float)Convert.ToDouble(dr["good_min"]);


                labR.Add(lab);

            }

            return labR;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }

    }

    public int ChangePat(string numPatient)
    {

        SqlConnection con;
        SqlCommand cmd;
        int numAffected = 0;

        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String pStr = BuildUpdatePatientActive(numPatient);      // helper method to build the insert string

        cmd = CreateCommand(pStr, con);             // create the command




        try
        {
            int idTest = Convert.ToInt16(cmd.ExecuteScalar());

            numAffected = cmd.ExecuteNonQuery(); // execute the command    

            return numAffected;

        }

        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }



    }
    private String BuildUpdatePatientActive(string numPatient)
    {
        string cmdStr = "UPDATE Patients SET active =0 WHERE numPatient ='" + numPatient + "'";
        // helper method to build the insert string

        return cmdStr;
    }

    public AdminUsers Exist(string conString, string tableName, string EmailUser, string PasswordUser)
    {
        SqlConnection con = null;
        AdminUsers au = new AdminUsers();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName + " WHERE emailUser='" + EmailUser + "' AND " + "passwordUser='" + PasswordUser + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                au.NumUser = Convert.ToInt16(dr["numUser"]);
                au.IdUser = (string)dr["idUser"];
                au.UserName = (string)dr["userName"];
                au.EmailUser = (string)dr["emailUser"];
                au.PasswordUser = (string)dr["passwordUser"];
                au.Role = (string)dr["role"];
                au.roleId = (int)dr["roleId"];

            }

            return au;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }
    public AdminUsers ExistUserApp(string conString, string tableName, string userName, string passwordUser)
    {
        SqlConnection con = null;
        AdminUsers au = new AdminUsers();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName + " WHERE userName='" + userName + "' AND " + "passwordUser='" + passwordUser + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                au.NumUser = Convert.ToInt16(dr["numUser"]);
                au.IdUser = (string)dr["idUser"];
                au.UserName = (string)dr["userName"];
                au.EmailUser = (string)dr["emailUser"];
                au.PasswordUser = (string)dr["passwordUser"];
                au.Role = (string)dr["role"];
            }

            return au;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }


    public List<Patient> GetPatientsForAddLab()
    {
        SqlConnection con;
        List<Patient> lo = new List<Patient>();

        try
        {
            con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            String selectSTR = "select convert(nvarchar,numPatient)+' '+firstNamePatient+' '+lastNamePatient as name, numPatient from Patients ";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
                List<int> sen = new List<int>();
                Patient patient = new Patient();
                patient.NumPatient = Convert.ToInt16(dr["numPatient"]);
                //patient.Id = Convert.ToString(dr["idPatient"]);
                patient.FirstName = Convert.ToString(dr["firstNamePatient"]);
                patient.LastName = Convert.ToString(dr["lastNamePatient"]);

                lo.Add(patient);
            }
            return lo;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public int ChangeActiveLabTest(string idTest)
    {
        SqlConnection con;
        SqlCommand cmd;
        int numAffected = 0;

        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String pStr = BuildUpdateLabTestActive(idTest);      // helper method to build the insert string

        cmd = CreateCommand(pStr, con);             // create the command

        try
        {
            //int idTest = Convert.ToInt16(cmd.ExecuteScalar());

            numAffected = cmd.ExecuteNonQuery(); // execute the command    

            return numAffected;
        }

        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }
    }

    private String BuildUpdateLabTestActive(string idTest)
    {
        string cmdStr = "UPDATE LaboratoryTests SET activeLab =0 WHERE idTest ='" + idTest + "'";
        // helper method to build the insert string

        return cmdStr;
    }

    public int insertProductForWeekMenu(ProductForWeeklyMenu prodWeek)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String pStr = BuildInsertProductForWeekMenuCommand(prodWeek);      // helper method to build the insert string

        cmd = CreateCommand(pStr, con);             // create the command

        int numAffected = 0;

        try
        {
            //int idTest = Convert.ToInt16(cmd.ExecuteScalar());

            numAffected = cmd.ExecuteNonQuery(); // execute the command    

            return numAffected;

        }

        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private String BuildInsertProductForWeekMenuCommand(ProductForWeeklyMenu prodWeek)
    {

        String command;

        StringBuilder sb = new StringBuilder();

        sb.AppendFormat("Values( {0}, {1}, {2}, {3}, {4}, {5}, {6} )", prodWeek.IdProduct, prodWeek.IdDay, prodWeek.WeekNumber, prodWeek.IdMealType, prodWeek.IsBlender, prodWeek.MenuNumber, prodWeek.IdCategory);
        String prefix = "INSERT INTO ProductForWeeklyMenu (idProduct, idDay, weekNumber, idMealType, isBlender, menuNumber, idCategory) OUTPUT INSERTED.numProduct_menu ";
        command = prefix + sb.ToString();

        return command;
    }

    //public List<ProductForWeeklyMenu> GetProductForWeekMenu()
    //{
    //    SqlConnection con;
    //    List<ProductForWeeklyMenu> lProdForWeek = new List<ProductForWeeklyMenu>();

    //    try
    //    {
    //        con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file
    //    }

    //    catch (Exception ex)
    //    {
    //        // write to log
    //        throw (ex);
    //    }

    //    try
    //    {
    //        String selectSTR = "SELECT numProduct_menu, pfwm.idProduct, day, mealType, isBlender, menuNumber,idCategory FROM ProductForWeeklyMenu  pfwm inner join Products  pr on pfwm.idProduct = pr.idProduct ";

    //        SqlCommand cmd = new SqlCommand(selectSTR, con);

    //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

    //        while (dr.Read())
    //        {// Read till the end of the data into a row
    //         // read first field from the row into the list collection
    //            ProductForWeeklyMenu productForWeeklyMenu = new ProductForWeeklyMenu();

    //            productForWeeklyMenu.NumProduct_menu = Convert.ToInt16(dr["numProduct_menu"]);
    //            productForWeeklyMenu.IdProduct = Convert.ToInt16(dr["idProduct"]);
    //            productForWeeklyMenu.Day = Convert.ToString(dr["day"]);
    //            productForWeeklyMenu.WeekNumber = Convert.ToInt16(dr["weekNumber"]);
    //            productForWeeklyMenu.MealType = Convert.ToString(dr["mealType"]);
    //            productForWeeklyMenu.IsBlender = Convert.ToInt16(dr["isBlender"]);
    //            productForWeeklyMenu.MenuNumber=Convert.ToInt16(dr["menuNumber"]);

    //            lProdForWeek.Add(productForWeeklyMenu);
    //        }
    //        return lProdForWeek;
    //    }
    //    catch (Exception ex)
    //    {
    //        // write to log
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        if (con != null)
    //        {
    //            con.Close();
    //        }
    //    }
    //}

    public List<Product> GetProducts(string conString)
    {

        SqlConnection con = null;
        List<Product> ls = new List<Product>();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file



            String selectSTR = "SELECT * FROM Products INNER JOIN CategoryMeal ON Products.idCategory = CategoryMeal.idCategory;";


            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end


            while (dr.Read())
            {   // Read till the end of the data into a row
                Product product = new Product();
                product.IdProduct = Convert.ToInt16(dr["idProduct"]);
                product.NameProduct = Convert.ToString(dr["nameProduct"]);
                product.Calories = (float)Convert.ToDouble(dr["calories"]);
                product.IdCategory = Convert.ToInt16(dr["idCategory"]);
                product.CategoryName = Convert.ToString(dr["categoryName"]);
                product.MainGroup = Convert.ToString(dr["MainFoodGroups"]);
                //product.Weight = (float)Convert.ToDouble(dr["weight"]);
                ls.Add(product);
            }
            return ls;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }
    public List<ProductForWeeklyMenu> GetProductsByMealType(string conString, string date, string hour)
    {

        SqlConnection con = null;
        List<ProductForWeeklyMenu> ls = new List<ProductForWeeklyMenu>();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file



            String selectSTR = "SELECT * FROM Products INNER JOIN ProductForWeeklyMenu ON Products.idProduct = ProductForWeeklyMenu.idProduct INNER JOIN MealType On MealType.idMealType = ProductForWeeklyMenu.idMealType INNER JOIN menu_date On menu_date.menuNumber = ProductForWeeklyMenu.menuNumber WHERE menu_date.date ='" + date + "'";


            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end


            while (dr.Read())
            {   // Read till the end of the data into a row
                int startHour = Int32.Parse(Convert.ToString(dr["start_hour"]));
                int endHour = Int32.Parse(Convert.ToString(dr["end_hour"]));
                int hourNumber = Int32.Parse(hour);

                if (hourNumber >= startHour && hourNumber <= endHour)
                {
                    ProductForWeeklyMenu product = new ProductForWeeklyMenu();
                    product.IdProduct = Convert.ToInt16(dr["idProduct"]);
                    product.NameProduct = Convert.ToString(dr["nameProduct"]);
                    product.IdCategory = Convert.ToInt16(dr["idCategory"]);
                    product.NameProduct = Convert.ToString(dr["nameProduct"]);
                    ls.Add(product);
                }
            }
            return ls;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public List<TherapistComments> GetTherapistComments()
    {
        SqlConnection con;
        List<TherapistComments> TherapistCommentsList = new List<TherapistComments>();

        try
        {
            con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            //String selectSTR = "SELECT idComment, dateCommentTherapist, numPatient, numTherapist, foodConsumption, generalComment FROM Therapist_Comments where activeCommentTherapist=1 ";


            //String selectSTR = "SELECT idComment, dateCommentTherapist, pt.firstNamePatient+' '+pt.lastNamePatient as name, numTherapist, foodConsumption, generalComment FROM Therapist_Comments thc inner join patients pt on thc.numPatient = pt.numPatient where activeCommentTherapist = 1 ";

            String selectSTR = "SELECT idComment, dateCommentTherapist, pt.firstNamePatient, pt.lastNamePatient,pt.idPatient, th.nameTherapist, foodConsumption, generalComment FROM Therapist_Comments thc inner join  patients pt on thc.numPatient = pt.numPatient inner join Therapist th  on thc.numTherapist = th.numTherapist where activeCommentTherapist=1 ";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
                TherapistComments TherapistComment = new TherapistComments();

                TherapistComment.IdComment = Convert.ToInt16(dr["idComment"]);
                TherapistComment.DateCommentTherapist = Convert.ToString(dr["dateCommentTherapist"]);

                //TherapistComment.NumTherapist = Convert.ToInt16(dr["numTherapist"]);

                TherapistComment.FoodConsumption = Convert.ToString(dr["foodConsumption"]);

                TherapistComment.GeneralComment = Convert.ToString(dr["generalComment"]);

                TherapistComment.FirstName = Convert.ToString(dr["firstNamePatient"]);
                TherapistComment.LastName = Convert.ToString(dr["lastNamePatient"]);
                TherapistComment.NameTherapist = Convert.ToString(dr["nameTherapist"]);
                TherapistComment.Id = Convert.ToString(dr["idPatient"]);

                TherapistCommentsList.Add(TherapistComment);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        return TherapistCommentsList;
    }

    public List<FoodReport> GetTherapistCommentsForPatient(int NumPatient)
    {
        SqlConnection con;
        List<FoodReport> TherapistCommentsList = new List<FoodReport>();

        try
        {
            con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            //String selectSTR = "SELECT idComment, dateCommentTherapist, numPatient, numTherapist, foodConsumption, generalComment FROM Therapist_Comments where activeCommentTherapist=1  ";


            //String selectSTR = "SELECT idComment, dateCommentTherapist, pt.firstNamePatient+' '+pt.lastNamePatient as name, numTherapist, foodConsumption, generalComment FROM Therapist_Comments thc inner join patients pt on thc.numPatient = pt.numPatient where activeCommentTherapist = 1 ";

            String selectSTR = "select * From FoodReports  inner join  Therapist on FoodReports.numTherapist = Therapist.numTherapist WHERE FoodReports.numPatient = " + NumPatient+" AND FoodReports.comments <> 'NULL'";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection
               FoodReport commentsT = new FoodReport();

                commentsT.Date = Convert.ToString(dr["dateReport"]);

                //TherapistComment.NumTherapist = Convert.ToInt16(dr["numTherapist"]);

                commentsT.Comments = Convert.ToString(dr["comments"]);


                //TherapistComment.FirstName = Convert.ToString(dr["firstNamePatient"]);
                //TherapistComment.LastName = Convert.ToString(dr["lastNamePatient"]);
                commentsT.NameTherapist = Convert.ToString(dr["nameTherapist"]);
                //TherapistComment.Id = Convert.ToString(dr["idPatient"]);

                TherapistCommentsList.Add(commentsT);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        return TherapistCommentsList;
    }



    public List<Therapist> GetTherapist()
    {
        SqlConnection con;
        List<Therapist> lt = new List<Therapist>();

        try
        {
            con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            String selectSTR = "select * from Therapist ";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {// Read till the end of the data into a row
             // read first field from the row into the list collection

                Therapist therapist = new Therapist();

                therapist.NumTherapist = Convert.ToInt16(dr["numTherapist"]);
                therapist.IdTherapist = Convert.ToString(dr["idTherapist"]);
                therapist.NameTherapist = Convert.ToString(dr["nameTherapist"]);

                lt.Add(therapist);
            }
            return lt;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public int ChangeActiveTheraCom(string idComment)
    {
        SqlConnection con;
        SqlCommand cmd;
        int numAffected = 0;

        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String pStr = BuildUpdateTheraComActive(idComment);      // helper method to build the insert string

        cmd = CreateCommand(pStr, con);             // create the command

        try
        {
            //int idTest = Convert.ToInt16(cmd.ExecuteScalar());

            numAffected = cmd.ExecuteNonQuery(); // execute the command    

            return numAffected;
        }

        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }
    }

    private String BuildUpdateTheraComActive(string idComment)
    {
        string cmdStr = "UPDATE Therapist_Comments SET activeCommentTherapist =0 WHERE idComment ='" + idComment + "'";
        // helper method to build the insert string

        return cmdStr;
    }

    public int InsertMeal(Meal m)
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlCommand cmd1;
        SqlCommand cmd2;

        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String pStr = BuildInsertMealCommand(m);      // helper method to build the insert string

        cmd = CreateCommand(pStr, con);             // create the command

        try
        {

            int mealId = Convert.ToInt16(cmd.ExecuteScalar());

            int numAffected = 0;
            if (mealId > 0)
            {
                for (int i = 0; i < m.Categories.Length; i++)
                {
                    String cStr1 = BuildInsertMealCategories(m.Categories[i], mealId);
                    cmd1 = CreateCommand(cStr1, con);
                    numAffected += cmd1.ExecuteNonQuery();
                }
            }
            return numAffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }


        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    private string BuildInsertMealCategories(string c, int mealId)
    {
        String command;
        StringBuilder sb = new StringBuilder();

        sb.AppendFormat("Values('{0}', '{1}',{2})", mealId, c, 1);
        String prefix = "INSERT INTO Meals_categories (meal_id, category_id,active)";

        command = prefix + sb.ToString();

        return command;
    }

    internal List<Meal> GetAllMeals()
    {
        SqlConnection con;
        List<Meal> meals = new List<Meal>();

        try
        {
            con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            String selectSTR = @"select  m.*,mt.*
                                        from meals m
                                        inner join MealType mt
                                        on m.meal_type_id = mt.idmealtype where m.active=1";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //ReadFromDataBase("dietDBConnectionString", "Sensitivity_Patient");
            Meal m;

            while (dr.Read())
            {
                m = new Meal();
                m.Id = Convert.ToInt32(dr["id"]);
                m.Name = dr["NameMealType"].ToString();
                m.MealTypeId = dr["meal_type_id"].ToString();
                m.Description = dr["description"].ToString();
                meals.Add(m);
            }
            return meals;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    internal int DeleteMealsOfDayInAWeek(int dayMealId, int week, int day)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = "delete from meals_days where meal_id=" + dayMealId + " and day=" + day + " and week=" + week;

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numAffected = cmd.ExecuteNonQuery(); // execute the comm
            return numAffected;
        }
        catch (Exception ex)
        {

            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    internal List<Product> GetMealProductsByMealTypeIdAndWeekAndDay(int week, int day, int mealTypeId)
    {
        SqlConnection con;
        List<Product> products = new List<Product>();

        try
        {
            con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            String selectSTR = @"select m.meal_type_id,md.*,p.*
                                  from meals m
                                  inner join  meals_days md
                                  on m.id=md.meal_id
                                  inner join [Meals_Categories] mc
                                  on mc.meal_id=md.meal_id
                                  inner join products p
                                  on mc.category_id=p.idProduct
                                  where md.week=" + week + " and md.day=" + day + " and m.meal_type_id=" + mealTypeId;

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //ReadFromDataBase("dietDBConnectionString", "Sensitivity_Patient");
            Product c;

            while (dr.Read())
            {
                c = new Product();
                c.IdProduct = Convert.ToInt32(dr["idProduct"]);
                c.NameProduct = dr["nameProduct"].ToString();
                products.Add(c);
            }
            return products;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    //internal List<DayMealView> GetMealViewByWeek(int week, int day)
    //{
    //    SqlConnection con;
    //    List<DayMealView> lst = new List<DayMealView>();

    //    try
    //    {
    //        con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file
    //    }

    //    catch (Exception ex)
    //    {
    //        // write to log
    //        throw (ex);
    //    }

    //    try
    //    {
    //        String selectSTR = @"select * from products where idCategory=" + catId;

    //        SqlCommand cmd = new SqlCommand(selectSTR, con);

    //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
    //        //ReadFromDataBase("dietDBConnectionString", "Sensitivity_Patient");
    //        Product product;

    //        while (dr.Read())
    //        {   // Read till the end of the data into a row
    //            product = new Product();
    //            product.IdProduct = Convert.ToInt16(dr["idProduct"]);
    //            product.NameProduct = Convert.ToString(dr["nameProduct"]);
    //            //product.Calories = (float)Convert.ToDouble(dr["calories"]);
    //            //product.IdCategory = Convert.ToInt16(dr["idCategory"]);
    //            //product.CategoryName = Convert.ToString(dr["categoryName"]);
    //            //product.MainGroup = Convert.ToString(dr["MainFoodGroups"]);
    //            //product.Weight = (float)Convert.ToDouble(dr["weight"]);
    //            lst.Add(product);
    //        }
    //        return lst;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        if (con != null)
    //        {
    //            con.Close();
    //        }
    //    }
    //}

    internal List<Product> GetCatItems(int catId)
    {
        SqlConnection con;
        List<Product> lst = new List<Product>();

        try
        {
            con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            String selectSTR = @"select * from products where idCategory=" + catId;

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //ReadFromDataBase("dietDBConnectionString", "Sensitivity_Patient");
            Product product;

            while (dr.Read())
            {   // Read till the end of the data into a row
                product = new Product();
                product.IdProduct = Convert.ToInt16(dr["idProduct"]);
                product.NameProduct = Convert.ToString(dr["nameProduct"]);
                //product.Calories = (float)Convert.ToDouble(dr["calories"]);
                //product.IdCategory = Convert.ToInt16(dr["idCategory"]);
                //product.CategoryName = Convert.ToString(dr["categoryName"]);
                //product.MainGroup = Convert.ToString(dr["MainFoodGroups"]);
                //product.Weight = (float)Convert.ToDouble(dr["weight"]);
                lst.Add(product);
            }
            return lst;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    internal int CreateMealsOfDayInAWeek(DayMeal dm)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = "insert into meals_days(meal_id,day,week,curr_date) values(" + dm.MealId + "," + dm.Day + "," + dm.Week + ",'" + dm.Date.ToString("yyyy-MM-dd HH:mm:ss") + "')";

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numAffected = cmd.ExecuteNonQuery(); // execute the comm
            return numAffected;
        }
        catch (Exception ex)
        {

            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    internal List<Meal> GetMealsOfDayInAWeek(int day, int week)
    {
        SqlConnection con;
        List<Meal> meals = new List<Meal>();

        try
        {
            con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            String selectSTR = @"select  md.*,m.*,mt.NameMealType
                                        from meals_days md
                                        inner join meals m
                                        on md.meal_id=m.id
                                        inner join MealType mt
                                        on m.meal_type_id = mt.idmealtype
                                        where day=" + day + " and week=" + week + " order by meal_type_id";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //ReadFromDataBase("dietDBConnectionString", "Sensitivity_Patient");
            Meal m;

            while (dr.Read())
            {
                m = new Meal();
                m.Id = Convert.ToInt32(dr["meal_id"]);
                m.Name = dr["NameMealType"].ToString();
                m.MealTypeId = dr["meal_type_id"].ToString();
                m.Description = dr["description"].ToString();
                meals.Add(m);
            }
            return meals;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }



    internal List<DayMeal> GetDateWeek(int year)
    {
        SqlConnection con;
        List<DayMeal> d = new List<DayMeal>();

        try
        {
            con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            String selectSTR = @"select week, date
                                    from(
	                                    select ROW_NUMBER() OVER(PARTITION BY week ORDER BY date ASC) AS Row_num,week, date
	                                    from Dates 
	                                    where year="+year+") x where Row_num=1";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //ReadFromDataBase("dietDBConnectionString", "Sensitivity_Patient");
            DayMeal m;

            while (dr.Read())
            {
                m = new DayMeal();
                m.Week = Convert.ToInt32(dr["week"]);
                m.DateToWeek = dr["date"].ToString();
      
                d.Add(m);
            }
            return d;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }



    internal int UpdateMeal(Meal meal)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = @"update meals set description='" + meal.Description + "' where id=" + meal.Id;

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numAffected = cmd.ExecuteNonQuery(); // execute the comm
            return numAffected;
        }
        catch (Exception ex)
        {

            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    internal int AddProductToMeal(string mealid, string prodId)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = "insert into meals_categories(meal_id,category_id) values(" + mealid + "," + prodId + ")";

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numAffected = cmd.ExecuteNonQuery(); // execute the comm
            return numAffected;
        }
        catch (Exception ex)
        {

            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    internal int DeleteMealProduct(string mealid, string prodId)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = BuildDeleteMealCategory(mealid, prodId);      // helper method to build the insert string

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numAffected = cmd.ExecuteNonQuery(); // execute the comm
            return numAffected;
        }
        catch (Exception ex)
        {

            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    private string BuildDeleteMealCategory(string mealid, string prodID)
    {
        string cmdStr = "DELETE  FROM meals_categories  WHERE meal_id=" + mealid + " and category_id=" + prodID;
        return cmdStr;
    }

    internal List<Product> GetMealProducts(string id)
    {
        SqlConnection con;
        List<Product> products = new List<Product>();

        try
        {
            con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            String selectSTR = @"select mc.*,p.*
                                  from meals_categories mc
                                  inner join products p
                                  on mc.category_id=p.idProduct
                                  where mc.meal_id=" + id;

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //ReadFromDataBase("dietDBConnectionString", "Sensitivity_Patient");
            Product c;

            while (dr.Read())
            {
                c = new Product();
                c.IdProduct = Convert.ToInt32(dr["idProduct"]);
                c.NameProduct = dr["nameProduct"].ToString();
                products.Add(c);
            }
            return products;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    private String BuildInsertMealCommand(Meal m)
    {
        String command;

        StringBuilder sb = new StringBuilder();

        sb.AppendFormat("Values('{0}', '{1}','{2}')", m.MealTypeId, 1, m.Description);
        String prefix = "INSERT INTO Meals (meal_type_id, active,description) OUTPUT INSERTED.id ";

        command = prefix + sb.ToString();

        return command;
    }

    public int InsertReport(FoodReport report)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String pStr = BuildInsertReportCommand(report);      // helper method to build the insert string

        cmd = CreateCommand(pStr, con);             // create the command


        int numAffected = 0;

        try
        {
            //int idTest = Convert.ToInt16(cmd.ExecuteScalar());

            numAffected = cmd.ExecuteNonQuery(); // execute the command    

            return numAffected;

        }

        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    private String BuildInsertReportCommand(FoodReport report)
    {
        String command;

        StringBuilder sb = new StringBuilder();
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values({0}, {1} ,'{2}', '{3}', '{4}',{5},{6},{7},{8})", report.NumTherapist, report.NumPatient, report.Date, report.Hour, report.MealName, report.Eat, report.Eat_hlaf, report.No_eat, report.IdProduct);
        String prefix = "INSERT INTO FoodReports (numTherapist,numPatient, dateReport,hourReport , mealName, eat, eat_half,not_eat,idProduct )OUTPUT INSERTED.reportId ";

        command = prefix + sb.ToString();

        return command;
    }



    internal int GenerateFoodReportData(Patient p, Therapist randomTherapist, DateTime randomDate,
            Product product, bool isEat)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = string.Format(@"INSERT INTO [dbo].[FoodReports]
           ([numTherapist]
           ,[numPatient]
           ,[dateReport]
           ,[eat]
           ,[not_eat]
           ,[idProduct])
     
     VALUES
           ({0}
           ,{1}
           ,'{2}'
           ,{3}
           ,{4}
           ,{5}
           )", randomTherapist.NumTherapist,
           p.NumPatient
           , randomDate.ToString("yyyy-MM-dd HH:mm:ss")
           , isEat ? "1" : "0", isEat ? "0" : "1", product.IdProduct);

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numAffected = cmd.ExecuteNonQuery(); // execute the comm
            return numAffected;
        }
        catch (Exception ex)
        {

            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    internal List<FoodReport> GetFoodReportByPatienty(string v, int numPatient)
    {
        SqlConnection con;
        List<FoodReport> foodReportsList = new List<FoodReport>();

        try
        {
            con = connect("dietDBConnectionString"); // create a connection to the database using the connection String defined in the web config file
        }

        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        try
        {
            String selectSTR = "SELECT * FROM [FoodReports] where [numPatient]= " + numPatient;

            //String selectSTR = "SELECT * FROM LaboratoryTests ";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                FoodReport foodReport = new FoodReport();

                foodReport.IdReport = Convert.ToInt32(dr["reportId"]);
                foodReport.IdProduct = Convert.ToInt32(dr["idProduct"]);
                foodReport.Date = Convert.ToString(dr["dateReport"]);
                foodReport.Eat = dr["eat"].ToString() == "True" ? 1 : 0;

                foodReportsList.Add(foodReport);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        return foodReportsList;
    }

    internal int GenerateBloodData(int numPatient, double Albumin, int Cholesterol, int Protein, double Crp, int Lymphocytes)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = string.Format(@"INSERT INTO [dbo].[LaboratoryTests]
           ([numPatient]
           ,[dateLab]
           ,[albumin]
           ,[lymphocytes]
           ,[cholesterol]
           ,[crp]
           ,[proteinTotal]
           ,[activeLab])
     VALUES
           ({0}
           ,'{1}'
           ,{2}
           ,{3}
           ,{4}
           ,{5}
           ,{6}
           ,1)", numPatient, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
           , Albumin, Lymphocytes, Cholesterol, Crp, Protein);

        cmd = CreateCommand(cStr, con);             // create the command

        try
        {
            int numAffected = cmd.ExecuteNonQuery(); // execute the comm
            return numAffected;
        }
        catch (Exception ex)
        {

            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    internal int DeleteData()
    {
        SqlConnection con;
        SqlCommand cmd;


        try
        {
            con = connect("dietDBConnectionString"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String cStr = @"delete FROM [bgroup67_test2].[dbo].[LaboratoryTests]
                        delete FROM [bgroup67_test2].[dbo].[FoodReports]";

        cmd = CreateCommand(cStr, con);             // create the command


        try
        {
            int numAffected = cmd.ExecuteNonQuery(); // execute the comm

            return numAffected;


        }
        catch (Exception ex)
        {

            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

}







