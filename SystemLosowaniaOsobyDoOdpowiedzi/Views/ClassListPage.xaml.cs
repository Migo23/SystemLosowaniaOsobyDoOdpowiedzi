using SystemLosowaniaOsobyDoOdpowiedzi.Models;
using SystemLosowaniaOsobyDoOdpowiedzi.Services;

namespace SystemLosowaniaOsobyDoOdpowiedzi.Views
{
    public partial class ClassListPage : ContentPage
    {
        private Class _selectedClass;
        private FileService _fileService = new FileService();

        public ClassListPage(Class selectedClass)
        {
            InitializeComponent();
            _selectedClass = selectedClass;
            StudentsListView.ItemsSource = _selectedClass.Students;
        }

        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var classes = _fileService.LoadClasses();
            var classToUpdate = classes.FirstOrDefault(c => c.ClassName == _selectedClass.ClassName);
            if (classToUpdate != null)
            {
                classToUpdate.Students = _selectedClass.Students;
                _fileService.SaveClasses(classes);
            }
            DisplayAlert("Sukces", "Zmiany zosta³y zapisane.", "OK");
        }
    }
}