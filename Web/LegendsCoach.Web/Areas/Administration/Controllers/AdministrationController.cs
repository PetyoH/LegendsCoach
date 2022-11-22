namespace LegendsCoach.Web.Areas.Administration.Controllers
{
    using LegendsCoach.Common;
    using LegendsCoach.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
