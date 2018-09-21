using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Abstractions;
using Sitecore.DependencyInjection;
using Sitecore.Web;
using Sitecore.XA.Foundation.Multisite;
using Sitecore.XA.Foundation.Multisite.Services;
using Sitecore.XA.Foundation.Multisite.SiteResolvers;

namespace Sitecore.Support.XA.Foundation.Multisite.Pipelines.IoC
{
  public class RegisterMultisiteServices : IServicesConfigurator
  {
    public void Configure(IServiceCollection serviceCollection)
    {
      serviceCollection.AddSingleton<IMultisiteContext, MultisiteContext>();
      serviceCollection.AddSingleton<ISharedSitesContext, SharedSitesContext>();
      serviceCollection.AddSingleton<ISiteInfoResolver, SiteInfoResolver>();
      serviceCollection.AddSingleton<ISiteMediaRootProvider, SiteMediaRootProvider>();
      serviceCollection.AddSingleton<IEnvironmentSiteSortingService, EnvironmentSiteSortingService>();
      serviceCollection.AddSingleton<IEnvironmentSitesResolver, EnvironmentSitesResolver>();
      serviceCollection.AddSingleton<ISharedSitesContext, SharedSitesContext>();
      serviceCollection.AddSingleton<ISiteDefinitionParser, SiteDefinitionParser>();
      serviceCollection.AddSingleton<IItemSourceInfoService, ItemSourceInfoService>();
      serviceCollection.AddSingleton<IDelegatedAreaService, DelegatedAreaService>();
      serviceCollection.AddSingleton<IPushCloneService, PushCloneService>();
      serviceCollection.AddSingleton<IPushCloneCoordinatorService, DelegatedAreaCoodinatorService>();
      serviceCollection.AddSingleton<IPushCloneHandlerService, PushCloneHandlerService>();
    }
  }

  public class SiteInfoResolver : Sitecore.XA.Foundation.Multisite.SiteInfoResolver
  {

    private IEnumerable<SiteInfo> _sites;

    public override IEnumerable<SiteInfo> Sites
    {
      get
      {
        BaseSiteContextFactory service = ServiceLocator.ServiceProvider.GetService<BaseSiteContextFactory>();
        return _sites ?? (_sites = from s in service.GetSites()
                 orderby s.RootPath descending
                 select s);
      }
    }
  }
}