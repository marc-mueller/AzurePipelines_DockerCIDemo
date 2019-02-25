namespace DevFun.DataInitializer.Dtos
{
    public class JokeDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public string Tags { get; set; }
        public int LikeCount { get; set; }

        public virtual int CategoryId { get; set; }
        public virtual CategoryDto Category { get; set; }
    }
}