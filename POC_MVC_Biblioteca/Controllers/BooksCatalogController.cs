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
                CataegoriesList = _as.GetBookCategories()
            };
            return View(model);
        }
        //[Authorize(Roles = "Administrator")]
        public ActionResult BooksCatalogNavigation(string partialViewName)
        {
            switch (partialViewName)
            {
                case "_CadastroLivros":
                    CreateBookViewModel model = new CreateBookViewModel() { BookCategories = GetBookCategories() };
                    return PartialView(partialViewName, model);
                case "_ConsultaLivros":
                    return RedirectToAction("GetBooks", new BooksConsultViewModel());
                case "_ReservaLivros":
                    return PartialView(partialViewName);
                case "_EmprestimoLivros":
                    return PartialView(partialViewName);
                case "_EntregaLivros":
                    return PartialView(partialViewName);
                default:
                    return null;
            }
        }

        //[Authorize(Roles = "Administrator")]
        public ActionResult CreateBook(CreateBookViewModel livro)
        {
            Book catalogação = new Book()
            {
                ISBN = livro.ISBN,
                Title = livro.Title,
                Author = livro.Author,
                BookYear = livro.BookYear,
                Category = _as.GetBookCategories().SingleOrDefault(b => b.Id == Convert.ToInt32(livro.CategoryId)),
                Editor = livro.Editor,
                Quantity = livro.Quantity,
                Description = livro.Description,
                Observation = livro.Observation,
                LocalizationShelf = livro.LocalizationShelf
            };
            _as.AddBook(catalogação);
            return PartialView("_CadastroLivros", new CreateBookViewModel() {BookCategories = GetBookCategories() });
        }


        public ActionResult GetBooks(BooksConsultViewModel filtros)
        {

            IEnumerable<Book> livros = _as.GetBooks(filtros);
            BooksConsultViewModel result = new BooksConsultViewModel();
            result.CataegoriesList = _as.GetBookCategories();
            IList<Book> parseList = new List<Book>();
            if (result.BooksList == null)
            {
                result.BooksList = new List<Book>();
            }
            livros.ToList().ForEach(livro =>
            {
                parseList.Add(livro);
            });
            result.BooksList = parseList;
            return PartialView("_ConsultaLivros", result);
        }


        private IEnumerable<SelectListItem> GetBookCategories()
        {
            return _as.GetBookCategories().Select(bc => new SelectListItem { Value = bc.Id.ToString(), Text = bc.Name });
        }


    }
}