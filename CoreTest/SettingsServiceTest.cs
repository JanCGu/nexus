using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Service;

namespace CoreTest {
    [TestClass]
    public class SettingsServiceTest {
        [TestMethod]
        public void GetStorageLocationTest() {
            Assert.AreEqual("Temp4567/.bin", SettingsService.GetStorageLocation());
        }

        [TestMethod]
        public void GetChipCardSourceURLTest() {
            Assert.AreEqual(@"http://carddesk.westeurope.cloudapp.azure.com:8901/DEMOService/Dataset", SettingsService.GetChipCardSourceURL());
        }

        [TestMethod]
        public void GetStorageLocationIfNullOrWhiteSpaceTest() {
            Assert.AreEqual(@"Temp4567/.bin", SettingsService.GetStorageLocationIfNullOrWhiteSpace(null));
            Assert.AreEqual(@"Temp4567/.bin", SettingsService.GetStorageLocationIfNullOrWhiteSpace(""));
            Assert.AreEqual(@"Temp4567/.bin", SettingsService.GetStorageLocationIfNullOrWhiteSpace(" "));
            string pass = "pass";
            Assert.AreEqual(pass, SettingsService.GetStorageLocationIfNullOrWhiteSpace(pass));
        }

        [TestMethod]
        public void GetTimeoutTest() {
            Assert.AreEqual(5.0, SettingsService.GetTimeout());
        }

        [TestMethod]
        public void GetTriesTest() {
            Assert.AreEqual(5, SettingsService.GetTries());
        }
    }
}
