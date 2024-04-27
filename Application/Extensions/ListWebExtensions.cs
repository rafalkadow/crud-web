using Domain.Modules.Base.Queries;
using Domain.Modules.Base.ViewModels;
using Shared.Extensions.GeneralExtensions;

namespace Application.Extensions
{
    [Serializable]
    public static class ListWebExtensions
    {
        public static void StartListAdd(this List<string> list, GetBaseResultFilter x, ValueViewModel model)
        {
            string lineOne = "";
            lineOne += "<label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'><input type='checkbox' class='checkboxes' value='" + x.Id + "'/><span></span></label>";
            list.Add(lineOne);

            string lineTwo = "<a href='" + model.DataMenu().SubMenuUrlUpdate + x.Id + "' class='btn btn-sm btn-clean btn-icon' title='Update'><i class='fas fa-edit'></i> </a>";
            lineTwo += "<a href='javascript:;' tagOperation='" + x.Id + "' class='btn btn-sm btn-clean btn-icon delete' title='Delete'><i class='fas fa-trash'></i> </a>";
			list.Add(lineTwo);
		}

		public static void EndListAdd(this List<string> list, GetBaseResultFilter x)
		{
			list.Add(x.CreatedUserName);
			
			list.Add(x.CreatedOnDateTimeUTC.HasValue ? x.CreatedOnDateTimeUTC.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty);
		
			list.Add(x.ModifiedUserName);
			
			list.Add(x.ModifiedOnDateTimeUTC.HasValue ? x.ModifiedOnDateTimeUTC.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty);
		
			list.Add("<span class='label label-lg font-weight-bold  label-inline " + x.RecordStatus.StatusGetDataGrid() + "'>" + x.RecordStatus.StatusTextGetDataGrid() + "</span>");
		}

	}
}