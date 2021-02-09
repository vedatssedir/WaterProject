using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.ViewModels;

namespace Models.Interfaces
{
    public interface IEmailService
    {

        Task<bool> EmailOlustur(EmailAddViewModel model);
        Task<IList<EmailListViewModel>> EmailList();
        Task<bool> EmailSil(int Id);
        Task<bool> EmailGonder(EmailSendViewModel model);
        Task<string> GetEMailAdress(int Id);

    }
}
