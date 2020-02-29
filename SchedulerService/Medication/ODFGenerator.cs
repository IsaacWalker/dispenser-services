using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.EntityData;

namespace Web.SchedulerService.Medication
{
    public class ODFGenerator : IODFGenerator
    {
        private readonly IConfiguration m_config;


        public ODFGenerator(IConfiguration config)
        {
            m_config = config;
        }


        public ODF Run(Prescription prescription)
        {
            float length = m_config.GetValue<float>("OdfSettings:Length");
            float width = m_config.GetValue<float>("OdfSettings:Width");
            float height = m_config.GetValue<float>("OdfSettings:Height");

            Patient patient = prescription.Patient;
            string label = CreateLabel(patient.FirstName, patient.LastName);
            float density = CalculateDensity(prescription.Dosage);
            Guid id = Guid.NewGuid();

            ODF odf = new ODF();
          /*  {
                Length = length,
                Width = width,
                Height = height,
                Label = label,
                DrugName = prescription.Drug,
                Density = density,
                Id = id.ToString()
            };*/

            return odf;
        }

        private string CreateLabel(string first_name, string last_name)
        {
            return first_name[0] + ". " + last_name;
        }


        private float CalculateDensity(float dosage)
        {
            return dosage;
        }
    }
}
