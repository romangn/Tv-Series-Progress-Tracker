using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TvSeriesProgressTracker.Tests
{
    [TestClass]
    public class RepoTest
    {
        private ShowRepository _repo;

        public RepoTest()
        {
            _repo = new ShowRepository();
        }

        [TestMethod]
        public void DifferentShowId()
        {
            ShowRecord show1 = new ShowRecord() { Title = "Game of Thrones" };
            Assert.AreEqual(_repo.checkIfShowAlreadyExists(show1), true);
        }
        //TODO make tests for getting info from API since its the most fragile part of the code

    }
}
