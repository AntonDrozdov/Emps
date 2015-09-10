using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emps.Models;
using System.Data.Entity;
using emps.DataContex.CFContext;

namespace emps.Controllers
{
    public class AdminController : Controller
    {
        CFContex db = new CFContex();
        
        //EMPLOYEES
        public ActionResult ListEmployees()
        {
            var list = db.Employees.Include(e=>e.Position);
            return View(list.ToList());
        }
        [HttpGet]
        public ActionResult CreateEmployee()
        {
            // Формируем список команд для передачи в представление
            SelectList list = new SelectList(db.Positions, "Id", "Title");
            ViewBag.Positions = list;
            return View();
        }
        [HttpPost]
        public ActionResult CreateEmployee(Employee employee)
        {
            //Добавляем игрока в таблицу
            db.Employees.Add(employee);
            db.SaveChanges();
            // перенаправляем на главную страницу
            return RedirectToAction("ListEmployees");
        }
        [HttpGet]
        public ActionResult EditEmployee(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            // Находим в бд футболиста
            Employee player = db.Employees.Find(id);
            if (player != null)
            {
                // Создаем список команд для передачи в представление
                SelectList list = new SelectList(db.Positions, "Id", "Title", player.PositionId);
                ViewBag.Positions = list;
                return View(player);
            }
            return RedirectToAction("ListEmployees");
        }
        [HttpPost]
        public ActionResult EditEmployee(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListEmployees");
        }
        [HttpGet]
        public ActionResult DeleteEmployee(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Employee e = db.Employees.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            return View(e);
        }
        [HttpPost, ActionName("DeleteEmployee")]
        public ActionResult DeleteEmployeeConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Employee b = db.Employees.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Employees.Remove(b);
            db.SaveChanges();
            return RedirectToAction("ListEmployees");
        }



        public ActionResult ListPositions()
        {
            var list = db.Positions;
            return View(list.ToList());
        }
        public ActionResult ListSkills()
        {
            var list = db.Skills;
            return View(list.ToList());
        }

    }
}
