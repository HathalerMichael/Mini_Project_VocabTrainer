using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LearningApp.Data;

namespace LearningApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{

    public MainWindowViewModel()
    {
        _newGermanWord = string.Empty;
        _newTranslationWord = string.Empty;
        _newLanguage = string.Empty;
        _newExampleSentence = string.Empty;
        
        _difficultyLevel = 0;
        _newVocabSetName = string.Empty;
        _currentCardIndex = 0;
        _showTranslation = false;

        LoadVocabSetsAsync();
    }

    private async void LoadVocabSetsAsync()
    {
        var sets = await VocabRepository.GetAllVocabSetsAsync();
        VocabSets = new ObservableCollection<VocabSet>(sets);

        if (VocabSets.Count > 0)
        {
            SelectedVocabSet = VocabSets[0];
        }
    }

    [ObservableProperty]
    private ObservableCollection<VocabSet> _vocabSets = new();

    [ObservableProperty]
    private VocabSet? _selectedVocabSet;

    [ObservableProperty]
    private ObservableCollection<VocabCard> _currentVocabCards = new();

    [ObservableProperty]
    private VocabCard? _currentCard;

    [ObservableProperty]
    private int _currentCardIndex;

    [ObservableProperty]
    private bool _showTranslation;

    [ObservableProperty]
    private string _newGermanWord;

    [ObservableProperty]
    private string _newTranslationWord;

    [ObservableProperty]
    private string _newLanguage;

    [ObservableProperty]
    private string _newExampleSentence;

    [ObservableProperty]
    private int _difficultyLevel;

    [ObservableProperty]
    private ObservableCollection<string> _availableLanguages = new()
    {
        "Englisch",
        "Französisch",
        "Spanisch",
        "Italienisch"
    };

    [ObservableProperty]
    private string _selectedLanguage = string.Empty;

    [ObservableProperty]
    private string _newVocabSetName = string.Empty;

    partial void OnSelectedVocabSetChanged(VocabSet? value)
    {
        if (value != null)
        {
            LoadVocabCardsAsync(value.Id);
        }
    }

    private async void LoadVocabCardsAsync(int setId)
    {
        var cards = await VocabRepository.GetVocabCardsBySetIdAsync(setId);
        CurrentVocabCards = new ObservableCollection<VocabCard>(cards);
        CurrentCardIndex = 0;
        ShowTranslation = false;
        UpdateCurrentCard();
    }

    private void UpdateCurrentCard()
    {
        if (CurrentVocabCards.Count > 0 && CurrentCardIndex >= 0 && CurrentCardIndex < CurrentVocabCards.Count)
        {
            CurrentCard = CurrentVocabCards[CurrentCardIndex];
        }
        else
        {
            CurrentCard = null;
        }
    }

    [RelayCommand]
    private void NextCard()
    {
        if (CurrentCardIndex < CurrentVocabCards.Count - 1)
        {
            CurrentCardIndex++;
            ShowTranslation = false;
            UpdateCurrentCard();
        }
    }

    [RelayCommand]
    private void PreviousCard()
    {
        if (CurrentCardIndex > 0)
        {
            CurrentCardIndex--;
            ShowTranslation = false;
            UpdateCurrentCard();
        }
    }

    [RelayCommand]
    private void ToggleTranslation()
    {
        ShowTranslation = !ShowTranslation;
    }

    [RelayCommand]
    private async Task AddVocabSet()
    {
        if (string.IsNullOrWhiteSpace(NewVocabSetName)) return;

        var newSet = new VocabSet { Name = NewVocabSetName };
        await VocabRepository.AddVocabSetAsync(newSet);

        VocabSets.Add(newSet);
        SelectedVocabSet = newSet;
        NewVocabSetName = string.Empty;
    }

    [RelayCommand]
    private async Task AddVocab()
    {
        if (SelectedVocabSet == null) return;
        if (string.IsNullOrWhiteSpace(NewLanguage)) return;

        if (NewGermanWord == null && NewTranslationWord == null && NewLanguage == null)
        {
            return;
        }
        {
            var newCard = new VocabCard
            {
                Original = NewGermanWord,
                Translation = NewTranslationWord,
                Language = NewLanguage,
                ExampleSentence = NewExampleSentence,
                DifficultyLevel = DifficultyLevel,
                VocabSetId = SelectedVocabSet.Id
            };

            await VocabRepository.AddVocabCardAsync(newCard);

            CurrentVocabCards.Add(newCard);

            NewGermanWord = string.Empty;
            NewTranslationWord = string.Empty;
            NewExampleSentence = string.Empty;
            DifficultyLevel = 0;

            if (CurrentCard == null)
            {
                UpdateCurrentCard();
            }
        }
    }
}
