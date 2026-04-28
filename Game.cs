namespace MauiApp1.Models;

public class Game
{
    public Player CurrentPlayer { get; private set; }
    public Theme CurrentTheme { get; private set; }
    public double DurationMs { get; private set; }

    public event Action<string>? OnShowSymbol;
    public event Action? OnHideSymbol;
    public event Action? OnGameFinished;

    private bool isRunning;
    private Random rng = new Random();

    public Game(Player player, Theme theme, double durationMs)
    {
        CurrentPlayer = player;
        CurrentTheme = theme;
        DurationMs = durationMs;
    }

    public async void Start()
    {
        isRunning = true;
        var start = DateTime.Now;

        while (isRunning && (DateTime.Now - start).TotalMilliseconds < DurationMs)
        {
            OnShowSymbol?.Invoke(CurrentPlayer.Symbol);

            // ?????? ????? 1.5 ??????? ?????? 0.5
            await Task.Delay(1500);

            OnHideSymbol?.Invoke();

            // ????? ????? ????????? 300–600 ?? ?????? 500–2000
            int pause = rng.Next(300, 600);
            await Task.Delay(pause);
        }

        isRunning = false;
        OnGameFinished?.Invoke();
    }

    public void Stop() => isRunning = false;
}