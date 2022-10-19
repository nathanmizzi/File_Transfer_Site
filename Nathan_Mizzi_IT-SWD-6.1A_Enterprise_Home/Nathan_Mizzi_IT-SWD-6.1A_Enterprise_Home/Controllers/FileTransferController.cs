using Application.ViewModels;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RestSharp;
using RestSharp.Authenticators;
using Ionic.Zip;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    public class FileTransferController : Controller
    {

        private IFileTransferService fileTransferService;
        private ILogService logService;
        private IWebHostEnvironment webHostingEnvironment;

        public FileTransferController(IFileTransferService _fileTransferService,ILogService _logService, IWebHostEnvironment _webHostingEnvironment)
        {
            fileTransferService = _fileTransferService;
            logService = _logService;
            webHostingEnvironment = _webHostingEnvironment;
        }

        [Authorize]
        public IActionResult ListTransfers(string userEmail)
        {
            var listOfFileTransfers = fileTransferService.getUsersFileTransfers(HttpContext.User.Identity.Name);
            return View(listOfFileTransfers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddFileTransferViewModel model, IFormFile FilePath)
        {
            if (string.IsNullOrEmpty(model.SenderEmail) || string.IsNullOrEmpty(model.RecipientEmail) || string.IsNullOrEmpty(model.Title))
            {
                ViewBag.Error = "Neither: Email, Recipient Email, Title or the File itself may be left empty.";
                return View();
            }
            else
            {
                try
                {
                    if (FilePath != null)
                    {
                        string password;
                        string fileNameUncomplete = Guid.NewGuid().ToString();
                        string fileName = fileNameUncomplete + Path.GetExtension(FilePath.FileName);

                        string absolutePath = webHostingEnvironment.WebRootPath + "\\files\\" + fileName;

                        using (FileStream fs = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write))
                        {
                            FilePath.CopyTo(fs);
                            fs.Close();
                        }
                        model.FilePath = @"\files\" + fileNameUncomplete + ".zip";

                        //Zipping Portion

                        using (ZipFile zip = new ZipFile(webHostingEnvironment.WebRootPath + "\\files\\"))
                        {
                            if (!string.IsNullOrEmpty(model.Password))
                            {
                                zip.Password = model.Password;
                            }

                            zip.AddFile(absolutePath, "SentFiles");
                            zip.Save(webHostingEnvironment.WebRootPath + "\\files\\" + fileNameUncomplete + ".zip");
                        }

                        //Email Portion

                        RestClient client = new RestClient();
                        client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                        client.Authenticator =
                        new HttpBasicAuthenticator("api",
                                                    "{INSERT API KEY HERE}}");
                        RestRequest request = new RestRequest();
                        request.AddParameter("domain", "{INSERT YOUR DOMAIN HERE}}", ParameterType.UrlSegment);
                        request.Resource = "{domain}/messages";

                        request.AddParameter("from", model.SenderEmail);
                        request.AddParameter("to", model.RecipientEmail);
                        request.AddParameter("subject", model.Title);

                        if (string.IsNullOrEmpty(model.Password))
                        {
                            password = "N/A";
                        }
                        else
                        {
                            password = model.Password;
                        }

                        var httpRequest = HttpContext.Request;

                        var absoluteUri = string.Concat(
                            httpRequest.Scheme,
                            "://",
                            httpRequest.Host.ToUriComponent());

                        string webrootFilesPath = absoluteUri + "/files/";

                        string zipFilePath = webrootFilesPath + fileNameUncomplete + ".zip";

                        request.AddParameter("html",
                            $"<html> {model.Message} <br><br> A file was also attached, access it with this link: <a href='{zipFilePath}' >Click Here</a> <br>Due to the website not being https, the file may not immedietly download on chrome, if this happens, simply copy the link address, paste it in the url, and press enter. <br><br> The password for your file is: {password} </html>");

                        ViewBag.absoluteUri = webrootFilesPath;

                        request.Method = Method.POST;
                        client.Execute(request);

                        logService.AddLog("File Succesfully compressed and sent!", false, HttpContext.User.Identity.Name);
                    }
                    else
                    {
                        logService.AddLog("Error Finding the File Supplied, Please check that the path is correct.", true, HttpContext.User.Identity.Name);

                        return View();
                    }
                }catch(Exception)
                {
                    logService.AddLog("Application Ran into an error during email/File Compression process", true, HttpContext.User.Identity.Name);
                }

                fileTransferService.AddFileTransfer(model);
                ViewBag.Message = "File Transfer Saved successfully";
            }

            return RedirectToAction("Create");
        }
    }
}
