namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    // Model for the document Links-
    public class DocumentLinks
    {
        public string IDOrPassport { get; set; }
        public string CV { get; set; }
        public string ProofOfAddress { get; set; }
        public string ProofOfTaxRegistration { get; set; }
        public string CompletedIRP5 { get; set; }
        public string ProofOfBank { get; set; }

        // Empty Constructor
        public DocumentLinks()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iDOrPassport">Link of the ID or Passport Doc</param>
        /// <param name="cV">Link of the CV Doc</param>
        /// <param name="proofOfAddress">Link of the Address Doc</param>
        /// <param name="proofOfTaxRegistration">Link of the tax reg Doc</param>
        /// <param name="completedIRP5">Link of the irp5 Doc</param>
        /// <param name="proofOfBank">Link of the bank Doc</param>
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
