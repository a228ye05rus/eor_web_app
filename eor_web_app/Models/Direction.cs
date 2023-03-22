namespace eor_web_app.Models
{
    public class Direction
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }


        public List<Profile> Profiles { get; set; } /*= new List<Profile>();*/
    }
}
