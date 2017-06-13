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
        private readonly BooksCatalogManager _bcm;
        private readonly LoanManager _lm;
        public BooksCatalogController()
        {
            _bcm = new BooksCatalogManager();
            _lm = new LoanManager();
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
        //Metodo de empréstimos vão aqui
        public ActionResult BookLocator(int bookId)
        {
            string userName = HttpContext.User.Identity.Name;

            _lm.Locator(bookId, userName);
            BooksViewModel result = new BooksViewModel();
            result.BookCategories = _bcm.GetBookCategories();
            result.BooksList = _bcm.GetBooks();
            return PartialView("_ConsultaLivros", result);

        }

        [HttpGet]
        public ActionResult BookUserDeliver(int loanId)
        {
            var result = _lm.deliverBookToClient(loanId);
            return PartialView("_EmprestimoLivros", result);
        }


        [HttpGet]
        public ActionResult BookUserReturner(int loanId)
        {
            _lm.BookToLibraryReturner(loanId);
            BooksLoanViewModel result = new BooksLoanViewModel();
            result.BookLoanList = _lm.GetLoans();
            return PartialView("_EmprestimoLivros", result);
        }

        [HttpGet]
        public ActionResult BookUserLoanCanceler(int loanId)
        {
            _lm.CancelLoan(loanId);
            BooksLoanViewModel result = new BooksLoanViewModel();
            result.BookLoanList = _lm.GetLoans();
            return PartialView("_EmprestimoLivros", result);
        }


        #endregion
    }
}