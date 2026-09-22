namespace Project26.Views;

public partial class AuthPage : ContentPage
{
	public AuthPage()
	{
		InitializeComponent();
		BindingContext = new ViewModels.AuthPageVM();
    }
}