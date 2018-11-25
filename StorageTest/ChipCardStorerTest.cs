using Domain;
using DomainTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace StorageTest {
    [TestClass]
    public class ChipCardStorerTest {

        private (FormatterMock, ChipCardStorer, Stream, ChipCardDeleterMock) Setup() {
            string storageLocation = "hier";
            var outputs = new HashSet<IChipCard>{
                new ChipCardMock(DateTime.Parse("2017-01-01"), DateTime.Parse("2017-02-02"), true, "id1"),
                new ChipCardMock(DateTime.Now, DateTime.Now, true, "id2"),
                new ChipCardMock(DateTime.Now, DateTime.Now, false, "id3")
            };

            var formater = new FormatterMock {
                Output = outputs,
                ThrowException = false
            };

            //Does not matter as the FormatterMock ignores it. 
            //Is only Important to circumvent the FileStreamer.
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("whatever"));

            var deleter = new ChipCardDeleterMock {
                Output = true
            };

            var storer = new ChipCardStorer(storageLocation, formater, deleter);
            return (formater, storer, stream,deleter);
        }

        [TestMethod]
        public void InsertTest() {
            (var formatter, var storer, var stream, var deleter)=Setup();
            Assert.AreEqual(formatter.Output, storer.Insert(formatter.Output as HashSet<IChipCard>, stream).Result);
            deleter.Output = false;
            Assert.IsTrue(!storer.Insert(formatter.Output as HashSet<IChipCard>, stream).Result.Any());

        }

        [TestMethod]
        public void DeleteTest() {
            (var formatter, var storer, var stream, var deleter) = Setup();
            Assert.AreEqual(formatter.Output, storer.Delete(formatter.Output as HashSet<IChipCard>).Result);
            deleter.Output = false;
            Assert.IsTrue(!storer.Insert(formatter.Output as HashSet<IChipCard>).Result.Any());

        }
    }
}
