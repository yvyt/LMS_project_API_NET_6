using MailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Services
{
    public interface IMailServices
    {
        void SendEmail(Message message);
    }
}
