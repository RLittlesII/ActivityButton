using ReactiveUI;
using Splat;
using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ActivityButton
{
    public class MainViewModel : ReactiveObject, IRoutableViewModel, INotifyPropertyChanging
    {
        private readonly ObservableAsPropertyHelper<string> _controlText;
        private readonly ObservableAsPropertyHelper<bool> _isLoading;
        private readonly ObservableAsPropertyHelper<bool> _isValid;
        private string _email;
        private ObservableAsPropertyHelper<double> _isActive;
        private string _password;

        public double Active => _isActive?.Value ?? .3;
        public string EmailAddress { get => _email; set => this.RaiseAndSetIfChanged(ref _email, value); }
        public IScreen HostScreen => Locator.Current.GetService<IScreen>();
        public bool IsLoading => _isLoading?.Value ?? false;
        public bool IsValid => _isValid?.Value ?? false;
        public ReactiveCommand LoginCommand { get; set; }
        public string LoginText => _controlText.Value ?? "LOGIN";
        public string Password { get => _password; set => this.RaiseAndSetIfChanged(ref _password, value); }
        public string UrlPathSegment => string.Empty;

        public MainViewModel()
        {
            var canExecuteLogin = this.WhenAnyValue(x => x.IsLoading,
                    x => x.IsValid,
                    (isLoading, isValid) => !isLoading && isValid)
                .Do(x => Debug.WriteLine($"Can Login: {x}"));

            LoginCommand = ReactiveCommand.CreateFromTask(Authenticate, canExecuteLogin);

            LoginCommand.ThrownExceptions.Subscribe(x =>
            {
                Debug.WriteLine(x.Message);
                Debug.WriteLine(x.InnerException);
            });

            _isValid = this.WhenAnyValue(x => x.EmailAddress,
                    x => x.Password,
                    (email, password) => ValidateEmail(email) && ValidatePassword(password))
                .ToProperty(this, x => x.IsValid);

            _isLoading = this.WhenAnyObservable(x => x.LoginCommand.IsExecuting)
                .ToProperty(this, x => x.IsLoading);

            _isActive = this.WhenAnyValue(x => x.IsValid).Select(x => SetOpacity(x)).ToProperty(this, x => x.Active);

            _controlText = this.WhenAnyValue(x => x.IsLoading).Select(x => SetText(x)).ToProperty(this, x => x.LoginText);
        }

        private static bool ValidateEmail(string email) => !string.IsNullOrEmpty(email) && Regex.Matches(email, "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$").Count == 1;

        private static bool ValidatePassword(string password) => !string.IsNullOrEmpty(password) && password.Length > 5;

        private async Task Authenticate() => await Authenticate(EmailAddress, Password);

        private async Task Authenticate(string email, string password)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
        }

        private double SetOpacity(bool changed) => changed ? 1 : .3;

        private string SetText(bool loading) => loading ? "AUTHENTICATING" : "LOGIN";
    }
}