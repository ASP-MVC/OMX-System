using OMX.Models;

namespace OMX.Infrastructure.Populators
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface IDropDownListPopulator
    {
        IEnumerable<Category> GetAllCategories();

        IEnumerable<Category> GetCategoriesWithSubCategories();

        IEnumerable<SelectListItem> GetCategories();

        IEnumerable<SelectListItem> GetSubCategories();
    }
}