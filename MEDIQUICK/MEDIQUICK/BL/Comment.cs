namespace MEDIQUICK.BL
{
    public class Comment
    {
        int commentId;
        int issueId;
        int userId;
        string content;
        DateTime createdAt;
        DBServices dbs = new DBServices();

        public Comment()
        {

        }

        public Comment(int commentId, int issueId, int userId, string content, DateTime createdAt)
        {
            this.commentId = commentId;
            this.issueId = issueId;
            this.userId = userId;
            this.content = content;
            this.createdAt = createdAt;
        }

        public int CommentId { get => commentId; set => commentId = value; }
        public int IssueId { get => issueId; set => issueId = value; }
        public int UserId { get => userId; set => userId = value; }
        public string Content { get => content; set => content = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }

        public int Insert()
        {
            return dbs.InsertComment(this);
        }




    }
}
