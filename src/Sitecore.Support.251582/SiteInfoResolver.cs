namespace Sitecore.Support.XA.Foundation.Multisite
{
  using Microsoft.Extensions.DependencyInjection;
  using Sitecore.Abstractions;
  using Sitecore.DependencyInjection;
  using Sitecore.Web;
  using System.Collections.Generic;
  using System.Linq;

  public class SiteInfoResolver : Sitecore.XA.Foundation.Multisite.SiteInfoResolver
  {

    private IEnumerable<SiteInfo> _sites;

    public override IEnumerable<SiteInfo> Sites
    {
      get
      {
        BaseSiteContextFactory service = ServiceLocator.ServiceProvider.GetService<BaseSiteContextFactory>();
        return _sites ?? (_sites = from s in service.GetSites()
                                   where s.Properties.AllKeys.Contains("IsSxaSite")
                                   orderby s.RootPath descending
                                   select s);
      }
    }
  }
}