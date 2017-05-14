using System.Threading.Tasks;
using PublishingHouse.Data.Repository.Interface;
using PublishingHouse.Services.Service.Interface;
using PublishingHouse.Data.Entities;
using System.Web.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PublishingHouse.Controllers
{
    public class LoadController : Controller
    {
        private readonly IArticleWriteRepository articleWriteRepository;
        private readonly ISearchService searchService;
        public LoadController( IArticleWriteRepository articleWriteRepository,
                                ISearchService searchService)
        {
            this.searchService = searchService;
            this.articleWriteRepository = articleWriteRepository;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(Article article)
        {
            await articleWriteRepository.AddArticle(article);
            await searchService.DoIndexation(article);

            return View("Index", new Article());
        }        
    }
}
