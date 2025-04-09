using SystemLosowaniaOsobyDoOdpowiedzi.Models;

namespace SystemLosowaniaOsobyDoOdpowiedzi.Views
{
    public partial class ClassViewPage : ContentPage
    {
        public ClassViewPage(Class selectedClass)
        {
            InitializeComponent();
            StudentsListView.ItemsSource = selectedClass.Students;
        }
    }
}