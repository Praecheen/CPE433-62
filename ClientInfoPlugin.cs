using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
    class ClientInfoPlugin : IPlugin
    {
        protected static Dictionary<String, int> statDictionary = null;
        public ClientInfoPlugin()
        {
            if (statDictionary == null)
            {
                statDictionary = new Dictionary<String, int>();

            }
        }

        public void PreProcessing(HTTPRequest request)
        {
            if (statDictionary.ContainsKey(request.Url))
            {
                statDictionary[request.Url] = (int)statDictionary[request.Url] + 1;
            }
            else
            {
                statDictionary[request.Url] = 1;
            }
        }
        public HTTPResponse GetResponse(HTTPRequest request)
        {
            StringBuilder sb = new StringBuilder();
            String keep = request.getPropertyByKey("remoteendpoint");
            string[] IP = keep.Split(':');
            String support = request.getPropertyByKey("user-agent");
            String language = request.getPropertyByKey("accept-language");
            String encoding = request.getPropertyByKey("accept-encoding");
            sb.Append("<html><body><h1>Client:</h1>");
            sb.Append("Client IP : " + IP[0]);
            sb.Append("<br>Client Port: " + IP[1]);
            sb.Append("<br>Browser Information: " + support);
            sb.Append("<br>Accept Language: " + language);
            sb.Append("<br>Accept Encoding: " + encoding);
            sb.Append("</body></html>");
            HTTPResponse response = null;
            Console.WriteLine("data");
            sb.Append("</body></html>");
            response = new HTTPResponse(200);
            response.body = Encoding.UTF8.GetBytes(sb.ToString());
            return response;
        }

        public HTTPResponse PostProcessing(HTTPResponse response)
        {
            throw new NotImplementedException();
        }
    }
}