namespace OMX.Infrastructure.HtmlHelpers
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.UI.Fluent;

    public static class KendoHelper
    {
        public static GridBuilder<T> BuiltFeaturedGrid<T>(
                this HtmlHelper helper,
                string controllerName,
                Expression<Func<T, object>> modelIdExpression,
                Action<GridColumnFactory<T>> columns = null) 
            where T : class
        {
            if (columns == null)
            {
                columns = cols =>
                {
                    cols.AutoGenerate(true);
                    cols.Command(c => c.Edit()).Title("Edit");
                    cols.Command(c => c.Destroy()).Title("Delete");
                };
            }

            return helper.Kendo()
                .Grid<T>()
                .Name("grid")
                .Columns(columns)
                .ColumnMenu()
                .Pageable(page => page.Refresh(true))
                .Sortable()
                .Groupable()
                .Filterable()
                .Editable(edit => edit.Mode(GridEditMode.PopUp))
                .DataSource(data =>
                    data
                        .Ajax()
                        .Model(m => m.Id(modelIdExpression))
                        .Read(read => read.Action("Read", controllerName))
                        .Update(update => update.Action("Edit", controllerName))
                        .Destroy(destroy => destroy.Action("Destroy", controllerName))
                        );
        }
    }
}