using Kryptos.Hrms.API.Models;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Providers
{
    public class MedicalDatumProvider : IMedicalDatumProvider
    {
        private readonly IMedicalDatumRepository _repository;

        public MedicalDatumProvider(IMedicalDatumRepository repository)
        {
            _repository = repository;
        }

        public List<MedicalDatum> GetAllMedicalDataForEmployee(int employeeId)
        {
            return _repository.GetAllMedicalDataForEmployee(employeeId);
        }

        public void AddMedicalData(MedicalDatum medicalData)
        {
            _repository.AddMedicalData(medicalData);
        }

        public void UpdateMedicalData(MedicalDatum medicalData)
        {
            _repository.UpdateMedicalData(medicalData);
        }

        public void DeleteMedicalData(int medicalDataId)
        {
            _repository.DeleteMedicalData(medicalDataId);
        }
    }
}
