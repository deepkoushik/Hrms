using Kryptos.Hrms.API.Models;

using System.Collections.Generic;



namespace Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface
{
    public interface IAcademicQualificationProvider
    {


        void AddAcademicQualification(AcademicQualification academicQualification);
        void SaveChanges();
    }
}
