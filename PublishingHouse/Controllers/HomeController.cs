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
using PublishingHouse.SearchServices;

namespace PublishingHouse.Controllers
{
    public class HomeController : Controller
    {
        private int ItemsPerPage = 5;
        private readonly IArticleReadRepository articleReadRepository;
        private readonly ICustomizedSearchService searchService;
        public HomeController(IArticleReadRepository articleReadRepository,
                                ICustomizedSearchService searchService)
        {
            this.articleReadRepository = articleReadRepository;
            this.searchService = searchService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int? page)
        {
            var articles = await GetArticles();
            return View(articles.ToPagedList(page ?? 1, ItemsPerPage));
        }
        
        [HttpPost]
        public async Task<ActionResult> Search(SearchRequest searchRequset)
        {
            if (searchRequset.SearchString == string.Empty)
            {
                var articles = await GetArticles();
                return View(articles.ToPagedList(1, ItemsPerPage));
            }
            else
            {
                var articles = await searchService.DoSearch(searchRequset);
                var page = (articles.Count() == 0) ? 1 : articles.Count();

                return View("Index", articles.Select(CreateArticleViewModel).ToPagedList(1, page));
            }
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
