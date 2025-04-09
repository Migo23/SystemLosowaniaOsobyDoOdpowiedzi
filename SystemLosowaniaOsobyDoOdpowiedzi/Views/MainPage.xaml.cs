using SystemLosowaniaOsobyDoOdpowiedzi.Models;
using SystemLosowaniaOsobyDoOdpowiedzi.Services;
using System.Linq;

namespace SystemLosowaniaOsobyDoOdpowiedzi.Views
{
    public partial class MainPage : ContentPage
    {
        private List<Class> _classes;
        private FileService _fileService = new FileService();
        private LotteryService _lotteryService = new LotteryService();

        public MainPage()
        {
            InitializeComponent();
            LoadClasses();
            GenerateLuckyNumber();
        }

        private void LoadClasses()
        {
            _classes = _fileService.LoadClasses();
            ClassPicker.ItemsSource = _classes.Select(c => c.ClassName).ToList();
        }

        private void GenerateLuckyNumber()
        {
            if (_classes.Any())
            {
                var selectedClass = _classes.First();
                selectedClass.LuckyNumber = _lotteryService.GenerateLuckyNumber(selectedClass.Students.Count);
                LuckyNumberLabel.Text = $"Szcz�liwy numerek: {selectedClass.LuckyNumber}";
            }
        }

        private void OnDrawButtonClicked(object sender, EventArgs e)
        {
            var selectedClassName = ClassPicker.SelectedItem as string;
            var selectedClass = _classes.FirstOrDefault(c => c.ClassName == selectedClassName);

            if (selectedClass != null)
            {
                if (selectedClass.Students == null || !selectedClass.Students.Any())
                {
                    DisplayAlert("B��d", "Ta klasa nie ma �adnych uczni�w. Dodaj uczni�w, aby kontynuowa�.", "OK");
                    return;
                }

                var student = _lotteryService.DrawStudent(selectedClass);
                if (student != null)
                {
                    ResultLabel.Text = $"Wylosowano: {student.Name}";
                }
                else
                {
                    DisplayAlert("B��d", "Nie uda�o si� wylosowa� ucznia.", "OK");
                }
            }
            else
            {
                DisplayAlert("B��d", "Nie wybrano klasy.", "OK");
            }
        }

        private async void OnViewClassButtonClicked(object sender, EventArgs e)
        {
            var selectedClassName = ClassPicker.SelectedItem as string;
            var selectedClass = _classes.FirstOrDefault(c => c.ClassName == selectedClassName);
            if (selectedClass != null)
            {
                await Navigation.PushAsync(new ClassViewPage(selectedClass));
            }
        }

        private async void OnEditButtonClicked(object sender, EventArgs e)
        {
            var selectedClassName = ClassPicker.SelectedItem as string;
            var selectedClass = _classes.FirstOrDefault(c => c.ClassName == selectedClassName);
            if (selectedClass != null)
            {
                await Navigation.PushAsync(new EditClassPage(selectedClass));
            }
        }

        private async void OnAddClassButtonClicked(object sender, EventArgs e)
        {
            string className = await DisplayPromptAsync("Dodaj klas�", "Podaj nazw� klasy:");
            if (!string.IsNullOrWhiteSpace(className))
            {
                var newClass = new Class { ClassName = className };
                _classes.Add(newClass);
                _fileService.SaveClasses(_classes);
                LoadClasses();
                await Navigation.PushAsync(new EditClassPage(newClass)); 
            }
        }
    }
}