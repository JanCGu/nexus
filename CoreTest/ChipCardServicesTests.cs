using Core.Service;
using Domain;
using DomainTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreTest {
    [TestClass]
    public class ChipCardServicesTests {

        

        [TestMethod]
        public void GetFromJsonTest() {

            (string json, var expectedSolution) = ChipCardTestServices.ValidChipCards();

            var casts = ChipCardServices.GetFromJson<ChipCardMock>(json);
            if (!casts.Any() || casts.Count() != 3)
                Assert.Fail("Expected three elements as return values!");
            ExactCompare(casts, expectedSolution);

            string swaped = "[{\"ChipUid\":\"00001536026278\",\"Active\":true,\"ValidFrom\":\"2000-12-31T23:00:00.000Z\",\"ValidTo\":\"2039-12-31T22:59:59.000Z\"}]";
            var swapedSolution = new List<IChipCard>() {
                new ChipCardMock(DateTime.Parse("2000-12-31T23:00:00.000Z").ToUniversalTime(),DateTime.Parse("2039-12-31T22:59:59.000Z").ToUniversalTime(),true,"00001536026278"),
            };
            casts = ChipCardServices.GetFromJson<ChipCardMock>(swaped);
            ExactCompare(casts, swapedSolution);

            //this only works because of the use of ChipCardMock
            string missing = "[" +
                "{\"Active\":true,\"ValidFrom\":\"2000-12-31T23:00:00.000Z\",\"ValidTo\":\"2039-12-31T22:59:59.000Z\"}," +
                "{\"ChipUid\":\"00001536026278\",\"ValidFrom\":\"2000-12-31T23:00:00.000Z\",\"ValidTo\":\"2039-12-31T22:59:59.000Z\"}," +
                "{\"ChipUid\":\"00001536026278\",\"Active\":true,\"ValidTo\":\"2039-12-31T22:59:59.000Z\"}," +
                "{\"ChipUid\":\"00001536026278\",\"Active\":true,\"ValidFrom\":\"2000-12-31T23:00:00.000Z\"}," +
                "{}" +
                "]";
            ChipCardServices.GetFromJson<ChipCardMock>(missing);

        }
        public void ExactCompare(IEnumerable<IChipCard> first, List<IChipCard> second) {
            if (first == null || second == null || first.Count() != second.Count)
                Assert.Fail("Expected the same number of elements!");
            int pos = 0;
            foreach (var element in first) {
                var compare = second[pos];
                pos++;
                if (compare.ChipUId != element.ChipUId)
                    Assert.Fail("ChipUID does not match!");
                if (compare.Active != element.Active)
                    Assert.Fail("Active does not match!");
                if (compare.ValidFrom != element.ValidFrom)
                    Assert.Fail($"ValidFrom does not match!");
                if (compare.ValidTo != element.ValidTo)
                    Assert.Fail("ValidFrom does not match!");
            }
        }

        [TestMethod]
        public void ConvertToJsonTest() {
            (string output, var input) = ChipCardTestServices.ValidChipCards();
            string cast = ChipCardServices.ConvertToJson(input);
            string solution = "[" +
                "{\"ValidFrom\":\"2000-12-31T23:00:00Z\",\"ValidTo\":\"2039-12-31T22:59:59Z\",\"Active\":true,\"ChipUId\":\"00001536026278\"}," +
                "{\"ValidFrom\":\"2000-12-31T23:00:00Z\",\"ValidTo\":\"2015-11-30T22:59:00Z\",\"Active\":false,\"ChipUId\":\"00000506917745\"}," +
                "{\"ValidFrom\":\"2004-12-31T23:00:00Z\",\"ValidTo\":\"2015-11-30T22:59:00Z\",\"Active\":true,\"ChipUId\":\"00001821993344\"}]";
            Assert.AreEqual(solution, cast);
        }
    }
}
