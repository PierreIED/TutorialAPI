using Tutorials.Model;

namespace Tutorials.Services
{
	public interface IAuthenticationService
    {
        string GenerateToken(Member user);
    }
}
