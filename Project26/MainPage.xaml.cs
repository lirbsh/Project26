using Project26.ViewModels;

namespace Project26
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageVM mpVm = new();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = mpVm;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            mpVm.AddSnapshotListener();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            mpVm.RemoveSnapshotListener();
        }
    }
}
