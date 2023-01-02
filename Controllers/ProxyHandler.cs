namespace proxy_app.Controllers
{
    public class ProxyHandler
    {
        private readonly ClientService service;

        public ProxyHandler(ClientService service)
        {
            this.service = service;
        }
        public async Task<string> solveGet(HttpRequest request)
        {
            GetInfo getInfo = this.GetInfoCollector(request);
            string response = await service.MakeGet(getInfo);
            return response;
        }

        private GetInfo GetInfoCollector(HttpRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<string> solvePost(HttpRequest request)
        {
            PostInfo postInfo = this.PostInfoCollector(request);
            string response = await service.MakePost(postInfo);
            return response;
        }

        private PostInfo PostInfoCollector(HttpRequest request)
        {
            throw new NotImplementedException();
        }
    }

    public class AppConfigurationInfo
    {
        
    }

    public class ClientService
    {
        private readonly AppConfigurationInfo info;

        HttpClient client;

        public ClientService(AppConfigurationInfo info)
        {
            this.info = info;
            this.client = new HttpClient();
            this.setUpClient();
        }

        private void setUpClient()
        {
            //take the appconfguratioInfo and sets up the client
        }

        public async Task<string> MakeGet(GetInfo getInfo)
        {
            client.BaseAddress= new Uri(getInfo.SourceUrl);
            Uri destination = new Uri(getInfo.DestinationUrl);
            var response= await client.GetStringAsync(destination);
            return response;
        }

        public async Task<string> MakePost(PostInfo postInfo)
        {
            client.BaseAddress= new Uri(postInfo.SourceUrl);
            Uri destination = new Uri(postInfo.DestinationUrl);
            var response= await client.PostAsync(destination,postInfo.Content);
            return response.Content!=null?response.Content.ToString():"";
        }
    }

    public interface HttpBasicInfo
    {
        public string? SourceUrl { get; set; }
        public string? SourcePort { get; set; }
        public string? DestinationUrl {get;set;}
        public string? DestinationPort { get; set; }

    }

    public class PostInfo : HttpBasicInfo
    {
        public string? SourceUrl {get;set;}
        public string? SourcePort {get;set;}
        public string? DestinationUrl {get;set;}
        public string? DestinationPort {get;set;}
        public HttpContent? Content { get; internal set; }
    }

    public class GetInfo: HttpBasicInfo
    {
        public string? SourceUrl {get;set;}
        public string? SourcePort {get;set;}
        public string? DestinationUrl {get;set;}
        public string? DestinationPort {get;set;}
    }
}