namespace Chaos.Portal.Core.Request
{
    public class RequestDelegate
    {
        public delegate void PortalRequestHandler(object sender, PortalRequestArgs args);

        public class PortalRequestArgs
        {
            public IPortalRequest Request { get; set; }

            public PortalRequestArgs(IPortalRequest request)
            {
                Request = request;
            }
        } 
    }
}