using Entity.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
[HtmlTargetElement("td", Attributes = "user-role")]
public class RolesTagHelper : TagHelper
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;

    public RolesTagHelper(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HtmlAttributeName("user-role")]
    public string RoleId { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        List<string> names = new List<string>();
        IdentityRole role = await _roleManager.FindByIdAsync(RoleId);

        if (role != null)
        {
            foreach (var user in _userManager.Users)
            {
                if (user != null && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    names.Add(user.UserName);
                }
            }
        }

        output.Content.SetContent(names.Count == 0 ? "No users" : string.Join(", ", names));
    }
}
}

