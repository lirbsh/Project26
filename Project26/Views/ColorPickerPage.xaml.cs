using Project26.Models;

namespace Project26.Views;

public partial class ColorPickerPage : ContentPage
{
	internal ColorPickerPage(CustomColor customColor)
	{
		InitializeComponent();
        BindingContext = new ViewModels.ColorPickerPageVM(customColor);
    }
}