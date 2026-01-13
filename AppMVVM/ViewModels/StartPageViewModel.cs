using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Person = AppMVVM.Models.Person;

namespace AppMVVM.ViewModels
{
    public class StartPageViewModel : INotifyPropertyChanged
    {
        #region To Form.
        private Person _person;
        public Person Person
        {
            get { return _person; }
            set { _person = value; OnPropertyChanged(nameof(Person)); }
        }
        public ICommand SaveCommand { get; set; }

        #endregion

        #region List
        public ObservableCollection<Person> People { get; set; }

        #endregion

        public StartPageViewModel()
        {
            SaveCommand = new Command(Save);
            Person = new Person();
            People = new ObservableCollection<Person>();
        }
        private void Save()
        {
            People.Add(Person);
            Person = new Person();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
