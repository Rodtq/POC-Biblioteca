using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using POC_MVC_Biblioteca.ViewModels;
using POC_MVC_Biblioteca.Models;
using POC_MVC_Biblioteca.Data;

namespace POC_MVC_Biblioteca.Services
{
    public class MailService
    {
        private SmtpClient _mailClient;
        private string _sysMail;
        private string _groupMail;
        public MailService()
        {
            _sysMail = "no-reply@smartm.com";
            _groupMail = "BRZ-SmartLibrary@smartm.com";
            _mailClient = new SmtpClient();
            _mailClient.Port = 465;
            _mailClient.EnableSsl = false;
            _mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            _mailClient.UseDefaultCredentials = true;
            _mailClient.Host = "smtpint.smartm.internal";
        }
        public void CheckForLateDeliveries()
        {
            MailMessage AboutToExpiremail = new MailMessage(_sysMail, _groupMail);
            MailMessage Expiredmail = new MailMessage(_sysMail, _groupMail);

            ICollection<Loan> loanList = new List<Loan>();
            using (POC_Database db = new POC_Database())
            {
                loanList = db.Loan.ToList();
                foreach (var item in loanList)
                {
                    User user = db.Users.Find(item.Id_User);
                    if (user != null)
                    {
                        if (item.PullOutDate != item.DevolutionDate)
                        {
                            if (DateTime.Now >= item.RenewingDate && DateTime.Now <= item.DevolutionDate)
                            {
                                AboutToExpiremail.CC.Add(user.eMail);
                            }
                            else if (DateTime.Now >= item.DevolutionDate)
                            {
                                Expiredmail.CC.Add(user.eMail);
                            }
                        }
                        else if (item.LocationlDate.AddHours(48) <= DateTime.Now)
                        {
                            LoanManager loanManager = new LoanManager();
                            var loan = loanManager.GetLoans().First(x => x.Id == item.Id);
                            var msg = string.Format("Olá, você deixou de retirar o livro {0} dentro do prazo estipulado de 48 horas. Sua locação foi automaticamente cancelada",loan.BookName);
                            var subject = "[SmartBooks] Locação de livros cancelada";
                            MailSender(loan, msg, subject);
                            loanManager.CancelLoan(item.Id);
                        }
                    }
                }
                try
                {
                    if (AboutToExpiremail.CC.Any())
                    {
                        AboutToExpiremail.Subject = "[SmartBooks]Livro prestes a expirar";
                        AboutToExpiremail.Body = string.Format("Olá, você tem menos de 48 horas para devolver ou renovar o livro da biblioteca, seu periodo de empréstimo esta acabando!");
                        _mailClient.Send(AboutToExpiremail);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                try
                {
                    if (Expiredmail.CC.Any())
                    {
                        Expiredmail.Subject = "[SmartBooks]Pendência de entrega";
                        Expiredmail.Body = string.Format("Olá, você tem um livro da biblioteca atrasado, por favor entre em contato com o RH");
                        _mailClient.Send(Expiredmail);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        public bool MailSender(BooksLoanViewModel loan, string msg , string subject)
        {
            User usman = new User();
            using (POC_Database db = new POC_Database())
            { usman = db.Users.First(u => u.Id == loan.UserId); }
            MailMessage mail = new MailMessage(usman.eMail, _sysMail);
            mail.Subject = subject;
            mail.Body = msg;
            try
            {
                _mailClient.Send(mail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal void MailSender(object reservedBook, string msg, string subject)
        {
            throw new NotImplementedException();
        }
    }
}