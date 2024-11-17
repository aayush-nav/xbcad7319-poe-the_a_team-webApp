using RestSharp;
using System.Text.Json;

namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    public class OnboardingService
    {
        private readonly RestClient _client;

        public OnboardingService()
        {
            // Initialize the RestClient with the base URL of your API
            _client = new RestClient("https://onboarding-sparkline-api-a-team.onrender.com/api/auth/");
        }

        // Method to get the next employee ID from the API
        public async Task<string> GetNextEmployeeIdAsync()
        {
            // Define the endpoint for the next employee ID
            var nextEmpIDRequest = new RestRequest("nextempid", Method.Get);

            try
            {
                // Make the API call to get the next employee ID
                var response = await _client.ExecuteAsync(nextEmpIDRequest);
                if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                {
                    // Parse the JSON response to get the "nextEmployeeId" field
                    var jsonResponse = JsonDocument.Parse(response.Content);
                    string nextEmployeeId = jsonResponse.RootElement.GetProperty("nextEmployeeId").GetString();

                    return nextEmployeeId;
                }
                else
                {
                    // Handle errors based on the response
                    throw new Exception($"Failed to get next employee ID: {response.ErrorMessage ?? response.Content}");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw new Exception($"Exception occurred while fetching next employee ID: {ex.Message}");
            }
        }

        // Method to onboard an employee using the next employee ID
        public async Task<string> OnboardEmployeeAsync(string email)
        {
            try
            {
                // Get the next employee ID first
                var nextEmployeeId = await GetNextEmployeeIdAsync();

                // Define the endpoint and request method for onboarding
                var request = new RestRequest("onboard", Method.Post);

                // Set up the request payload (add other fields as required)
                request.AddJsonBody(new
                {
                    // employeeId = nextEmployeeId, // Use the retrieved employee ID
                    email = email
                });

                // Make the API call to onboard the employee
                var response = await _client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    return nextEmployeeId;
                }
                else
                {
                    // Handle errors based on the response
                    return $"Error: {response.ErrorMessage ?? response.Content}";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return $"Exception: {ex.Message}";
            }
        }
    }
}