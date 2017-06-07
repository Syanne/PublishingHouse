using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublishingHouse.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace PublishingHouse.UnitTest
{
    [TestClass]
    public class NGramTests : TestDependencies
    {
        [TestMethod]
        public void TestNGramCollectionGeneration()
        {
            //arrange
            var query = "Алгоритм";
            int NGramCount = 6;

            //act            
            var result = NGramAlgorithm.GetNGramsCollection(query);

            //assert   
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == NGramCount);
        }

        [TestMethod]
        public async Task TestIndexationNGramAlgorythm()
        {
            //arrange
            var article = new Article
            {
                Name = "Заместитель",
                Annotation = "Паттерн Заместитель (Proxy) предоставляет объект-заместитель, который управляет доступом к другому объекту. То есть создается объект-суррогат, который может выступать в роли другого объекта и замещать его",
                Authors = "Кори Кормонар",
                ArticlePath = "somepath\\here"
            };
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
            var article = new Article
            {
                Name = "Методы add и update",
                Annotation = "Метод remove отличается от метода удаления, определенного в DAO тем, что принимает Account в качестве параметра вместо userName (идентификатора аккаунта). Представление репозитория как коллекции меняет его восприятие. Вы избегаете раскрытия типа идентификатора аккаунта репозиторию. Это сделает вашу жизнь легче в том случае, если вы захотите использовать long для идентрификации аккаунтов.",
                Authors = "Martha Green",
                ArticlePath = "somepath\\here"
            };

            var addedArticle = await ArticleWriteRepository.AddArticle(article);
            await SearchService.DoIndexation(article);

            var query = "Методы отличаются";

            //act            
            var result = await SearchService.DoSearch(query);

            //assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() >= 1);
            Assert.IsTrue(result.First().Id == addedArticle.Id);
        }

        [TestMethod]
        public async Task TestSimplifiedSearcNGramAlgorythm()
        {
            //arrange
            var article = new Article
            {
                Name = "Нова стаття для тестування",
                Annotation = "Анотація до статті, що буде використано при тестуванні",
                Authors = "Світлана Вермська",
                ArticlePath = "somepath\\here"
            };

            var addedArticle = await ArticleWriteRepository.AddArticle(article);

            var query = "Вермська нова";

            //act            
            var result = await SearchService.DoSearch(query);

            //assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() >= 1);
            Assert.IsTrue(result.First().Id == addedArticle.Id);
        }

        [TestMethod]
        public async Task TestOnlySearch()
        {
            //arrange
            var query = "паттерн";

            var watch = System.Diagnostics.Stopwatch.StartNew();
            //act            
            var result = await SearchService.DoSearch(query);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            //assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() >= 1);
        }
    } 
}
