using DisplayRazor.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace IntegrationTest {
    [TestClass]
    public class Integration {

        /// <summary>
        /// Tests the use case of an inital setup.
        /// Basicly uses the ChipCartController in DisplayRazor to do all the tests.
        /// This is done, because it is the entrypoint for a user and one step
        /// bevore GUI Tests.
        /// </summary>
        [TestMethod]
        public void InitialSetup() {
            EnsureNoStorage();
            var ccControler = new ChipCardController {
                Tries = 4
            };

            //Reset
            var resetTask = ccControler.Reset(); //the user enters through ResetApp. The result is the same.
            resetTask.Wait();
            Assert.IsTrue(resetTask.Result);

            //Get All
            var allTask = ccControler.All();
            allTask.Wait();
            Assert.IsTrue(allTask.Result.Count > 0);

            //Get Active
            var activeTask = ccControler.ByActive(true);
            activeTask.Wait();
            var activeFromAll = allTask.Result.Where(card => card.Active);
            Assert.IsTrue(activeFromAll.Count() == activeTask.Result.Count);

            //Clean up
            EnsureNoStorage();

        }

        private void EnsureNoStorage() {
            var deleter = Config.ChipCardDeleterFactory.CompleteStorageDeleter();
            try {
                deleter.DeleteAll();
            }
            catch {
                //does not matter <= either no file here or no permisison.
                //If no file exists it is the wanted results.
                //If no permission to exists to do storage operations a later step will fail.
            }
        }
    }
}
