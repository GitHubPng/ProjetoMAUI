using AppMVVMCommunityToolkit.Libraries.Messages;
using AppMVVMCommunityToolkit.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
namespace AppMVVMCommunityToolkit.ViewModels
{
    public partial class PubSubPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string inputText;

        [RelayCommand]
        private void SendText()
        { // Publish the InputText to subscribers
            WeakReferenceMessenger.Default.Send(new TextMessage(InputText));
        }
        [RelayCommand]
        private void AddNewPerson()
        {
            var person = new Person()
            {
                Name = "Pessoa da segunda página",
                Email = "pessoa02@gmail.com"
            };
            WeakReferenceMessenger.Default.Send(new PersonMessage(person));

        }
    }
}
