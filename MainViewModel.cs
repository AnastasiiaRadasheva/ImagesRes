using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MauiApp1.Models;

namespace MauiApp1.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public ObservableCollection<HabitCategory> Categories { get; set; }

    private HabitCategory _selectedCategory = null!;
    public HabitCategory SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            if (_selectedCategory != value)
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }
    }

    public MainViewModel()
    {
        Categories = new ObservableCollection<HabitCategory>
        {
            new HabitCategory { Emoji = "🌅", Title = "Hommik", Habits =
            {
                new Habit { Name = "Ärka enne 7:00" },
                new Habit { Name = "Mediteeri 10 minutit" }
            }},
            new HabitCategory { Emoji = "🍎", Title = "Toitumine", Habits =
            {
                new Habit { Name = "Tervislik hommikusöök" },
                new Habit { Name = "Ilma suhkruta" }
            }},
            new HabitCategory { Emoji = "🏃", Title = "Treening", Habits =
            {
                new Habit { Name = "Jookse 3 km" },
                new Habit { Name = "Venitus 5 minutit" }
            }},
            new HabitCategory { Emoji = "📖", Title = "Õppimine", Habits =
            {
                new Habit { Name = "Loe 10 lk raamatut" },
                new Habit { Name = "Õpi 15 min midagi uut" }
            }},
            new HabitCategory { Emoji = "😴", Title = "Puhkus", Habits =
            {
                new Habit { Name = "Mine magama enne 23:00" },
                new Habit { Name = "Pole ekraani enne und" }
            }}
        };

        SelectedCategory = Categories.First();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string name = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
