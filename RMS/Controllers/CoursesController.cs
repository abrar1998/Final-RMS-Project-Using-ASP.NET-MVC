using Microsoft.AspNetCore.Mvc;
using RMS.AccountRepository;
using RMS.DatabaseContext;
using RMS.Models;
using RMS.StudentCourseRepository;

namespace RMS.Controllers
{
    public class CoursesController : Controller
    {


        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICourseRepo courseRepo;

        public CoursesController(IWebHostEnvironment webHostEnvironment, ICourseRepo courseRepo)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.courseRepo = courseRepo;
        }

        [HttpGet]
        public IActionResult CourseRegistration()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CourseRegistration(CourseViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                string PdfLocation = UploadPdf(cvm);

                var course = new Course()
                {
                    CourseName = cvm.CourseName,
                    CourseFee = cvm.CourseFee,
                    CoursePdf = PdfLocation,
                };

                await courseRepo.RegisterCourse(course);
                return RedirectToAction("Index", "Home");

            }
            return View();
        }



        private string UploadPdf(CourseViewModel cvm)
        {
            string PdfName = null;
            if (cvm.UploadPdf != null && cvm.UploadPdf.Length > 0)
            {
                string PdfDir = Path.Combine(webHostEnvironment.WebRootPath, "PdfFolder");
                PdfName = Guid.NewGuid().ToString() + "-" + cvm.UploadPdf.FileName;
                string filepath = Path.Combine(PdfDir, PdfName);
                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    cvm.UploadPdf.CopyTo(filestream);
                }
            }
            return PdfName;
        }


        public  IActionResult GetCourseList()
        {
            var data =  courseRepo.GetCourseList();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadPdf(int courseId)
        {
            var course = await courseRepo.GetCourseById(courseId);

            if (course != null && !string.IsNullOrEmpty(course.CoursePdf))
            {
                var pdfPath = Path.Combine(webHostEnvironment.WebRootPath, "PdfFolder", course.CoursePdf);

                if (System.IO.File.Exists(pdfPath))
                {
                    var pdfBytes = System.IO.File.ReadAllBytes(pdfPath);
                    return File(pdfBytes, "application/pdf", $"{course.CourseName}_Course.pdf");
                }
            }

            // Handle if the course or PDF is not found
            return NotFound();
        }



        //CRUD ON COURSE TABLE

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await courseRepo.GetCourseById(id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseTodelete = courseRepo.GetCourseStudentJoinById(id);
            if (courseTodelete != null)
            {
                var pdfTodeletePath = Path.Combine(webHostEnvironment.WebRootPath, "PdfFolder", courseTodelete.CoursePdf);

                 courseRepo.DeleteCourse(courseTodelete);

                if (System.IO.File.Exists(pdfTodeletePath))
                {
                    System.IO.File.Delete(pdfTodeletePath);
                }

                return RedirectToAction("GetCourseList", "Courses");
            }
            return View();
        }





        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await courseRepo.GetCourseById(id);
            if (course != null)
            {
                var courseView = await courseRepo.GetCourseViewEditById(id);
                return View(courseView);
            }
            return NotFound();
        }



        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId, CourseName, CourseFee, CoursePdf")] CourseViewEdit course, IFormFile? newPdf)
        {
            var oldcourse = await courseRepo.GetCourseById(id);

            if (oldcourse == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (newPdf != null && newPdf.Length > 0)
                {
                    // Handle the new PDF upload
                    var newPdfFileName = GetUniqueFileName(newPdf.FileName);
                    var newPdfFilePath = Path.Combine(webHostEnvironment.WebRootPath, "PdfFolder", newPdfFileName);

                    using (var fileStream = new FileStream(newPdfFilePath, FileMode.Create))
                    {
                        await newPdf.CopyToAsync(fileStream);
                    }

                    // Delete the old PDF file
                    if (!string.IsNullOrEmpty(oldcourse.CoursePdf))
                    {
                        var oldPdfPath = Path.Combine(webHostEnvironment.WebRootPath, "PdfFolder", oldcourse.CoursePdf);
                        if (System.IO.File.Exists(oldPdfPath))
                        {
                            System.IO.File.Delete(oldPdfPath);
                        }
                    }

                    // Update the CoursePdf property with the new file name
                    oldcourse.CoursePdf = newPdfFileName;
                }

                // Update other properties
                oldcourse.CourseName = course.CourseName;
                oldcourse.CourseFee = course.CourseFee;

                // Save changes to the database using course repository
                courseRepo.UpdateCourse(oldcourse);

                return RedirectToAction("GetCourseList");
            }

            // If ModelState is not valid, return to the edit view with errors
            return View(course);
        }


        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }

    }



}

