using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces {
    /// <summary>
    /// This Interface is needed to decouble the DeleteAll Functionallity from the IChipCardSetter.
    /// This functionality is not and should not be part of the SetterFunctionality, as it 
    /// circumvents security concerns. This functionality is necessary due to the timly implementation
    /// of the IChipCardSetter.
    /// </summary>
    public interface IChipCardDeleter {
        bool DeleteAll();
    }
}
