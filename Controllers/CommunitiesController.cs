using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab4.Data;
using Lab4.Models;
using Lab4.Models.ViewModels;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Azure;
using System.IO;



namespace Lab4.Controllers
{
    public class CommunitiesController : Controller
    {
        private readonly SchoolCommunityContext _context;



        private readonly BlobServiceClient _blobServiceClient;
        private readonly string containerName = "answerimages";

        public CommunitiesController(SchoolCommunityContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;


            _blobServiceClient = blobServiceClient;
        }

        // GET: Communities
        // Did you really check my example? You missed changing the index here
        public async Task<IActionResult> Index(string ID)
        {
            var viewModel = new CommunityViewModel();
            // TODO: Please explain back to me by email what this is doing
            viewModel.Communities = await _context.Communities
                  .Include(i => i.Membership)
                    .ThenInclude(i => i.Student)
                  .AsNoTracking()
                  .OrderBy(i => i.Title)
                  .ToListAsync();

            if (ID != null)
            {
                ViewData["CommunityID"] = ID;
                viewModel.CommunityMemberships = viewModel.Communities.Where(
                    x => x.ID == ID).Single().Membership;
            }

            return View(viewModel);
        }



        /*
        // GET: Communities
        // Did you really check my example? You missed changing the index here
        public async Task<IActionResult> showAdd(string ID)
        {
            var viewModel = new AdsViewModel();
            // TODO: Please explain back to me by email what this is doing
            viewModel.Advertisements = await _context.AnswerImages
                  .Include(i => i.AnswerImageId)
                    .Include(i => i.FileName)
                     .Include(i => i.Url)
                  .AsNoTracking()
                  .OrderBy(i => i.AnswerImageId)
                  .ToListAsync();

            if (ID != null)
            {
           //     ViewData["CommunityID"] = ID;
               // viewModel.Advertisements = viewModel.Advertisements.Where(
                //    x => x.ID == ID).Single().Membership;
            }

            return View(viewModel);
        }
        */
        

        
                    public async Task<IActionResult> showAdd(string id)
                    {


      




            var viewModel = new AdsViewModel();
            // TODO: Please explain back to me by email what this is doing
            viewModel.Advertisements = await _context.AnswerImages
                .AsNoTracking() 
                  .ToListAsync();


            viewModel.comID = id;




            return View(viewModel);


        }









        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeAdd(IFormFile file,string id)
        {


            string bob = id;




          /*  Console.WriteLine(id);

            string lastPart = id.Split('%').Last();

            string bob = lastPart.Substring(2);*/




            BlobContainerClient containerClient;
            // Create the container and return a container client object
            try
            {
                containerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName);
                containerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            }
            catch (RequestFailedException)
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }


            try
            {
                // create the blob to hold the data
                var blockBlob = containerClient.GetBlobClient(file.FileName);
                if (await blockBlob.ExistsAsync())
                {
                    await blockBlob.DeleteAsync();
                }

                using (var memoryStream = new MemoryStream())
                {
                    // copy the file data into memory
                    await file.CopyToAsync(memoryStream);

                    // navigate back to the beginning of the memory stream
                    memoryStream.Position = 0;

                    // send the file to the cloud
                    await blockBlob.UploadAsync(memoryStream);
                    memoryStream.Close();
                }

                // add the photo to the database if it uploaded successfully
                var image = new AnswerImage();
                image.Url = blockBlob.Uri.AbsoluteUri;
                image.FileName = file.FileName;
               
                
                image.Memb = bob;


                _context.AnswerImages.Add(image);
                _context.SaveChanges();
            }
            catch (RequestFailedException)
            {
                View("Error");
            }

             return RedirectToAction("Index");
          // return Redirect("http://www.example.com");

        }






















        /*
        // GET: AnswerImages/Create
        public IActionResult showAd(AdsViewModel model)
        {
            return View(model);
        }

        */

        /*

        // GET: Communities
        // Did you really check my example? You missed changing the index here
        public async Task<IActionResult> showAd(string ID)
        {
            var viewModel = new AdsViewModel();
            // TODO: Please explain back to me by email what this is doing
            viewModel.Advertisements = await _context.AnswerImage
                  .Include(i => i.FileName)
                   // .ThenInclude(i => i.)
                  .AsNoTracking()
                  .OrderBy(i => i.AnswerImageId)
                  .ToListAsync();

            if (ID != null)
            {
             //   ViewData["CommunityID"] = ID;
               // viewModel.CommunityMemberships = viewModel.Communities.Where(
                   // x => x.ID == ID).Single().Membership;
            }

            return View(viewModel);
        }


        */






        // GET: AnswerImages/Create
        public IActionResult MakeAdd()
        {








            return View();
        }









        // GET: AnswerImages/Delete/5
        public async Task<IActionResult> DeleteAdd(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answerImage = await _context.AnswerImages
                .FirstOrDefaultAsync(m => m.AnswerImageId == id);
            if (answerImage == null)
            {
                return NotFound();
            }

            return View(answerImage);
        }

        // POST: AnswerImages/Delete/5
        [HttpPost, ActionName("DeleteAdd")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answerImage = await _context.AnswerImages.FindAsync(id);
            _context.AnswerImages.Remove(answerImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

























        // GET: Communities/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var community = await _context.Communities
                .FirstOrDefaultAsync(m => m.ID == id);
            if (community == null)
            {
                return NotFound();
            }

            return View(community);
        }

        // GET: Communities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Communities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Budget")] Community community)
        {
            if (ModelState.IsValid)
            {
                _context.Add(community);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(community);
        }

        // GET: Communities/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var community = await _context.Communities.FindAsync(id);
            if (community == null)
            {
                return NotFound();
            }
            return View(community);
        }

        // POST: Communities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Title,Budget")] Community community)
        {
            if (id != community.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(community);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommunityExists(community.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(community);
        }

        // GET: Communities/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var community = await _context.Communities
                .FirstOrDefaultAsync(m => m.ID == id);
            if (community == null)
            {
                return NotFound();
            }

            return View(community);
        }

        // POST: Communities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var community = await _context.Communities.FindAsync(id);
            _context.Communities.Remove(community);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommunityExists(string id)
        {
            return _context.Communities.Any(e => e.ID == id);
        }



    }

}
