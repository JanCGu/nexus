using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace DomainTest {
    [TestClass]
    public class ChipCardTest {
        [TestMethod]
        public void TestEqual() {
            ChipCardGeneralTests.EqualBehaviour((uid, from, to, active) => new ChipCard(from,to,active,uid));
        }

        [TestMethod]
        public void TestHash() {
            ChipCardGeneralTests.HashBehaviour((uid, from, to, active) => new ChipCard(from, to, active, uid));
        }

        [TestMethod]
        public void TestInterfaceInitialisation() {
            ChipCardGeneralTests.InterfaceInitialisation(icc => new ChipCard(icc));
        }

        [TestMethod]
        public void TestValueInitalisiation() {
            ChipCardGeneralTests.ValueInitalisiation((cuid, vfrom, vto, active) => new ChipCard(vfrom, vto, active, cuid));
        }
    }
}
