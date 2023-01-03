using System.Net.Http.Headers;

namespace proxy_app.Controllers
{
    public class ProxyHandler
    {
        public static string QUESTION_MARK = "?";
        public static string WHITE_SPACE = " ";
        public static string WHITE_SPACE_ENCODED = "%20";

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
            GetInfo getInfo = new GetInfo();
            string pathInfo = request.Path;
            var requestQuery = request.QueryString;
            var queryString = requestQuery != null ?
                                      String.Concat(QUESTION_MARK, requestQuery.Value)
                                      : String.Empty;

            var finalUrl = String.Concat(pathInfo, queryString)
                              .Replace(WHITE_SPACE, WHITE_SPACE_ENCODED);

            getInfo.URLToRequest = finalUrl;
            return getInfo;
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
        public string BACK_END_URL => "http://probando.com";
    }

    public class ClientService
    {
        private ClientInfo clientInfo;

        HttpClient client;

        public ClientService(AppConfigurationInfo appConfigInfo)
        {

            this.client = new HttpClient();
            this.clientInfo = this.setUpClient(appConfigInfo);
        }

        private ClientInfo setUpClient(AppConfigurationInfo appConfigInfo)
        {
            //take the appconfguratioInfo and sets up the client
            var myBackEnd = new Uri(appConfigInfo.BACK_END_URL);
            var host = myBackEnd.Host;
            var baseURL = myBackEnd.GetLeftPart(UriPartial.Authority);
            var protocol = baseURL.Contains("https") ? "HTTPS" : "HTTP";
            var splitted = baseURL.Split(':');
            var port = splitted.Length == 3 ? splitted[3] : "";

            return new ClientInfo()
            {
                BACK_END_URL = appConfigInfo.BACK_END_URL,
                BackEndHost = host,
                BackEndPort = port,
                BackEndProtocol = protocol
            };

        }

        public async Task<string> MakeGet(GetInfo getInfo)
        {
            string completeURL = String.Concat(clientInfo.BACK_END_URL, getInfo.URLToRequest);
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, completeURL))
            {
                requestMessage.Headers.Add("Content-Type", "application/json;charset=utf-8");


                var received_response = await client.SendAsync(requestMessage);

                return await received_response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> MakePost(PostInfo postInfo)
        {
            throw new NotImplementedException();
        }
    }

    public class ClientInfo
    {
        public string? BackEndHost { get; set; }
        public string? BackEndPort { get; set; }
        public string? BackEndProtocol { get; set; }
        public string? BACK_END_URL { get; internal set; }
    }

    public class PostInfo
    {
        public string URLToRequest { get; set; }
        public HttpContent? Content { get; internal set; }
    }

    public class GetInfo
    {
        public string URLToRequest { get; set; }
    }
}