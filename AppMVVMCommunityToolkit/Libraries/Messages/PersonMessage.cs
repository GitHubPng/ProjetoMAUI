using AppMVVMCommunityToolkit.Models;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AppMVVMCommunityToolkit.Libraries.Messages
{
    public class PersonMessage : ValueChangedMessage<Person>
    {
        public PersonMessage(Person person) : base(person)
        {

        }
    }
}
