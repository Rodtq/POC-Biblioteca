using POC_MVC_Biblioteca.Models;
using POC_MVC_Biblioteca.Services;
using POC_MVC_Biblioteca.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace POC_MVC_Biblioteca.Controllers
{
    public class BooksCatalogController : Controller
    {
        private readonly MailService _mservice;
        private readonly BooksCatalogManager _bcm;
        private readonly LoanManager _lm;
        private readonly Reserve _rm;
        public BooksCatalogController()
        {
            _bcm = new BooksCatalogManager();
            _lm = new LoanManager();
            _mservice = new MailService();
            _rm = new Reserve();
        }

        // GET: Acervo
        [AllowAnonymous]
        public ActionResult Index()
        {
            BooksViewModel model = new BooksViewModel()
            {
                BookCategories = _bcm.GetBookCategories()
            };
            return View(model);
        }
        //[Authorize(Roles = "Administrator")]
        [NoCache]
        public ActionResult BooksCatalogNavigation(string partialViewName)
        {
            switch (partialViewName)
            {
                case "_CadastroLivros":
                    BooksViewModel model = new BooksViewModel() { BookCategories = _bcm.GetBookCategories() };
                    return PartialView(partialViewName, model);
                case "_ConsultaLivros":
                    return RedirectToAction("GetBooks", new BooksViewModel());
                case "_ReservaLivros":
                    return PartialView(partialViewName);
                case "_EmprestimoLivros":
                    BooksLoanViewModel booksLoan = new BooksLoanViewModel
                    {
                        BookLoanList = _lm.GetLoans()
                    };

                    return PartialView(partialViewName, booksLoan);
                case "_EntregaLivros":
                    return PartialView(partialViewName);
                default:
                    return null;
            }
        }

        #region add
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateBook(BooksViewModel book)
        {
            if (!ModelState.IsValid)
            {
                book.BookCategories = _bcm.GetBookCategories();
                return PartialView("_CadastroLivros", book);
            }
            _bcm.AddBook(book);
            ModelState.Clear();
            return PartialView("_CadastroLivros", new BooksViewModel()
            {
                BookCategories = _bcm.GetBookCategories(),
            });
        }
        #endregion
        #region query

        [NoCache]
        public ActionResult GetBooks(BooksViewModel filters)
        {
            BooksViewModel result = new BooksViewModel();
            result.BookCategories = _bcm.GetBookCategories();
            result.BooksList = _bcm.GetBooks(filters);
            return PartialView("_ConsultaLivros", result);
        }

        public ActionResult GetBooksPerCategory(int categoryId)
        {
            BooksViewModel result = new BooksViewModel();
            result.BookCategories = _bcm.GetBookCategories();
            result.BooksList = _bcm.GetBooks().Where(b => b.Category.Id == categoryId);
            return PartialView("_ConsultaLivros", result);
        }
        #endregion
        #region delete

        public ActionResult DeleteBook(int bookId)
        {
            _bcm.DeleteBook(bookId);
            BooksViewModel result = new BooksViewModel();
            result.BookCategories = _bcm.GetBookCategories();
            result.BooksList = _bcm.GetBooks();

            return PartialView("_ConsultaLivros", result);
        }
        #endregion
        #region update
        [NoCache]
        [HttpGet]
        public ActionResult EditBook(int bookId)
        {
            BooksViewModel result = new BooksViewModel();
            result = _bcm.GetBookPerId(bookId);
            return PartialView("_EditBook", result);
        }

        [HttpGet]
        public ActionResult DetailBook(int bookId)
        {
            BooksViewModel result = new BooksViewModel();
            result = _bcm.GetBookPerId(bookId);
            return PartialView("_BookDetails", result);
        }



        [HttpPost]
        public ActionResult UpdateBook(BooksViewModel book)
        {
            if (!ModelState.IsValid)
            {
                book.BookCategories = _bcm.GetBookCategories();
                return PartialView("_EditBook", book);
            }

            var result = _bcm.UpdateBook(book);
            result.BookCategories = _bcm.GetBookCategories();
            return PartialView("_EditBook", result);
        }
        #endregion
        #region Loans
        //Metodos de empréstimos vão aqui
        public ActionResult BookLocator(int bookId)
        {
            string userName = HttpContext.User.Identity.Name;
            var bookLocated = _lm.Locator(bookId, userName);
            BooksViewModel result = new BooksViewModel();
            result.BookCategories = _bcm.GetBookCategories();
            result.BooksList = _bcm.GetBooks();
            string subject = "[SmartBooks] Locação de livro";
            string msg = string.Format("Olá, você locou o livro {0} com sucesso data: {1}. Você tem 48 horas para retirá-lo.", bookLocated.BookName, DateTime.Now);
            _mservice.MailSender(bookLocated, msg, subject);
            return PartialView("_ConsultaLivros", result);

        }

        [HttpGet]
        public ActionResult BookUserDeliver(int loanId)
        {
            var result = _lm.deliverBookToClient(loanId);
            string msg = string.Format("Olá, você retirou o livro {0} com sucesso data: {1}. Você tem 30 dias ({2}) para devolvê-lo.", result.BookName, DateTime.Now, result.DevolutionDate);
            string subject = "[SmartBooks] Retirada de livro";
            _mservice.MailSender(result, msg, subject);
            return PartialView("_EmprestimoLivros", result);
        }


        [HttpGet]
        public ActionResult BookUserReturner(int loanId)
        {
            var loanBook = _lm.GetLoans().First(x => x.Id == loanId);
            _lm.BookToLibraryReturner(loanId);
            BooksLoanViewModel result = new BooksLoanViewModel();
            result.BookLoanList = _lm.GetLoans();
            string msg = string.Format("Olá, você devolveu o livro {0} com sucesso data: {1}.", result.BookName, DateTime.Now);
            string subject = "[SmartBooks] Entrega de livro";
            _mservice.MailSender(loanBook, msg, subject);
            return PartialView("_EmprestimoLivros", result);
        }

        [HttpGet]
        public ActionResult BookUserLoanCanceler(int loanId)
        {
            var loanBook = _lm.GetLoans().First(x => x.Id == loanId);
            _lm.CancelLoan(loanId);
            BooksLoanViewModel result = new BooksLoanViewModel();
            result.BookLoanList = _lm.GetLoans();
            string subject = "[SmartBooks] Locação cancelada";
            string msg = string.Format("Olá, a locação do livro {0} foi cancelada data:{1}", result.BookName, DateTime.Now);
            _mservice.MailSender(loanBook, msg, subject);
            return PartialView("_EmprestimoLivros", result);
        }

        #endregion

        #region Reservation
        //métodos para a reserva de livros
        [HttpGet]
        public ActionResult ReservaLivros(int Id_Book)
        {
            string userName = HttpContext.User.Identity.Name;
            var reservedBook = _rm.reserved(Id_Book, userName);
            BooksViewModel result = new BooksViewModel();
            result.BookCategories = _bcm.GetBookCategories();
            result.BooksList = _bcm.GetBooks();
            string subject = "[SmartBooks] Reserva de livro";
            string msg = string.Format("Olá, você reservou o livro {0} com sucesso data: {1}. Você será notificado quando o livro estiver disponível.", reservedBook.BookName, DateTime.Now);
            _mservice.MailSender(reservedBook, msg, subject);
            return PartialView("_ConsultaLivros", result);

        }
    }
}
    #endregion