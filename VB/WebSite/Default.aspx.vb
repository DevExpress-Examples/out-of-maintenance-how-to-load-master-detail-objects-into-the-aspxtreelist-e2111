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

	Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
		If Not IsPostBack Then
            Session.Clear()
            Dim session_xpo As Session = XpoHelper.GetNewSession()
            Session("collection") = New XPCollection(Of Category)(session_xpo)
        End If
	End Sub

	Protected Sub tree_VirtualModeCreateChildren(ByVal sender As Object, ByVal e As TreeListVirtualModeCreateChildrenEventArgs)
		If e.NodeObject Is Nothing Then
			e.Children = TryCast(Session("collection"), XPCollection(Of Category))
		Else
			e.Children = DirectCast(e.NodeObject, Category).Articles
		End If
	End Sub

	Protected Sub tree_VirtualModeNodeCreating(ByVal sender As Object, ByVal e As TreeListVirtualModeNodeCreatingEventArgs)
		Dim key As Guid
		Dim text As String

		Dim obj As XPCustomObject = DirectCast(e.NodeObject, XPCustomObject)
		If TypeOf obj Is Category Then
			key = DirectCast(obj, Category).UniqueID
			text = DirectCast(obj, Category).Name
			e.IsLeaf = False
		Else
			key = DirectCast(obj, Article).UniqueID
			text = DirectCast(obj, Article).Name
			e.IsLeaf = True
		End If

		e.NodeKeyValue = key
		e.SetNodeValue("Text", text)
	End Sub
End Class