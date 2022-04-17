using System.Windows;

namespace LibraryExample.Views
{
    /// <summary>
    /// Логика взаимодействия для BookEditorView.xaml
    /// </summary>
    public partial class BookEditorView : Window
    {
        public BookEditorView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // IsDefault по какой-то причине не закрывал окно и не ставил DialogResult = true.
            // Чтобы не заморачиваться лишний раз, решил сделать так.
            DialogResult = true;

            Close();
        }
    }
}
