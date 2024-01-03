using System.Collections.Generic;
using Kryptos.Hrms.API.Models; // Replace with your actual model namespace




namespace Kryptos.Hrms.API.Repositories
{
    public interface IAcademicQualificationRepository
    {
        void Add(AcademicQualification academicQualification);
        void SaveChanges();
    }
}
