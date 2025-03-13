using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Windows.Input;

namespace File_Manager_MEDGEN.ViewModel
{
    public class SignUpViewModel : ViewModelBase
    {
        private string _username;
        private string _email;
        private SecureString _password;
        private string _errorMessage;
        private FirebaseHelper _firebaseHelper;

        // Properties
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public SecureString Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        // Commands
        public ICommand SignUpCommand { get; }

        // Constructor
        public SignUpViewModel()
        {
            _firebaseHelper = new FirebaseHelper();
            SignUpCommand = new ViewModelCommand(ExecuteSignUpCommand, CanExecuteSignUpCommand);
        }

        // Sign Up
        private async void ExecuteSignUpCommand(object obj)
        {
            try
            {
                // Register user in Firebase
                var result = await _firebaseHelper.SignUpAsync(Email, ConvertToUnsecureString(Password));

                if (result != null)
                {
                    // Register username to Firebase Realtime Database or Firestore
                    await _firebaseHelper.SaveUsernameAsync(Username, Email);
                    ErrorMessage = "Registration successful!";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"* Sign up failed: {ex.Message}";
            }
        }

        private bool CanExecuteSignUpCommand(object obj)
        {
            return !string.IsNullOrWhiteSpace(Username) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   Password != null && Password.Length > 6;
        }

        // Convert Password from SecureString to String
        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
                return string.Empty;

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return System.Runtime.InteropServices.Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
