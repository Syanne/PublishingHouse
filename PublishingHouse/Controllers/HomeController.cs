using System;
using PublishingHouse.Data.Entities;
using PublishingHouse.Data.Repository.Interface;
using PublishingHouse.Services.Service.Interface;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;
using PagedList;
using System.Net;
using PublishingHouse.Models;

namespace PublishingHouse.Controllers
{
    public class HomeController : Controller
    {
        private int ItemsPerPage = 5;
        private readonly IArticleReadRepository articleReadRepository;
        private readonly ISearchService searchService;
        public HomeController(  IArticleReadRepository articleReadRepository,
                                ISearchService searchService)
        {
            this.articleReadRepository = articleReadRepository;
            this.searchService = searchService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int? page)
        {
            int pageNumber = (page ?? 1);
            var articles = await GetArticles(); 
            return View(articles.ToPagedList(pageNumber, ItemsPerPage));
        }

        [HttpPost]
        public async Task<ActionResult> Index(string searchString, int? page)
        {
            if (searchString == string.Empty)
                page = 1;

            var articles = await searchService.DoSearch(searchString);
            var result = articles
                            .Select(CreateArticleViewModel)
                            .ToPagedList(1, articles.Count());

            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = await articleReadRepository.GetArticle(id.Value);

            if (article == null)
            {
                return HttpNotFound();
            }

            return View(CreateArticleViewModel(article));
        }

        public ActionResult Error()
        {
            return View();
        }

        private async Task<IEnumerable<ArticleViewModel>> GetArticles()
        {
            var articles = await articleReadRepository.GetArticles(null);
            return articles
                .Where(article => article.Name != null)
                .Select(CreateArticleViewModel)
                .OrderByDescending(artilce => artilce.Id);
        }

        private ArticleViewModel CreateArticleViewModel(Article article)
        {
            return new ArticleViewModel
            {
                Id = article.Id,
                Name = article.Name,
                Annotation = article.Annotation,
                File = null,
                Authors = article.Authors
            };
        }
    }
}
