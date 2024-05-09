namespace MEDIQUICK.BL
{
    public class Question
    {
        int questionSerialNumber;
        int difficulty=1;
        string content;
        string correctAnswer;
        string wrongAnswer1;
        string wrongAnswer2;
        string wrongAnswer3;
        string explanation;
        bool status;
        int creator;
        int totalAnswers;
        int totalCorrectAnswers;
        DBServices dbs = new DBServices();


        public Question()
        {

        }
        public Question(int questionSerialNumber, int difficulty, string content, string correctAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3, string explanation, bool status, int creator, int totalAnswers, int totalCorrectAnswers)
        {
            QuestionSerialNumber = questionSerialNumber;
            Difficulty = difficulty;
            Content = content;
            CorrectAnswer = correctAnswer;
            WrongAnswer1 = wrongAnswer1;
            WrongAnswer2 = wrongAnswer2;
            WrongAnswer3 = wrongAnswer3;
            Explanation = explanation;
            Status = status;
            Creator = creator;
            TotalAnswers = totalAnswers;
            TotalCorrectAnswers = totalCorrectAnswers;
        }

        public int QuestionSerialNumber { get => questionSerialNumber; set => questionSerialNumber = value; }

        public int Difficulty { get => difficulty; set => difficulty = value; }
        public string Content { get => content; set => content = value; }
        public string CorrectAnswer { get => correctAnswer; set => correctAnswer = value; }
        public string WrongAnswer1 { get => wrongAnswer1; set => wrongAnswer1 = value; }
        public string WrongAnswer2 { get => wrongAnswer2; set => wrongAnswer2 = value; }
        public string WrongAnswer3 { get => wrongAnswer3; set => wrongAnswer3 = value; }
        public string Explanation { get => explanation; set => explanation = value; }
        public bool Status { get => status; set => status = value; }
        public int Creator { get => creator; set => creator = value; }
        public int TotalAnswers { get => totalAnswers; set => totalAnswers = value; }
        public int TotalCorrectAnswers { get => totalCorrectAnswers; set => totalCorrectAnswers = value; }

        public int Insert()
        {
            return dbs.InsertQuestion(this);
        }

        public void updateScore(Question q, bool isCorrect)
        {
            if(isCorrect)
            {
                dbs.updateRightScore(q.QuestionSerialNumber);
            }
            else
            {
                dbs.updateWrongScore(q.QuestionSerialNumber);
            }

            q = q.GetQuestion(q.questionSerialNumber);
            updateDifficulty(q);
        }

        public Question GetQuestion(int id)
        {
            return dbs.GetQuestion(id);
        }


        public void updateDifficulty(Question q)
        {
            float totalcorrect = (float)q.TotalCorrectAnswers;
            float totalanswers = (float)q.TotalAnswers;
            float tmpDifficulty = 1- (totalcorrect / totalanswers);
            if(tmpDifficulty >= 0 && tmpDifficulty <= 0.2)
            {
                q.Difficulty = 1;
            }
            else if (tmpDifficulty > 0.2 && tmpDifficulty <= 0.4)
            {
                q.Difficulty = 2;
            }
            else if (tmpDifficulty > 0.4 && tmpDifficulty <= 0.6)
            {
                q.Difficulty = 3;
            }
            else if (tmpDifficulty > 0.6 && tmpDifficulty <= 0.8)
            {
                q.Difficulty = 4;
            }
            else
            {
                q.Difficulty = 5;
            }
        }

    }
}
