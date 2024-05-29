namespace MEDIQUICK.BL
{
    public class Topic
    {
        int topidId;
        string topicName;
        DBServices dbs = new DBServices();
        public Topic() { }

        public Topic(int topidId, string topicName)
        {
            TopidId = topidId;
            TopicName = topicName;
        }

        public int TopidId { get => topidId; set => topidId = value; }
        public string TopicName { get => topicName; set => topicName = value; }

        public List<Topic> GetTopics()
        {
            return dbs.GetTopics();
        }

        public List<object> GetUserProgress(int userID)
        {
            return dbs.GetUserProgress(userID);
        }
    }
}
