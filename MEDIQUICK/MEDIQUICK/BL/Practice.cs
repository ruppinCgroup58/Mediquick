namespace MEDIQUICK.BL
{
    public class Practice
    {
        string practiceSerialNuber;
        List<Question> questionsList;

        public Practice(string practiceSerialNuber, List<Question> questionsList)
        {
            PracticeSerialNuber = practiceSerialNuber;
            QuestionsList = questionsList;
        }

        public string PracticeSerialNuber { get => practiceSerialNuber; set => practiceSerialNuber = value; }
        public List<Question> QuestionsList { get => questionsList; set => questionsList = value; }
    }
}
