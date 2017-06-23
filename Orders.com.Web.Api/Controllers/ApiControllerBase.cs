using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Diagnostics.Contracts;
using System.Collections.Generic;
using Peasy;
using Peasy.Exception;

namespace Orders.com.Web.Api
{
    public abstract class ApiControllerBase<T, TKey> : ApiController where T : IDomainObject<TKey>, new()
    {
        protected IService<T, TKey> _businessService;

        // GET api/contracts
        public virtual HttpResponseMessage Get()
        {
            try
            {
                var result = _businessService.GetAllCommand().Execute();

                if (result.Success)
                {
                    var results = result.Value;
                    // http://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html#sec9.3
                    return Request.CreateResponse(HttpStatusCode.OK, results);
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join("\n", result.Errors));
            }
            catch (NotImplementedException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotImplemented, ex.Message);
            }
        }

        // GET api/contracts/5
        public virtual HttpResponseMessage Get(TKey id)
        {
            try
            {
                // http://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html#sec9.3
                var result = _businessService.GetByIDCommand(id).Execute();
                if (result.Success)
                {
                    if (result.Value == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, BuildNotFoundHttpError(id));
                    }
                    var response = Request.CreateResponse(HttpStatusCode.OK, result.Value);
                    return response;
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join("\n", result.Errors));
            }
            catch (DomainObjectNotFoundException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
            catch (NotImplementedException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotImplemented, ex.Message);
            }
        }

        // POST api/contracts
        public virtual HttpResponseMessage Post(T value)
        {
            if (value == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The request payload could not be parsed");

            try
            {
                var result = _businessService.InsertCommand(value).Execute();

                if (result.Success)
                {
                    T newEntity = result.Value;
                    // http://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html#sec9.5
                    var responseMessage = Request.CreateResponse(HttpStatusCode.Created, newEntity);
                    responseMessage.Headers.Location = new Uri(BuildNewResourceUriString(newEntity.ID));
                    return responseMessage;
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.ClearFirst().ThenAddRange(result.Errors));
            }
            catch (NotImplementedException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotImplemented, ex.Message);
            }
        }

        // PUT api/contracts/5
        public virtual HttpResponseMessage Put(TKey id, T value)
        {
            if (value == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The request payload could not be parsed");

            try
            {
                var result = _businessService.UpdateCommand(value).Execute();

                if (result.Success)
                {
                    value.ID = id;
                    T updatedEntity = result.Value;
                    // http://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html#sec9.6
                    var response = Request.CreateResponse(HttpStatusCode.OK, updatedEntity);
                    return response;
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState.ClearFirst().ThenAddRange(result.Errors));
            }
            catch (DomainObjectNotFoundException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
            catch (ConcurrencyException)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "A concurrency issue occurred.  Try a GET on the resource and attempt the PUT again.");
            }
            catch (NotImplementedException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotImplemented, ex.Message);
            }
        }

        // DELETE api/contracts/5
        public virtual HttpResponseMessage Delete(TKey id)
        {
            try
            {
                if (_businessService.GetByIDCommand(id).Execute().Value == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, BuildNotFoundHttpError(id));

                var result = _businessService.DeleteCommand(id).Execute();

                if (result.Success)
                {
                    // http://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html#sec9.7
                    var responseMessage = Request.CreateResponse(HttpStatusCode.NoContent);
                    return responseMessage;
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join("\n", result.Errors));
            }
            catch (DomainObjectNotFoundException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
            catch (ConcurrencyException)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, "A concurrency issue occurred.  Try a GET on the resource and attempt the DELETE again.");
            }
            catch (NotImplementedException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotImplemented, ex.Message);
            }
        }

        protected virtual string BuildNewResourceUriString(TKey id)
        {
            return BuildNewResourceUriString(id, Constants.ROUTE_NAME_DEFAULT);
        }

        protected virtual string BuildNewResourceUriString(TKey id, string routeName)
        {
            var link = Url.Link(routeName, new { id = id });
            return link;
        }

        private static string BuildNotFoundString(string className, TKey id)
        {
            return string.Format("{0} ID {1} could not be found.", className, id.ToString());
        }

        private static HttpResponseMessage BuildNotFoundResponseMessage(string className, TKey id)
        {
            var message = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(BuildNotFoundString(className, id)),
                ReasonPhrase = string.Format("{0} not found.", className),
            };
            return message;
        }

        protected static HttpError BuildNotFoundHttpError(TKey id)
        {
            var className = BuildDomainClassName();
            string errorMessage = BuildNotFoundString(className, id);
            return new HttpError(errorMessage);
        }

        private static string BuildDomainClassName()
        {
            var domainObject = new T();
            return domainObject.ClassName();
        }
    }
}