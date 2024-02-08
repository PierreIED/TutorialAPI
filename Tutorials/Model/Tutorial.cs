namespace Tutorials.Model
{
    public class Tutorial
    {
        public int Id { get; set; }
        public string  Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public Category Category { get; set; }

        public List<Comment> Comments { get; set; }

        public Author Author { get; set; }
    }
}
