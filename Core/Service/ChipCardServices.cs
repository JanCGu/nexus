using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Service {
    /// <summary>
    /// This class contains all chipcard services.
    /// 
    /// A service is a function/algorithm which is not explicitly part
    /// of the business logic, as it is present in domain, but needed
    /// for diffrent infastructures or other services to work together.
    /// Basicly it is a glue function and not a model function.
    /// 
    /// Therefore they are all static.
    /// </summary>
    public static class ChipCardServices {
        /// <summary>
        /// Converts a json Object to a ienumerable IChipCard through
        /// the ChipCardImplementation, which adheres to the IChipCard.
        /// 
        /// This generic implementation allows for diffren IChipCards to be used.
        /// Therefore a non generic method for each ACL or other use does not have to be implemented.
        /// </summary>
        /// <param name="json"></param>
        /// <exception cref="InvalidCastException">If an error occured douring
        /// the transformation a InvalidCastException is thrown.</exception>
        public static IEnumerable<IChipCard> GetFromJson<ChipCardImplementation>(string json) where ChipCardImplementation  : IChipCard{
            try {
                var chipcards = JsonConvert.DeserializeObject<List<ChipCardImplementation>>(json) ;
                return chipcards.Select(card => (IChipCard)card);
            }
            catch (Exception ex) {
                throw new InvalidCastException("The json string which should contain a single chipcard was not valid!", ex);
            }
        }

        /// <summary>
        /// Converts a ienumerable of IChipCards to a json.
        /// </summary>
        public static string ConvertToJson(IEnumerable<IChipCard> toConvert) {
            return JsonConvert.SerializeObject(toConvert);
        }
    }
}
