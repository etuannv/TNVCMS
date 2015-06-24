using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcSiteMapProvider;
using TNVCMS.Domain.Services;

namespace TNVCMS.Web
{
    public class DetailDynamicNodeProvider : DynamicNodeProviderBase
    {
        T_NewsServices _newServices;
        T_TagServices _tagServices;

        public DetailDynamicNodeProvider()
        {
            _newServices = new T_NewsServices();
            _tagServices = new T_TagServices();
        }
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            // Build value 
            var returnValue = new List<DynamicNode>();

            // Create a node for each Arrticle
            foreach (var article in _newServices.GetAll())
            {
                DynamicNode dynamicNode = new DynamicNode();
                dynamicNode.Title = article.Title;
                dynamicNode.RouteValues.Add("id", article.ID);
                dynamicNode.RouteValues.Add("slug", article.Slug);
                dynamicNode.PreservedRouteParameters = new List<string> { article.Slug };
                string Description = Utilities.Common.GetDescription(article.ContentNews, 50);
                dynamicNode.Description = (Description== null)? article.Title: Description;
                dynamicNode.ParentKey = "Chuyên mục " +  _newServices.GetCateByNewsID(article.ID).Title;
                returnValue.Add(dynamicNode);
            }

            // Return 
            return returnValue;
        }

    }


    public class CategoryDynamicNodeProvider : DynamicNodeProviderBase
    {
        T_TagServices _tagServices;

        public CategoryDynamicNodeProvider()
        {
            _tagServices = new T_TagServices();
        }
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            // Build value 
            var returnValue = new List<DynamicNode>();

            // Create a node for each Category
            foreach (var tag in _tagServices.GetByTaxonomy(TNVCMS.Utilities.Constants.TAXONOMY_CATEGORY))
            {
                DynamicNode dynamicNode = new DynamicNode();
                dynamicNode.Key = "Chuyên mục " + tag.Title;
                dynamicNode.Title = tag.Title;
                dynamicNode.RouteValues.Add("id", tag.ID);
                dynamicNode.RouteValues.Add("slug", tag.Slug);
                dynamicNode.Description = tag.Description;
                if (tag.ParentID != null)
                {
                    //Add parent key
                    dynamicNode.ParentKey = "Chuyên mục " + _tagServices.GetByID((int)tag.ParentID).Title;
                }

                returnValue.Add(dynamicNode);
            }

            // Return 
            return returnValue;
        }
    }


    public class AlbumDynamicNodeProvider : DynamicNodeProviderBase
    {
        IT_AlbumServices _albumService;

        public AlbumDynamicNodeProvider()
        {
            _albumService = new T_AlbumServices();
        }
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            // Build value 
            var returnValue = new List<DynamicNode>();

            // Create a node for each Category
            foreach (var item in _albumService.GetAll())
            {
                DynamicNode dynamicNode = new DynamicNode();
                dynamicNode.Title = item.Title;
                dynamicNode.RouteValues.Add("id", item.ID);
                dynamicNode.RouteValues.Add("slug", item.Slug);
                dynamicNode.PreservedRouteParameters = new List<string> { item.Slug };
                dynamicNode.Description = item.Description;
                dynamicNode.ParentKey = "Album";
                returnValue.Add(dynamicNode);
            }

            // Return 
            return returnValue;
        }
    }        
}