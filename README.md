# CalculateurAge

TP1 .NET MAUI : calculateur d'age, routing Shell et MVVM.

## Lancer le projet

    dotnet workload install maui
    dotnet restore
    dotnet build -t:Run -f net8.0-windows10.0.19041.0

Pour Android : `dotnet build -t:Run -f net8.0-android` (emulateur ou telephone branche).

## Structure

    Views/ResultatPage        seconde page, liee au ViewModel
    ViewModels/               BaseViewModel, RelayCommand, CalculateurViewModel
    MainPage                  page principale, liee au ViewModel
    AppShell                  navigation et route ResultatPage

## Fonctionnalites ajoutees (Activite 6)

- Message Majeur ou Mineur
- Commande Effacer
- Jours restants avant le prochain anniversaire
- Historique des calculs
- Refus des dates de naissance futures
- Resultat envoye a ResultatPage par le ViewModel, sans parametre d'URL

## Historique git

Un commit par etape : etape 0, phases A, B, C, puis une fonctionnalite par commit.
