﻿using Google.Cloud.AIPlatform.V1;
using MEDIQUICK.BL;
using MEDIQUICK.Controllers;
using System.Data;
using System.Data.SqlClient;
using static Google.Api.Gax.Grpc.Gcp.AffinityConfig.Types;

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

    public List<Object> GetQuestionsByTopic(int qId, string topicName)
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

        cmd = CreateGetQuestionsByTopicCommandWithStoredProcedure("sp_Question_getQuestionByTopic", con, qId, topicName);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
            List<Object> ObjectList = new List<Object>();

            while (dataReader.Read())
            {
                ObjectList.Add(new
                {
                    QuestionSerialNumber = Convert.ToInt32(dataReader["questionSerialNumber"]),
                    Content = dataReader["content"].ToString(),
                    CorrectAnswer = dataReader["correctAnswer"].ToString(),
                    WrongAnswer1 = dataReader["wrongAnswer1"].ToString(),
                    WrongAnswer2 = dataReader["wrongAnswer2"].ToString(),
                    WrongAnswer3 = dataReader["wrongAnswer3"].ToString(),
                    Explanation = dataReader["explanation"].ToString(),
                    Topic = dataReader["topicId"].ToString(),
                    difficulty = Convert.ToInt32(dataReader["difficulty"]),
                    status = Convert.ToInt32(dataReader["status"]),
                    creator = dataReader["topicId"].ToString(),
                    totalAnswers = Convert.ToInt32(dataReader["totalAnswers"]),
                    totalCorrectAnswers =Convert.ToInt32(dataReader["correctAnswers"])
                });
            }

            return ObjectList;
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
    public List<Object> GetQuestionsByTopicAndId(int topicId, int userId)
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

        cmd = CreateGetQuestionsByTopicAndIdCommandWithStoredProcedure("sp_Question_getQuestionByTopicAndId", con, topicId, userId);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
            List<Object> ObjectList = new List<Object>();

            while (dataReader.Read())
            {
                ObjectList.Add(new
                {
                    QuestionSerialNumber = Convert.ToInt32(dataReader["questionSerialNumber"]),
                    Content = dataReader["content"].ToString(),
                    CorrectAnswer = dataReader["correctAnswer"].ToString(),
                    WrongAnswer1 = dataReader["wrongAnswer1"].ToString(),
                    WrongAnswer2 = dataReader["wrongAnswer2"].ToString(),
                    WrongAnswer3 = dataReader["wrongAnswer3"].ToString(),
                    Explanation = dataReader["explanation"].ToString(),
                    isFavourite = dataReader["isFavourite"].ToString(),
                    userAnswered = dataReader["userAnswered"].ToString()
                });
            }

            return ObjectList;
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

    public bool updateQuestionDetail(Question q)
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

        cmd = CreateQuestionChangeDetailsCommandWithStoredProcedureWithParameters("sp_Question_updateQuestionDetailr", con, q);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            if (numEffected > 0) { return true; } else { return false; }
        }
        catch (Exception ex)
        {
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

        cmd = CreateQuestionGetCommandWithStoredProcedure("sp_Question_getQuestion", con, id);             // create the command

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
            q.Status = Convert.ToInt32(dataReader["status"]);
            q.Creator = dataReader["creatorID"].ToString();
            q.TotalAnswers = Convert.ToInt32(dataReader["totalAnswers"]);
            q.TotalCorrectAnswers = Convert.ToInt32(dataReader["correctAnswers"]);
            q.Topic = dataReader["topicId"].ToString();
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
    public List<Question> GetFavouriteQuestionsUser(int userId)
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

        cmd = GetFavouriteQuestionsUserCommandWithStoredProcedure("sp_Question_getUserFavouriteQuestions", con, userId);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
            List<Question> qList = new List<Question>();

            while (dataReader.Read())
            {
                Question q = new Question();
                q.QuestionSerialNumber = Convert.ToInt32(dataReader["questionSerialNumber"]);
                q.Content = dataReader["content"].ToString();
                q.CorrectAnswer = dataReader["correctAnswer"].ToString();
                q.WrongAnswer1 = dataReader["wrongAnswer1"].ToString();
                q.WrongAnswer2 = dataReader["wrongAnswer2"].ToString();
                q.WrongAnswer3 = dataReader["wrongAnswer3"].ToString();
                q.Explanation = dataReader["explanation"].ToString();
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

        cmd = CreateReadQuestionsCommandWithStoredProcedure("sp_Question_ReadQuestions", con);             // create the command

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
                q.Topic = dataReader["topicName"].ToString();
                q.Status = Convert.ToInt32(dataReader["status"]);
                q.Creator = dataReader["creatorID"].ToString();
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

        cmd = CreateQuestionInsertCommandWithStoredProcedure("sp_Question_insertQuestion", con, question);             // create the command

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
    public int toggleFavouriteQ(int questionId, int userId)
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

        cmd = toggleFavouriteQuestionCommandWithStoredProcedure("sp_Question_toggleFavouriteQuestion", con, questionId, userId);             // create the command

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

    public int HandleQAnswer(int questionId, int userId, bool isCorrect)
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

        cmd = HandleQuestionAnswerWithStoredProcedure("sp_Question_HandleQuestionAnswer", con, questionId, userId, isCorrect);             // create the command

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
    public int UpdateDifficultyLevel(int id, bool isCorrect)
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

        cmd = CreateUpdateDLCommandWithStoredProcedure("sp_Question_UpdateQuestioncounts", con, id, isCorrect);             // create the command

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

    public int changeQuestionStatus(int id, int newStatus)
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

        cmd = CreateUpdateQuestionStatusCommandWithStoredProcedure("sp_Question_changeQuestionStatus", con, id, newStatus);             // create the command

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

    private SqlCommand CreateQuestionChangeDetailsCommandWithStoredProcedureWithParameters(String spName, SqlConnection con, Question q)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@qSerialNumber", q.QuestionSerialNumber);
        cmd.Parameters.AddWithValue("@content", q.Content);
        cmd.Parameters.AddWithValue("@rightAnswer", q.CorrectAnswer);
        cmd.Parameters.AddWithValue("@wrongAnswer1", q.WrongAnswer1);
        cmd.Parameters.AddWithValue("@wrongAnswer2", q.WrongAnswer2);
        cmd.Parameters.AddWithValue("@wrongAnswer3", q.WrongAnswer3);
        cmd.Parameters.AddWithValue("@explanation", q.Explanation);
        cmd.Parameters.AddWithValue("@Topic", q.Topic);
        return cmd;
    }

    private SqlCommand CreateGetQuestionsByTopicAndIdCommandWithStoredProcedure(String spName, SqlConnection con, int topicId, int userId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@topicId", topicId);

        cmd.Parameters.AddWithValue("@userId", userId);

        return cmd;
    }

    private SqlCommand CreateGetQuestionsByTopicCommandWithStoredProcedure(String spName, SqlConnection con, int qId, string topicName)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@qId", qId);

        cmd.Parameters.AddWithValue("@topicName", topicName);

        return cmd;
    }

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

    private SqlCommand GetFavouriteQuestionsUserCommandWithStoredProcedure(String spName, SqlConnection con, int userId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@userId", userId);

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
        cmd.Parameters.AddWithValue("@topicId", question.Topic);

        return cmd;
    }

    private SqlCommand toggleFavouriteQuestionCommandWithStoredProcedure(String spName, SqlConnection con, int questionId, int userId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@questionId", questionId);
        cmd.Parameters.AddWithValue("@userId", userId);

        return cmd;
    }

    private SqlCommand HandleQuestionAnswerWithStoredProcedure(String spName, SqlConnection con, int questionId, int userId,bool isCorrect)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@questionId", questionId);
        cmd.Parameters.AddWithValue("@userId", userId);
        cmd.Parameters.AddWithValue("@isCorrect", isCorrect);

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

    private SqlCommand CreateUpdateDLCommandWithStoredProcedure(String spName, SqlConnection con, int id, bool isCorrect)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@isCorrect", isCorrect);


        return cmd;
    }

    private SqlCommand CreateUpdateQuestionStatusCommandWithStoredProcedure(String spName, SqlConnection con, int id, int newStatus)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@id", id);

        cmd.Parameters.AddWithValue("@newStatus", newStatus);


        return cmd;
    }


    #endregion



    //-----------User class Functions-----------
    #region User's Functions
    public User InsertUser(User user)
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

        cmd = CreateUserInsertCommandWithStoredProcedure("sp_User_insertUser", con, user);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return user;
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

    public bool ChangeUsersStatus(string email, bool newStatus)
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

        cmd = CreateUserChangeStatusCommandWithStoredProcedureWithParameters("sp_user_changeUserStatus", con, email, newStatus);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            if (numEffected > 0) { return true; } else { return false; }
        }
        catch (Exception ex)
        {
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

    public bool updateUserDetail(User u)
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

        cmd = CreateUserChangeDetailsCommandWithStoredProcedureWithParameters("sp_User_updateUserDetail", con, u);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            if (numEffected > 0) { return true; } else { return false; }
        }
        catch (Exception ex)
        {
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

    public bool ChangeAdminStatus(string email, bool newAdminStatus)
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

        cmd = CreateUserChangeAdminStatusCommandWithStoredProcedureWithParameters("sp_user_changeUserAdminStatus", con, email, newAdminStatus);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            if (numEffected > 0) { return true; } else { return false; }
        }
        catch (Exception ex)
        {
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

        cmd = CreateUserLoginCommandWithStoredProcedureWithParameters("sp_user_LoginUser", con, email, password);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dataReader.Read();
            User u = new User();
            //u.FirstName = dataReader["firstName"].ToString();
            //u.FamilyName = dataReader["familyName"].ToString();
            u.UserID = dataReader.GetInt32("userId");
            u.Email = dataReader["email"].ToString();
            u.Password = dataReader["password"].ToString();
            u.IsAdmin = bool.Parse(dataReader["isAdmin"].ToString());
            u.IsActive = bool.Parse(dataReader["isActive"].ToString());

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

    public List<User> ReadUsers()
    {

        SqlConnection con;
        SqlCommand cmd;
        List<User> usersList = new List<User>();

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateGetUserCommandWithStoredProcedureWithoutParameters("sp_user_getUsers", con);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dataReader.Read())
            {
                User u = new User();
                u.UserID = Convert.ToInt32(dataReader["UserID"]);
                u.FirstName = dataReader["firstName"].ToString();
                u.LastName = dataReader["lastName"].ToString();
                u.Email = dataReader["email"].ToString();
                u.Password = dataReader["password"].ToString();
                u.PhoneNumber = dataReader["phoneNumber"].ToString();
                u.IsAdmin = bool.Parse(dataReader["isAdmin"].ToString());
                u.IsActive = bool.Parse(dataReader["isActive"].ToString());
                usersList.Add(u);
            }
            return usersList;
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

    private SqlCommand CreateUserChangeStatusCommandWithStoredProcedureWithParameters(String spName, SqlConnection con, string email, bool newStatus)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@email", email);

        cmd.Parameters.AddWithValue("@newStatus", newStatus);

        return cmd;
    }

    private SqlCommand CreateUserChangeAdminStatusCommandWithStoredProcedureWithParameters(String spName, SqlConnection con, string email, bool newAdminStatus)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@email", email);

        cmd.Parameters.AddWithValue("@newAdminStatus", newAdminStatus);

        return cmd;
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

    private SqlCommand CreateGetUserCommandWithStoredProcedureWithoutParameters(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;
    }

    private SqlCommand CreateUserChangeDetailsCommandWithStoredProcedureWithParameters(String spName, SqlConnection con, User u)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@firstName", u.FirstName);
        cmd.Parameters.AddWithValue("@lastName", u.LastName);
        cmd.Parameters.AddWithValue("@email", u.Email);
        cmd.Parameters.AddWithValue("@password", u.Password);
        cmd.Parameters.AddWithValue("@phoneNumber", u.PhoneNumber);

        return cmd;
    }

    #endregion


    //-----------Topic class Functions-----------
    #region Topic's Functions
    public List<Topic> GetTopics()
    {

        SqlConnection con;
        SqlCommand cmd;
        List<Topic> topicsList = new List<Topic>();

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureWithoutParameters("sp_Topic_getTopics", con);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dataReader.Read())
            {
                Topic t = new Topic();
                t.TopicId = Convert.ToInt32(dataReader["topicId"]);
                t.TopicName = dataReader["topicName"].ToString();
                topicsList.Add(t);
            }
            return topicsList;
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

    public List<object> GetUserProgress(int userID)
    {
        SqlConnection con;
        SqlCommand cmd;
        List<Object> objectList = new List<Object>();       //Ad-Hoc Objects

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateGetUPPCommandWithStoredProcedure("sp_user_getUserProgress", con, userID);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dataReader.Read())
            {
                objectList.Add(new
                {
                    topicId = Convert.ToInt32(dataReader["topicId"]),
                    topicName = dataReader["topicName"].ToString(),
                    AnsweredRatio = Convert.ToInt32(dataReader["AnsweredRatio"])
                });
            }
            return objectList;
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

    private SqlCommand CreateGetUPPCommandWithStoredProcedure(String spName, SqlConnection con, int userID)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@userID", userID);


        return cmd;
    }
    #endregion

    //-----------Practice class Functions-----------
    #region Practice's Functions
    public List<Object> GeneratePractice(string selectedTopics, string selectedDiffLevels, int userId)
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

        cmd = CreateGeneratePracticeCommandWithStoredProcedure("sp_Practice_GetFilteredQuestions", con, selectedTopics, selectedDiffLevels, userId);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
            List<Object> ObjectList = new List<Object>();

            while (dataReader.Read())
            {
                ObjectList.Add(new
                {
                    QuestionSerialNumber = Convert.ToInt32(dataReader["questionSerialNumber"]),
                    Content = dataReader["content"].ToString(),
                    CorrectAnswer = dataReader["correctAnswer"].ToString(),
                    WrongAnswer1 = dataReader["wrongAnswer1"].ToString(),
                    WrongAnswer2 = dataReader["wrongAnswer2"].ToString(),
                    WrongAnswer3 = dataReader["wrongAnswer3"].ToString(),
                    Explanation = dataReader["explanation"].ToString(),
                    isFavourite = dataReader["isFavourite"].ToString()
                });
            }

            return ObjectList;
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

    private SqlCommand CreateGeneratePracticeCommandWithStoredProcedure(String spName, SqlConnection con, string selectedTopics, string selectedDiffLevels, int userId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@topics", selectedTopics);

        cmd.Parameters.AddWithValue("@difficulties", selectedDiffLevels);

        cmd.Parameters.AddWithValue("@userId ", userId);

        return cmd;
    }

    #endregion


    //-----------Test class Functions-----------
    #region Test's Functions

    public int CreateTest(int userId)
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

        cmd = CreateTestWithStoredProcedure("sp_Test_CreateTest", con, userId);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
                                                     // Read the output parameter value after executing the stored procedure
            int testId = Convert.ToInt32(cmd.Parameters["@TestID"].Value);
            return testId;
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

    public Question Test_HandleQuestionAnswer(int userId, int testId, int questionId, bool isCorrect)
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

        cmd = Test_HandleQuestionAnswerWithStoredProcedure("sp_Test_HandleQuestionAnswer", con, userId, testId, questionId,isCorrect);             // create the command

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
            q.Topic = dataReader["topicId"].ToString();
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

    public int EndTest(int testId)
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

        cmd = EndTestWithStoredProcedure("sp_Test_EndTest", con, testId);             // create the command

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
    private SqlCommand CreateTestWithStoredProcedure(String spName, SqlConnection con, int userId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@UserID", userId);
        // Add output parameter
        SqlParameter outputParam = new SqlParameter("@TestID", SqlDbType.Int);
        outputParam.Direction = ParameterDirection.Output;
        cmd.Parameters.Add(outputParam);

        return cmd;
    }

    private SqlCommand Test_HandleQuestionAnswerWithStoredProcedure(String spName, SqlConnection con, int userId,int testId, int questionId, bool isCorrect)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@UserID", userId);
        cmd.Parameters.AddWithValue("@testId", testId);
        cmd.Parameters.AddWithValue("@questionId", questionId);
        cmd.Parameters.AddWithValue("@isCorrect", isCorrect);

        return cmd;
    }

    private SqlCommand EndTestWithStoredProcedure(String spName, SqlConnection con, int testId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@TestID", testId);

        return cmd;
    }


    #endregion


    //-----------forumIssue class Functions-----------
    #region forumIssue's Functions

    public List<object> GetIssuesWithCommentCountByTopic(int topicid)
    {
        SqlConnection con;
        SqlCommand cmd;
        List<Object> objectList = new List<Object>();       //Ad-Hoc Objects

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = GetIssuesWithCommentCountWithStoredProcedure("sp_Forum_GetIssuesWithCommentCount", con, topicid);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dataReader.Read())
            {
                objectList.Add(new
                {
                    issueId = Convert.ToInt32(dataReader["issueId"]),
                    topicid = Convert.ToInt32(dataReader["topicid"]),
                    userId = Convert.ToInt32(dataReader["userId"]),
                    title = dataReader["title"].ToString(),
                    UserFullName = dataReader["UserFullName"].ToString(),
                    IssueContent = dataReader["IssueContent"].ToString(),
                    CommentCount = Convert.ToInt32(dataReader["CommentCount"]),
                    isClosed = bool.Parse(dataReader["isClosed"].ToString()),
                    createdAt = dataReader.GetDateTime(dataReader.GetOrdinal("createdAt")) 
                });
            }
            return objectList;
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

    public List<object> GetIssueWithComments(int issueId)
    {
        SqlConnection con;
        SqlCommand cmd;
        List<Object> objectList = new List<Object>();       //Ad-Hoc Objects

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = GetIssueWithCommentsWithStoredProcedure("sp_Forum_GetIssueWithComments", con, issueId);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dataReader.Read())
            {
                objectList.Add(new
                {
                    issueId = Convert.ToInt32(dataReader["issueId"]),
                    topicid = Convert.ToInt32(dataReader["topicid"]),
                    title = dataReader["title"].ToString(),
                    IssueContent = dataReader["IssueContent"].ToString(),
                    CommentCount = Convert.ToInt32(dataReader["CommentCount"]),
                    commentId = Convert.ToInt32(dataReader["commentId"]),
                    userId = Convert.ToInt32(dataReader["userId"]),
                    UserFullName = dataReader["UserFullName"].ToString(),
                    CommentContent = dataReader["CommentContent"].ToString(),
                    CommentCreatedAt = dataReader.GetDateTime(dataReader.GetOrdinal("CommentCreatedAt"))
                });
            }
            return objectList;
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

    public int InsertIssue(Issue issue)
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

        cmd = CreateIssueInsertCommandWithStoredProcedure("sp_Forum_AddIssue", con, issue);             // create the command

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

    public bool updateIssueDetail(Issue issue)
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

        cmd = CreateIssueChangeDetailsCommandWithStoredProcedureWithParameters("sp_Forum_updateIssueDetail", con, issue);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            if (numEffected > 0) { return true; } else { return false; }
        }
        catch (Exception ex)
        {
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

    public bool toggleIssueStatus(int issueid)
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

        cmd = toggleIssueStatusWithStoredProcedureWithParameters("sp_Forum_toggleIssueStatus", con, issueid);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            if (numEffected > 0) { return true; } else { return false; }
        }
        catch (Exception ex)
        {
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

    private SqlCommand GetIssuesWithCommentCountWithStoredProcedure(String spName, SqlConnection con, int topicid)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@topicid", topicid);


        return cmd;
    }

    private SqlCommand GetIssueWithCommentsWithStoredProcedure(String spName, SqlConnection con, int issueId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@issueId", issueId);


        return cmd;
    }

    private SqlCommand CreateIssueInsertCommandWithStoredProcedure(String spName, SqlConnection con, Issue issue)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@topicId", issue.TopicId);
        cmd.Parameters.AddWithValue("@userId", issue.UserId);
        cmd.Parameters.AddWithValue("@title", issue.Title);
        cmd.Parameters.AddWithValue("@content ", issue.Content);


        return cmd;
    }

    private SqlCommand CreateIssueChangeDetailsCommandWithStoredProcedureWithParameters(String spName, SqlConnection con, Issue issue)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@issueId", issue.IssueId);
        cmd.Parameters.AddWithValue("@title", issue.Title);
        cmd.Parameters.AddWithValue("@content", issue.Content);
        return cmd;
    }

    private SqlCommand toggleIssueStatusWithStoredProcedureWithParameters(String spName, SqlConnection con, int issueid)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@issueId", issueid);
        return cmd;
    }

    #endregion

    //-----------forumComment class Functions-----------
    #region forumComment's Functions
    public int InsertComment(Comment comment)
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

        cmd = CreateCommentInsertCommandWithStoredProcedure("sp_Forum_AddForumComment", con, comment);             // create the command

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

    public bool updateCommentDetail(Comment comment)
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

        cmd = CreateCommentChangeDetailsCommandWithStoredProcedureWithParameters("sp_Forum_updateCommentDetail", con, comment);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            if (numEffected > 0) { return true; } else { return false; }
        }
        catch (Exception ex)
        {
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

    public bool updateCommentInactive(Comment comment)
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

        cmd = updateCommenInctiveWithStoredProcedureWithParameters("sp_Forum_setCommentInactive", con, comment); // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            if (numEffected > 0) { return true; } else { return false; }
        }
        catch (Exception ex)
        {
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

    private SqlCommand CreateCommentInsertCommandWithStoredProcedure(String spName, SqlConnection con, Comment comment)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@issueId", comment.IssueId);
        cmd.Parameters.AddWithValue(" @userId", comment.UserId);
        cmd.Parameters.AddWithValue("@content ", comment.Content);

        return cmd;
    }

    private SqlCommand CreateCommentChangeDetailsCommandWithStoredProcedureWithParameters(String spName, SqlConnection con, Comment comment)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@commentId", comment.CommentId);
        cmd.Parameters.AddWithValue("@content", comment.Content);
        return cmd;
    }

    private SqlCommand updateCommenInctiveWithStoredProcedureWithParameters(String spName, SqlConnection con, Comment comment)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@issueId", comment.IssueId);
        cmd.Parameters.AddWithValue("@commentId", comment.CommentId);
        return cmd;


       
    } 
    #endregion
}



