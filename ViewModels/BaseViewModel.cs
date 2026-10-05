using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CalculateurAge.ViewModels;

// Classe mere de tous les ViewModels.
public class BaseViewModel : INotifyPropertyChanged
{
    // Le moteur de binding s'abonne a cet evenement.
    public event PropertyChangedEventHandler PropertyChanged;

    // Previent la vue que cette propriete a change.
    protected void OnPropertyChanged(
        [CallerMemberName] string nom = null)
        => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(nom));

    // Affecte une valeur ET notifie. Renvoie true si la valeur a change.
    protected bool SetField<T>(ref T champ, T valeur,
        [CallerMemberName] string nom = null)
    {
        // Evite les notifications inutiles et les boucles en TwoWay.
        if (EqualityComparer<T>.Default.Equals(champ, valeur))
            return false;
        champ = valeur;
        OnPropertyChanged(nom);
        return true;
    }
}
