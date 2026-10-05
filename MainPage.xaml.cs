using CalculateurAge.Views;

namespace CalculateurAge;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCalculerClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryNom.Text))
        {
            await DisplayAlert("Erreur", "Entrez un nom", "OK");
            return;
        }

        DateTime d = pickerDate.Date;
        int age = DateTime.Today.Year - d.Year;
        if (d.Date > DateTime.Today.AddYears(-age)) age--;

        // Le nom est encode pour supporter espaces et caracteres speciaux.
        string nom = Uri.EscapeDataString(entryNom.Text);
        await Shell.Current.GoToAsync(
            $"{nameof(ResultatPage)}?nom={nom}&age={age}");
    }
}
