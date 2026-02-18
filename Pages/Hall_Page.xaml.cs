namespace Vidi_Health.Pages;

public partial class Hall_Page : ContentPage
{
    // Swipe ile geçilecek route sýrasý
    private static readonly string[] _pageRoutes =
        ["//Home", "//Stats", "//Add", "//Market", "//Settings"];
    private int _currentIndex = 0;

    public Hall_Page()
    {
        InitializeComponent();
    }

    private async void OnSwipedLeft(object sender, SwipedEventArgs e)
    {
        if (_currentIndex < _pageRoutes.Length - 1)
            await Shell.Current.GoToAsync(_pageRoutes[++_currentIndex]);
    }

    private async void OnSwipedRight(object sender, SwipedEventArgs e)
    {
        if (_currentIndex > 0)
            await Shell.Current.GoToAsync(_pageRoutes[--_currentIndex]);
    }

    private async void OnProfileTapped(object sender, EventArgs e) => await Shell.Current.GoToAsync("//Profile");
    private async void OnCartClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync("//Market");
    private async void OnNavHome(object sender, EventArgs e) => await Shell.Current.GoToAsync("//Home");
    private async void OnNavStats(object sender, EventArgs e) => await Shell.Current.GoToAsync("//Stats");
    private async void OnNavAdd(object sender, EventArgs e) => await Shell.Current.GoToAsync("//Add");
    private async void OnNavMarket(object sender, EventArgs e) => await Shell.Current.GoToAsync("//Market");
    private async void OnNavSettings(object sender, EventArgs e) => await Shell.Current.GoToAsync("//Settings");

    private void Button_Clicked(object sender, EventArgs e) { }
}