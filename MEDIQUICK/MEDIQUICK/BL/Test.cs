namespace MEDIQUICK.BL
{
    public class Test
    {
        int grade;
        DateTime duration;
        List<Question> questionsList;

        public Test(int grade, DateTime duration, List<Question> questionsList)
        {
            Grade = grade;
            Duration = duration;
            QuestionsList = questionsList;
        }

        public int Grade { get => grade; set => grade = value; }
        public DateTime Duration { get => duration; set => duration = value; }
        public List<Question> QuestionsList { get => questionsList; set => questionsList = value; }
    }
}
