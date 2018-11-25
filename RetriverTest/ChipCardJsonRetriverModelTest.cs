using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomainTest;
using Retriver;

namespace RetriverTest {
    [TestClass]
    public class ChipCardJsonRetriverModelTest {
        [TestMethod]
        public void TestEqual() {
            ChipCardGeneralTests.EqualBehaviour((uid, from, to, active) => new ChipCardJsonRetriverModel(from, to, active, uid));
        }

        [TestMethod]
        public void TestHash() {
            ChipCardGeneralTests.HashBehaviour((uid, from, to, active) => new ChipCardJsonRetriverModel(from, to, active, uid));
        }

        [TestMethod]
        public void TestInterfaceInitialisation() {
            ChipCardGeneralTests.InterfaceInitialisation(icc => new ChipCardJsonRetriverModel(icc));
        }

        [TestMethod]
        public void TestValueInitalisiation() {
            ChipCardGeneralTests.ValueInitalisiation((cuid, vfrom, vto, active) => new ChipCardJsonRetriverModel(vfrom, vto, active, cuid));
        }
    }
}
