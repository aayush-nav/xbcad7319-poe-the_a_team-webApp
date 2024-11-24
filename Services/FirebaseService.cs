using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using Newtonsoft.Json;
using System.Threading.Tasks;
using XBCAD7319_SparkLine_HR_WebApp.Models;
using XBCAD7319_SparkLine_HR_WebApp.ViewModel;

public class FirebaseService
{
    private readonly FirebaseClient _firebaseClient;
    private readonly FirebaseStorage _firebaseStorage;

    /// <summary>
    /// Initialize the firebase client and the storage
    /// </summary>
    public FirebaseService()
    {
        _firebaseClient = new FirebaseClient("https://hrappstorage-default-rtdb.firebaseio.com/"); // Replace with your Firebase Realtime Database URL
        _firebaseStorage = new FirebaseStorage("hrappstorage.appspot.com");
    }

    /// <summary>
    /// Method to save the employees to the database
    /// </summary>
    /// <param name="employeeId">Employee id </param>
    /// <param name="employeeDetails">employee deatils</param>
    /// <returns></returns>
    public async Task SaveEmployee(string employeeId, EmployeeDetailsViewModelAllFour employeeDetails)
    {
        await _firebaseClient
            .Child("SparkLineHR")
            .Child("employees_sparkline")
            .Child(employeeId)
            .PutAsync(employeeDetails);
    }

    /// <summary>
    /// Method to get all the employees
    /// </summary>
    /// <returns>List of all the employees</returns>
    public async Task<List<EmployeeDetailsViewModelAllFour>> GetAllEmployeesAsync()
    {
        var snapshot = await _firebaseClient.Child("SparkLineHR").Child("employees_sparkline").OnceAsync<object>();
        var employees = new List<EmployeeDetailsViewModelAllFour>();

        foreach (var item in snapshot)
        {
            try
            {
                var empId = item.Key;

                // Attempt to directly deserialize to EmployeeDetailsViewModelAllFour
                var record = JsonConvert.DeserializeObject<EmployeeDetailsViewModelAllFour>(item.Object.ToString());
                if (record != null)
                {
                    employees.Add(record);
                    continue;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing employee record: {ex.Message}");
            }
        }

        return employees;
    }

    /// <summary>
    /// Method to get the details of the specific employee
    /// </summary>
    /// <param name="empID">Employee id</param>
    /// <returns>Details of the employee</returns>
    public async Task<EmployeeDetailsViewModelAllFour> EmployeeDetailsByID(string empID)
    {
        // Fetch data from Firebase Realtime Database
        var employee = await _firebaseClient
            .Child("SparkLineHR")
            .Child("employees_sparkline")
            .Child(empID)
            .Child("employee")
            .OnceSingleAsync<Employee>();

        var jobDetails = await _firebaseClient
            .Child("SparkLineHR")
            .Child("employees_sparkline")
            .Child(empID)
            .Child("jobdetails")
            .OnceSingleAsync<JobDetails>();

        var documentLinks = await _firebaseClient
            .Child("SparkLineHR")
            .Child("employees_sparkline")
            .Child(empID)
            .Child("documentlinks")
            .OnceSingleAsync<DocumentLinks>();

        var payroll = await _firebaseClient
            .Child("SparkLineHR")
            .Child("employees_sparkline")
            .Child(empID)
            .Child("payroll")
            .OnceSingleAsync<Payroll>();

        var leave = await _firebaseClient
            .Child("SparkLineHR")
           .Child("employees_sparkline")
           .Child(empID)
           .Child("leavebalance")
           .OnceSingleAsync<LeaveBalance>();

        // Create the view model with the retrieved data
        var viewModel = new EmployeeDetailsViewModelAllFour(empID, employee, jobDetails, documentLinks, payroll, leave);

        return (viewModel);
    }

    /// <summary>
    /// Get the list of the deocuments stored for a specific employee
    /// </summary>
    /// <param name="employeeId">Employee ID</param>
    /// <returns>List of the documents snapshots</returns>
    public async Task<DocumentLinks> GetEmployeeDocumentLinksAsync(string employeeId)
    {
        try
        {
            // Assuming _firebaseDatabase is your Firebase Realtime Database reference
            var documentSnapshot = await _firebaseClient
                .Child("SparkLineHR")
                .Child("employees_sparkline") // Root node where employee data is stored
                .Child(employeeId) // Employee ID node
                .Child("documentlinks") // Document links for the specific employee
                .OnceSingleAsync<DocumentLinks>(); // Retrieve the document links object

            return documentSnapshot; // Return the existing links or empty if not found
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving document links for {employeeId}: {ex.Message}");
            return new DocumentLinks(); // Return empty if any error occurs
        }
    }

    /// <summary>
    /// Get the info of the leave balance of a specific employee
    /// </summary>
    /// <param name="employeeId">employee id</param>
    /// <returns>Leave balance</returns>
    public async Task<LeaveBalance> GetEmployeeLeaveBalanceAsync(string employeeId)
    {
        try
        {
            // Assuming _firebaseDatabase is your Firebase Realtime Database reference
            var leaveSnapshot = await _firebaseClient
                .Child("SparkLineHR")
                .Child("employees_sparkline") // Root node where employee data is stored
                .Child(employeeId) // Employee ID node
                .Child("leavebalance") // Document links for the specific employee
                .OnceSingleAsync<LeaveBalance>(); // Retrieve the document links object

            return leaveSnapshot; // Return the existing links or empty if not found
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving document links for {employeeId}: {ex.Message}");
            return new LeaveBalance(); // Return empty if any error occurs
        }
    }

}

