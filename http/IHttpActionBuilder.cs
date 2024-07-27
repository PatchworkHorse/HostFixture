using System.Net;

namespace HostFixture.Configuration; 

public interface IHttpActionBuilder 
{
    /// <summary>
    /// Sets a filter to determine if the request URI matches the criteria for this action
    /// </summary>
    /// <param name="filter">The filter action to evaluate</param>
    /// <returns>This builder instance</returns>
    public IHttpActionBuilder RequestUriFilter(Func<Uri, bool> filter);

    /// <summary>
    /// Sets a filter to determine if the request method matches the criteria for this action
    /// </summary>
    /// <param name="filter">The filter action to evaluate</param>
    /// <returns>This builder instance</returns>
    public IHttpActionBuilder RequestMethodFilter(Func<HttpMethod, bool> filter);

    /// <summary>
    /// Sets the response code to be returned in the HttpResponseMessage
    /// </summary>
    /// <param name="code">The status code to return </param>
    /// <returns>This builder instance</returns>
    public IHttpActionBuilder SetResponseCode(HttpStatusCode code);

    /// <summary>
    /// Define the content to be returned in the HttpResponseMessage
    /// </summary>
    /// <param name="content">The HttpContent to return</param>
    /// <returns>This builder instance</returns>
    public IHttpActionBuilder SetResponseContent(HttpContent content);

    /// <summary>
    /// Adds a callback to be executed when the request is received
    /// </summary>
    /// <param name="callback">The action to execute</param>
    /// <returns>This builder instance</returns>
    /// <remarks>Any number of callbacks can be added</remarks>
    public IHttpActionBuilder AddRequestCallback(Action<HttpRequestMessage> callback);

    /// <summary>
    /// Adds a callback to be executed when the response is generated
    /// </summary>
    /// <param name="callback">The action to execute</param>
    /// <returns>This builder instance</returns>
    /// <remarks>Any number of callbacks can be added</remarks>
    public IHttpActionBuilder AddResponseCallback(Action<HttpResponseMessage> callback);


    /// <summary>
    /// Determines if the request matches the criteria for this action
    /// </summary>
    /// <param name="request">The request to evaluate</param>
    /// <returns>True if filter criteria are met, otherwise false.</returns>
    public bool Matches (HttpRequestMessage request);
}