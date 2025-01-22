using Note;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace NoteApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendInput(object sender, RoutedEventArgs e)
        {
            string? titleInput = TitleInput.Text;
            string? contentInput = TitleInput.Text;

            if (string.IsNullOrWhiteSpace(titleInput))
            {
                MessageBox.Show("Inserisci del testo!", "Errore", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AddNote(titleInput, contentInput);

            ClosePanel(sender, e);
        }

        private void AddNote(string titleInput, string contentInput)
        {
            NotePanel notePanel = new()
            {
                Title = titleInput,
                Content = contentInput
            };

            NoteList.Children.Add(notePanel);

            System.Diagnostics.Debug.WriteLine($"Aggiunto bottone: {notePanel.Title}");
        }

        private void OpenPanel(object sender, RoutedEventArgs e)
        {
            // Mostra il pannello
            OverlayPanel.Visibility = Visibility.Visible;

            // Esegui animazione
            var storyboard = (Storyboard)FindResource("ShowPanelAnimation");
            storyboard.Begin();
        }

        private void ClosePanel(object sender, RoutedEventArgs e)
        {
            // Esegui animazione
            var storyboard = (Storyboard)FindResource("HidePanelAnimation");
            storyboard.Completed += (s, ev) =>
            {
                // Nasconde il pannello solo dopo l'animazione
                OverlayPanel.Visibility = Visibility.Collapsed;
            };
            storyboard.Begin();
            TitleInput.Text = string.Empty;
            ContentInput.Text = string.Empty;
        }
    }
}