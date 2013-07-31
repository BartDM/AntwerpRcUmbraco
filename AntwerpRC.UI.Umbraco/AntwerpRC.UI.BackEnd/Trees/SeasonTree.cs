using System;
using System.Globalization;
using System.Text;
using umbraco.businesslogic;
using umbraco.cms.presentation.Trees;

namespace AntwerpRC.UI.BackEnd.Trees
{
    [Tree("CalendarAdmin", "Seasons", "Seasons")]
    public class SeasonTree : BaseTree
    {
        public SeasonTree(string application) : base(application)
        {
        }

        public override void RenderJS(ref StringBuilder Javascript)
        {
            Javascript.Append(
            @"function openSeasonOverview() {
                UmbClientMgr.contentFrame('/CalendarAdmin/SeasonSurface');
            }
            function openDemoController2(id) {
                UmbClientMgr.contentFrame('/CalendarAdmin/SeasonSurface/Edit/id');
            }
            ");
        }

        public override void Render(ref XmlTree tree)
        {
            var seasonManager = new BLL.SeasonManager();
            var seasons = seasonManager.GetSeasons();
            foreach (var season in seasons)
            {
                XmlTreeNode subNode = XmlTreeNode.Create(this);
                subNode.NodeID = season.SeasonId.ToString(CultureInfo.InvariantCulture);
                subNode.Text = season.Description;
                subNode.Action = "javascript:openDemoController2(" + season.SeasonId + ");";
                subNode.Icon = "folder.gif";
                subNode.OpenIcon = "folder_o.gif";
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
            rootNode.Action = "javascript:openSeasonOverview()";
        }
    }
}