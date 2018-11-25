using Domain;
using System;

namespace DomainTest {

    /// <summary>
    /// Mock implementation of IChipCard for testing porpuses.
    /// </summary>
    public class ChipCardMock : IChipCard {
        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public bool Active { get; set; }

        public string ChipUId { get; set; }

        public ChipCardMock() { }

        public ChipCardMock(DateTime vFrom, DateTime vTo, bool active, string cuid) {
            ValidFrom = vFrom;
            ValidTo = vTo;
            Active = active;
            ChipUId = cuid;
        }
    }
}
