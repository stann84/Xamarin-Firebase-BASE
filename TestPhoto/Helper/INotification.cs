using System;
using TestXamarinFirebase.Model;

namespace TestXamarinFirebase.Helper
{
    public interface INotification
    {
        void CreateNotification(String title, String message);
        void SendMessage(Message message);
    }
}
