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

    Session session = XpoHelper.GetNewSession();

    protected void Page_Init(object sender, EventArgs e) {
    }

    protected void tree_VirtualModeCreateChildren(object sender, DevExpress.Web.ASPxTreeList.TreeListVirtualModeCreateChildrenEventArgs e) {
        if(e.NodeObject == null)
            e.Children = new XPCollection<Category>(session);
        else
            e.Children = ((Category)e.NodeObject).Articles;
    }

    protected void tree_VirtualModeNodeCreating(object sender, DevExpress.Web.ASPxTreeList.TreeListVirtualModeNodeCreatingEventArgs e) {
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