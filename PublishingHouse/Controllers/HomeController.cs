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
            var result = await GetArticles();
            return View(result.ToPagedList(pageNumber, ItemsPerPage));
        }

        [HttpPost]
        public async Task<ActionResult> Index(string searchString, int? page)
        {
            if (searchString == string.Empty)
                page = 1;

            var result = await searchService.DoSearch(searchString);

            return View(result.ToPagedList(1, ItemsPerPage));
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

            return View(article);
        }

        public ActionResult Error()
        {
            return View();
        }

        private async Task<IEnumerable<Article>> GetArticles()
        {
            var articles = await articleReadRepository.GetArticles(null);
            return articles
                .Where(article => article.Name != null)
                .OrderByDescending(artilce => artilce.Id);
        }
    }
}
