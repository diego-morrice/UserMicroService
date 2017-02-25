using System.Net.Http;
using System.Web.Http;
using Application.User.Interfaces;
using Application.User.Messages;
using Infrastructure.CrossCutting.Tools.Extensions;

namespace WebApi.User.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : ApiController
    {
        private readonly IUserAppService _userAppService;

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [Route()]
        [HttpPost]
        public IHttpActionResult SignIn(HttpRequestMessage request, [FromBody] SignInMessage sign)
        {

            string token;
            var result = _userAppService.SignIn(sign, out token);

            if (result.IsValid) return Ok($"/users/{token}");

            return this.BadRequest(result);
        }

        [Route]
        [HttpGet]
        public IHttpActionResult GetUser(HttpRequestMessage request, [FromUri] string token)
        {
            UserMessage user = null;
            var result = _userAppService.GetByToken(token, out user);

            if (result.IsValid)  return Ok(user);

            return this.BadRequest(result);
        }
    }
}
