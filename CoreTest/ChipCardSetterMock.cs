using Core.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreTest {

    /// <summary>
    /// Mock implementation of IChipCardSetter for testing.
    /// 
    /// This is done in an async manner in order to also test timing.
    /// </summary>
    internal class ChipCardSetterMock : IChipCardSetter {
        public HashSet<IChipCard> Output { get; set; }
        public HashSet<IChipCard> Input { get; set; }
        public bool ThrowException { get; set; }
        public int WaitMs { get; set; }

        private Task<HashSet<IChipCard>> doOperation(HashSet<IChipCard> store) {
            Input = store;
            if (ThrowException)
                throw new Exception();
            if (WaitMs > 0)
                Task.Delay(WaitMs);
            return Task.FromResult(Output);
        }

        public async Task<HashSet<IChipCard>> Delete(HashSet<IChipCard> toDelete) {
            return await doOperation(toDelete);
        }

        public async Task<HashSet<IChipCard>> Insert(HashSet<IChipCard> toUpdate) {
            return await doOperation(toUpdate);
        }
    }
}
