using Tutorials.Model;
using Tutorials.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tutorials.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class MembersController : ControllerBase
	{
		private IApiDbContext DbContext
		{
			get;
		}

		private IAuthenticationService AuthenticationService
		{
			get;
		}

		public MembersController(
			IApiDbContext dbContext,
			IAuthenticationService authenticationService)
		{
			DbContext = dbContext;
			AuthenticationService = authenticationService;
		}

		[HttpPost("login")]
		public IActionResult Login(Member member)
		{
			Member? existingMember = DbContext.Members.FirstOrDefault(
				u => u.Username == member.Username &&
				u.Password == member.Password);

			if (existingMember == null)
				return new UnauthorizedResult();

			string accessToken = AuthenticationService.GenerateToken(existingMember);
			return new ObjectResult(new
			{
				existingMember.Id,
				existingMember.Username,
				access_token = accessToken
			});
		}

		[HttpGet("{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public IActionResult Get(int id)
		{
			Member? member = DbContext.Members.FirstOrDefault(u => u.Id == id);
			if (member == null)
				return new UnauthorizedResult();

			return new ObjectResult(new
			{
				member.Id,
				member.Username,
				member.Firstname,
				member.Lastname,
				member.Age
			});
		}

		[HttpPost]
		public IActionResult Post(Member member)
		{
            // Valider l’unicité du nom d’utilisateur soumis
            if (DbContext.Members.FirstOrDefault(u => u.Username == member.Username) != null)
				return BadRequest(new { error = "Username already in use" });
			// Auto incrémenter Id
			member.Id = (DbContext.Members.Count == 0) ? 1 : DbContext.Members.Last().Id + 1 ;
			DbContext.Members.Add(member);
			DbContext.SaveChanges();

			string accessToken = AuthenticationService.GenerateToken(member);
			return new ObjectResult(new
			{
				member.Id,
				member.Username,
				access_token = accessToken
			});
		}
	}
}