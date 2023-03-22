namespace eor_web_app.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }


        public int GroupId { get; set; }
        public Group Group { get; set; }


        public FileModel FileModel { get; set; } /*= new FileModel();*/
    }
}
