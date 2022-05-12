using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SistemaCaminhoesMVC.Models.Entities;
using SistemaCaminhoesMVC.Models.ViewModels;
using static SistemaCaminhoesMVC.Models.Enums.EnumsCommons;
using SistemaCaminhoesMVC.Services.Interfaces;
using System.Threading.Tasks;
using System.Diagnostics;
using SistemaCaminhoesMVC.Models;
using SistemaCaminhoesMVC.Services.Exceptions;
using System;
using System.Net;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SistemaCaminhoesMVC.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IModelService _modelService;
        public VehiclesController(IVehicleService vehicleService, IModelService modelService)
        {
            _vehicleService = vehicleService;
            _modelService = modelService;
        }
        public async Task<IActionResult> Index()
        {
            var listVeiculos = await _vehicleService.GetAllAsync();
            return View(listVeiculos);
        }
        public async Task<IActionResult> Create()
        {
            var Values = _vehicleService.GetYearsAvailable();
            var aList = Values.Select((x, i) => new { Value = x, Data = x }).ToList();
            ViewBag.yearsList = new SelectList(aList, "Value", "Data");

            var models = await _modelService.GetAllAsync();
            var viewModel = new VehicleFormViewModels { Models = models };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                var models = await _modelService.GetAllAsync();
                var viewModel = new VehicleFormViewModels { Models = models, Vehicle = vehicle };
                var Values = _vehicleService.GetYearsAvailable();
                var aList = Values.Select((x, i) => new { Value = x, Data = x }).ToList();
                ViewBag.yearsList = new SelectList(aList, "Value", "Data");

                return View(viewModel);
            }

            try
            {
                await _vehicleService.InsertAsync(vehicle);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                var Values = _vehicleService.GetYearsAvailable();
                var aList = Values.Select((x, i) => new { Value = x, Data = x }).ToList();
                ViewBag.yearsList = new SelectList(aList, "Value", "Data");

                ModelState.AddModelError("CustomError", e.Message);
                var models = await _modelService.GetAllAsync();
                var viewModel = new VehicleFormViewModels { Models = models, Vehicle = vehicle };
                return View(viewModel);
            }



        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Vehicles Not found" });
            }
            var obj = await _vehicleService.GetByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Vehicles not found" });
            }

            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return RedirectToAction(nameof(Error), new { message = "Vehicles not found" });
                }
                await _vehicleService.DeleteAsync(id);
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Vehicles not found" });
            }
            var obj = await _vehicleService.GetByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Vehicles not found" });
            }
            return View(obj);
        }
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Vehicles not found" });
            }
            var obj = await _vehicleService.GetByIdAsync(id.Value);

            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Seller not found" });
            }
            var Values = _vehicleService.GetYearsAvailable();
            var aList = Values.Select((x, i) => new { Value = x, Data = x }).ToList();
            ViewBag.yearsList = new SelectList(aList, "Value", "Data");
            List<Model> models = await _modelService.GetAllAsync();
            VehicleFormViewModels viewModels = new VehicleFormViewModels { Models = models, Vehicle = obj };
            return View(viewModels);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                var models = await _modelService.GetAllAsync();
                var viewModel = new VehicleFormViewModels { Models = models, Vehicle = vehicle };
                var Values = _vehicleService.GetYearsAvailable();
                var aList = Values.Select((x, i) => new { Value = x, Data = x }).ToList();
                ViewBag.yearsList = new SelectList(aList, "Value", "Data");


                return View(viewModel);
            }
            if (id != vehicle.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _vehicleService.UpdateAsync(vehicle);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                var Values = _vehicleService.GetYearsAvailable();
                var aList = Values.Select((x, i) => new { Value = x, Data = x }).ToList();
                ViewBag.yearsList = new SelectList(aList, "Value", "Data");

                ModelState.AddModelError("CustomError", e.Message);
                var models = await _modelService.GetAllAsync();
                var viewModel = new VehicleFormViewModels { Models = models, Vehicle = vehicle };
                return View(viewModel);
            }

        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View(viewModel);
        }
    }
}
