namespace Kryptos.Hrms.API.Repository.WorkforceManagementRepository
{
    public class CustomException : Exception
    {
        public CustomException()
        {

        }
        public CustomException(string message)
            : base(message)
        {
        }
        public CustomException(string message, Exception innerException)
        : base(message, innerException)
        {
        }
    }
}
