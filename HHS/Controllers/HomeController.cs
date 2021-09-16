using HHS.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HHS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<JobInJaModel> jobs = new List<JobInJaModel>();

            HtmlDocument html = new HtmlDocument();
            HtmlWeb web = new HtmlWeb();

            html = web.Load("https://jobinja.ir/jobs");

            var data = html.DocumentNode.Descendants("ul").Where(x => x.GetAttributeValue("class", "").Equals("o-listView__list c-jobListView__list")).ToList()[0];
            var data2 = data.Descendants("li").Where(x => x.GetAttributeValue("class", "").Trim().StartsWith("o-listView__item o-listView__item--hasIndicator c-jobListView__item")).ToList();

            foreach (var item in data2)
            {
                var Title = item.Descendants("a").Where(x => x.GetAttributeValue("class", "").Equals("c-jobListView__titleLink")).First().InnerHtml;
                var Image = item.Descendants("img").Where(x => x.GetAttributeValue("class", "").Equals("o-listView__itemIndicatorImage")).Select(e => e.GetAttributeValue("src", null)).First();
                var JobFeatures = item.Descendants("li").Where(x => x.GetAttributeValue("class", "").Equals("c-jobListView__metaItem")).ToList();
                var Company = JobFeatures[0].Descendants("span").First().InnerHtml;
                var Location = JobFeatures[1].Descendants("span").First().InnerHtml;
                var ContractType = JobFeatures[2].Descendants("span").First().Descendants("span").First().InnerHtml.Replace("&zwnj;", " ");

                JobInJaModel job = new JobInJaModel(Title, Image, Company, Location, ContractType);
                jobs.Add(job);
            }

            return View(jobs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
