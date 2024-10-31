using System.Net;

namespace HostFixture.Http;

public class HttpActionBuilder : IHttpActionBuilder
{
    IList<HttpRequestCallback> RequestCallbacks { get; } = new List<HttpRequestCallback>();

    IList<HttpResponseCallback> ResponseCallbacks { get; } = new List<HttpResponseCallback>();

    IList<Func<Uri, bool>> RequestUriPredicates = new List<Func<Uri, bool>>();

    Func<HttpMethod, bool>? RequestMethodPredicate;

    HttpStatusCode ResponseCode { get; set; }

    HttpContent? ResponseContent { get; set; }


    public IHttpActionBuilder AddRequestCallback(Action<HttpRequestMessage> callback)
    {
        RequestCallbacks.Add(HttpRequestCallback.Create(callback)); 
        return this;
    }

    public IHttpActionBuilder AddResponseCallback(Action<HttpResponseMessage> callback)
    {
        ResponseCallbacks.Add(HttpResponseCallback.Create(callback));
        return this;
    }

    public bool Matches(HttpRequestMessage request)
    {
        if(request.RequestUri == null)
            return false;

        // Never match on zero predicates. If a user wants to match on all requests they can create create 
        // a predicate that always returns true i.e., _ => true
        if(RequestUriPredicates.Count() == 0)
            return false; 

        if(RequestUriPredicates.Any(predicate => predicate(request.RequestUri) == false))
            return false;

        if(RequestMethodPredicate != null && RequestMethodPredicate(request.Method) == false)
            return false;
        
        return true;
    }

    public IHttpActionBuilder WithRequestMethodFilter(Func<HttpMethod, bool> filter)
    {
        RequestMethodPredicate = filter;
        return this;
    }

    public IHttpActionBuilder WithRequestUriFilter(Func<Uri, bool> filter)
    {
        RequestUriPredicates.Add(filter);
        return this;
    }

    public IHttpActionBuilder SetResponseCode(HttpStatusCode code)
    {
        ResponseCode = code;
        return this;
    }

    public IHttpActionBuilder SetResponseContent(HttpContent content)
    {
        ResponseContent = content;
        return this;
    }

    public Task<HttpResponseMessage> BuildResponseAsync()
    {
        var response = new HttpResponseMessage(ResponseCode);
        response.Content = ResponseContent;
        return Task.FromResult(response);
    }
}