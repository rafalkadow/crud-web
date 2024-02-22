using Domain.Modules.Base.ViewModels;
using Domain.Modules.Error.Consts;
using Domain.Modules.SignIn.Consts;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Base.Redirects
{
    public static class BaseRedirectHelper
    {
        public static RedirectResult Redirect404(this ControllerBase controller, string? errorUrl = null)
        {
            if (string.IsNullOrEmpty(errorUrl))
                return controller.Redirect(ErrorConsts.Error404Url);
            else
                return controller.Redirect(ErrorConsts.Error404Url + "/Url?errorUrl=" + errorUrl);
        }

        public static RedirectResult Redirect500(this ControllerBase controller)
        {
            return controller.Redirect(ErrorConsts.Error500Url);
        }

        public static RedirectResult RedirectToLanguage(this ControllerBase controller, ValueViewModel model, string languageCode)
        {
            string url = model.SignInLanguageSwitch(SignInConsts.Url, languageCode);
            return controller.Redirect(url);
        }

        public static RedirectResult RedirectToPageApp(this ControllerBase controller, string url)
        {
            return controller.Redirect(url);
        }
    }
}