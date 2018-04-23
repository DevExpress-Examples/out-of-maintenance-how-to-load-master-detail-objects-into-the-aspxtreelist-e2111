Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo

Public Class Category
	Inherits XPCustomObject
	Public Sub New(ByVal session As Session)
		MyBase.New(session)
	End Sub

	Private _UniqueID As Guid
	<Key(True)> _
	Public Property UniqueID() As Guid
		Get
			Return _UniqueID
		End Get
		Set(ByVal value As Guid)
			SetPropertyValue("UniqueID", _UniqueID, value)
		End Set
	End Property

	<Association("Category-Articles")> _
	Public ReadOnly Property Articles() As XPCollection(Of Article)
		Get
			Return GetCollection(Of Article)("Articles")
		End Get
	End Property

	Private _Name As String
	Public Property Name() As String
		Get
			Return _Name
		End Get
		Set(ByVal value As String)
			SetPropertyValue("Name", _Name, value)
		End Set
	End Property
End Class