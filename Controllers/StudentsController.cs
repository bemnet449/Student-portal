using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.web.Data;
using StudentPortal.web.Models;
using StudentPortal.web.Models.Entities;

namespace StudentPortal.web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: StudentsController/Add
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddStudentsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel); // Re-display form with validation errors
            }

            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed
            };
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();
             return RedirectToAction("List", "Students"); 
        }

        [HttpGet]
        public async Task<ActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);
            return View(student); 
        }

        // POST: StudentsController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Student viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel); // Return to the edit form with validation errors
            }

            var student = await dbContext.Students.FindAsync(viewModel.Id);

            if (student != null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students"); // Redirect to the List page after saving changes
        }
        [HttpPost]
        public async Task<ActionResult> Delete(Student viewModel){

            var student = await dbContext.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(x=>x.Id==viewModel.Id);

            if(student is not null){
                dbContext.Students .Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
             return RedirectToAction("List", "Students");

        }
    }
}
