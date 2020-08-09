using System;
using System.Collections.Generic;
using System.Text;

namespace dieuxe.ViewModels
{
    public interface ISendMessage
    {
        void SendMessage(string phoneNumber, string content);
    }
}
