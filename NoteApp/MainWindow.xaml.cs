using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace NoteApp
{
    public partial class MainWindow : Window
    {
        private readonly DatabaseHelper dbHelper; // Gestione database per salvare e recuperare le note.

        public MainWindow()
        {
            InitializeComponent();

            dbHelper = new DatabaseHelper();
            dbHelper.CreateTable(); // Assicura che la tabella esista nel database.

            LoadNotes(); // Carica tutte le note salvate al momento dell'avvio.
        }

        private void SendInput(object sender, RoutedEventArgs e)
        {
            string? titleInput = TitleInput.Text;
            string? contentInput = ContentInput.Text;

            // Controlla se il titolo è vuoto o nullo.
            if (string.IsNullOrWhiteSpace(TitleInput.Text))
            {
                TitleInput.Tag = "Error"; // Segnala un errore visivo all'utente.

                // Rimuove l'errore dopo un breve ritardo.
                Task.Delay(1000).ContinueWith(_ =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        TitleInput.Tag = null;
                    });
                });
                return;
            }

            CreateNote(titleInput, contentInput); // Crea una nuova nota.

            ClosePanel(sender, e); // Chiude il pannello di input.
        }

        private void CreateNote(string titleInput, string contentInput)
        {
            Note note = new()
            {
                Id = NoteList.Children.Count + 1, // Genera un ID basato sul numero di note esistenti.
                Title = titleInput,
                Content = contentInput,
                CreatedAt = DateTime.Now // Registra la data di creazione.
            };

            dbHelper.SaveNote(note); // Salva la nota nel database.
            NoteList.Children.Add(note); // Aggiunge la nota alla lista visiva.
        }

        private void OpenPanel(object sender, RoutedEventArgs e)
        {
            try
            {
                // Logica per aprire il pannello
                OverlayPanel.Visibility = Visibility.Visible;
                Storyboard showStoryboard = (Storyboard)FindResource("ShowPanelAnimation");
                showStoryboard.Begin();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening panel: {ex.Message}");
            }
        }


        private void ClosePanel(object sender, RoutedEventArgs e)
        {
            var storyboard = (Storyboard)FindResource("HidePanelAnimation");
            storyboard.Completed += (s, ev) =>
            {
                OverlayPanel.Visibility = Visibility.Collapsed; // Nasconde il pannello dopo l'animazione.
            };
            storyboard.Begin();

            // Ripristina i campi di input.
            TitleInput.Text = string.Empty;
            ContentInput.Text = string.Empty;
        }

        //---------------------------------------------

        private RoutedEventHandler? _oldHandler; // Memorizza un riferimento al vecchio gestore di eventi.

        private void SendModifiedInput(object sender, RoutedEventArgs e)
        {
            // Verifica se il titolo modificato è valido.
            if (string.IsNullOrWhiteSpace(ModifiedTitleInput.Text))
            {
                ModifiedTitleInput.Tag = "Error";

                Task.Delay(1000).ContinueWith(_ =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        ModifiedTitleInput.Tag = null;
                    });
                });
                return;
            }

            Note selectedNote = (sender as Note)!; // Nota selezionata da modificare.

            selectedNote?.ModifyNote(ModifiedTitleInput.Text, ModifiedContentInput.Text); // Aggiorna la nota con i nuovi dati.
            CloseEditPanelRequired(sender, e); // Chiude il pannello di modifica.
        }

        public void OpenNotePanelRequired(object sender, RoutedEventArgs e)
        {
            NotePanelOverlayPanel.Visibility = Visibility.Visible;

            var storyboard = (Storyboard)FindResource("ShowNotePanelAnimation");
            storyboard.Begin();

            var notePanel = sender as Note;
            if (_oldHandler != null)
            {
                SubmitModifyNote.Click -= _oldHandler; // Rimuove il precedente handler per evitare duplicati.
            }
            _oldHandler = (s, ev) => SendModifiedInput(sender, e); // Assegna un nuovo handler.
            SubmitModifyNote.Click += _oldHandler;

            // Popola i campi con i dati della nota selezionata.
            ModifiedTitleInput.Text = notePanel?.Title;
            ModifiedContentInput.Text = notePanel?.Content;
        }

        private void CloseEditPanelRequired(object sender, RoutedEventArgs e)
        {
            var storyboard = (Storyboard)FindResource("HideNotePanelAnimation");
            storyboard.Completed += (s, ev) =>
            {
                NotePanelOverlayPanel.Visibility = Visibility.Collapsed; // Nasconde il pannello dopo l'animazione.
            };
            // Resetta i campi di input.
            ModifiedTitleInput.Text = string.Empty;
            ModifiedContentInput.Text = string.Empty;
            storyboard.Begin();
        }

        private void LoadNotes()
        {
            var notes = dbHelper.GetAllNotes(); // Recupera tutte le note dal database.
            foreach (var note in notes)
            {
                // Crea un elemento visivo per ogni nota.
                Note notePanel = new()
                {
                    Id = note.Id,
                    Title = note.Title,
                    Content = note.Content,
                    CreatedAt = note.CreatedAt,
                };
                NoteList.Children.Add(notePanel); // Aggiunge la nota alla lista visiva.
            }
        }
    }
}
