namespace MEDIQUICK.BL
{
    public class Subject
    {
        string subjectName;
        List<string> keyWords;

        public Subject(string subjectName, List<string> keyWords)
        {
            SubjectName = subjectName;
            KeyWords = keyWords;
        }

        public string SubjectName { get => subjectName; set => subjectName = value; }
        public List<string> KeyWords { get => keyWords; set => keyWords = value; }
    }
}
