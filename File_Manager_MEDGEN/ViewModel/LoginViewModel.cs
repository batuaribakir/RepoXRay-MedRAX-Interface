using System;
using System.Net;
using System.Security;
using System.Threading;
using System.Windows.Input;
using File_Manager_MEDGEN.Repositories;
using File_Manager_MEDGEN.Model;
using System.Security.Principal;

namespace File_Manager_MEDGEN.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;
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

        public bool IsViewVisible
        {
            get => _isViewVisible;
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        // Commands
        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }
        public ICommand ResetPasswordCommand { get; }

        // Constructor
        public LoginViewModel()
        {
            _firebaseHelper = new FirebaseHelper();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            SignUpCommand = new ViewModelCommand(ExecuteSignUpCommand, CanExecuteSignUpCommand);
            ResetPasswordCommand = new ViewModelCommand(p => ExecuteResetPasswordCommand(Username));
        }

        // Login
        private async void ExecuteLoginCommand(object obj)
        {
            try
            {
                var result = await _firebaseHelper.SignInAsync(Username, ConvertToUnsecureString(Password));
                if (result != null)
                {
                    IsViewVisible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"* Login failed: {ex.Message}";
            }
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            return !string.IsNullOrWhiteSpace(Username) && Password != null && Password.Length > 0;
        }

        // SignUp
        private async void ExecuteSignUpCommand(object obj)
        {
            try
            {
                var result = await _firebaseHelper.SignUpAsync(Username, ConvertToUnsecureString(Password));
                if (result != null)
                {
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
            return !string.IsNullOrWhiteSpace(Username) && Password != null && Password.Length > 0;
        }

        // Reset Password
        private async void ExecuteResetPasswordCommand(string email)
        {
            try
            {
                var result = await _firebaseHelper.ResetPasswordAsync(email);
                ErrorMessage = "Password reset email sent!";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"* Reset password failed: {ex.Message}";
            }
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

        // Clearing Login Information
        public void ClearLoginCredentials()
        {
            Username = string.Empty;
            Password = null;
            ErrorMessage = string.Empty;
            IsViewVisible = true;  // Make login screen visible again
        }
    }
}