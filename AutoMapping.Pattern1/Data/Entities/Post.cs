namespace AutoMapping.Pattern1.Data.Entities
{
    public class Post : BaseEntity<long>
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int CatgeoryId { get; set; }

        public Category Category { get; set; }
    }
}
