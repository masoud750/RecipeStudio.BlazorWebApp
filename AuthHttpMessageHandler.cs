public class AuthHttpMessageHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            return await base.SendAsync(request, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            // This is normal when the user navigates or the circuit refreshes.
            return new HttpResponseMessage(System.Net.HttpStatusCode.RequestTimeout);
        }
    }

}

