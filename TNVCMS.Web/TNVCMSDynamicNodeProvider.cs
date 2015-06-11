using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcSiteMapProvider;
using TNVCMS.Domain.Services;

namespace TNVCMS.Web
{
    public class TNVCMSDynamicNodeProvider : DynamicNodeProviderBase
    {
        T_NewsServices _newServices = new T_NewsServices();

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node) 
        {
            // Build value 
            var returnValue = new List<DynamicNode>();

            // Create a node for each album 
            foreach (var article in _newServices.GetAll())
            {
                DynamicNode dynamicNode = new DynamicNode();
                dynamicNode.Title = article.Title;
                dynamicNode.RouteValues.Add("id", article.ID);
                dynamicNode.RouteValues.Add("slug", article.Slug);
                dynamicNode.Description = article.Title;
                returnValue.Add(dynamicNode);
            }

            // Return 
            return returnValue;
        }
    }
}