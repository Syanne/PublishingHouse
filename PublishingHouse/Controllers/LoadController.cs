using System.Threading.Tasks;
using PublishingHouse.Data.Repository.Interface;
using PublishingHouse.Services.Service.Interface;
using PublishingHouse.Data.Entities;
using System.Web.Mvc;
using PublishingHouse.Models;
using System.IO;
using System;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PublishingHouse.Controllers
{
    public class LoadController : Controller
    {
        private readonly IArticleWriteRepository articleWriteRepository;
        private readonly ISignatureService signatureService;
        private readonly ISearchService searchService;
        public LoadController(  IArticleWriteRepository articleWriteRepository,
                                ISignatureService signatureService,
                                ISearchService searchService)
        {
            this.searchService = searchService;
            this.articleWriteRepository = articleWriteRepository;
            this.signatureService = signatureService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(ArticleViewModel articleViewModel)
        {
            if (articleViewModel.File != null && articleViewModel.File.ContentLength > 0)
                try
                {

                    var article = CreateArticle(articleViewModel);
                    article.ArticlePath = Path.Combine(Server.MapPath("~/Articles"),
                                               Path.GetFileName(articleViewModel.File.FileName));

                    articleViewModel.File.SaveAs(article.ArticlePath);
                    ViewBag.Message = "File uploaded successfully";
                    await articleWriteRepository.AddArticle(article);
                    await searchService.DoIndexation(article);
                    await signatureService.DoIndexation(article);

                    return RedirectToAction("Index", "Load");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();

        }

        private Article CreateArticle(ArticleViewModel viewModel)
        {
            return new Article
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Annotation = viewModel.Annotation,
                ArticlePath = viewModel.File.FileName,
                Authors = viewModel.Authors
            };
        }
    }
}
