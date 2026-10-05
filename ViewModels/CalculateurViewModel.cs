using System.Collections.ObjectModel;

namespace CalculateurAge.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
    // Champs prives : la vraie donnee.
    private string _nom = "";
    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);
    private string _resultat = "";
    private string _message = "";
    private string _anniversaire = "";
    private bool _resultatVisible;

    // Proprietes publiques : ce que le XAML voit.
    public string Nom
    {
        get => _nom;
        set
        {
            if (SetField(ref _nom, value))
                CalculerCommand.Rafraichir();
        }
    }

    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set
        {
            if (SetField(ref _dateNaissance, value))
                CalculerCommand.Rafraichir();
        }
    }

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    public string Message
    {
        get => _message;
        set => SetField(ref _message, value);
    }

    public string Anniversaire
    {
        get => _anniversaire;
        set => SetField(ref _anniversaire, value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set => SetField(ref _resultatVisible, value);
    }

    public ObservableCollection<string> Historique { get; } = new();

    public bool HistoriqueVisible => Historique.Count > 0;

    // Lie a Button.Command dans le XAML.
    public RelayCommand CalculerCommand { get; }
    public RelayCommand EffacerCommand { get; }

    public CalculateurViewModel()
    {
        CalculerCommand = new RelayCommand(
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom)
                  && DateNaissance.Date <= DateTime.Today);
        EffacerCommand = new RelayCommand(Effacer);
        Historique.CollectionChanged +=
            (s, e) => OnPropertyChanged(nameof(HistoriqueVisible));
    }

    // La logique metier : aucun controle d'interface ici.
    private void Calculer()
    {
        int age = DateTime.Today.Year - DateNaissance.Year;
        if (DateNaissance.Date > DateTime.Today.AddYears(-age)) age--;

        Resultat = $"{Nom}, vous avez {age} ans";
        Message = age >= 18 ? "Majeur" : "Mineur";
        Historique.Insert(0, $"{Nom} : {age} ans (ne le {DateNaissance:dd/MM/yyyy})");
        Anniversaire = TexteAnniversaire(DateNaissance);
        ResultatVisible = true;
    }

    private static string TexteAnniversaire(DateTime naissance)
    {
        DateTime aujourdhui = DateTime.Today;
        DateTime prochain = AnniversaireEn(naissance, aujourdhui.Year);
        if (prochain < aujourdhui)
            prochain = AnniversaireEn(naissance, aujourdhui.Year + 1);

        int jours = (prochain - aujourdhui).Days;
        if (jours == 0) return "Joyeux anniversaire !";
        if (jours == 1) return "Prochain anniversaire demain";
        return $"Prochain anniversaire dans {jours} jours";
    }

    // Le 29 fevrier devient le 28 les annees non bissextiles.
    private static DateTime AnniversaireEn(DateTime naissance, int annee)
    {
        int jour = naissance.Month == 2 && naissance.Day == 29
                   && !DateTime.IsLeapYear(annee) ? 28 : naissance.Day;
        return new DateTime(annee, naissance.Month, jour);
    }

    // Remet tous les champs a zero.
    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = "";
        Message = "";
        Anniversaire = "";
        ResultatVisible = false;
    }
}
