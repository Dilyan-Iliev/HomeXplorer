namespace HomeXplorer.Filters
{
    using System.Text;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Mvc.Filters;

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
            PathString pageUrl = context.HttpContext.Request.Path;
            string sessionKey = "UniqueVisit_" + pageUrl;

            // Check if the unique visit flag is set in the session for the specific page
            if (context.HttpContext.Session.GetString(sessionKey) == null)
            {
                // Unique visit flag doesn't exist for the page, increment the visit count
                IEnumerable<PageVisit>? pages = await this.repo
                    .All<PageVisit>()
                    .ToListAsync();

                PageVisit? existingPage = pages
                    .FirstOrDefault(v => (string)v.Url == pageUrl);

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

                // Set the unique visit flag in the session for the specific page
                context.HttpContext.Session.SetString(sessionKey, "true");

                await repo.SaveChangesAsync();
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
