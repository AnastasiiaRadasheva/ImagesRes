using Microsoft.Maui.Layouts;
using AppTheme = MauiApp1.Models.Theme;
using AppGame = MauiApp1.Models.Game;
using AppPlayer = MauiApp1.Models.Player;

namespace MauiApp1;

public class Main11 : ContentPage
{
    private AppGame? game;
    private int counter = 0;
    private int hits = 0;
    private bool symbolVisible = false;
    private Random rng = new Random();

    private readonly Picker themePicker;
    private readonly Entry symbolEntry;
    private readonly Slider durationSlider;
    private readonly Label counterLabel;
    private readonly Label hitsLabel;
    private readonly Label symbolLabel;
    private readonly Border symbolBorder;  // кликабельная область

    public Main11()
    {
        themePicker = new Picker { Title = "Vali teema" };
        themePicker.ItemsSource = new List<AppTheme>
        {
            new AppTheme("Hele",   Colors.White,     Colors.Black,    "OpenSansRegular"),
            new AppTheme("Tume",   Colors.Black,     Colors.White,    "OpenSansRegular"),
            new AppTheme("Sinine", Colors.LightBlue, Colors.DarkBlue, "OpenSansRegular")
        };
        themePicker.SelectedIndexChanged += ThemePicker_SelectedIndexChanged;
        themePicker.SelectedIndex = 0;

        symbolEntry = new Entry { Placeholder = "Sisesta sümbol" };

        durationSlider = new Slider { Minimum = 5000, Maximum = 60000, Value = 20000 };

        var durationLabel = new Label { Text = "Mängu kestus (ms)", FontSize = 12 };

        var startButton = new Button { Text = "Alusta mängu" };
        startButton.Clicked += OnStartClicked;

        counterLabel = new Label { Text = "Ilmunud: 0", FontSize = 16 };
        hitsLabel = new Label { Text = "Tabasid: 0", FontSize = 16, TextColor = Colors.Green };

        // Символ внутри большого Border — он и есть зона нажатия
        symbolLabel = new Label
        {
            FontSize = 48,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
        };

        symbolBorder = new Border
        {
            IsVisible = false,
            WidthRequest = 120,   // фиксированная широкая область
            HeightRequest = 120,
            BackgroundColor = Colors.Transparent,
            StrokeThickness = 0,
            Padding = new Thickness(20),
            Content = symbolLabel,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start,
        };

        // Нажатие на всю область Border
        var tap = new TapGestureRecognizer();
        tap.Tapped += OnSymbolTapped;
        symbolBorder.GestureRecognizers.Add(tap);

        var topStack = new VerticalStackLayout
        {
            Spacing = 10,
            Children = { themePicker, symbolEntry, durationSlider, durationLabel, startButton, counterLabel, hitsLabel }
        };

        AbsoluteLayout.SetLayoutBounds(topStack, new Rect(0, 0, 1, 0.4));
        AbsoluteLayout.SetLayoutFlags(topStack, AbsoluteLayoutFlags.All);

        var rootLayout = new AbsoluteLayout { Padding = new Thickness(10) };
        rootLayout.Children.Add(topStack);
        rootLayout.Children.Add(symbolBorder);

        Content = rootLayout;
    }

    private void ThemePicker_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (themePicker.SelectedItem is AppTheme theme)
            theme.Apply(this);
    }

    private async void OnStartClicked(object? sender, EventArgs e)
    {
        if (themePicker.SelectedItem is not AppTheme theme)
        {
            await DisplayAlertAsync("Viga", "Palun vali teema", "OK");
            return;
        }

        var player = new AppPlayer("Mängija", symbolEntry.Text ?? "★");

        if (game != null)
        {
            game.OnShowSymbol -= ShowSymbol;
            game.OnHideSymbol -= HideSymbol;
            game.OnGameFinished -= GameFinished;
            game.Stop();
        }

        game = new AppGame(player, theme, durationSlider.Value);
        game.OnShowSymbol += ShowSymbol;
        game.OnHideSymbol += HideSymbol;
        game.OnGameFinished += GameFinished;

        counter = 0;
        hits = 0;
        counterLabel.Text = "Ilmunud: 0";
        hitsLabel.Text = "Tabasid: 0";

        game.Start();
    }

    private void OnSymbolTapped(object? sender, TappedEventArgs e)
    {
        if (!symbolVisible) return;

        hits++;
        hitsLabel.Text = $"Tabasid: {hits}";
        symbolBorder.IsVisible = false;
        symbolVisible = false;
        symbolLabel.TextColor = Colors.Green;
    }

    private void ShowSymbol(string symbol)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            counter++;
            counterLabel.Text = $"Ilmunud: {counter}";

            // случайная позиция — с учётом размера области 120x120
            double x = rng.NextDouble() * 0.85;
            double y = rng.NextDouble() * 0.5 + 0.45;

            symbolLabel.Text = symbol;
            symbolLabel.TextColor = Colors.Red;

            AbsoluteLayout.SetLayoutBounds(symbolBorder, new Rect(x, y, -1, -1));
            AbsoluteLayout.SetLayoutFlags(symbolBorder, AbsoluteLayoutFlags.PositionProportional);
            symbolBorder.IsVisible = true;
            symbolVisible = true;
        });
    }

    private void HideSymbol()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            symbolBorder.IsVisible = false;
            symbolVisible = false;
        });
    }

    private void GameFinished()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            symbolBorder.IsVisible = false;
            int missed = counter - hits;
            await DisplayAlertAsync(
                "Mäng läbi",
                $"Ilmus: {counter}\nTabasid: {hits}\nMööda: {missed}",
                "OK"
            );
        });
    }
}