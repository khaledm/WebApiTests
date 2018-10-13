using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Castle.Components.DictionaryAdapter.Xml;
using Castle.Windsor;

namespace WebApiTests.Filters
{
    /// <summary>
    /// initializes a new instance of the <see cref="ConfigurableFilterProvider"/> class.
    /// </summary>
    public class ConfigurableFilterProvider : ActionDescriptorFilterProvider, IFilterProvider
    {
        private readonly IWindsorContainer _container;

        /// <inheritdoc />
        public ConfigurableFilterProvider(IWindsorContainer container)
        {
            _container = container;
        }

        ///// <inheritdoc />
        //IEnumerable<FilterInfo> IFilterProvider.GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        //{
        //    List<FilterInfo> filters = new List<FilterInfo>(ConfigureGlobalFilters());
        //    filters.AddRange(ConfigureLocalFilters(actionDescriptor.GetFilters(), FilterScope.Action));
        //    filters.AddRange(ConfigureLocalFilters(actionDescriptor.ControllerDescriptor.GetFilters(), FilterScope.Controller));

        //    return filters;
        //}

        //private IEnumerable<FilterInfo> ConfigureLocalFilters(IEnumerable<IFilter> filters, FilterScope scope)
        //{
        //    Debug.Assert(filters != null);

        //    foreach (IFilter filter in filters)
        //    {
        //        if (filter is ValidationFilter)
        //        {
        //            yield return new FilterInfo( (ValidationFilter)_container.Resolve(filter.GetType()), scope);
        //        }
        //        else
        //        {
        //            yield return new FilterInfo(filter, scope);
        //        }
        //    }
        //}

        //private IEnumerable<FilterInfo> ConfigureGlobalFilters()
        //{
        //    foreach (FilterInfo filter in GlobalConfiguration.Configuration.Filters)
        //    {
        //        if (filter.Instance is ValidationFilter)
        //        {
        //            yield return new FilterInfo((ValidationFilter)_container.Resolve(filter.Instance.GetType()), FilterScope.Global);
        //        }
        //        else
        //        {
        //            yield return new FilterInfo(filter.Instance, FilterScope.Global);
        //        }
        //    }
        //}
        /// <inheritdoc />
        public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(configuration, actionDescriptor).ToList();

            foreach (var f in filters)
            {
                if (_container.Kernel.HasComponent(f.Instance.GetType()))
                {
                    yield return new FilterInfo(_container.Resolve(f.Instance.GetType()) as ActionFilterAttribute, f.Scope);
                }
                else
                {
                    yield return new FilterInfo(f.Instance, f.Scope);
                }
            }
        }
    }
}