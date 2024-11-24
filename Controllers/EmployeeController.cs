using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Globalization;
using XBCAD7319_SparkLine_HR_WebApp.Models;
using XBCAD7319_SparkLine_HR_WebApp.Token;


namespace XBCAD7319_SparkLine_HR_WebApp.Controllers
{
    /// <summary>
    ///  Employee controller - handles all the methods for the related to employee 
    ///  onboarding a new employee
    ///  managing existing employees details
    ///  makes use of the TokenAuthorizationFilter for all the methods to block the routes
    /// </summary>
    public class EmployeeController : Controller
    {
        // Initialized the service to onboard an employees and to connect to the database
        private readonly OnboardingService _onboardingService;
        private readonly FirebaseService _firebaseService;

        public EmployeeController()
        {
            _onboardingService = new OnboardingService();
            _firebaseService = new FirebaseService();
        }

        /// <summary>
        /// Use the onboarding service api to get the next employee id
        /// </summary>
        /// <returns>next employee id</returns>
        [TokenAuthorizationFilter]
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

        // <summary>
        /// Returns the Index View
        /// </summary>
        [TokenAuthorizationFilter]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns the details View
        /// </summary>
        [TokenAuthorizationFilter]
        public IActionResult Details()
        {
            return View();
        }

        /// <summary>
        /// Returns the Management View with a list of all the employees
        /// </summary>
        [TokenAuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> Management()
        {
            var employees = await _firebaseService.GetAllEmployeesAsync();
            return View(employees);
        }

        /// <summary>
        /// search the employees list to get the details of the one with the same id
        /// </summary>
        /// <param name="id">employee id</param>
        /// <returns>The employee details</returns>
        [TokenAuthorizationFilter]
        public async Task<IActionResult> EmployeeDetails(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound("Employee ID is required");
            }

            // Call the service method to get employee details by ID
            var employeeDetails = await _firebaseService.EmployeeDetailsByID(id);

            if (employeeDetails == null)
            {
                return NotFound("Employee not found");
            }

            // Pass the model to the Edit view
            return View(employeeDetails);
        }

        /// <summary>
        /// Method to reset the password incase the otp expires
        /// </summary>
        /// <param name="EmailInput">email the user inputs</param>
        /// <param name="Email">email of the account the user is managing</param>
        /// <returns>json message of the result</returns>
        [TokenAuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string EmailInput, string Email)
        {
            try
            {
                // Send onboarding email (or any other process)
                if (EmailInput.Equals(Email))
                {
                    await _onboardingService.OnboardEmployeeAsync(EmailInput);
                    return Json(new { success = true, message = "Password reset successfully and emailed to the employee." });
                }
                else
                {
                    return Json(new { success = false, message = "Incorrect Email." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }

        /// <summary>
        /// Method to read the data from the personal information form 
        /// store in the employee model and saved to the employee onboarding manager 
        /// </summary>
        /// <param name="model">Employee model</param>
        /// <returns>json message with the result</returns>
        [TokenAuthorizationFilter]
        [HttpPost]
        public JsonResult PersonalInformationCaptureAJAX(Employee model)
        {
            if (ModelState.IsValid)
            {
                OnboardingManager._employee = model;
                // Return a success response
                return Json(new { success = true });
            }

            // If validation fails, log the errors
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                           .Select(e => e.ErrorMessage)
                                           .ToList();

            // Return a failure response with the error messages
            return Json(new { success = false, message = "There were validation errors.", errors = errors });
        }

        /// <summary>
        /// Method to read the data from the job details form 
        /// store in the Job details model and saved to the job details onboarding manager 
        /// </summary>
        /// <param name="model">Job details model</param>
        /// <returns>json message with the result</returns>
        [TokenAuthorizationFilter]
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

        /// <summary>
        /// Method to get the uploaded documents and store them in firebase storage 
        /// once stored get a link of the document and save to the document model
        /// store in the document links model and saved to the document links onboarding manager 
        /// </summary>
        /// <returns>json message with the result</returns>
        [TokenAuthorizationFilter]
        [HttpPost]
        public async Task<JsonResult> UploadDocumentsAJAX(
        List<IFormFile> IDOrPassport,
        List<IFormFile> CV,
        List<IFormFile> ProofOfAddress,
        List<IFormFile> ProofOfTaxRegistration,
        List<IFormFile> CompletedIRP5,
        List<IFormFile> ProofOfBank,
         string employeeID)
        {
            Console.WriteLine("Received employeeID: " + employeeID); // Log the employeeID

            var documentLinks = await _firebaseService.GetEmployeeDocumentLinksAsync(employeeID); // Fetch existing links
            var empID = !string.IsNullOrEmpty(employeeID) ? employeeID : await _onboardingService.GetNextEmployeeIdAsync();


            // Validate that at least one file is uploaded
            var documentFiles = new List<IFormFile>();
            if (IDOrPassport != null && IDOrPassport.Any()) documentFiles.AddRange(IDOrPassport);
            if (CV != null && CV.Any()) documentFiles.AddRange(CV);
            if (ProofOfAddress != null && ProofOfAddress.Any()) documentFiles.AddRange(ProofOfAddress);
            if (ProofOfTaxRegistration != null && ProofOfTaxRegistration.Any()) documentFiles.AddRange(ProofOfTaxRegistration);
            if (CompletedIRP5 != null && CompletedIRP5.Any()) documentFiles.AddRange(CompletedIRP5);
            if (ProofOfBank != null && ProofOfBank.Any()) documentFiles.AddRange(ProofOfBank);

            if (documentFiles.Count == 0)
            {
                // If no document is uploaded, use existing model values
                documentLinks = documentLinks ?? new DocumentLinks();
                return Json(new { success = true, message = "No new documents uploaded. Existing links retained.", documentLinks });
            }

            try
            {
                // Validate file types and sizes
                var validExtensions = new[] { ".pdf", ".docx", ".jpg", ".png" };
                foreach (var documentFile in documentFiles)
                {
                    if (documentFile.Length > 5 * 1024 * 1024) // 5MB size limit
                    {
                        return Json(new { success = false, message = $"File {documentFile.FileName} exceeds size limit." });
                    }

                    if (!validExtensions.Contains(Path.GetExtension(documentFile.FileName)?.ToLower()))
                    {
                        return Json(new { success = false, message = $"File {documentFile.FileName} has an invalid format." });
                    }
                }

                // Map files to their respective document types
                var fileTypeMappings = new Dictionary<int, Action<string>>
        {
            { 0, url => documentLinks.IDOrPassport = url },
            { 1, url => documentLinks.CV = url },
            { 2, url => documentLinks.ProofOfAddress = url },
            { 3, url => documentLinks.ProofOfTaxRegistration = url },
            { 4, url => documentLinks.CompletedIRP5 = url },
            { 5, url => documentLinks.ProofOfBank = url },
        };

                // Existing links (from the database or a session variable)
                var existingLinks = new Dictionary<int, string>
        {
            { 0, documentLinks.IDOrPassport },
            { 1, documentLinks.CV },
            { 2, documentLinks.ProofOfAddress },
            { 3, documentLinks.ProofOfTaxRegistration },
            { 4, documentLinks.CompletedIRP5 },
            { 5, documentLinks.ProofOfBank }
        };

                // Process the uploaded documents and update links
                foreach (var (index, documentFile) in documentFiles.Select((file, i) => (i, file)))
                {
                    if (documentFile.Length > 0)
                    {
                        // Construct the file path to store the document
                        var filePath = $"employee-documents/{empID}_{documentFile.FileName}";
                        var firebaseStorage = new FirebaseStorage("hrappstorage.appspot.com").Child(filePath);

                        using var fileStream = documentFile.OpenReadStream();
                        string downloadUrl = await firebaseStorage.PutAsync(fileStream);

                        // Update the document link if the file is uploaded
                        if (!string.IsNullOrEmpty(downloadUrl))
                        {
                            fileTypeMappings[index](downloadUrl);
                        }
                        else
                        {
                            // Retain the existing link if file upload fails
                            fileTypeMappings[index](existingLinks[index]);
                        }
                    }
                    else
                    {
                        // If no file uploaded for this field, retain the existing document link
                        fileTypeMappings[index](existingLinks[index]);
                    }
                }

                // Save updated document links
                OnboardingManager._documentLinks = documentLinks;

                return Json(new { success = true, message = "Documents uploaded successfully.", documentLinks });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading documents: {ex.Message}");
                return Json(new { success = false, message = "Failed to upload documents. Please try again." });
            }
        }

        /// <summary>
        /// Method to read the data from the payroll information form 
        /// store in the payroll model and saved to the payroll onboarding manager 
        /// </summary>
        /// <param name="model">payroll model</param>
        /// <returns>json message with the result</returns>
        [TokenAuthorizationFilter]
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


        /// <summary>
        /// This methoed takes all the objects stored in the onboarding manager and passes them to the save employees method in the firebase service
        /// </summary>
        /// <returns>json message with the result</returns>
        [TokenAuthorizationFilter]
        [HttpPost]
        public async Task<JsonResult> OnboardEmployee()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Generate Employee ID
                    var empID = await _onboardingService.GetNextEmployeeIdAsync();

                    LeaveBalance leave = new LeaveBalance();

                    // Create the Employee Details ViewModel
                    EmployeeDetailsViewModelAllFour employeeDetailsViewModel = new EmployeeDetailsViewModelAllFour(empID, OnboardingManager._employee,
                        OnboardingManager._jobDetails, OnboardingManager._documentLinks, OnboardingManager._payroll, leave);

                    // Save the employee to Firebase
                    await _firebaseService.SaveEmployee(empID, employeeDetailsViewModel);
                    // await _firebaseService.SaveEmployeeLeave(empID, leave);

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

        /// <summary>
        /// this method is called when changing an existing employees details
        /// works similar to the onboarding except this method stores the already existing data and only
        /// override the new data that is changed when updating the inforamtion
        /// </summary>
        /// <param name="employeeId">Employee id of the employees whose details are being changed</param>
        /// <returns>json message with the result</returns>
        [TokenAuthorizationFilter]
        [HttpPost]
        public async Task<JsonResult> SaveEmployee(string employeeId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    OnboardingManager._leaveBalance = await _firebaseService.GetEmployeeLeaveBalanceAsync(employeeId);
                    // Get the employee data using the employeeId
                    var employeeDetailsViewModel = new EmployeeDetailsViewModelAllFour(
                        employeeId,
                        OnboardingManager._employee,
                        OnboardingManager._jobDetails,
                        OnboardingManager._documentLinks,
                        OnboardingManager._payroll,
                        OnboardingManager._leaveBalance
                    );

                    // Save the employee to Firebase
                    await _firebaseService.SaveEmployee(employeeId, employeeDetailsViewModel);

                    // Return success response with a message
                    return Json(new { success = true, message = "Employee updated successfully." });
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

        /// <summary>
        /// This method gets a list of all the emplyes that are in the database
        /// </summary>
        /// <returns>List of the employees</returns>
        [TokenAuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var firebase = new FirebaseClient("https://hrappstorage-default-rtdb.firebaseio.com/");
            var employees = await firebase.Child("SparkLineHR").Child("employees_sparkline").OnceAsync<EmployeeDetailsViewModelAllFour>();

            var employeeList = employees.Select(e => new
            {
                empID = e.Key,
                employee = e.Object.employee,
                name = e.Object.employee.Name

            }).ToList();

            return Json(new { success = true, data = employeeList });
        }

        /// <summary>
        /// Method to get eh hiredate of the employee
        /// </summary>
        /// <param name="empID">Employee id</param>
        /// <returns>Hiredate of the employee</returns>
        [TokenAuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeDetails(string empID)
        {
            var firebase = new FirebaseClient("https://hrappstorage-default-rtdb.firebaseio.com/");
            var employee = await firebase.Child("SparkLineHR").Child("employees_sparkline").Child(empID).OnceSingleAsync<EmployeeDetailsViewModelAllFour>();

            var hireDate = employee.jobdetails.HireDate;


            return Json(new { success = true, data = hireDate });
        }

        /// <summary>
        /// The method is called when the admin releases the payslip of the employee for a specific month
        /// </summary>
        /// <param name="request">payslip request</param>
        /// <returns>json message</returns>
        [TokenAuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> ReleasePayslip([FromBody] PayslipRequest request)
        {
            // Firebase connection
            var firebase = new FirebaseClient("https://hrappstorage-default-rtdb.firebaseio.com/");
            var payslipPath = $"SparkLineHR/payslips/{request.EmpId}/{request.MonthYear}";

            // Check if payslip already exists
            var existingPayslip = await firebase
                .Child(payslipPath)
                .OnceSingleAsync<object>();

            if (existingPayslip != null)
            {
                return Json(new { success = false, message = "Payslip already exists" });
            }

            // Get employee details
            var employee = await firebase
                .Child("SparkLineHR")
                .Child("employees_sparkline")
                .Child(request.EmpId)
                .OnceSingleAsync<EmployeeDetailsViewModelAllFour>();

            //string montheyear = request.MonthYear;

            if (employee == null)
            {
                return Json(new { success = false, message = "Employee not found" });
            }

            // Generate payslip data
            var payslipData = new
            {
                grossSalary = employee.payroll.GrossPay,
                company = "SparkLine",
                empName = employee.employee.Name,
                empNum = employee.jobdetails.EmployeeId,
                empPos = employee.jobdetails.JobTitle,
                issueDate = DateTime.Now.ToString("yyyy-MM-dd"),
                payslipPeriod = GetPayslipPeriod(request.MonthYear),
                pensionPercent = employee.payroll.Retirement,
                taxNum = employee.payroll.TaxNumber,
                uifPercent = employee.payroll.UIF
            };

            // Save payslip to Firebase
            await firebase
                .Child(payslipPath)
                .PutAsync(payslipData);

            return Json(new { success = true });
        }

        /// <summary>
        /// Merthod to egt the payslip period for the month the payslip is release for
        /// </summary>
        /// <param name="monthYear">name of the month and the year </param>
        /// <returns>Period</returns>
        [TokenAuthorizationFilter]
        private string GetPayslipPeriod(string monthYear)
        {
            try
            {
                var firstDay = DateTime.ParseExact("01-" + monthYear, "dd-MMMM yyyy", CultureInfo.InvariantCulture);
                var lastDay = new DateTime(firstDay.Year, firstDay.Month, DateTime.DaysInMonth(firstDay.Year, firstDay.Month));
                return $"{firstDay:yyyy-MM-dd} - {lastDay:yyyy-MM-dd}";
            }
            catch
            {
                return "Invalid period"; // Fallback for unexpected parsing issues
            }
        }

        /// <summary>
        /// Method to retrieve the incident reports from the database
        /// </summary>
        /// <returns>list of incident reports</returns>
        [TokenAuthorizationFilter]
        public async Task<IActionResult> GetIncidentReports()
        {
            // Firebase connection
            var firebase = new FirebaseClient("https://hrappstorage-default-rtdb.firebaseio.com/");

            try
            {
                // Fetch all incident reports
                var issueReports = await firebase
                    .Child("SparkLineHR")
                    .Child("IssueReports")
                    .OnceAsync<IncidentDBModel>();

                // Map Firebase data to a view model
                var incidentReportsList = issueReports.Select(report => new IncidentReportViewModel
                {
                    EmployeeNumber = report.Key.Split(',')[0],
                    Title = report.Object.passTitle,
                    Description = report.Object.passDesc,
                    DateSubmitted = report.Key.Split(',')[1]
                }).ToList();

                return Json(new { success = true, data = incidentReportsList });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Failed to fetch incident reports.", error = ex.Message });
            }
        }

    }
}