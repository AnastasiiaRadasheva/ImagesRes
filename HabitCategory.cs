using System.Collections.ObjectModel;

namespace MauiApp1.Models;

public class HabitCategory
{
    public string Emoji { get; set; } = "";
    public string Title { get; set; } = "";
    public ObservableCollection<Habit> Habits { get; set; } = new();
}
