using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.EntityData;

namespace Web.SchedulerService
{
    /// <summary>
    /// Generates mock Data
    /// </summary>
    public static class MockData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ServiceDbContext>();

                //TODO - Add Patients
                Patient p1 = new Patient()
                {
                   
                };

                context.SaveChanges();

                //TODO - Add prescriptions 
                Prescription pre1 = new Prescription();
                {

                };

                context.SaveChanges();

                //TODO - Assign prescriptions to Patients
                p1.Prescriptions.Add(pre1);

                context.SaveChanges();

            }
        }
    }
}
