namespace eor_web_app.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public string OName { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }
        public string Path { get; set; }


        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
