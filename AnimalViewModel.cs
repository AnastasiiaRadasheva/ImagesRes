using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;

namespace MauiApp1
{



    public class AnimalViewModel : INotifyPropertyChanged
    {
        private string _currentAnimalImage = string.Empty;

        public string CurrentAnimalImage
        {
            get => _currentAnimalImage;
            set
            {
                _currentAnimalImage = value;
                OnPropertyChanged();
            }
        }

        public AnimalViewModel()
        {
            // Kui AppResources.resx on projekti juurkaustas:
            CurrentAnimalImage = AppResources.AnimalCat;
        }

        public void ChangeAnimal(string type)
        {
            CurrentAnimalImage = type switch
            {
                "Dog" => AppResources.AnimalDog,
                "Fish" => AppResources.AnimalFish,
                _ => AppResources.AnimalCat
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }






}
