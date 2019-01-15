namespace AutoMapping.Pattern2.Data.Entities
{
    public class Post : BaseEntity<long>
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
