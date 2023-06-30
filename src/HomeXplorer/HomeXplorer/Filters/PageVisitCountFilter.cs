namespace HomeXplorer.Filters
{
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Data.Entities;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.EntityFrameworkCore;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

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
            //get current url from request
            string url = context.HttpContext.Request.GetDisplayUrl();
            string encodedUrl = WebUtility.UrlEncode(url);

            //track only unique visits:
            bool hasVisitCookie = context.HttpContext.Request.Cookies
                .TryGetValue(encodedUrl, out string? visitCookieValue);

            if (!hasVisitCookie)
            {
                visitCookieValue = Guid.NewGuid().ToString();
                string hashedVisitCookieValue = CookiHash(visitCookieValue);

                context.HttpContext.Response.Cookies.Append(encodedUrl, hashedVisitCookieValue);

                var currentPage = repo.All<PageVisit>()
                    .FirstOrDefaultAsync(p => p.Url == url && p.HashedVisitCookie == hashedVisitCookieValue)
                    .GetAwaiter()
                    .GetResult();

                if (currentPage == null)
                {
                    currentPage = new PageVisit()
                    {
                        Url = url,
                        VisitsCount = 1,
                        HashedVisitCookie = hashedVisitCookieValue,
                    };

                    await repo.AddAsync(currentPage);
                }
                else
                {
                    currentPage.VisitsCount += 1;
                    repo.Update(currentPage);
                }

                await repo.SaveChangesAsync();
            }

            await next();
        }

        private string CookiHash(string cookieValue)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(cookieValue);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var item in hashBytes)
            {
                sb.Append(item.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
