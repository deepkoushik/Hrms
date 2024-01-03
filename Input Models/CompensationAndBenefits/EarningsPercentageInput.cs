namespace Kryptos.Hrms.API.Input_Models.CompensationAndBenefits
{
    public class EarningsPercentageInput
    {
        public int Id { get; set; }

        public double? BasicEarnings { get; set; }

        public double? HraEarnings { get; set; }

        public double? ConveyanceEarnings { get; set; }

        public double? MedicalEarnings { get; set; }

        public double? SpecialAllowance { get; set; }

        public double? ProvidentFundDeduction { get; set; }

        public double? Mediclaim { get; set; }

        public int UpdatedById { get; set; }

        public DateTime? UpdateTime { get; set; }

    }
}
