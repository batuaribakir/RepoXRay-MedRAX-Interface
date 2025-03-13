using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace File_Manager_MEDGEN
{
    public class FirebaseHelper
    {
        private const string FirebaseApiKey = "AIzaSyCJ2BXeFHN0yfycyjXELwoDow7AmpxsDhs";
        private IFirebaseClient client;

        public FirebaseHelper()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "1cLJCS3svFB7U2yZlniQulstcqM6bPBhbF7g40vl",  // Secret key for database
                BasePath = "https://secret-message-74103-default-rtdb.europe-west1.firebasedatabase.app/"  // Firebase URL
            };

            client = new FireSharp.FirebaseClient(config);

            if (client == null)
            {
                throw new Exception("Firebase bağlantısı başarısız.");
            }
        }

        // Sign Up
        public async Task<string> SignUpAsync(string email, string password)
        {
            using (var client = new HttpClient())
            {
                var content = new JObject
            {
                { "email", email },
                { "password", password },
                { "returnSecureToken", true }
            };

                var response = await client.PostAsync(
                    $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={FirebaseApiKey}",
                    new StringContent(content.ToString(), System.Text.Encoding.UTF8, "application/json"));

                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }

        // Log In
        public async Task<string> SignInAsync(string email, string password)
        {
            using (var client = new HttpClient())
            {
                var content = new JObject
            {
                { "email", email },
                { "password", password },
                { "returnSecureToken", true }
            };

                var response = await client.PostAsync(
                    $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={FirebaseApiKey}",
                    new StringContent(content.ToString(), System.Text.Encoding.UTF8, "application/json"));

                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }

        // Reset Password
        public async Task<string> ResetPasswordAsync(string email)
        {
            using (var client = new HttpClient())
            {
                var content = new JObject
            {
                { "requestType", "PASSWORD_RESET" },
                { "email", email }
            };

                var response = await client.PostAsync(
                    $"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key={FirebaseApiKey}",
                    new StringContent(content.ToString(), System.Text.Encoding.UTF8, "application/json"));

                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }
        public async Task SaveUsernameAsync(string username, string email)
        {
            var data = new { email = email };
            await client.SetAsync("users/" + username, data);
        }
    }
}