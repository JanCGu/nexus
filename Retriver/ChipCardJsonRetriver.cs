using Core.Interfaces;
using Core.Service;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Retriver {
    /// <summary>
    /// Allows to get a JSON response through an IHttpClient.
    /// 
    /// For concerns using a fully and OS independent core implrementation see:
    /// https://stackoverflow.com/questions/43118384/microsoft-aspnet-webapi-client-supported-in-net-core-or-not
    /// </summary>
    public class ChipCardJsonRetriver : IChipCardGetter, IDisposable {
        private readonly IHttpHandler client;

        private readonly string chipCardGetUrl;

        /// <summary>
        /// Allows to overwrite the used HttpClient.
        /// The Client will be deposed together with the class.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="ChipCardGetUrl">If not null,empty or white spaces
        /// this string will set the URL Address where to get the json.
        /// Otherwise the Settings will be retrived from App.config</param>
        public ChipCardJsonRetriver(IHttpHandler client, string ChipCardGetUrl = null) {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            chipCardGetUrl = ChipCardGetUrl;
            if (string.IsNullOrWhiteSpace(ChipCardGetUrl))
                chipCardGetUrl = SettingsService.GetChipCardSourceURL();
        }

        public async Task<HashSet<IChipCard>> All() {

            var response = await client.GetAsync(chipCardGetUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var chipcards = ChipCardServices.GetFromJson<ChipCardJsonRetriverModel>(responseBody);
            return chipcards.ToHashSet();
        }

        public void Dispose() {
            if (client != null)
                client.Dispose();
        }
    }
}
