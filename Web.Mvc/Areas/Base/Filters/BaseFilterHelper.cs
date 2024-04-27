using Domain.Modules.Base.Queries;
using Domain.Modules.Base.ViewModels;
using Shared.Extensions.GeneralExtensions;
using Shared.Web;

namespace Web.Areas.Base.Filters
{
    public static class BaseFilterHelper
    {
        public static void GetFilterDataAll(this HttpRequest request, GetBaseResultFilter filter, ValueViewModel model)
        {
            Int32.TryParse(FilterUtilityHelper.GetValueRequest("length", request), out int iDisplayLength);
            filter.DisplayLength = iDisplayLength;

            Int32.TryParse(FilterUtilityHelper.GetValueRequest("start", request), out int iDisplayStart);
            filter.DisplayStart = iDisplayStart;

            Int32.TryParse(FilterUtilityHelper.GetValueRequest("draw", request), out int sEcho);
            filter.Echo = sEcho;

            if (DateTime.TryParse(FilterUtilityHelper.GetValueRequest("CreatedFrom", request), out DateTime CreatedFrom))
            {
                filter.CreatedFrom = CreatedFrom;
            }

            if (DateTime.TryParse(FilterUtilityHelper.GetValueRequest("CreatedTo", request), out DateTime CreatedTo))
            {
                filter.CreatedTo = CreatedTo.GetDateTimeMaxHoursMinutesSeconds();
            }

            if (filter.DisplayLength == 0)
                filter.DisplayLength = 100;
        }
    }
}