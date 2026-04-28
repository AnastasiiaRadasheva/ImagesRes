namespace MauiApp1.Models;

public class Theme
{
    public string Name { get; set; }
    public Color BackgroundColor { get; set; }
    public Color TextColor { get; set; }
    public string FontFamily { get; set; }

    public Theme(string name, Color background, Color text, string fontFamily)
    {
        Name = name;
        BackgroundColor = background;
        TextColor = text;
        FontFamily = fontFamily;
    }

    public override string ToString() => Name;

    public void Apply(ContentPage page)
    {
        page.BackgroundColor = BackgroundColor;

        Application.Current.Resources["GlobalLabelStyle"] = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontFamilyProperty, Value = FontFamily },
                new Setter { Property = Label.TextColorProperty, Value = TextColor }
            }
        };

        Application.Current.Resources["GlobalEntryStyle"] = new Style(typeof(Entry))
        {
            Setters =
            {
                new Setter { Property = Entry.FontFamilyProperty, Value = FontFamily },
                new Setter { Property = Entry.TextColorProperty, Value = TextColor }
            }
        };

        Application.Current.Resources["GlobalButtonStyle"] = new Style(typeof(Button))
        {
            Setters =
            {
                new Setter { Property = Button.FontFamilyProperty, Value = FontFamily },
                new Setter { Property = Button.TextColorProperty, Value = TextColor }
            }
        };
    }
}
