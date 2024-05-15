using MEDIQUICK.BL;
using System.Data;
using System.Data.SqlClient;

public class DBServices
{
    public DBServices() { }

    public SqlConnection connect(String conString)
    {
        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    private SqlCommand CreateCommandWithStoredProcedureWithoutParameters(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;
    }

    //-----------Question class Functions-----------
    #region Question's Functions
    public Question GetQuestion(int id)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateQuestionGetCommandWithStoredProcedure("sp_getQuestion", con, id);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
            dataReader.Read();
            Question q = new Question();
            
            q.QuestionSerialNumber = Convert.ToInt32(dataReader["questionSerialNumber"]);
            q.Difficulty = Convert.ToInt32(dataReader["difficulty"]);
            q.Content = dataReader["content"].ToString();
            q.CorrectAnswer = dataReader["correctAnswer"].ToString();
            q.WrongAnswer1 = dataReader["wrongAnswer1"].ToString();
            q.WrongAnswer2 = dataReader["wrongAnswer2"].ToString();
            q.WrongAnswer3 = dataReader["wrongAnswer3"].ToString();
            q.Explanation = dataReader["explanation"].ToString();
            q.Status = dataReader.GetBoolean(dataReader.GetOrdinal("status"));
            q.Creator = Convert.ToInt32(dataReader["creatorID"]); 
            q.TotalAnswers = Convert.ToInt32(dataReader["totalAnswers"]); 
            q.TotalCorrectAnswers = Convert.ToInt32(dataReader["correctAnswers"]); 
            return q;
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

    public List<Question> ReadQuestions()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateReadQuestionsCommandWithStoredProcedure("sp_ReadQuestions", con);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
            List<Question> qList = new List<Question>();

            while (dataReader.Read())
            {
                Question q = new Question();
                q.QuestionSerialNumber = Convert.ToInt32(dataReader["questionSerialNumber"]);
                q.Difficulty = Convert.ToInt32(dataReader["difficulty"]);
                q.Content = dataReader["content"].ToString();
                q.CorrectAnswer = dataReader["correctAnswer"].ToString();
                q.WrongAnswer1 = dataReader["wrongAnswer1"].ToString();
                q.WrongAnswer2 = dataReader["wrongAnswer2"].ToString();
                q.WrongAnswer3 = dataReader["wrongAnswer3"].ToString();
                q.Explanation = dataReader["explanation"].ToString();
                q.Status = dataReader.GetBoolean(dataReader.GetOrdinal("status"));
                q.Creator = Convert.ToInt32(dataReader["creatorID"]);
                q.TotalAnswers = Convert.ToInt32(dataReader["totalAnswers"]);
                q.TotalCorrectAnswers = Convert.ToInt32(dataReader["correctAnswers"]);
                qList.Add(q); 
            }
            return qList;
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
    public int InsertQuestion(Question question)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateQuestionInsertCommandWithStoredProcedure("sp_insertQuestion", con, question);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
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

    public int UpdateDifficultyLevel(Question q)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateUpdateDLCommandWithStoredProcedure("sp_updateDL", con, q);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
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

    //public string genQ()
    //{

    //}

    //public int updateRightScore(int id)
    //{
    //    SqlConnection con;
    //    SqlCommand cmd;

    //    try
    //    {
    //        con = connect("myProjDB"); // create the connection
    //    }
    //    catch (Exception ex)
    //    {
    //        // write to log
    //        throw (ex);
    //    }

    //    cmd = CreateQuestionRightScoreCommandWithStoredProcedure("sp_updateRightScore", con, id);             // create the command

    //    try
    //    {
    //        int numEffected = cmd.ExecuteNonQuery(); // execute the command
    //        return numEffected;
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
    //            // close the db connection
    //            con.Close();
    //        }
    //    }
    //}

    //public int updateWrongScore(int id)
    //{
    //    SqlConnection con;
    //    SqlCommand cmd;

    //    try
    //    {
    //        con = connect("myProjDB"); // create the connection
    //    }
    //    catch (Exception ex)
    //    {
    //        // write to log
    //        throw (ex);
    //    }

    //    cmd = CreateQuestionWrongScoreCommandWithStoredProcedure("sp_updateWrongScore", con, id);             // create the command

    //    try
    //    {
    //        int numEffected = cmd.ExecuteNonQuery(); // execute the command
    //        return numEffected;
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
    //            // close the db connection
    //            con.Close();
    //        }
    //    }
    //}
    private SqlCommand CreateQuestionGetCommandWithStoredProcedure(String spName, SqlConnection con, int id)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@id", id);

        return cmd;
    }

    private SqlCommand CreateReadQuestionsCommandWithStoredProcedure(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;
    }

    private SqlCommand CreateQuestionInsertCommandWithStoredProcedure(String spName, SqlConnection con, Question question)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@content", question.Content);
        cmd.Parameters.AddWithValue("@correctAnswer", question.CorrectAnswer);
        cmd.Parameters.AddWithValue("@wrongAnswer1", question.WrongAnswer1);
        cmd.Parameters.AddWithValue("@wrongAnswer2", question.WrongAnswer2);
        cmd.Parameters.AddWithValue("@wrongAnswer3", question.WrongAnswer3);
        cmd.Parameters.AddWithValue("@explanation", question.Explanation);
        cmd.Parameters.AddWithValue("@creatorID", question.Creator);
        cmd.Parameters.AddWithValue("@difficulty", question.Difficulty);

        return cmd;
    }

    private SqlCommand CreateQuestionRightScoreCommandWithStoredProcedure(String spName, SqlConnection con, int id)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@id", id);

        return cmd;
    }

    private SqlCommand CreateQuestionWrongScoreCommandWithStoredProcedure(String spName, SqlConnection con, int id)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@id", id);

        return cmd;
    }

    private SqlCommand CreateUpdateDLCommandWithStoredProcedure(String spName, SqlConnection con, Question q)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@id", q.QuestionSerialNumber);
        cmd.Parameters.AddWithValue("@totalCorrectAnswers", q.TotalCorrectAnswers);
        cmd.Parameters.AddWithValue("@totalAnswers", q.TotalAnswers);
        cmd.Parameters.AddWithValue("@difficulty", q.Difficulty);

        return cmd;
    }


    #endregion



    //-----------User class Functions-----------
    #region User's Functions
    public int InsertUser(User user)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateUserInsertCommandWithStoredProcedure("sp_insertUser", con, user);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
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

    public User Login(string email, string password)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateUserLoginCommandWithStoredProcedureWithParameters("sp_loginUser", con, email, password);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dataReader.Read();
            User u = new User();
            //u.FirstName = dataReader["firstName"].ToString();
            //u.FamilyName = dataReader["familyName"].ToString();
            u.Email = dataReader["email"].ToString();
            u.Password = dataReader["password"].ToString();
            //u.IsAdmin = bool.Parse(dataReader["isAdmin"].ToString());
            //u.IsActive = bool.Parse(dataReader["isActive"].ToString());

            return u;
        }
        catch (Exception ex)
        {
            throw new HttpRequestException("User Not Found", null, System.Net.HttpStatusCode.NotFound);
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

    private SqlCommand CreateUserInsertCommandWithStoredProcedure(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
        cmd.Parameters.AddWithValue("@lastName", user.LastName);
        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
        cmd.Parameters.AddWithValue("@Password", user.Password);

        return cmd;
    }

    private SqlCommand CreateUserLoginCommandWithStoredProcedureWithParameters(String spName, SqlConnection con, string email, string password)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@email", email);

        cmd.Parameters.AddWithValue("@password", password);

        return cmd;
    }

    #endregion
}

