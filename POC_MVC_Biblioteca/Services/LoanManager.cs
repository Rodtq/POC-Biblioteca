using POC_MVC_Biblioteca.Data;
using POC_MVC_Biblioteca.Models;
using POC_MVC_Biblioteca.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.Services
{
    public class LoanManager
    {


        public BooksLoanViewModel Locator(int bookId, string samAccName)
        {
            if (string.IsNullOrEmpty(samAccName))
            {
                return null;
            }
            User userModel = null;
            Book bookModel = null;
            Loan loan = null;
            using (POC_Database db = new POC_Database())
            {
                bookModel = db.Books.FirstOrDefault(b => b.Id == bookId);
                bookModel.Status = (int)BookStatus.Located;
                bookModel.Quantity -= 1;
                userModel = db.Users.FirstOrDefault(u => string.Compare(u.SamAccountName, samAccName, StringComparison.OrdinalIgnoreCase) == 0);
                loan = new Loan()
                {
                    Id_Book = bookModel.Id,
                    Id_User = userModel.Id,
                    Tenant = userModel,
                    Book = bookModel,
                    LocationlDate = DateTime.Now
                };

                DbEntityEntry dbEntityEntry = db.Entry(loan);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    dbEntityEntry.State = EntityState.Added;
                }
                else
                {
                    db.Loan.Attach(loan);
                    db.Loan.Add(loan);
                }

                dbEntityEntry = db.Entry(bookModel);
                dbEntityEntry.State = EntityState.Modified;
                db.SaveChanges();
            }
            BooksLoanViewModel result = ParseLoanModelToParseBookLoanViewModel(loan);
            return result;
        }


        public BooksLoanViewModel deliverBookToClient(int loanId)
        {
            Loan loanModel = null;
            BooksLoanViewModel result = null;
            using (POC_Database db = new POC_Database())
            {
                loanModel = db.Loan.First(l => l.Id == loanId);
                loanModel.PullOutDate = DateTime.Now;
                loanModel.DevolutionDate = DateTime.Now.AddMonths(1);
                loanModel.RenewingDate = DateTime.Now.AddDays(28);
                DbEntityEntry dbEntityEntry = db.Entry(loanModel);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    db.Loan.Attach(loanModel);
                }
                dbEntityEntry.State = EntityState.Modified;
                if (loanModel.Equals(null))
                {
                    loanModel = new Loan();
                }
                Book bookModel = db.Books.First(b => b.Id == loanModel.Id_Book);
                bookModel.Status = (int)BookStatus.OnReaderHands;
                dbEntityEntry = db.Entry(bookModel);
                dbEntityEntry.State = EntityState.Modified;

                db.SaveChanges();
                result = ParseLoanModelToParseBookLoanViewModel(loanModel);
                result.BookLoanList = GetLoans();
            }
            return result;
        }

        public IEnumerable<BooksLoanViewModel> GetLoans()
        {
            IEnumerable<BooksLoanViewModel> result = new List<BooksLoanViewModel>();
            using (POC_Database db = new POC_Database())
            {
                var loansList = db.Loan;
                IList<BooksLoanViewModel> tempList = new List<BooksLoanViewModel>();
                foreach (Loan item in loansList)
                {
                    tempList.Add(ParseLoanModelToParseBookLoanViewModel(item));
                }
                result = tempList;
            }
            return result;
        }


        public void CancelLoan(int loanId)
        {
            using (POC_Database db = new POC_Database())
            {
                var loanEntity = db.Loan.SingleOrDefault(l => l.Id == loanId);
                db.Loan.Remove(loanEntity);
                Book bookModel = db.Books.First(b => b.Id == loanEntity.Id_Book);
                DbEntityEntry dbEntityEntry = db.Entry(bookModel);
                bookModel.Status = (int)BookStatus.Available;
                bookModel.Quantity += 1;
                dbEntityEntry = db.Entry(bookModel);
                dbEntityEntry.State = EntityState.Modified;
                db.SaveChanges();
            }
        }



        public void BookToLibraryReturner(int loanId)
        {
            Book bookModel = null;
            Loan loanModel = null;
            using (POC_Database db = new POC_Database())
            {
                loanModel = db.Loan.First(l => l.Id == loanId);
                bookModel = db.Books.FirstOrDefault(b => b.Id == loanModel.Id_Book);
                bookModel.Status = (int)BookStatus.Available;
                bookModel.Quantity += 1;
                DbEntityEntry dbEntityEntry = db.Entry(bookModel);
                dbEntityEntry = db.Entry(bookModel);
                dbEntityEntry.State = EntityState.Modified;
                db.Loan.Remove(loanModel);
                db.SaveChanges();
            }

        }


        #region parse
        private Loan ParseBookLoanViewModelToLoanModel(BooksLoanViewModel viewModel)
        {
            Loan dbModel = null;
            using (POC_Database db = new POC_Database())
            {
                dbModel = new Loan
                {
                    Id = viewModel.Id,
                    Id_User = viewModel.UserId,
                    Id_Book = viewModel.BookId,
                    PullOutDate = viewModel.PullOutlDate,
                    DevolutionDate = viewModel.DevolutionDate,
                    RenewingDate = viewModel.RenewingDate,
                    Book = db.Books.First(b => b.Id == viewModel.BookId),
                    Tenant = db.Users.First(u => u.Id == viewModel.UserId),
                };
            }
            return dbModel;
        }


        private BooksLoanViewModel ParseLoanModelToParseBookLoanViewModel(Loan model)
        {
            BooksLoanViewModel viewModel = new BooksLoanViewModel
            {
                Id = model.Id,
                BookId = model.Id_Book,
                UserId = model.Id_User,
                BookName = model.Book.Title,
                UserName = model.Tenant.Name,
                PullOutlDate = model.PullOutDate ?? null,
                RenewingDate = model.RenewingDate ?? null,
                DevolutionDate = model.DevolutionDate ?? null,
                BookStatus = (BookStatus)model.Book.Status
            };
            return viewModel;
        }
        #endregion


        public void ExpirationChecker()
        {
            using (POC_Database db = new POC_Database())
            {

                IEnumerable<Loan> loanList = db.Loan.Where(l => l.DevolutionDate <= DateTime.Now);

                foreach (var loan in loanList)
                {
                    //Actions to be Confirmed

                }
            }

        }


        public bool LoanCanceler()
        {
            using (POC_Database db = new POC_Database())
            {

                IEnumerable<Loan> loanList = db.Loan.Where(l => l.LocationlDate >= DateTime.Now.AddHours(-48));
                foreach (var item in loanList)
                {
                    //Send Email check wit Alexandre the smtp service 
                    CancelLoan(item.Id);
                }

            }
            return false;
        }


    }
}