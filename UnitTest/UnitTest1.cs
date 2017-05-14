using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using PublishingHouse.Data.Repository.Interface;
using PublishingHouse.Services.Algorithm.Interface;
using PublishingHouse.Data.Repository;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        //DataContext context = new DataContext();
        [TestMethod]
        public async void TestIndexationNGramAlgorythm()
        {
            ////arrange
            //INGramsReadRepository nGramsRepository = new NGramsReadRepository(ref context);
            //INGramsWriteRepository nGramsWriteRepository = new NGramsWriteRepository(ref context);
            //IArticleReadRepository articleReadRepository = new ArticleReadRepository(context);
            //IArticleWriteRepository articleWriteRepository = new ArticleWriteRepository(context);
            //INGramAlgorithm nGramAlgorythm = new NGramAlgorythm();

            //var service = new NGramService(nGramsRepository, nGramsWriteRepository, articleReadRepository, articleWriteRepository, nGramAlgorythm);
            //var article = new Article { Name = "Work with repository", Annotation = "Here we add some annotation", Authors = "May Green", ArticlePath = "somepath\\here" };

            ////act            
            //await service.DoIndexation(article);

            ////assert            
            //var result = await articleReadRepository.GetArticle(article.Id.Value);
            //Assert.IsNotNull(result);
            //Assert.IsTrue(result.NGrams.Count > 0);
        }

        [TestMethod]
        public async void TestSearchNGramAlgorythm()
        {
            ////arrange
            //INGramsReadRepository nGramsRepository = new NGramsReadRepository(ref context);
            //INGramsWriteRepository nGramsWriteRepository = new NGramsWriteRepository(ref context);
            //IArticleReadRepository articleReadRepository = new ArticleReadRepository(context);
            //IArticleWriteRepository articleWriteRepository = new ArticleWriteRepository(context);
            //INGramAlgorithm nGramAlgorythm = new NGramAlgorythm();

            //var service = new NGramService(nGramsRepository, nGramsWriteRepository, articleReadRepository, articleWriteRepository, nGramAlgorythm);
            //var article = new Article { Name = "Another article", Annotation = "Gallera", Authors = "Martha Green", ArticlePath = "somepath\\here" };

            //await articleWriteRepository.AddArticle(article);
            //await service.DoIndexation(article);

            //var query = "Martha Green";
            ////act            
            //var result = await service.DoSearch(query);

            ////assert
            //Assert.IsNotNull(result);
            //Assert.IsTrue(result.Count() >= 1);
            //Assert.IsTrue(result.FirstOrDefault().Authors == query);
        }

        //[TestMethod]
        //public void AddWord()
        //{
        //    //arrange
        //    IWordsWriteRepository wordWriteRepository = new WordsWriteRepository(ref context);
        //    IWordsReadRepository wordsRepository = new WordsReadRepository(ref context);

        //    //act
        //    string word = "arrangements";
        //    wordWriteRepository.AddWord(new Words { Word = word });

        //    //assert
        //    var result = wordsRepository.GetWord(word);
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void GetWords()
        //{
        //    //arrange
        //    IWordsReadRepository wordsRepository = new WordsReadRepository(ref context);

        //    //act
        //    var result = context.GetEntity();
        //    //assert
        //    Assert.IsTrue(result.Count() == 4);
        //}
    }
}
