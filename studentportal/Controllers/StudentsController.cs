using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using studentportal.Data;
using studentportal.Models;
using studentportal.Models.Entities;

namespace studentportal.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)//add form dat in the database
        {
            var students = new Student
            {
                Id = Guid.NewGuid(), // Ensure you generate a new ID for the student
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed
            };

            await dbContext.Students.AddAsync(students);
            await dbContext.SaveChangesAsync(); // Await this method to save changes
            return View(); // Redirect to a different action after saving
        }

        [HttpGet]

        public async Task<IActionResult> List() { //print the data from the database,"List" is a view file to do something
            var students = await dbContext.Students.ToListAsync();//"Studnts" here is the model

            return View(students);//view the data
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id) {
            var student = await dbContext.Students.FindAsync(id);//edit the data by the user id
            return View(student);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Student viewModel) {
            var student = await dbContext.Students.FindAsync(viewModel.Id);

            if (student == null)
            {
                return NotFound();
            }

            // Update the student's properties
            student.Name = viewModel.Name;
            student.Email = viewModel.Email;
            student.Phone = viewModel.Phone;
            student.Subscribed = viewModel.Subscribed;

            await dbContext.SaveChangesAsync();

            return RedirectToAction("List","Students"); // Redirect to the list of students after edit the data
        }

        [HttpPost]

        public async Task<IActionResult> Delete(Student viewModel) {
            var student = await dbContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (student is not null) {
                dbContext.Students.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students"); // Redirect to the list of students after edit the data

        }
    }
}
