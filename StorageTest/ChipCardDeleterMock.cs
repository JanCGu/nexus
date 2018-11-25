using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageTest {
    public class ChipCardDeleterMock : IChipCardDeleter {
        public bool Output { get; set; }
        public ChipCardDeleterMock() { }

        public bool DeleteAll() {
            return Output;
        }
    }
}
