namespace Tutorials.Model
{
    public interface IApiDbContext
	{
		List<Member> Members { get; }

		void SaveChanges();
	}
}