using CoreTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Retriver;
using System;
using System.Net.Http;

namespace RetriverTest {
    [TestClass]
    public class ChipCardJsonRetriverTest {

        private HttpHandlerMock GetMock(HttpResponseMessage response) {
            return new HttpHandlerMock {
                WaitMs = 0,
                ThrowTimeOut = false,
                Timeout = TimeSpan.FromTicks(500),
                Response = response
            };
        }

        [TestMethod]
        public void GoodAllTest() {
            (string json, var chipcards) = ChipCardTestServices.ValidChipCards();

            var goodresponse = new HttpResponseMessage(System.Net.HttpStatusCode.Accepted) {
                Content = new StringContent(json)
            };
            var mockClient = GetMock(goodresponse);

            string testUrl = "testURL";
            var retriev = new ChipCardJsonRetriver(mockClient,testUrl);
            
            var outTask = retriev.All();
            outTask.Wait();//no wait time
            var outputs = outTask.Result;
            foreach (var output in outputs) {
                Assert.IsTrue(chipcards.Exists(card => card.ChipUId.Equals(output.ChipUId)), $"Output not found with cuid {output.ChipUId}");
            }
            Assert.AreEqual(testUrl, mockClient.Url);
            
        }

        [TestMethod]
        public void GoodAllWithWaitTest() {
            (string json, var chipcards) = ChipCardTestServices.ValidChipCards();

            var goodresponse = new HttpResponseMessage(System.Net.HttpStatusCode.Accepted) {
                Content = new StringContent(json)
            };
            var mockClient = GetMock(goodresponse);
            string testUrl = "testURL";
            var retriev = new ChipCardJsonRetriver(mockClient, testUrl);
            mockClient.WaitMs = 500;
            var start = DateTime.Now;
            retriev.All().Wait();
            if (DateTime.Now < start.Add(TimeSpan.FromMilliseconds(400)))
                Assert.Fail("Did not wait");
        }


        [TestMethod]
        public void BadAllTest() {
            var badresponse = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            var mockClient = GetMock(badresponse);
            mockClient.ThrowTimeOut = true;

            string testUrl = "testURL";
            var retriev = new ChipCardJsonRetriver(mockClient, testUrl);
            try {
                retriev.All().Wait();
                Assert.Fail("An error was expected");
            }
            catch (Exception) {
                //this case is expected to happen
            }
            mockClient.ThrowTimeOut = false;
            mockClient.Response = badresponse;
            try {
                retriev.All().Wait();
                Assert.Fail("An error was expected");
            }
            catch (Exception) {
                //this case is expected to happen
            }
        }
    }
}
