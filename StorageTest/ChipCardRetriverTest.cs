using Domain;
using DomainTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StorageTest {
    [TestClass]
    public class ChipCardRetriverTest {


        [TestMethod]
        public void AllTest() {
            ( var formatter, var retriver, var stream) = Setup();
            var retrived = retriver.All(stream).Result;
            var input = formatter.Output as HashSet<ChipCardDTO>;
            Assert.IsTrue(retrived.Count == input.Count);//is diffrent due to cast.

            try {
                formatter.ThrowException = true;
                retriver.All(stream);
                Assert.Fail("Not expected to reach as an error should be thrown");
            }
            catch (Exception) {
                Assert.IsTrue(true);
            }
        }

        private (FormatterMock, ChipCardRetriver,Stream) Setup(){
            string storageLocation = "hier";
            var outputs = new HashSet<ChipCardDTO>{
                new ChipCardDTO(new ChipCardMock(DateTime.Parse("2017-01-01"), DateTime.Parse("2017-02-02"), true, "id1")),
                new ChipCardDTO(new ChipCardMock(DateTime.Now, DateTime.Now, true, "id2")),
                new ChipCardDTO(new ChipCardMock(DateTime.Now, DateTime.Now, false, "id3"))
            };

            var formater = new FormatterMock {
                Output = outputs,
                ThrowException = false
            };

            //Does not matter as the FormatterMock ignores it. 
            //Is only Important to circumvent the FileStreamer.
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("whatever"));

            var retriver = new ChipCardRetriver(formater, storageLocation);
            return (formater,retriver, stream);
        }

        [TestMethod]
        public void ByActiveTest() {
            (var formatter, var retriver, var stream) = Setup();
            Assert.IsTrue(retriver.ByActive(true,stream).Result.Count == 2);
            Assert.IsTrue(retriver.ByActive(false, stream).Result.Count == 1);
        }

        [TestMethod]
        public void LikeIdTest() {
            (var formatter, var retriver, var stream) = Setup();
            Assert.IsTrue(retriver.LikeId("id", stream).Result.Count == 3);
            Assert.IsTrue(retriver.LikeId("id2", stream).Result.Count == 1);
            Assert.IsTrue(retriver.LikeId("nope", stream).Result.Count == 0);
        }

        [TestMethod]
        public void WhereTest() {
            (var formatter, var retriver, var stream) = Setup();
            Assert.IsTrue(retriver.Where(card => card.ChipUId=="id3", stream).Result.Count == 1);
        }

        [TestMethod]
        public void WithinTest() {
            (var formatter, var retriver, var stream) = Setup();
            Assert.IsTrue(retriver.Within(DateTime.Parse("2017-01-01"), DateTime.Parse("2017-02-02"), stream).Result.Count == 1,"One element expected");
            Assert.IsTrue(retriver.Within(DateTime.Parse("2015-01-02"), DateTime.Parse("2015-02-01"), stream).Result.Count == 0,"Outside box");
        }

    }
}
