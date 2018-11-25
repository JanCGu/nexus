using Core.Interfaces;
using Core.Service;
using Domain;
using DomainTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CoreTest {
    [TestClass]
    public class ChipCardResetterTest {

        private (ChipCardSetterMock, ChipCardGetterMock, ChipCardGetterMock, int, ChipCardResetter) Setup() {
            var output = new HashSet<IChipCard>{
                new ChipCardMock(DateTime.Now,DateTime.Now,true,"id1"),
                new ChipCardMock(DateTime.Now, DateTime.Now, true, "id2"),
            };
            var storageSetter = new ChipCardSetterMock {
                ThrowException = false,
                Output = output,
                WaitMs = 0
            };
            var storageGetter = new ChipCardGetterMock {
                ThrowException = false,
                Output = output,
                WaitMs = 0
            };
            var sourceGetter = new ChipCardGetterMock {
                ThrowException = false,
                Output = output,
                WaitMs = 0
            };
            int tries = 3;
            var resetter = new ChipCardResetter(storageSetter, storageGetter, sourceGetter, tries);
            return (storageSetter, storageGetter, sourceGetter, tries, resetter);
        }

        [TestMethod]
        public void ResetInitalisiationTest() {
            (var storageSetter, var storageGetter, var sourceGetter, int tries, var resetter) = Setup();
            Assert.IsTrue(true, "Easies good cases was tested through setup.");
            var inputs = new List<ValueTuple<IChipCardSetter, IChipCardGetter, IChipCardGetter, int>> {
                //tries is necessary as a negative number triggers a Service.
                (null,storageGetter,sourceGetter,tries),
                (storageSetter,null,sourceGetter,tries),
                (storageSetter,storageGetter,null,tries),
                (null,null,null,tries),
            };
            foreach (var input in inputs) {
                //no lambda for better debugging
                (var a, var b, var c, int d) = input;
                try {
                    new ChipCardResetter(a, b, c, d);
                    Assert.Fail("Expected all null Interfaces to throw an exception");
                }
                catch {
                    Assert.IsTrue(true);
                }
            }
            new ChipCardResetter(storageSetter, storageGetter, sourceGetter);
            Assert.IsTrue(true, "Initialisation with Settings worked!");
        }

        [TestMethod]
        public void ResetTest() {
            //Storage does exists
            (var storageSetter, var storageGetter, var sourceGetter, int tries, var resetter) = Setup();
            doReset(resetter);

            //Storage does not exists
            storageGetter.ThrowException = true;
            doReset(resetter);

            //source takes longer than storage
            storageGetter.ThrowException = false;
            sourceGetter.WaitMs = 500;
            doReset(resetter);

            //storage takes longer than source
            sourceGetter.WaitMs = 0;
            storageSetter.WaitMs = 300;
            storageGetter.WaitMs = 300;
            doReset(resetter);
        }

        private void doReset(ChipCardResetter resetter) {
            var task = resetter.Reset();
            task.Wait();
            Assert.IsTrue(task.Result);
        }

        [TestMethod]
        public void ResetTestTimeouts() {
            (var storageSetter, var storageGetter, var sourceGetter, int tries, var resetter) = Setup();

            //source allways unavailable
            sourceGetter.ThrowException = true;
            try {
                doReset(resetter);
                Assert.Fail("This should not be reached!");
            }
            catch {
                Assert.IsTrue(true);
            }

            //storageSetter can't write
            sourceGetter.ThrowException = false;
            storageSetter.ThrowException = true;
            try {
                doReset(resetter);
                Assert.Fail("This should not be reached!");
            }
            catch {
                Assert.IsTrue(true);
            }
        }
    }
}
