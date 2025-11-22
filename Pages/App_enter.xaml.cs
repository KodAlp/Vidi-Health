namespace Vidi_Health.Pages;

public partial class App_enter : ContentPage
{
	public App_enter()
	{
		InitializeComponent();
	}
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            await DisplayAlert("Hata", "Lütfen adýnýzý giriniz", "Tamam");
            return;
        }

        if (GenderPicker.SelectedIndex == -1)
        {
            await DisplayAlert("Hata", "Lütfen cinsiyetinizi seçiniz", "Tamam");
            return;
        }

        if (EthnicityPicker.SelectedIndex == -1)
        {
            await DisplayAlert("Hata", "Lütfen etnik kökeninizi seçiniz", "Tamam");
            return;
        }

        if (ActivityLevelPicker.SelectedIndex == -1)
        {
            await DisplayAlert("Hata", "Lütfen aktivite seviyenizi seçiniz", "Tamam");
            return;
        }

        // Geçici - Database olmadan test
        string message = $"Ýsim: {NameEntry.Text}\n" +
                       $"Cinsiyet: {GenderPicker.SelectedItem}\n" +
                       $"Köken: {EthnicityPicker.SelectedItem}\n" +
                       $"Aktivite: {ActivityLevelPicker.SelectedItem}";

        await DisplayAlert("Kayýt Baþarýlý", message, "Tamam");

        // Sonraki sayfaya geç (þimdilik MainPage'e dön)
        await Navigation.PopAsync();
    }
}