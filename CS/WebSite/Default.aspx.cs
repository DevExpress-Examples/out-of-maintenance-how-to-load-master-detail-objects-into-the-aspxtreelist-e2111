using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxTreeList;

public partial class _Default : System.Web.UI.Page {

    protected void Page_Init(object sender, EventArgs e) {
        if (!IsPostBack)
        {
            Session.Clear();
            Session session = XpoHelper.GetNewSession();
            Session["collection"] = new XPCollection<Category>(session);
        }
    }

    protected void tree_VirtualModeCreateChildren(object sender, TreeListVirtualModeCreateChildrenEventArgs e) {
        if(e.NodeObject == null)
            e.Children = Session["collection"] as XPCollection<Category>;
        else
            e.Children = ((Category)e.NodeObject).Articles;
    }

    protected void tree_VirtualModeNodeCreating(object sender, TreeListVirtualModeNodeCreatingEventArgs e) {
        Guid key;
        string text;

        XPCustomObject obj = (XPCustomObject)e.NodeObject;
        if(obj is Category) {
            key = ((Category)obj).UniqueID;
            text = ((Category)obj).Name;
            e.IsLeaf = false;
        }
        else {
            key = ((Article)obj).UniqueID;
            text = ((Article)obj).Name;
            e.IsLeaf = true;
        }

        e.NodeKeyValue = key;
        e.SetNodeValue("Text", text);
    }
}