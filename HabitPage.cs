using MauiApp1.Models;
using MauiApp1.ViewModels;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;

namespace MauiApp1;

public class HabitPage : ContentPage
{
    private readonly MainViewModel vm = new MainViewModel();

    // Левая часть
    private readonly Label titleLabel;
    private readonly VerticalStackLayout habitsList;

    // Правая панель — кнопки категорий
    private readonly VerticalStackLayout rightMenu;

    public HabitPage()
    {
        Title = "HabitFlow";
        BackgroundColor = Color.FromArgb("#f9f9f9");

        // --- Заголовок категории ---
        titleLabel = new Label
        {
            FontSize = 28,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 10)
        };

        // --- Список привычек ---
        habitsList = new VerticalStackLayout { Spacing = 8 };

        var leftScroll = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Children = { titleLabel, habitsList }
            },
            Margin = new Thickness(20)
        };

        // --- Правое меню с эмодзи ---
        rightMenu = new VerticalStackLayout
        {
            BackgroundColor = Color.FromArgb("#eeeeee"),
            VerticalOptions = LayoutOptions.Fill,
            Spacing = 10,
            Padding = new Thickness(5)
        };

        foreach (var category in vm.Categories)
        {
            var cat = category; // локальная копия для замыкания

            var emojiLabel = new Label
            {
                Text = cat.Emoji,
                FontSize = 30,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            var frame = new Border
            {
                WidthRequest = 60,
                HeightRequest = 60,
                StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(15) },
                BackgroundColor = Colors.White,
                Padding = new Thickness(5),
                Content = emojiLabel,
                HorizontalOptions = LayoutOptions.Center
            };

            var tap = new TapGestureRecognizer();
            tap.Tapped += (s, e) =>
            {
                vm.SelectedCategory = cat;
                RefreshHabits();
                RefreshMenuHighlight(rightMenu, cat);
            };
            frame.GestureRecognizers.Add(tap);

            rightMenu.Children.Add(frame);
        }

        // --- Grid: левая часть + правое меню ---
        var grid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = new GridLength(80) }
            }
        };

        Grid.SetColumn(leftScroll, 0);
        Grid.SetColumn(rightMenu, 1);
        grid.Children.Add(leftScroll);
        grid.Children.Add(rightMenu);

        Content = grid;

        // Начальное состояние
        RefreshHabits();
        RefreshMenuHighlight(rightMenu, vm.SelectedCategory);
    }

    // Перерисовываем список привычек
    private void RefreshHabits()
    {
        habitsList.Children.Clear();
        titleLabel.Text = vm.SelectedCategory.Title;

        foreach (var habit in vm.SelectedCategory.Habits)
        {
            var check = new CheckBox { IsChecked = habit.IsCompleted };
            check.CheckedChanged += (s, e) => habit.IsCompleted = e.Value;

            var nameLabel = new Label
            {
                Text = habit.Name,
                FontSize = 18,
                VerticalOptions = LayoutOptions.Center
            };

            var row = new Border
            {
                StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(10) },
                Padding = new Thickness(10),
                BackgroundColor = Colors.White,
                Margin = new Thickness(0, 4),
                Content = new HorizontalStackLayout
                {
                    Spacing = 10,
                    Children = { check, nameLabel }
                }
            };

            habitsList.Children.Add(row);
        }
    }

    // Подсвечиваем активную категорию в меню
    private void RefreshMenuHighlight(VerticalStackLayout menu, HabitCategory active)
    {
        for (int i = 0; i < vm.Categories.Count; i++)
        {
            if (menu.Children[i] is Border frame)
            {
                frame.BackgroundColor = vm.Categories[i] == active
                    ? Color.FromArgb("#d0f0c0")
                    : Colors.White;
            }
        }
    }
}