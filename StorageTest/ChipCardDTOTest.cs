using Microsoft.VisualStudio.TestTools.UnitTesting;
using Storage;
using DomainTest;

namespace StorageTest {
    [TestClass]
    public class ChipCardDTOTest {
        [TestMethod]
        public void TestEqual() {
            ChipCardGeneralTests.EqualBehaviour((uid, from, to, active) => new ChipCardDTO(from, to, active, uid));
        }

        [TestMethod]
        public void TestHash() {
            ChipCardGeneralTests.HashBehaviour((uid, from, to, active) => new ChipCardDTO(from, to, active, uid));
        }

        [TestMethod]
        public void TestInterfaceInitialisation() {
            ChipCardGeneralTests.InterfaceInitialisation(icc => new ChipCardDTO(icc));
        }

        [TestMethod]
        public void TestValueInitalisiation() {
            ChipCardGeneralTests.ValueInitalisiation((cuid, vfrom, vto, active) => new ChipCardDTO(vfrom, vto, active, cuid));
        }
    }
}
