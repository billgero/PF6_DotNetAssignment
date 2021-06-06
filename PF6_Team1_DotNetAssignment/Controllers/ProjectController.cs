﻿using Microsoft.AspNetCore.Mvc;
using PF6_Team1_DotNetAssignment.Models;
using PF6_Team1_DotNetAssignment.Options;
using PF6_Team1_DotNetAssignment.Services;
using System.Threading.Tasks;

namespace PF6_Team1_DotNetAssignment.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController (IProjectService projectService)
        {
            _projectService = projectService;
        }
        
        // Get All Projects
        public async Task<IActionResult> Index()
        {
            return View(await _projectService.GetProjectsAsync());
        }
        // Get a single Project 
        public async Task<IActionResult> Details(int? id)           // id is Nullable
        {
            if (id == null)
            {
                return NotFound();
            }
            var project = await _projectService.GetProjectByIdAsync(id.Value);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
        //Create a Project 

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("ProjectId, Title, Description," +
            "Category, Country, MyImage, MyVideo, RequiredFunds, CurrentFunds, CreatedDate, Deadline," +
            "AmountOfViews")] Project project)
        {
            if (ModelState.IsValid)
            {
                await _projectService.CreateProjectAsync(new ProjectOption
                {
                    Title = project.Title,                          // Mapping 
                    Description = project.Description,
                    Category = project.Category,
                    Country = project.Country,
                    MyImage = project.MyImage,
                    MyVideo = project.MyVideo,
                    RequiredFunds = project.RequiredFunds,
                    CurrentFunds = project.CurrentFunds,
                    CreatedDate = project.CreatedDate,
                    Deadline = project.Deadline,
                    AmountOfViews = project.AmountOfViews
                });

                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }
        //Delete a Project 

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _projectService.GetProjectByIdAsync(id.Value);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)        
        {
            await _projectService.DeleteProjectByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Update a Project .............................


        public async Task <IActionResult> Edit(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            //Validationsss.....
            return View(new ProjectOption { // Mapping.........
             ProjectId = project.ProjectId,
            Title = project.Title,
            Description = project.Description,
            Category = project.Category,
            Country = project.Country,
            MyImage = project.MyImage,
            MyVideo= project.MyVideo,
            RequiredFunds = project.RequiredFunds,
            Deadline = project.Deadline
            });           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int projectId, [Bind("ProjectId, Title, Description," +
            "Category, Country, MyImage, MyVideo, RequiredFunds, Deadline")] ProjectOption project)                             
        {
            await _projectService.UpdateProjectById(projectId, project);
            return RedirectToAction(nameof(Index));
        }
    }
}