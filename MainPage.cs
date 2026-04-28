namespace MauiApp1;

public partial class MainPage : ContentPage
{
    readonly AnimalViewModel vm = new AnimalViewModel();

    public MainPage()
    {
        BindingContext = vm;

        var image = new Image
        {
            HeightRequest = 200,
            WidthRequest = 200
        };
        image.SetBinding(Image.SourceProperty, nameof(vm.CurrentAnimalImage));

        var btnCat = new Button { Text = "Näita Kassi", HorizontalOptions = LayoutOptions.Fill };
        var btnDog = new Button { Text = "Näita Koera", HorizontalOptions = LayoutOptions.Fill };
        var btnFish = new Button { Text = "Näita Kala", HorizontalOptions = LayoutOptions.Fill };

        btnCat.Clicked += (s, e) => vm.ChangeAnimal("Cat");
        btnDog.Clicked += (s, e) => vm.ChangeAnimal("Dog");
        btnFish.Clicked += (s, e) => vm.ChangeAnimal("Fish");

        Content = new VerticalStackLayout
        {
            Spacing = 20,
            Padding = new Thickness(30),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Children = { image, btnCat, btnDog, btnFish }
        };
    }
}