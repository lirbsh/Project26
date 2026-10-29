using Project26.ViewModels;

namespace Project26.Views;

public partial class AuthPage : ContentPage
{
    private readonly AuthPageVM apVM = new();
    public AuthPage()
	{
		InitializeComponent();
		BindingContext = apVM;
    }
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        apVM.StartAnimations();
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        apVM.StopAnimations();
    }
}