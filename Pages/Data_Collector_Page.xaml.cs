using Microsoft.Maui;

namespace Vidi_Health.Pages;

public partial class App_enter : ContentPage
{
    private int _currentStep = 1;
    private const int TotalSteps = 4;

    private readonly Border[] _cards;
    private readonly BoxView[] _bars;

    public App_enter()
    {
        InitializeComponent();
        _cards = [Card1, Card2, Card3, Card4];
        _bars = [Step1Bar, Step2Bar, Step3Bar, Step4Bar];
    }

    private async void OnNextClicked(object sender, EventArgs e)
    {
        if (!ValidateStep()) return;

        if (_currentStep == TotalSteps)
        {
            await OnSaveClicked();
            return;
        }

        await AnimateTransition(_cards[_currentStep - 1], _cards[_currentStep]);
        _currentStep++;
        UpdateUI();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        if (_currentStep == 1) return;

        await AnimateTransition(_cards[_currentStep - 1], _cards[_currentStep - 2], goingBack: true);
        _currentStep--;
        UpdateUI();
    }

    private async Task AnimateTransition(Border hide, Border show, bool goingBack = false)
    {
        float fromX = goingBack ? -300 : 300;

        await hide.TranslateTo(goingBack ? 300 : -300, 0, 200, Easing.SinIn);
        hide.IsVisible = false;
        hide.TranslationX = 0;

        show.TranslationX = fromX;
        show.IsVisible = true;
        await show.TranslateTo(0, 0, 200, Easing.SinOut);
    }

    private void UpdateUI()
    {
        StepLabel.Text = $"{_currentStep} / {TotalSteps}";
        BackButton.IsVisible = _currentStep > 1;
        NextButton.Text = _currentStep == TotalSteps ? "Kaydet" : "Devam Et";

        for (int i = 0; i < _bars.Length; i++)
            _bars[i].BackgroundColor = Color.FromArgb(i < _currentStep ? "#FFFFFF" : "#FFFFFF55");
    }

    private bool ValidateStep()
    {
        switch (_currentStep)
        {
            case 1 when string.IsNullOrWhiteSpace(NameEntry.Text):
                DisplayAlert("Uyarý", "Lütfen isminizi girin.", "Tamam");
                return false;
            case 2 when GenderPicker.SelectedIndex == -1:
                DisplayAlert("Uyarý", "Lütfen cinsiyet seçin.", "Tamam");
                return false;
            case 3 when EthnicityPicker.SelectedIndex == -1:
                DisplayAlert("Uyarý", "Lütfen etnik köken seçin.", "Tamam");
                return false;
            default:
                return true;
        }
    }

    private async Task OnSaveClicked()
    {
        // DB kayýt buraya gelecek
        //await Shell.Current.GoToAsync("//Home");
    }
}