Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Web.ASPxTreeList

Partial Public Class _Default
	Inherits System.Web.UI.Page

	Private session As Session = XpoHelper.GetNewSession()

	Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
	End Sub

	Protected Sub tree_VirtualModeCreateChildren(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxTreeList.TreeListVirtualModeCreateChildrenEventArgs)
		If e.NodeObject Is Nothing Then
			e.Children = New XPCollection(Of Category)(session)
		Else
			e.Children = (CType(e.NodeObject, Category)).Articles
		End If
	End Sub

	Protected Sub tree_VirtualModeNodeCreating(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxTreeList.TreeListVirtualModeNodeCreatingEventArgs)
		Dim key As Guid
		Dim text As String

		Dim obj As XPCustomObject = CType(e.NodeObject, XPCustomObject)
		If TypeOf obj Is Category Then
			key = (CType(obj, Category)).UniqueID
			text = (CType(obj, Category)).Name
			e.IsLeaf = False
		Else
			key = (CType(obj, Article)).UniqueID
			text = (CType(obj, Article)).Name
			e.IsLeaf = True
		End If

		e.NodeKeyValue = key
		e.SetNodeValue("Text", text)
	End Sub
End Class