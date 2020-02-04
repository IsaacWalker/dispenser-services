using System;


using System.Threading.Tasks;

namespace Web.DispenserClient
{
    public interface IDispenserClient
    {
        Task<PrintMedicationResponse> PrintMedication(PrintMedicationRequest request);
    }

}
