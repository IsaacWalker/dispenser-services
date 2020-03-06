using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Models.ViewModels;

namespace Web.Frontend.SchedulerService
{
    /// <summary>
    /// Client for the scheduler service
    /// </summary>
    public class SchedulerClient : ISchedulerClient
    {
        /// <summary>
        /// Http client factory
        /// </summary>
        private readonly IHttpClientFactory m_httpClientFactory;


        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration m_configuration;


        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger m_logger;


        /// <summary>
        /// Base Uri for the scheduler service
        /// </summary>
        private readonly Uri m_baseUri;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public SchedulerClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<SchedulerClient> logger)
        {
            m_httpClientFactory = httpClientFactory;
            m_configuration = configuration;
            m_logger = logger;
            m_baseUri = new Uri(m_configuration.GetValue<string>("Settings:SchedulerServiceBaseUriProd"));
        }


        /// <summary>
        /// Gets the drug info model
        /// </summary>
        /// <param name="prescriptionId"></param>
        /// <returns></returns>
        public async Task<DrugInformationPageModel> GetDrugInfoModel(Guid prescriptionId)
        {
            Uri pathUri = new Uri(m_baseUri.AbsoluteUri + m_configuration.GetValue<string>("Settings:SchedulerDrugPath"));
            UriBuilder uriBuilder = new UriBuilder(pathUri)
            {
                Query = string.Format("{0}={1}", nameof(prescriptionId), prescriptionId)
            };

            using (var client = m_httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync(uriBuilder.Uri);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var responseString = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<DrugInformationPageModel>(responseString);

                return model;
            }
        }


        /// <summary>
        /// Gets the model for the Home Page
        /// </summary>
        /// <param name="nurseId"></param>
        /// <returns></returns>
        public async Task<HomePageModel> GetHomePageModel(Guid nurseId)
        {
            Uri pathUri = new Uri(m_baseUri.AbsoluteUri + m_configuration.GetValue<string>("Settings:SchedulerHomePath"));
            UriBuilder uriBuilder = new UriBuilder(pathUri)
            {
                Query = string.Format("{0}={1}", nameof(nurseId), nurseId)
            };

            using (var client = m_httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync(uriBuilder.Uri);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var responseString = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<HomePageModel>(responseString);

                return model;
            }
        }


        /// <summary>
        /// Gets the model for the patient info screen
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public async Task<PatientInformationPageModel> GetPatientInfoModel(Guid patientInfo)
        {
            Uri pathUri = new Uri(m_baseUri.AbsoluteUri + m_configuration.GetValue<string>("Settings:SchedulerPatientPath"));
            UriBuilder uriBuilder = new UriBuilder(pathUri)
            {
                Query = string.Format("{0}={1}", nameof(patientInfo), patientInfo)
            };

            using(var client = m_httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync(uriBuilder.Uri);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var responseString = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<PatientInformationPageModel>(responseString);

                return model;
            }
        }
    }
}
