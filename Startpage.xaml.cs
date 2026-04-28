using MauiApp1;

namespace VertHorisNaidis;

public class StartPage : ContentPage
{
    public List<ContentPage> Lehed = new List<ContentPage>()
    {
        new MainPage(),
        new Main11(),
        new HabitPage()
    };

    public List<string> LeheNimed = new List<string>()
    {
        "Images",
        "Mäng",
        "HabitFlow"
    };


    public StartPage()
    {
        Title = "Avaleht";

        var vst = new VerticalStackLayout { Padding = 20, Spacing = 15 };

        for (int i = 0; i < Lehed.Count; i++)
        {
            int index = i;

            var nupp = new Button
            {
                Text = LeheNimed[i],
                FontSize = 18,
                BackgroundColor = Colors.AliceBlue,
                TextColor = Colors.Black,
                HeightRequest = 50,
                CornerRadius = 10
            };

            nupp.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(Lehed[index]);
            };

            vst.Add(nupp);
        }

        Content = new ScrollView { Content = vst };
    }
}