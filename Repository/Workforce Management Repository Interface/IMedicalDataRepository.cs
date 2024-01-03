using Kryptos.Hrms.API.Models;
using System.Collections.Generic;

public interface IMedicalDatumRepository
{
    List<MedicalDatum> GetAllMedicalDataForEmployee(int employeeId);
    void AddMedicalData(MedicalDatum medicalData);
    void UpdateMedicalData(MedicalDatum medicalData);
    void DeleteMedicalData(int medicalDataId);
}
