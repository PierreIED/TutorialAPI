using System.ComponentModel.DataAnnotations;

namespace Tutorials.Model
{
    public class Member
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public int Age { get; set; }
    }
}
