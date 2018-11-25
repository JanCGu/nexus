using Config;
using Core.Interfaces;
using DisplayRazor.Models;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DisplayRazor.Controllers {
    public class ChipCardController : Controller, IChipCardWhereGetter, IChipCardResetter {
        private int tries = -1;

        /// <summary>
        /// Allows to get and set the private variable tries.
        /// At the same time no varialbe is exposed only interfaces.
        /// This is atleast needed for testing.
        /// </summary>
        public int Tries { get => tries; set => tries = value; }

        [HttpPost]
        public IActionResult Display(string Active, string ValidFrom, string ValidTo, string IdLike) {
            ViewData["Message"] = "";

            bool? active = Active.ToNullable<bool>();
            var vFrom = ValidFrom.ToNullableDateTime();
            var vTo = ValidTo.ToNullableDateTime();
            string id = IdLike?.Trim() ?? "";

            ViewData["Active"] = Active;
            ViewData["ValidFrom"] = ValidFrom;
            ViewData["ValidTo"] = ValidTo;
            ViewData["IdLike"] = id;

            return DisplayWhere(card => {
                bool choose = true;
                choose &= active.HasValue ? card.Active == active.Value : true;
                choose &= vFrom.HasValue ? card.ValidFrom >= vFrom.Value : true;
                choose &= vTo.HasValue ? card.ValidTo <= vTo.Value : true;
                choose &= card.ChipUId.Contains(id);
                return choose;
            });
        }

        /// <summary>
        /// This function either return the display page or the reset page if something went wrong.
        /// It also allows to filter the passed chipcards to the display page.
        /// </summary>
        /// <param name="where">This function specifies what conditions have to be met for the chipcards in order to be
        /// bassed to the display page.</param>
        private IActionResult DisplayWhere(Func<IChipCard, bool> where) {
            try {
                var task = Where(where);
                task.Wait();
                return View("Display", task.Result.Select(card => new ChipCardModel(card)));
            }
            catch (Exception) {
                ViewData["Message"] = "An error occured at the retrivial ob the stored chipcards! It might not be initalised. Please initalise it through resetting.";
                var reset = new ResetModel();
                return View("ResetApp", reset);
            }
        }

        [HttpGet]
        public IActionResult Display() {
            ViewData["Message"] = "";
            return DisplayWhere(card => true);//gets alls
        }

        [HttpGet]
        public IActionResult ResetApp() {
            var reset = new ResetModel();
            return View(reset);
        }

        [HttpPost]
        public IActionResult ResetApp(ResetModel reset) {
            if (!ModelState.IsValid) {
                ViewData["Message"] = "Please enter a number for the tries.";
                return View(reset);
            }
            tries = reset.Tries;
            try {
                var task = Reset();
                task.Wait();
                if (!task.Result) {
                    ViewData["Message"] = $"Reset failed after {reset.Tries} attempts!";
                    return View(reset);
                }
                ViewData["Message"] = "Sucessfully resetted the cached chip cards.";

            }
            catch (Exception ex) {
                ViewData["Message"] = "An error occured: " + ex.ToString();
            }
            return View(reset);

        }


        public IActionResult Documentation() {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<HashSet<IChipCard>> LikeId(string ChipUId) {
            var storageGetter = ChipCardWhereGetterFactory.WhereFromStorage();
            return await storageGetter.LikeId(ChipUId);
        }

        public async Task<HashSet<IChipCard>> Within(DateTime fromIncluding, DateTime tillIncluding) {
            var storageGetter = ChipCardWhereGetterFactory.WhereFromStorage();
            return await storageGetter.Within(fromIncluding, tillIncluding);
        }

        public async Task<HashSet<IChipCard>> ByActive(bool isActive) {
            var storageGetter = ChipCardWhereGetterFactory.WhereFromStorage();
            return await storageGetter.ByActive(isActive);
        }

        public async Task<HashSet<IChipCard>> Where(Func<IChipCard, bool> where) {
            var storageGetter = ChipCardWhereGetterFactory.WhereFromStorage();
            return await storageGetter.Where(where);
        }

        public async Task<HashSet<IChipCard>> All() {
            var storageGetter = ChipCardWhereGetterFactory.WhereFromStorage();
            return await storageGetter.All();
        }


        public async Task<bool> Reset() {
            var resetter = ChipCardResetterFactory.NetWorkStorageResetter(tries);
            return await resetter.Reset();
        }
    }
}
