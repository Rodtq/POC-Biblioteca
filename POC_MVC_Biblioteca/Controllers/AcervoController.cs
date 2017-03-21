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
    public class AcervoController : Controller
    {
        private readonly AcervoManager _as;
        public AcervoController()
        {
            _as = new AcervoManager();
        }

        // GET: Acervo
        public ActionResult Index()
        {
            AcervoViewModel model = new AcervoViewModel();
            model.abaCadastro = new CreateBookViewModel();
            IEnumerable<Catalogacao> livros = _as.GetCatalogacao(new ConsultaLivroViewModel());
            ConsultaLivroViewModel result = new ConsultaLivroViewModel();
            IList<Catalogacao> parseList = new List<Catalogacao>();
            if (result.ListaLivros == null)
            {
                result.ListaLivros = new List<Catalogacao>();
            }
            livros.ToList().ForEach(livro => {
                parseList.Add(livro);
            });
            result.ListaLivros = parseList;
            model.abaConsulta = result;
            return View(model);
        }


        public ActionResult CreateBook(CreateBookViewModel livro)
        {
            Catalogacao catalogação = new Catalogacao()
            {
                ISBN = livro.ISBN,
                TituloDaObra = livro.TituloDaObra,
                Autor = livro.Autor,
                AnoLivro = livro.AnoLivro,
                Categoria = livro.Categoria,
                Editora = livro.Editora,
                Quantidade = livro.Quantidade,
                Descricao = livro.Descricao,
                Observacao = livro.Observacao,
                EstanteLocalizacao = livro.EstanteLocalizacao
            };
            _as.AddCatalogacao(catalogação);
            return RedirectToAction("Index");
        }

        public ActionResult GetBooks(ConsultaLivroViewModel filtros)
        {
            IEnumerable<Catalogacao> livros = _as.GetCatalogacao(filtros);
            ConsultaLivroViewModel result = new ConsultaLivroViewModel();
            IList<Catalogacao> parseList = new List<Catalogacao>();
            if (result.ListaLivros == null)
            {
                result.ListaLivros = new List<Catalogacao>();
            }
            livros.ToList().ForEach(livro => {
                parseList.Add(livro);
            });
            result.ListaLivros = parseList;
            return PartialView("_ConsultaLivros",result);
        }

    }
}