/*
Todo: 
- Consider allowing real HTTP requests in case users want to test against a real server
*/

namespace HostFixture.Http;


public class HttpActionHandler(
    IEnumerable<IHttpActionBuilder> actionBuilders
) : DelegatingHandler
{

    IEnumerable<IHttpActionBuilder> ActionBuilders { get; } = actionBuilders;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var targetBuilders
            = ActionBuilders.Where(b => b.Matches(request))
            .ToList(); 

        if(targetBuilders.Count() == 0)
            throw new InvalidOperationException("No HTTP action builders matched the request");
        
        if(targetBuilders.Count() > 1)
            throw new InvalidOperationException("Multiple HTTP action builders matched the request");

        IHttpActionBuilder builder = targetBuilders.First();

        return await builder.BuildResponseAsync(); 
    }
}