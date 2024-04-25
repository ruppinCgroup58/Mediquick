namespace MEDIQUICK.BL
{
    public class Test
    {
        int grade;
        DateTime duration;

        public Test(int grade, DateTime duration)
        {
            Grade = grade;
            Duration = duration;
        }

        public int Grade { get => grade; set => grade = value; }
        public DateTime Duration { get => duration; set => duration = value; }
    }
}
