using System;
using System.Globalization;
using System.Text;
using umbraco.businesslogic;
using umbraco.cms.presentation.Trees;

namespace AntwerpRC.UI.BackEnd.Trees
{
    [Tree("CalendarAdmin", "Categories", "Categories")]
    public class CategoryTree : BaseTree
    {
        public CategoryTree(string application)
            : base(application)
        {
        }

        public override void RenderJS(ref StringBuilder Javascript)
        {
            Javascript.Append(
            @"function openCategoryOverview() {
                UmbClientMgr.contentFrame('/CalendarAdmin/CategorySurface');
            }
            function openCategoryDetail(id) {
                UmbClientMgr.contentFrame('/CalendarAdmin/SeasonSurface/Edit/'+ id);
            }
            ");
            //                resizePanel('body_Panel2',true,true);

        }

        public override void Render(ref XmlTree tree)
        {
            var categoryManager = new BLL.CategoryManager();
            var categories = categoryManager.GetAllCategories();
            foreach (var category in categories)
            {
                
                XmlTreeNode subNode = XmlTreeNode.Create(this);
                subNode.NodeID = category.CategoryId.ToString(CultureInfo.InvariantCulture);
                subNode.Text = category.Description;
                subNode.Action = "javascript:openCategoryDetail(" + category.CategoryId + ");";
                subNode.Icon = "folder.gif";
                subNode.OpenIcon = "folder_o.gif";
//                subNode.HasChildren = true;
//                subNode.Source = this.GetTreeServiceUrl((int)category.CategoryId);
                OnBeforeNodeRender(ref tree, ref subNode, EventArgs.Empty);
                if (subNode != null)
                {
                    tree.Add(subNode);
                    OnAfterNodeRender(ref tree, ref subNode, EventArgs.Empty);
                }
            }
        }

        protected override void CreateRootNode(ref XmlTreeNode rootNode)
        {
            rootNode.NodeType = "init" + TreeAlias;
            rootNode.NodeID = "init";
            rootNode.Action = "javascript:openCategoryOverview()";
        }
    }
}