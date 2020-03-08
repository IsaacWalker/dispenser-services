using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Web.DispenserClient
{
    /// <summary>
    /// Client to the Dispenser/ Printer
    /// </summary>
    public sealed class DispenserClient : IDispenserClient
    {
        /// <summary>
        /// Http Client Factory
        /// </summary>
        private readonly IHttpClientFactory m_clientFactory;


        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration m_configuration;


        /// <summary>
        /// Base Uri
        /// </summary>
        private readonly Uri m_baseUri;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="configuration"></param>
        public DispenserClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            m_clientFactory = httpClientFactory;
            m_configuration = configuration;
            m_baseUri = new Uri(configuration.GetValue<string>("DispenserClient:BaseUrl"));
        }


        /// <summary>
        /// Gets the status of the printer
        /// </summary>
        /// <returns></returns>
        public async Task<GetPrinterStatusResponse> GetPrinterStatusAsync()
        {
            Uri uri = new Uri(m_baseUri.AbsoluteUri + m_configuration.GetValue<string>("DispenserClient:GetPrinterStatusPath"));

            using(var client = m_clientFactory.CreateClient())
            {
                var response = await client.GetAsync(uri);

                if(response.IsSuccessStatusCode && 
                    Enum.TryParse(await response.Content.ReadAsStringAsync(), out PrinterStatus status))
                {
                    return new GetPrinterStatusResponse(status, true);
                }

                return new GetPrinterStatusResponse(default, false);
            }
        }


        /// <summary>
        /// Intializes the print job
        /// </summary>
        /// <param name="JobId"></param>
        /// <param name="ODFs"></param>
        /// <returns></returns>
        public async Task<InitializePrintJobResponse> InitializePrintJobAsync(Guid JobId, IList<ODFTablet> ODFs)
        {
            UriBuilder uri = new UriBuilder(m_baseUri.AbsoluteUri + m_configuration.GetValue<string>("DispenserClient:InitializePrintJobPath"))
            {
                Query = string.Format("{0}={1}", nameof(JobId), JobId)
            };

            using (var client = m_clientFactory.CreateClient())
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(ODFs));
                var response = await client.PutAsync(uri.Uri, httpContent);

                if(response.IsSuccessStatusCode
                    && DateTime.TryParse(await response.Content.ReadAsStringAsync(), out DateTime eta))
                {
                    return new InitializePrintJobResponse(eta, true);
                }

                return new InitializePrintJobResponse(default, false);
            }

        }


        /// <summary>
        /// Starts the specified print jpob
        /// </summary>
        /// <param name="JobId"></param>
        /// <returns></returns>
        public async Task<StartPrintJobResponse> StartPrintJobAsync(Guid jobId)
        {
            UriBuilder uri = new UriBuilder(m_baseUri.AbsoluteUri + m_configuration.GetValue<string>("DispenserClient:StartPrintJobPath"))
            {
                Query = string.Format("{0}={1}", nameof(jobId), jobId)
            };

            using (HttpClient client = m_clientFactory.CreateClient())
            {
                var response = await client.GetAsync(uri.Uri);
                return new StartPrintJobResponse(response.IsSuccessStatusCode);
            }
        }


        /// <summary>
        /// Unlocks the dispenser
        /// </summary>
        /// <returns></returns>
        public async Task<UnlockDispenserResponse> UnlockDispenserAsync()
        {
            Uri uri = new Uri(m_baseUri.AbsoluteUri + m_configuration.GetValue<string>("DispenserClient:UnlockDispenserPath"));

            using(var client = m_clientFactory.CreateClient())
            {
                var response = await client.GetAsync(uri);
                return new UnlockDispenserResponse(response.IsSuccessStatusCode);
            }
        }
    }
}
