namespace MEDIQUICK.BL
{
    public class Question
    {
        int difficulty;
        string content;
        string correctAnswer;
        string wrongAnswer1;
        string wrongAnswer2;
        string wrongAnswer3;
        string explanation;
        bool status;
        int creator;
        DBServices dbs = new DBServices();

        public Question(int difficulty, string content, string correctAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3, string explanation, bool status, int creator)
        {
            Difficulty = difficulty;
            Content = content;
            CorrectAnswer = correctAnswer;
            WrongAnswer1 = wrongAnswer1;
            WrongAnswer2 = wrongAnswer2;
            WrongAnswer3 = wrongAnswer3;
            Explanation = explanation;
            Status = status;
            Creator = creator;
        }

        public int Difficulty { get => difficulty; set => difficulty = value; }
        public string Content { get => content; set => content = value; }
        public string CorrectAnswer { get => correctAnswer; set => correctAnswer = value; }
        public string WrongAnswer1 { get => wrongAnswer1; set => wrongAnswer1 = value; }
        public string WrongAnswer2 { get => wrongAnswer2; set => wrongAnswer2 = value; }
        public string WrongAnswer3 { get => wrongAnswer3; set => wrongAnswer3 = value; }
        public string Explanation { get => explanation; set => explanation = value; }
        public bool Status { get => status; set => status = value; }
        public int Creator { get => creator; set => creator = value; }

        public int Insert()
        {
            return dbs.InsertQuestion(this);
        }



    }
}
