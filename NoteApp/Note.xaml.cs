using System.Windows;
using System.Windows.Controls;

namespace Note
{
    public partial class NotePanel : UserControl
    {
        public required string Title { get; set; }

        public required string Content { get; set; }

        public NotePanel()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OpenNote(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Nota: {Title}");
        }

        private void ModifyNote(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Modifica Nota: {Title}");
        }

        private void DeleteNote(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Elimina Nota: {Title}");
        }


        //private void OpenNote(object sender, RoutedEventArgs e)
        //{
        //    Button? clickedButton = sender as Button;
        //    MessageBox.Show($"Hai cliccato {clickedButton!.Content}");
        //}

        //private void ModifyPanel(object sender, RoutedEventArgs e)
        //{
        //    if (NoteList.Children.Count > 0)
        //    {
        //        Border panelBorder = (Border)NoteList.Children[^1];
        //        Grid panelGrid = (Grid)panelBorder.Child;
        //        Button NoteButton = (Button)panelGrid.Children[0];
        //        NoteButton.Content = "Nota modificata";
        //    }
        //}

        //private void RemovePanel(object sender, RoutedEventArgs e)
        //{
        //    if (NoteList.Children.Count > 0)
        //    {
        //        NoteList.Children.RemoveAt(NoteList.Children.Count - 1);
        //    }
        //}
    }
}
