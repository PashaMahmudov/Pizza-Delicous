namespace pizza.Models
{
    public class Comment

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }

        public int? ParentCommentId { get; set; } 
        public Comment ParentComment { get; set; }
        public List<Comment> Replies { get; set; }
    }
}
