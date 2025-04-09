using SystemLosowaniaOsobyDoOdpowiedzi.Models;
using SystemLosowaniaOsobyDoOdpowiedzi.Services;

namespace SystemLosowaniaOsobyDoOdpowiedzi.Views
{
    public partial class EditClassPage : ContentPage
    {
        private Class _selectedClass;
        private FileService _fileService = new FileService();

        public EditClassPage(Class selectedClass)
        {
            InitializeComponent();
            _selectedClass = selectedClass;
            ClassNameEntry.Text = _selectedClass.ClassName;
            StudentsListView.ItemsSource = _selectedClass.Students;
        }

        private void OnAddStudentButtonClicked(object sender, EventArgs e)
        {
            if (_selectedClass.Students.Count >= 40)
            {
                DisplayAlert("B³¹d", "Klasa mo¿e mieæ maksymalnie 40 uczniów.", "OK");
                return;
            }

            int newNumber = FindLowestAvailableNumber(_selectedClass.Students);

            var newStudent = new Student
            {
                Name = "Nowy uczeñ",
                Number = newNumber
            };
            _selectedClass.Students.Add(newStudent);
            StudentsListView.ItemsSource = null;
            StudentsListView.ItemsSource = _selectedClass.Students.OrderBy(s => s.Number).ToList();
        }
        private int FindLowestAvailableNumber(List<Student> students)
        {
            if (students.Count == 0)
                return 1;

 
            var sortedStudents = students.OrderBy(s => s.Number).ToList();

            for (int i = 0; i < sortedStudents.Count; i++)
            {
                int expectedNumber = i + 1;
                if (sortedStudents[i].Number != expectedNumber)
                {
                    return expectedNumber;
                }
            }

            return sortedStudents.Count + 1;
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var student = button.BindingContext as Student;
            _selectedClass.Students.Remove(student);
            StudentsListView.ItemsSource = null;
            StudentsListView.ItemsSource = _selectedClass.Students;
        }

        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            _selectedClass.ClassName = ClassNameEntry.Text;
            var classes = _fileService.LoadClasses();
            var classToUpdate = classes.FirstOrDefault(c => c.ClassName == _selectedClass.ClassName);
            if (classToUpdate != null)
            {
                classes.Remove(classToUpdate);
            }
            classes.Add(_selectedClass);
            _fileService.SaveClasses(classes);
            DisplayAlert("Sukces", "Klasa zosta³a zapisana.", "OK");
        }
    }
}