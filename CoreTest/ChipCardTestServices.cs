using Domain;
using DomainTest;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreTest {
    public static class ChipCardTestServices {
        public static (string, List<IChipCard>) ValidChipCards() {
            string json = "[" +
                "{\"Active\":true,\"ChipUid\":\"00001536026278\",\"ValidFrom\":\"2000-12-31T23:00:00.000Z\",\"ValidTo\":\"2039-12-31T22:59:59.000Z\"}," +
                "{\"Active\":false,\"ChipUid\":\"00000506917745\",\"ValidFrom\":\"2000-12-31T23:00:00.000Z\",\"ValidTo\":\"2015-11-30T22:59:00.000Z\"}," +
                "{\"Active\":true,\"ChipUid\":\"00001821993344\",\"ValidFrom\":\"2004-12-31T23:00:00.000Z\",\"ValidTo\":\"2015-11-30T22:59:00.000Z\"}" +
                "]";
            var list = new List<IChipCard>() {
                new ChipCardMock(DateTime.Parse("2000-12-31T23:00:00.000Z").ToUniversalTime(),DateTime.Parse("2039-12-31T22:59:59.000Z").ToUniversalTime(),true,"00001536026278"),
                new ChipCardMock(DateTime.Parse("2000-12-31T23:00:00.000Z").ToUniversalTime(),DateTime.Parse("2015-11-30T22:59:00.000Z").ToUniversalTime(),false,"00000506917745"),
                new ChipCardMock(DateTime.Parse("2004-12-31T23:00:00.000Z").ToUniversalTime(),DateTime.Parse("2015-11-30T22:59:00.000Z").ToUniversalTime(),true,"00001821993344")
            };
            return (json, list);
        }
    }
}
