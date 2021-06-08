using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System;
using DnsClient;
using DnsClient.Protocol;

namespace web_client
{
    public class Client
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<string> GetService1Response()
        {
            var serviceEndpoint = getService1Endpoint();
            if (serviceEndpoint != null)
            {
                var lookup = new LookupClient();
                var result = await lookup.QueryAsync(serviceEndpoint, QueryType.SRV);

                var record =  result.Answers.OfType<SrvRecord>().FirstOrDefault();
                var address = record?.Target;
                var port = record?.Port;
                string uri = String.Format("http://{0}:{1}", address, port);
                Console.WriteLine(uri);
                return await getData(uri);
            }
            else
            {
                return "No endpoint";
            }
        }

        public async Task<string> GetService2Response()
        {
            var serviceEndpoint = getService2Endpoint();
            if (serviceEndpoint != null)
            {
                var lookup = new LookupClient();
                var result = await lookup.QueryAsync(serviceEndpoint, QueryType.SRV);

                var record =  result.Answers.OfType<SrvRecord>().FirstOrDefault();
                var address = record?.Target;
                var port = record?.Port;
                string uri = String.Format("http://{0}:{1}", address, port);
                Console.WriteLine(uri);
                return await getData(uri);
            }
            else
            {
                return "No endpoint";
            }
        }

        private string getService1Endpoint()
        {
            var service1Endpoint = Environment.GetEnvironmentVariable("service_1_endpoint");
            return service1Endpoint;
        }

        private string getService2Endpoint()
        {
            var service2Endpoint = Environment.GetEnvironmentVariable("service_2_endpoint");
            return service2Endpoint;
        }

        private async Task<string> getData(string endpoint)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                Console.WriteLine(responseBody);
                return responseBody;
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ",e.ToString());
                return null;
            }
        }
    }
}

