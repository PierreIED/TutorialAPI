using System.Text.Json;

namespace Tutorials.Model
{
    public class ApiDbContext : IApiDbContext
    {
        public List<Member> Members { get; }

        const string __fileName = "./Datastore/members.json";

        public ApiDbContext()
        {
            Members = JsonSerializer.Deserialize<List<Member>>(
               File.ReadAllText(__fileName))!;
        }

        public void SaveChanges()
        {
            File.WriteAllText(__fileName, JsonSerializer.Serialize(Members));
        }
    }
}
