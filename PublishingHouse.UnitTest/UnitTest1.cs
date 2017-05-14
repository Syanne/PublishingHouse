using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublishingHouse.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace PublishingHouse.UnitTest
{
    [TestClass]
    public class UnitTest1 : TestDependencies
    {
        [TestMethod]
        public void TestNGramCollectionGeneration()
        {
            //arrange
            var autofacContainer = AutofacContainer;
            var query = "Алгоритм";

            //act            
            var result = NGramAlgorithm.GetNGramsCollection(query);

            //assert   
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 6);
        }

        [TestMethod]
        public async Task TestIndexationNGramAlgorythm()
        {
            //arrange
            var autofacContainer = AutofacContainer;
            var article = new Article { Name = "Work with repository", Annotation = "Here we add some annotation", Authors = "May Green", ArticlePath = "somepath\\here" };
            await ArticleWriteRepository.AddArticle(article);

            //act            
            await SearchService.DoIndexation(article);

            //assert            
            var result = await ArticleReadRepository.GetArticle(article.Id);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.NGrams.Count > 0);
        }

        [TestMethod]
        public async Task TestSearchNGramAlgorythm()
        {
            //arrange
            var article = new Article { Name = "Створена біла мережа", Annotation = "Оперета", Authors = "Martha Green", ArticlePath = "somepath\\here" };

            var addedArticle = await ArticleWriteRepository.AddArticle(article);
            await SearchService.DoIndexation(article);

            var query = "білаа створена";

            //act            
            var result = await SearchService.DoSearch(query);

            //assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() >= 1);
            Assert.IsTrue(result.First().Id == addedArticle.Id);
        }

        [TestMethod]
        public async Task RemoveAll()
        {
            //arrange 
            var articles = await ArticleReadRepository.GetArticles(null);

            //act
            foreach (var article in articles)
                await ArticleWriteRepository.RemoveArticle(article);

            //assert
            var result = await ArticleReadRepository.GetArticles(null);
            Assert.IsTrue(result.Count() == 0);
        }
    }
}
