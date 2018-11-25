using Core.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreTest {

    /// <summary>
    /// Mock implementation of IChipCardGetter for testing.
    /// </summary>
    internal class ChipCardGetterMock : IChipCardGetter {
        public HashSet<IChipCard> Output { get; set; }
        public bool ThrowException { get; set; }
        public int WaitMs { get; set; }

        public async Task<HashSet<IChipCard>> All() {
            if (ThrowException)
                throw new Exception();
            if (WaitMs > 0)
                await Task.Delay(WaitMs);
            return Output;
        }
    }
}
