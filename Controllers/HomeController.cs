using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewnetCore.Models;
using NewnetCore.ViewModels;

namespace NewnetCore.Controllers
{
      [Authorize]
    public class HomeController : Controller
    {
        private readonly IPerson _person;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly ILogger<HomeController> logger;

        public HomeController(IPerson person, 
                              IWebHostEnvironment hostingEnvironment, 
                              ILogger<HomeController> logger )
           {
            _person = person;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
        }
        [AllowAnonymous]
        public ViewResult Index()
        {
            var model = _person.GetAllPersonsP();
            return View(model);
        }
        [AllowAnonymous]
        public ViewResult Details(int? Id)
        {
            // throw new Exception("Error in Details View");
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");
            PersonsP personsP = _person.GetPersonsP(Id.Value);
            if(personsP == null)
            {
                Response.StatusCode = 404;
                return View("index", Id.Value);
            }
            HomeDetailsViewModels homeDetailsViewModels = new HomeDetailsViewModels()
            {
                PersonsP = personsP,
                PageTitle = "Persons Details"
            };

            return View(homeDetailsViewModels);
        }
         //create httpGet request..........................  
        [HttpGet] 
        [Authorize]
        public  ViewResult Create()
            {
            return View();
            }
        //
        [HttpGet]
        [Authorize]
        public ViewResult Edit( int? id)
        {
            var personsP = _person.GetPersonsP(id?? 1);
            PersonsEditViewModels personsEditViewModels = new PersonsEditViewModels
            {
                Id = personsP.Id,
                Name = personsP.Name,
                Task = personsP.Task,
                Date = personsP.Date,
                ExistingPhotoPath = personsP.PhotoPath
            };
            return View(personsEditViewModels);
        }
        //edit httpPost Resquest......................
        [HttpPost]
        [Authorize]
        public IActionResult Edit(PersonsEditViewModels model)
        {
            if (ModelState.IsValid)
            {
                PersonsP personsP = _person.GetPersonsP(model.Id);
                personsP.Name = model.Name;
                personsP.Task = model.Task;
                personsP.Date = model.Date;
                if(model.Photo != null)
                {
                 if(model.ExistingPhotoPath != null)
                    {
                  string filePath = Path.Combine (hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    personsP.PhotoPath = ProcessUploadedFile(model);
                }
               
                _person.Update(personsP);
                return RedirectToAction("index"); 
            }
            return View();
        }

        private string ProcessUploadedFile(PersonsCreateViewModels model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }

        //create  with picture post reqest..................
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create(PersonsCreateViewModels model)
        {
            if (ModelState.IsValid){
                string uniqueFileName = ProcessUploadedFile(model);
                PersonsP newPersonsP = new PersonsP
                {
                    Name = model.Name,
                    Task = model.Task,
                    Date = model.Date,
                    PhotoPath = uniqueFileName
                };
                _person.Add(newPersonsP);
              return RedirectToAction("details", new { id = newPersonsP.Id });
            }
            return View();
        }
        // PersonsP model = _person.GetPersonsP(1);
        // return View(model); 
        //_person.GetPersonsP(1).Name;
    }

    }
// ...........Image Process codes ......................
//string uniqueFileName = null;
//                if (model.Photo != null)
//                {
//                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
//uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
//                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
//model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

//                }
//  ...........CREATION OF PRERSONSP OBJECT .............
//PersonsP newPersonsP = new PersonsP
//{
//    Name = model.Name,
//    Task = model.Task,
//    Date = model.Date,
//    PhotoPath = uniqueFileName
//};

