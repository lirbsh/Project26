using Project26.ViewModels;

namespace Project26.Views;

public partial class ChatPage : ContentPage
{
    private readonly ChatPageVM cpVm;
    internal ChatPage(ModelsLogic.Chat chat)
	{
		InitializeComponent();
        cpVm = new ViewModels.ChatPageVM(chat);
        BindingContext = cpVm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        cpVm.AddSnapshotListener();
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        cpVm.RemoveSnapshotListener();
    }
}