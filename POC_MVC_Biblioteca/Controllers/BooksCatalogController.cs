using POC_MVC_Biblioteca.Models;
using POC_MVC_Biblioteca.Services;
using POC_MVC_Biblioteca.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POC_MVC_Biblioteca.Controllers
{
    public class BooksCatalogController : Controller
    {
        private readonly BooksCatalogManager _as;
        public BooksCatalogController()
        {
            _as = new BooksCatalogManager();
        }

        // GET: Acervo
        [AllowAnonymous]
        public ActionResult Index()
        {
            BooksViewModel model = new BooksViewModel()
            {
                BookCategories = _as.GetBookCategories()
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
                    BooksViewModel model = new BooksViewModel() { BookCategories = _as.GetBookCategories() };
                    return PartialView(partialViewName, model);
                case "_ConsultaLivros":
                    return RedirectToAction("GetBooks", new BooksViewModel());
                case "_ReservaLivros":
                    return PartialView(partialViewName);
                case "_EmprestimoLivros":
                    BooksLoanViewModel booksLoan = new BooksLoanViewModel();

                    List<BooksLoanViewModel> teste = new List<BooksLoanViewModel>();
                    teste.Add(new BooksLoanViewModel() { UserName = "Alexandre", BookName = "CartolaFC",InitialDate = DateTime.Now,ExpirationDate = DateTime.Now.AddDays(1) });
                    teste.Add(new BooksLoanViewModel() { UserName = "Maria", BookName = "RobinhoFC", InitialDate = DateTime.Now, ExpirationDate = DateTime.Now.AddDays(2) });
                    teste.Add(new BooksLoanViewModel() { UserName = "João", BookName = "Ontem", InitialDate = DateTime.Now, ExpirationDate = DateTime.Now.AddDays(1) });
                    teste.Add(new BooksLoanViewModel() { UserName = "Fulano", BookName = "jovemPão", InitialDate = DateTime.Now, ExpirationDate = DateTime.Now.AddDays(6) });
                    teste.Add(new BooksLoanViewModel() { UserName = "Beltrano", BookName = "lalalala", InitialDate = DateTime.Now, ExpirationDate = DateTime.Now.AddDays(2) });
                    booksLoan.BookLoanList = teste;


                    return PartialView(partialViewName,booksLoan);
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
                book.BookCategories = _as.GetBookCategories();
                return PartialView("_CadastroLivros", book);
            }
            _as.AddBook(book);
            ModelState.Clear();
            return PartialView("_CadastroLivros", new BooksViewModel()
            {
                BookCategories = _as.GetBookCategories(),
            });
        }
        #endregion
        #region query

        [NoCache]
        public ActionResult GetBooks(BooksViewModel filters)
        {
            BooksViewModel result = new BooksViewModel();
            result.BookCategories = _as.GetBookCategories();
            result.BooksList = _as.GetBooks(filters);
            return PartialView("_ConsultaLivros", result);
        }

        public ActionResult GetBooksPerCategory(int categoryId)
        {
            BooksViewModel result = new BooksViewModel();
            result.BookCategories = _as.GetBookCategories();
            result.BooksList = _as.GetBooks().Where(b => b.Category.Id == categoryId);
            return PartialView("_ConsultaLivros", result);
        }
        #endregion
        #region delete

        public ActionResult DeleteBook(int bookId)
        {
            _as.DeleteBook(bookId);
            BooksViewModel result = new BooksViewModel();
            result.BookCategories = _as.GetBookCategories();
            result.BooksList = _as.GetBooks();
            return PartialView("_ConsultaLivros", result);
        }
        #endregion
        #region update
        [NoCache]
        [HttpGet]
        public ActionResult EditBook(int bookId)
        {
            BooksViewModel result = new BooksViewModel();
            result = _as.GetBookPerId(bookId);
            return PartialView("_EditBook", result);
        }
        [HttpPost]
        public ActionResult UpdateBook(BooksViewModel book)
        {
            if (!ModelState.IsValid)
            {
                book.BookCategories = _as.GetBookCategories();
                return PartialView("_EditBook", book);
            }
            var result = _as.UpdateBook(book);
            result.BookCategories = _as.GetBookCategories();
            return PartialView("_EditBook", result);
        }
    }
    #endregion
    #region Loans
    //Metodo de empréstimos vão aqui

    #endregion
}