using CommunityToolkit.Mvvm.ComponentModel;

namespace AppMVVMCommunityToolkit.Models
{
    public partial class Person : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string email;
    }
}
