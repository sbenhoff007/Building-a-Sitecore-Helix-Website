﻿using Sitecon.Feature.PageContent.Models;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sitecon.Feature.PageContent.Controllers
{
  public class PageContentController : Controller
  {
    public ActionResult ListWithIcons()
    {
      if (Sitecore.Context.Item == null)
      {
        return new EmptyResult();
      }

      var dataSourceId = RenderingContext.CurrentOrNull.Rendering.DataSource;
      var item = !string.IsNullOrEmpty(dataSourceId)
          ? Sitecore.Context.Database.GetItem(dataSourceId)
          : Sitecore.Context.Item;
      var multiLineTextString = item.Fields[Templates.ListWithIcons.Fields.ListText].Value;

      ListWithIconsItems listWithIconsItems = new ListWithIconsItems();
      listWithIconsItems.ListItems = new List<string>();

      if (string.IsNullOrEmpty(multiLineTextString))
      {
        return View(listWithIconsItems);
      }

      string[] sep = new string[] { "\r\n" };
      string[] lines = multiLineTextString.Split(sep, StringSplitOptions.RemoveEmptyEntries);

      foreach (string listItem in lines)
      {
        if (!string.IsNullOrEmpty(listItem))
        {
          listWithIconsItems.ListItems.Add(listItem);
        }
      }

      listWithIconsItems.ListIcon = item.Fields[Templates.ListWithIcons.Fields.ListIcon].Value;

      return View(listWithIconsItems);
    }
  }
}
