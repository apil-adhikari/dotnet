using CrudStudentInfo.Models;
using CrudStudentInfo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudStudentInfo.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        // Reading list of students from the database(we use ApplicationDbContext) and pass that list to the view. To use we need constructor of the class ApplicationDbContext

        public StudentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Listing all students
        public async Task<IActionResult> Index()
        {
            var student = await dbContext.Students.ToListAsync();
            return View(student);
        }

        // Add Student Data
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(StudentInfo studentViewModel)
        {
            var newStudent = new StudentInfo
            {
                Name = studentViewModel.Name,
                Email = studentViewModel.Email,
                Department = studentViewModel.Department,
                Address = studentViewModel.Address,
            };

            await dbContext.AddAsync(newStudent);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //  return View();
        }

        //Edit Student Data
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await dbContext.Students.FindAsync(id);

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentInfo studentViewModel)
        {
            var student = await dbContext.Students.FindAsync(studentViewModel.Id);

            if (student != null)
            {
                student.Name = studentViewModel.Name;
                student.Email = studentViewModel.Email;
                student.Department = studentViewModel.Department;
                student.Address = studentViewModel.Address;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Delete student data
        public async Task<IActionResult> Delete(int id)
        {
            var student = await dbContext.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound(); // Handle student not found case
            }
            return View(student); // Display confirmation view
        }

        [HttpPost]
        public async Task<IActionResult> Delete(StudentInfo studentViewModel)
        {
            var student = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == studentViewModel.Id);
            if (student == null)
            {
                return NotFound();
            }
            dbContext.Students.Remove(student);
            await dbContext.SaveChangesAsync(); //Asynchrounous save changes
            return RedirectToAction(nameof(Index)); //Redirect to success view
        }

        // Show Deatil of selected student 
        [HttpGet]
        public async Task<IActionResult> Detail(StudentInfo studentViewModel)
        {
            var student = await dbContext.Students.FindAsync(studentViewModel.Id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

    }
}
