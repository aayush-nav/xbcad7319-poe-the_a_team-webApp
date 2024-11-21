namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    public class DocumentLinks
    {
        public string IDOrPassport { get; set; }
        public string CV { get; set; }
        public string ProofOfAddress { get; set; }
        public string ProofOfTaxRegistration { get; set; }
        public string CompletedIRP5 { get; set; }
        public string ProofOfBank { get; set; }

        public DocumentLinks()
        {
        }

        public DocumentLinks(string iDOrPassport, string cV, string proofOfAddress, string proofOfTaxRegistration, string completedIRP5, string proofOfBank)
        {
            IDOrPassport = iDOrPassport;
            CV = cV;
            ProofOfAddress = proofOfAddress;
            ProofOfTaxRegistration = proofOfTaxRegistration;
            CompletedIRP5 = completedIRP5;
            ProofOfBank = proofOfBank;
        }
    }

}
