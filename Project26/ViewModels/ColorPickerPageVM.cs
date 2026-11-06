using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project26.Models;

namespace Project26.ViewModels
{
    internal partial class ColorPickerPageVM(CustomColor customColor) : ObservableObject
    {
        private readonly CustomColor customColor = customColor;
        public double Red
        {
            get => customColor.Red;
            set
            {
                if (customColor.Red != value)
                {
                    customColor.Red = value;
                    OnPropertyChanged(nameof(Red));
                    OnPropertyChanged(nameof(SelectedColor));
                    OnPropertyChanged(nameof(HexValue));
                }
            }
  
        }
        public double Green
        {
            get => customColor.Green;
            set
            {
                if (customColor.Green != value)
                {
                    customColor.Green = value;
                    OnPropertyChanged(nameof(Green));
                    OnPropertyChanged(nameof(SelectedColor));
                    OnPropertyChanged(nameof(HexValue));
                }
            }
        }
        public double Blue
        {
            get => customColor.Blue;
            set
            {
                if (customColor.Blue != value)
                {
                    customColor.Blue = value;
                    OnPropertyChanged(nameof(Blue));
                    OnPropertyChanged(nameof(SelectedColor));
                    OnPropertyChanged(nameof(HexValue));
                }
            }
        }
        public double Alpha
        {
            get => customColor.Alpha;
            set
            {
                if (customColor.Alpha != value)
                {
                    customColor.Alpha = value;
                    OnPropertyChanged(nameof(Alpha));
                    OnPropertyChanged(nameof(SelectedColor));
                    OnPropertyChanged(nameof(HexValue));
                }
            }
        }
        public Color SelectedColor => customColor.SelectedColor;

        public string HexValue => customColor.HexValue;
        [RelayCommand]
        private void SelectColor()
        {
            Shell.Current.Navigation.PopAsync();
            customColor.OnColorChanged();
        }
        [RelayCommand]
        private static void Quit()
        {
            Shell.Current.Navigation.PopAsync();
        }
    }
}
