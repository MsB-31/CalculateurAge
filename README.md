# CalculateurAge

Application multiplateforme .NET MAUI qui calcule l'âge à partir d'un nom et d'une date de naissance. L'interface affiche également le statut majeur/mineur, le délai avant le prochain anniversaire et les calculs effectués pendant la session.

## Fonctionnalités

- Saisie d'un nom et d'une date de naissance avec un `DatePicker`.
- Calcul de l'âge révolu, en tenant compte du passage de l'anniversaire dans l'année courante.
- Refus d'une date de naissance future et désactivation du calcul lorsque le nom est vide.
- Affichage du statut « majeur » à partir de 18 ans, sinon « mineur ».
- Compte à rebours jusqu'au prochain anniversaire et message dédié le jour de l'anniversaire.
- Gestion du 29 février : dans une année non bissextile, l'anniversaire est évalué au dernier jour valide de février.
- Effacement des champs et des résultats.
- Historique des calculs, du plus récent au plus ancien. Il reste en mémoire pendant l'exécution de l'application ; il n'est pas enregistré sur disque et n'est pas effacé par la commande « Effacer ».

Les calculs sont effectués localement dans l'application. Le projet ne met en place ni compte utilisateur, ni synchronisation, ni stockage persistant.

## Technologies

- C# et .NET 10
- .NET MAUI et XAML
- Modèle MVVM léger, implémenté dans le projet avec `INotifyPropertyChanged`, `BaseViewModel` et `RelayCommand`
- `ObservableCollection<string>` pour mettre à jour l'historique lié à l'interface

## Prérequis

- SDK .NET 10 avec la charge de travail MAUI installée.
- Pour Android : Android SDK et un appareil ou émulateur configuré.
- Pour iOS ou Mac Catalyst : macOS et les outils Apple requis par .NET MAUI.
- Pour Windows : Windows avec les composants de développement .NET MAUI correspondants.

Le fichier projet cible Android par défaut. Les cibles iOS et Mac Catalyst sont ajoutées sur les systèmes non Linux ; la cible Windows est ajoutée sur Windows. Sous Linux, la cible déclarée est donc `net10.0-android`.

## Restaurer, compiler et lancer

Depuis le dossier contenant `CalculateurAge.csproj` :

```bash
dotnet workload restore
dotnet restore
dotnet build -f net10.0-android
```

Pour lancer l'application sur un émulateur ou appareil Android détecté :

```bash
dotnet build -t:Run -f net10.0-android
```

Les autres cibles disponibles dépendent de l'OS hôte et des SDK installés. Les cibles iOS et Mac Catalyst nécessitent macOS ; la cible Windows nécessite Windows.

## Organisation du projet

```text
CalculateurAge.csproj       Configuration MAUI et frameworks cibles
MainPage.xaml               Interface principale
MainPage.xaml.cs            Création et liaison du ViewModel
ViewModels/
  BaseViewModel.cs           Notifications de changement de propriétés
  CalculateurViewModel.cs    Validation et logique de calcul
  RelayCommand.cs            Commandes liées aux contrôles MAUI
Views/
  ResultatPage.xaml          Page de résultat secondaire
  ResultatPage.xaml.cs       Paramètres de navigation Shell de cette page
AppShell.xaml                Navigation et page d'accueil
MauiProgram.cs               Configuration et démarrage de l'application
Platforms/                   Points d'entrée et configuration par plateforme
Resources/                   Icônes, polices, images et styles
```

Le calcul et ses résultats sont actuellement présentés directement dans `MainPage`. `ResultatPage` est enregistrée comme route Shell, mais elle n'est pas utilisée par le flux de calcul principal.

## Tests

Aucun projet de tests automatisés n'est actuellement présent dans le dépôt.
