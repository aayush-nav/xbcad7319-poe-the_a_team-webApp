using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using XBCAD7319_SparkLine_HR_WebApp.Models;


namespace XBCAD7319_SparkLine_HR_WebApp.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly OnboardingService _onboardingService;
        private readonly FirebaseService _firebaseService;
        //private readonly OnboardingManager _onboardingManager;

        public EmployeeController()
        {
            _onboardingService = new OnboardingService();
            _firebaseService = new FirebaseService();
            //_onboardingManager = onboardingManager;
        }

        [HttpGet]
        public async Task<JsonResult> GetNextEmployeeId()
        {
            try
            {
                var nextEmployeeId = await _onboardingService.GetNextEmployeeIdAsync();
                return Json(new { success = true, employeeId = nextEmployeeId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Management()
        {
            return View();
        }


        public IActionResult Details()
        {
            return View();
        }


        [HttpPost]
        public JsonResult PersonalInformationCaptureAJAX(Employee model)
        {
            if (ModelState.IsValid)
            {
                OnboardingManager._employee = model;
                // Return a success response
                return Json(new { success = true });
            }

            // If validation fails, return an error message
            return Json(new { success = false, message = "There were validation errors." });
        }



        [HttpPost]
        public JsonResult JobInformationCaptureAJAX(JobDetails jobDetails)
        {
            if (ModelState.IsValid)
            {
                // Save the job details (this could be saving to a database, Firebase, etc.)
                OnboardingManager._jobDetails = jobDetails;

                // Return success response
                return Json(new { success = true });
            }

            // If validation fails, return an error message
            return Json(new { success = false, message = "There were validation errors." });
        }

        [HttpPost]
        public async Task<JsonResult> UploadDocumentsAJAX(List<IFormFile> IDOrPassport, List<IFormFile> CV, List<IFormFile> ProofOfAddress, List<IFormFile> ProofOfTaxRegistration, List<IFormFile> CompletedIRP5, List<IFormFile> ProofOfBank)
        {
            var documentFiles = new List<IFormFile>();
            if (IDOrPassport != null) documentFiles.AddRange(IDOrPassport);
            if (CV != null) documentFiles.AddRange(CV);
            if (ProofOfAddress != null) documentFiles.AddRange(ProofOfAddress);
            if (ProofOfTaxRegistration != null) documentFiles.AddRange(ProofOfTaxRegistration);
            if (CompletedIRP5 != null) documentFiles.AddRange(CompletedIRP5);
            if (ProofOfBank != null) documentFiles.AddRange(ProofOfBank);

            if (documentFiles == null || documentFiles.Count == 0 || documentFiles.Count < 6)
            {
                return Json(new { success = false, message = "Please select documents to upload." });
            }

            try
            {
                // Debug: Log the file names and sizes
                foreach (var documentFile in documentFiles)
                {
                    Console.WriteLine($"File: {documentFile.FileName}, Size: {documentFile.Length}");
                }

                DocumentLinks documentLinks = new DocumentLinks();
                var empID = await _onboardingService.GetNextEmployeeIdAsync();

                int count = 0;

                foreach (var documentFile in documentFiles)
                {

                    if (documentFile.Length > 0)
                    {

                        var filePath = $"employee-documents/{empID}_{documentFile.FileName}";

                        var firebaseStorage = new FirebaseStorage("hrappstorage.appspot.com").Child(filePath);

                        // Upload the file
                        using (var fileStream = documentFile.OpenReadStream())
                        {

                            string downloadUrl = await firebaseStorage.PutAsync(fileStream);
                            Console.WriteLine($"Uploaded to: {downloadUrl}");

                            // Map the file to the correct document type
                            if (count == 0)
                            {
                                documentLinks.IDOrPassport = downloadUrl;
                            }
                            else if (count == 1)
                            {
                                documentLinks.CV = downloadUrl;
                            }
                            else if (count == 2)
                            {
                                documentLinks.ProofOfAddress = downloadUrl;
                            }
                            else if (count == 3)
                            {
                                documentLinks.ProofOfTaxRegistration = downloadUrl;
                            }
                            else if (count == 4)
                            {
                                documentLinks.CompletedIRP5 = downloadUrl;
                            }
                            else if (count == 5)
                            {
                                documentLinks.ProofOfBank = downloadUrl;
                            }
                        }
                    }
                    count++;
                }

                OnboardingManager._documentLinks = documentLinks;

                return Json(new { success = true, message = "Documents uploaded successfully.", documentLinks });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading documents: {ex.Message}");
                return Json(new { success = false, message = "Failed to upload documents. Please try again." });
            }
        }


        [HttpPost]
        public JsonResult PayrollInformationCaptureAJAX(Payroll payroll)
        {
            if (ModelState.IsValid)
            {
                // Store the payroll information (simulated database save)
                OnboardingManager._payroll = payroll;

                // Return success response
                return Json(new { success = true, message = "Payroll information saved successfully." });
            }

            // If validation fails, return an error message with validation errors
            var errorMessages = ModelState.Values
                                           .SelectMany(v => v.Errors)
                                           .Select(e => e.ErrorMessage)
                                           .ToList();

            return Json(new { success = false, message = "Failed to save payroll information.", errors = errorMessages });
        }


        [HttpPost]
        public async Task<JsonResult> OnboardEmployee()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Generate Employee ID
                    var empID = await _onboardingService.GetNextEmployeeIdAsync();
                    // Create the Employee Details ViewModel
                    EmployeeDetailsViewModelAllFour employeeDetailsViewModel = new EmployeeDetailsViewModelAllFour(empID, OnboardingManager._employee, OnboardingManager._jobDetails, OnboardingManager._documentLinks, OnboardingManager._payroll);

                    // Save the employee to Firebase
                    await _firebaseService.SaveEmployee(empID, employeeDetailsViewModel);

                    // Send onboarding email (or any other process)
                    string email = OnboardingManager._employee.Email;
                    await _onboardingService.OnboardEmployeeAsync(email);

                    // Return success response with a message
                    return Json(new { success = true, message = "Employee onboarded successfully." });
                }
                catch (Exception ex)
                {
                    // Return failure response if an error occurs
                    return Json(new { success = false, message = $"Error during onboarding: {ex.Message}" });
                }
            }

            // Return failure response if model state is not valid
            return Json(new { success = false, message = "Invalid data. Please check the fields." });
        }
    }
}