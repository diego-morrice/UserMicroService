using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Infrastructure.CrossCutting.Tools.Web.ActionResult
{
    public class BadRequestWithGenericContent<T> : IHttpActionResult
    {
        private readonly NegotiatedContentResult<T> _negotiatedContentResult;

        public BadRequestWithGenericContent(T content, ApiController controller)
            : this(content, new NegotiatedContentResult<T>(HttpStatusCode.BadRequest, content, controller))
        {
        }

        private BadRequestWithGenericContent(T content, NegotiatedContentResult<T> negotiatedContentResult)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            _negotiatedContentResult = negotiatedContentResult;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return _negotiatedContentResult.ExecuteAsync(cancellationToken);
        }
    }
}
