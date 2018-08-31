using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using Sitecore.Diagnostics;
using Sitecore.IO;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Web;

namespace Sitecore.Support.XA.Feature.ErrorHandling.Pipelines.HtpRequestProcessed
{
  public class HandleServerErrorCode: Sitecore.XA.Feature.ErrorHandling.Pipelines.HtpRequestProcessed.HandleServerErrorCode
  {
    public override void Process(HttpRequestArgs args)
    {
      if (args.Context.Error != null && !Context.Site.Name.Equals("shell"))
      {
        FieldInfo field = typeof(Sitecore.Sites.SiteContext).GetField
          ("info", BindingFlags.Instance | BindingFlags.NonPublic);
        SiteInfo siteInfo = (field==null)?null:field.GetValue(Context.Site) as SiteInfo;
        if (siteInfo != null)
        {
          string staticErrorPageUrl = GetStaticErrorPageUrl(siteInfo);
          if (File.Exists(FileUtil.MapPath(staticErrorPageUrl)))
          {
            args.Context.Server.TransferRequest(staticErrorPageUrl);
          }
          else
          {
            Log.Warn($"Could not find proper static error page for site: {siteInfo.Name}. Please generate it.", this);
          }
        }
      }
    }
  }
}