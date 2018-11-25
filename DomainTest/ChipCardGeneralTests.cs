using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DomainTest {

    /// <summary>
    /// This static class allows to test each IChipCard implementation for the expected behaviour.
    /// </summary>
    public static class ChipCardGeneralTests {


        public static void EqualBehaviour(CreateIChipCard create) {
            var ccs = CreateTriple(create);
            Console.WriteLine("A chipcard should be equal to itself!");
            Assert.AreEqual(ccs[0], ccs[0]);

            Console.WriteLine("Only the id matters for equality.");
            Assert.AreEqual(ccs[0], ccs[1]);

            Console.WriteLine("If everything exepct the id is the same they are still two objects!");
            Assert.AreNotEqual(ccs[0], ccs[2]);
        }

        public static void HashBehaviour(CreateIChipCard create) {
            var ccs = CreateTriple(create);
            Console.WriteLine("A chipcard should be allways return the same hash!");
            Assert.AreEqual(ccs[0].GetHashCode(), ccs[0].GetHashCode());

            Console.WriteLine("Two chipcards with the same id should have the same hash!");
            Assert.AreEqual(ccs[0].GetHashCode(), ccs[1].GetHashCode());

            Console.WriteLine("If everything exepct the id is the same there should still be two hashes!");
            Assert.AreNotEqual(ccs[0].GetHashCode(), ccs[2].GetHashCode());
        }

        private static List<IChipCard> CreateTriple(CreateIChipCard create) {
            return new List<IChipCard> {
                create("id1", new DateTime(2017, 12, 10), new DateTime(2017, 12, 11), true),
                 create("id1", new DateTime(2015, 09, 20), new DateTime(2015, 09, 21), false),
                 create("id2", new DateTime(2017, 12, 10), new DateTime(2017, 12, 11), true)
            };
        }

        public static void InterfaceInitialisation(CreateIChipCardWithIChipCard interfaceCreate) {
            ValueInitalisiation((cuid, vfrom, vto, active) => {
                var mock = new ChipCardMock {
                    ChipUId = cuid,
                    Active = active,
                    ValidFrom = vfrom,
                    ValidTo = vto
                };
                return interfaceCreate(mock);
            });
        }

        public static void ValueInitalisiation(CreateIChipCard create) {
            var inputs = InputTests();
            foreach (var input in inputs) {
                (string cardUId, var validFrom, var validTo, bool active, bool valid) = input;
                try {
                    create(cardUId, validFrom, validTo, active);
                    Assert.IsTrue(valid, $"Expected to function with the dataset {input}");
                }
                catch (Exception) {
                    Assert.IsFalse(valid, $"Expected to fail the creation with the dataset {input}");
                }
            }
        }

        /// <summary>
        /// Used to setup the initalisiation tests.
        /// </summary>
        /// <returns>A List of Valuetuples with (chipUID,ValdiFrom,ValidTro,Active) and if a valid chipcard is expected.</returns>
        private static List<ValueTuple<string, DateTime, DateTime, bool, bool>> InputTests() {
            return new List<ValueTuple<string, DateTime, DateTime, bool, bool>>() {
                ("id",new DateTime(2017, 12, 10), new DateTime(2017, 12, 11), true,true),
                ("id",new DateTime(2017, 12, 10), new DateTime(2017, 12, 10), true,true),
                ("id",new DateTime(2017, 12, 10), new DateTime(2017, 12, 09), true,true),
                ("id",new DateTime(), new DateTime(2017, 12, 11), true,true),
                ("id",new DateTime(2017, 12, 10), new DateTime(), true,true),
                ("id",new DateTime(), new DateTime(), true,true),
                ("id",new DateTime(2017, 12, 10), new DateTime(2017, 12, 11), false,true),
                ("",new DateTime(2017, 12, 10), new DateTime(2017, 12, 11), true,false),
                (null,new DateTime(2017, 12, 10), new DateTime(2017, 12, 11), true,false)
            };
        }



    }

    /// <summary>
    /// Used to Create a IChipCard, which will create the interfaces then used for testing.
    /// </summary>
    /// <param name="ChipUId"></param>
    /// <param name="DateFrom"></param>
    /// <param name="DateTo"></param>
    /// <param name="Active"></param>
    /// <returns></returns>
    public delegate IChipCard CreateIChipCard(string ChipUId, DateTime DateFrom, DateTime DateTo, bool Active);
    public delegate IChipCard CreateIChipCardWithIChipCard(IChipCard toConvert);
}
