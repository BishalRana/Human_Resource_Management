using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Human_Resource_Management.Models;
using Human_Resource_Management.Service;
using Human_Resource_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Human_Resource_Management.Controllers
{
    public class PositionController : Controller
    {

        public PositionController(IPosition _Position)
        {
            _IPosition = _Position;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Position> positions = _IPosition.GetAll();
            PositionViewModel positionViewModel = new PositionViewModel();
            positionViewModel.Positions = positions;
            return View(positionViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        //POST: Position/Create
        // To protect from overposting attacks, enabling the specific fields to bind to,for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Position_Type")]Position position)
        {
            if (ModelState.IsValid)
            {
                if (_IPosition.Create(position))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Unable To create.";
                }
            }
           
            return View(position);

        }

        // GET: Position/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Position position = _IPosition.GetPosition((int)id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Position_Type")] Position position)
        {
            if (id != position.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                if (_IPosition.Update(position))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Unable To Update.";
                    return View(position);
                }
            }
            else
            {
                return View(position);
            }

        }

        //GET : Position/Delete/id
        public IActionResult Delete(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Position position = _IPosition.GetPosition((int)id);

            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        //Post : Position/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Position position = _IPosition.GetPosition(id);
            if (_IPosition.Delete(position))
            {
                return RedirectToAction("Index");

            }
            ViewBag.Message = "Unable To Delete.";
            return View(position);
        }


        private IPosition _IPosition;
    }
}
