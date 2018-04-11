
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Email
{
    public interface IEmailService
    {
        bool Send(EmailMessage emailMessage);
       // List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}
