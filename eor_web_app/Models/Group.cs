namespace eor_web_app.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public int ProfileId { get; set; }
        public Profile Profile { get; set; }


        public List<Subject> Subjects { get; set; }
    }
}
