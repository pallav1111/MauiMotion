using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiMotion.Sample;

public partial class ShowcaseViewModel : ObservableObject
{
    public ObservableCollection<string> Items { get; set; } = [];

    public ShowcaseViewModel()
    {
        // Populate with dummy data
        for (var i = 1; i <= 10; i++)
        {
            Items.Add($"Motion Card #{i}");
        }
    }

    [RelayCommand]
    private void Reload()
    {
        // Simple hack to refresh the list and re-trigger entrance animations
        var cache = new List<string>(Items);
        Items.Clear();
        foreach (var item in cache) Items.Add(item);
    }
}