namespace HomeXplorer.Filters
{
    using System.Net;
    using System.Text;
    using System.Security.Cryptography;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Http.Extensions;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;

    public class PageVisitCountFilter
        : ActionFilterAttribute
    {
        private readonly IRepository repo;

        public PageVisitCountFilter(IRepository repo)
        {
            this.repo = repo;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var pageUrl = context.HttpContext.Request.Path;
            var sessionKey = "UniqueVisit_" + pageUrl;

            // Check if the unique visit cookie exists
            if (context.HttpContext.Session.GetString(sessionKey) == null)
            {
                // Unique visit cookie doesn't exist, increment the visit count
                var page = await repo.All<PageVisit>().ToListAsync();
                var existingPage = page.FirstOrDefault(v => (string)v.Url == pageUrl);

                if (existingPage == null)
                {
                    existingPage = new PageVisit
                    {
                        Url = pageUrl,
                        VisitsCount = 1
                    };
                    await this.repo.AddAsync(existingPage);
                }
                else
                {
                    existingPage.VisitsCount++;
                }

                context.HttpContext.Session.SetString(sessionKey, "true");

                await repo.SaveChangesAsync();
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
