namespace eor_web_app.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public int DirectionId { get; set; }
        public Direction Direction { get; set; }


        public List<Group> Groups { get; set; }
    }
}
