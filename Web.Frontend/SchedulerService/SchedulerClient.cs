using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Web.Models;
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


        public async Task<bool> ConfirmAdministration(Guid nurseId, Guid odfId, DateTime administrationTime)
        {
            Uri pathUri = new Uri(m_baseUri.AbsoluteUri + m_configuration.GetValue<string>("Settings:SchedulerConfirmAdminPath"));
            
            using(var client = m_httpClientFactory.CreateClient())
            {
                var model = new ConfirmAdministrationModel()
                {
                    NurseId = nurseId,
                    OdfId = odfId,
                    AdministrationTime = administrationTime
                };

                HttpContent content = new StringContent(JsonConvert.SerializeObject(model),Encoding.Default, "application/json");
                
                var result = await client.PostAsync(pathUri,content);

                return result.IsSuccessStatusCode;
            }
        }


        /// <summary>
        /// Gets the administation model
        /// </summary>
        /// <param name="nurseId"></param>
        /// <param name="odfId"></param>
        /// <returns></returns>
        public async Task<AdministrationVerificationModel> GetAdministrationModel(Guid nurseId, Guid odfId)
        {
            Uri pathUri = new Uri(m_baseUri.AbsoluteUri + m_configuration.GetValue<string>("Settings:AdministrationPath"));
            UriBuilder builder = new UriBuilder(pathUri)
            {
                Query = string.Format("{0}={1}&{2}={3}", nameof(nurseId), nurseId, nameof(odfId), odfId)
            };

            using(var client = m_httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync(builder.Uri);

                if(response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<AdministrationVerificationModel>(await response.Content.ReadAsStringAsync());
                }
            }

            return default;
        }


        /// <summary>
        /// Gets the drug info model
        /// </summary>
        /// <param name="prescriptionId"></param>
        /// <returns></returns>
        public async Task<DrugInformationPageModel> GetDrugInfoModel(Guid nurseId, Guid prescriptionId)
        {
            Uri pathUri = new Uri(m_baseUri.AbsoluteUri + m_configuration.GetValue<string>("Settings:SchedulerDrugPath"));
            UriBuilder uriBuilder = new UriBuilder(pathUri)
            {
                Query = string.Format("{0}={1}&{2}={3}", nameof(prescriptionId), prescriptionId, nameof(nurseId), nurseId)
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
        /// Gets the navbar model
        /// </summary>
        /// <param name="nurseId"></param>
        /// <returns></returns>
        public async Task<NavbarPartialModel> GetNavbarModel()
        {
            Uri pathUri = new Uri(m_baseUri.AbsoluteUri + m_configuration.GetValue<string>("Settings:SchedulerNavbarPath"));

            using(var client = m_httpClientFactory.CreateClient())
            {
                var response = await client.GetAsync(pathUri);

                return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<NavbarPartialModel>(
                    await response.Content.ReadAsStringAsync())
                     : default;
            }
        }


        /// <summary>
        /// Gets the model for the patient info screen
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public async Task<PatientInformationPageModel> GetPatientInfoModel(Guid nurseId, Guid patientId)
        {
            Uri pathUri = new Uri(m_baseUri.AbsoluteUri + m_configuration.GetValue<string>("Settings:SchedulerPatientPath"));
            UriBuilder uriBuilder = new UriBuilder(pathUri)
            {
                Query = string.Format("{0}={1}&{2}={3}", nameof(patientId), patientId,nameof(nurseId), nurseId)
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
