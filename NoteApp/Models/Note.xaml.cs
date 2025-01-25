using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NoteApp
{
    // Rappresenta una nota individuale come elemento UI e logico.
    public partial class Note : UserControl, INotifyPropertyChanged
    {
        public int Id { get; set; } // Identificatore unico della nota.

        // Proprietà per il titolo della nota con notifica delle modifiche.
        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value); // Aggiorna il titolo e notifica i listener.
        }

        // Proprietà per il contenuto della nota con notifica delle modifiche.
        private string _content = string.Empty;
        public new string Content
        {
            get => _content;
            set => SetProperty(ref _content, value); // Aggiorna il contenuto e notifica i listener.
        }

        public DateTime CreatedAt { get; set; } // Data e ora di creazione della nota.

        private readonly DatabaseHelper dbHelper; // Gestore per l'interazione col database.

        public Note()
        {
            InitializeComponent();
            DataContext = this; // Associa i dati di questa classe al contesto del controllo.

            dbHelper = new DatabaseHelper();
            dbHelper.CreateTable(); // Assicura che la tabella esista nel database.
        }

        // Richiede alla finestra principale di aprire il pannello di modifica per questa nota.
        private void RequestToOpenTheNote(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.OpenNotePanelRequired(this, e);
        }

        // Modifica il titolo e il contenuto della nota, aggiornandoli sia in memoria che nel database.
        public void ModifyNote(string titleInput, string contentInput)
        {
            Title = titleInput; // Aggiorna il titolo.
            Content = contentInput; // Aggiorna il contenuto.
            CreatedAt = DateTime.Now; // Aggiorna la data di modifica.
            dbHelper.UpdateNote(this); // Aggiorna la nota nel database.
        }

        // Elimina la nota sia dalla UI che dal database.
        private void DeleteNote(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"ID nota eliminata: {Id}"); // Debug per confermare l'eliminazione.
            dbHelper.DeleteNote(Id); // Rimuove la nota dal database.

            // Trova il pannello genitore e rimuove la nota dall'interfaccia utente.
            StackPanel? parent = VisualTreeHelper.GetParent(this) as StackPanel;
            parent?.Children.Remove(this);
        }

        // Evento per notificare i cambiamenti delle proprietà (utile per il binding).
        public event PropertyChangedEventHandler? PropertyChanged;

        // Metodo per impostare una proprietà e notificare i listener di un cambiamento.
        protected void SetProperty<T>(ref T storage, T value, [System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            if (Equals(storage, value)) return; // Evita notifiche inutili se il valore non è cambiato.

            storage = value; // Aggiorna il valore.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // Notifica i listener.
        }
    }
}
