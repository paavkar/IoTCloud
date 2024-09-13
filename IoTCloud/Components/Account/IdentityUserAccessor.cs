using IoTCloud.Data;
using Microsoft.AspNetCore.Identity;

namespace IoTCloud.Components.Account
{
    internal sealed class IdentityUserAccessor(UserManager<ApplicationUser> userManager, IdentityRedirectManager redirectManager, IHttpContextAccessor httpContextAccessor)
    {
        public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext? context = null)
        {
            if (context is null) context = httpContextAccessor.HttpContext!;
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }
    }
}
