using eor_web_app.Models;

namespace eor_web_app.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Profile> Profiles { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
        public IEnumerable<FileModel> FileModels { get; set; }
    }
}
