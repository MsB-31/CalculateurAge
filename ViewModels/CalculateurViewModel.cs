namespace CalculateurAge.ViewModels;

// Contient l'ÉTAT de l'écran et les ACTIONS possibles.
public class CalculateurViewModel : BaseViewModel
{
    // Champs privés : la vraie donnée.
    private string _nom = "";
    private string _erreur = "";
    private DateTime _dateNaissance
        = DateTime.Today.AddYears(-20);
    private string _resultat = "";

    // Propriétés publiques : ce que le XAML voit.
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
            {
                Erreur = value.Date > DateTime.Today
                    ? "La date de naissance ne peut pas être dans le futur"
                    : "";
                CalculerCommand.Rafraichir();
            }
        }
    }
    public string Erreur
    {
        get => _erreur;
        set
        {
            if (SetField(ref _erreur, value))
                OnPropertyChanged(nameof(ErreurVisible));
        }
    }
    public bool ErreurVisible => !string.IsNullOrEmpty(Erreur);
    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    // Lié à Button.Command dans le XAML.
    public RelayCommand CalculerCommand { get; }
    public RelayCommand EffacerCommand { get; }

    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = "";
    }

    public CalculateurViewModel()
    {
        CalculerCommand = new RelayCommand(
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom)
                  && DateNaissance.Date <= DateTime.Today);
        EffacerCommand = new RelayCommand(Effacer);
    }

    // La logique métier : aucun contrôle d'interface ici.
    private void Calculer()
    {
        int age = DateTime.Today.Year - DateNaissance.Year;
        if (DateNaissance.Date > DateTime.Today.AddYears(-age)) age--;
        Resultat = $"{Nom}, vous avez {age} ans";
    }
}