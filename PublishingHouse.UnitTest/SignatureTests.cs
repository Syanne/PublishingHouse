using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using PublishingHouse.Data.Entities;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PublishingHouse.UnitTest
{
    [TestClass]
    public class SignatureTests : TestDependencies
    {
        [TestMethod]
        public void TestGetSignatures()
        {
            //arrange
            var query = "Фабрика";
            var definedResult = "1001101010000";

            //act            
            var result = SignatureAlgorithm.GetSignatures(query);

            //assert   
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.FirstOrDefault().Equals(definedResult));
        }

        [TestMethod]
        public async Task TestSignaturesIndexation()
        {
            //arrange
            var article = new Article { Name = "Проверка сигнатурной индексации", Annotation = "Аннотация к статье", Authors = "Петр Вележев", ArticlePath = "somepath\\here" };
            var keyWordsArray = Regex.Split(String.
                                                Join(" ", article.Name, article.Authors, article.Annotation), 
                                                @"[\s\p{P}]");

            await ArticleWriteRepository.AddArticle(article);

            //act             
            await SearchService.DoIndexation(article);

            //assert            
            var result = await ArticleReadRepository.GetArticle(article.Id);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Signatures.Count == keyWordsArray.Count());
        }

        [TestMethod]
        public async Task TestSignaturesSearch()
        {
            //arrange
            var article = new Article { Name = "проверка работы сигнатурного алгоритма",
                                        Annotation = "какая-то аннотация",
                                        Authors = "Петр Вележев",
                                        ArticlePath = "somepath\\here" };


            await ArticleWriteRepository.AddArticle(article);
            await SearchService.DoIndexation(article);
            string query = "проверка сигнатурного алгоритм васец";

            //act             
            var result = await SearchService.DoSearch(query);

            //assert            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() >= 1);
            Assert.IsTrue(result.First().Id == article.Id);
        }

        [TestMethod]
        public async Task IndexAll()
        {
            var result = await ArticleReadRepository.GetArticles(null);

            foreach(var article in result)
                await SearchService.DoIndexation(article);

            //assert            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() >= 1);
        }

        [TestMethod]
        public async Task TestOnlySearch()
        {
            //arrange
            var query = "албахари использование потоков";

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
