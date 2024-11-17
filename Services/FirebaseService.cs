using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using System.Threading.Tasks;
using XBCAD7319_SparkLine_HR_WebApp.Models;
using XBCAD7319_SparkLine_HR_WebApp.ViewModel;

public class FirebaseService
{
    private readonly FirebaseClient _firebaseClient;
    private readonly FirebaseStorage _firebaseStorage;

    public FirebaseService()
    {
        _firebaseClient = new FirebaseClient("https://mvc-hr-demo-default-rtdb.firebaseio.com/"); // Replace with your Firebase Realtime Database URL
        _firebaseStorage = new FirebaseStorage("hrappstorage.appspot.com");
    }

    public async Task SaveEmployee(string employeeId, EmployeeDetailsViewModelAllFour employeeDetails)
    {
        await _firebaseClient
            .Child("employees_sparkline")
            .Child(employeeId)
            .PutAsync(employeeDetails);
    }
}

