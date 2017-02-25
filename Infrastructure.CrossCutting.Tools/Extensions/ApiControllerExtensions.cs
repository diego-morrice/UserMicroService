using System.Web.Http;
using Infrastructure.CrossCutting.Tools.Web.ActionResult;

namespace Infrastructure.CrossCutting.Tools.Extensions
{
    public static class ApiControllerExtensions
    {
        public static BadRequestWithGenericContent<T> BadRequest<T>(this ApiController controller, T content)
        {
            return new BadRequestWithGenericContent<T>(content, controller);
        }
    }
}
