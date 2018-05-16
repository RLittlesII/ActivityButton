using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReactiveUI;
using System.Reactive.Disposables;
using ReactiveUI.XamForms;

namespace ActivityButton
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ReactiveContentPage<MainViewModel>
	{
		protected readonly CompositeDisposable SubscriptionDisposables = new CompositeDisposable();

		public MainPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			Debug.WriteLine("=== APPEARING! ===");

			SetupReactiveBindings();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			Debug.WriteLine("=== DISAPPEARING! ===");

			SubscriptionDisposables.Clear();
		}

		protected void SetupReactiveBindings()
		{
			this.WhenActivated(disposables =>
			{
				this.Bind(ViewModel, vm => vm.EmailAddress, page => page.EmailEntry.Text)
					.DisposeWith(SubscriptionDisposables);

				this.Bind(ViewModel, vm => vm.Password, page => page.PasswordEntry.Text)
					.DisposeWith(SubscriptionDisposables);

				this.OneWayBind(ViewModel, vm => vm.LoginText, page => page.LoginControl.Text);

				this.OneWayBind(ViewModel, vm => vm.Active, page => page.LoginControl.Opacity)
					.DisposeWith(SubscriptionDisposables);

				this.OneWayBind(ViewModel, vm => vm.IsLoading, page => page.LoginControl.IsBusy)
					.DisposeWith(SubscriptionDisposables);
			});
		}
	}
}