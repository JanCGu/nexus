using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Config;
using Core.Service;


namespace Reset {
    class ChipCardResetter : IChipCardResetter {

        private readonly IChipCardSetter storageSetter;
        private readonly IChipCardGetter networkGetter;

        public ChipCardResetter(IChipCardSetter storageSetter, IChipCardGetter networkGetter) {
            this.storageSetter = storageSetter ?? throw new ArgumentNullException(nameof(storageSetter));
            this.networkGetter = networkGetter ?? throw new ArgumentNullException(nameof(networkGetter));
        }

        public Task<bool> Reset() {
            throw new NotImplementedException();
        }
    }
}
