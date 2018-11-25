using Core.Interfaces;
using Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Config {
     public static class ChipCardDeleterFactory {
        public static IChipCardDeleter CompleteStorageDeleter() {
            return new ChipCardFileDeleter();
        }
    }
}
