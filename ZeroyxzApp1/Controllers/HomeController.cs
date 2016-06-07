using System;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;


namespace ZeroyxzApp1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(System.Configuration.ConfigurationManager.AppSettings["CloudStorageAccount"]);
                CloudQueueClient client = storageAccount.CreateCloudQueueClient();
                CloudQueue queue = client.GetQueueReference("zeroyxzqueue");
                queue.CreateIfNotExists();

                Trace.TraceInformation("About to write a message to the queue");
                queue.AddMessage(new CloudQueueMessage("The time is now" + DateTime.Now.ToShortTimeString()));
                Trace.TraceWarning("Message has been written");
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error:" + ex.Message.ToString());
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}