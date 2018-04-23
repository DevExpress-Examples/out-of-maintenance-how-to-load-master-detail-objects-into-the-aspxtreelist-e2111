Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB

''' <summary>
''' Summary description for XpoHelper
''' </summary>
Public NotInheritable Class XpoHelper
	Private Sub New()
	End Sub
	Shared Sub New()
		CreateDefaultObjects()
	End Sub

	Public Shared Function GetNewSession() As Session
		Return New Session(DataLayer)
	End Function

	Public Shared Function GetNewUnitOfWork() As UnitOfWork
		Return New UnitOfWork(DataLayer)
	End Function

	Private ReadOnly Shared lockObject As Object = New Object()

	Private Shared fDataLayer As IDataLayer
	Private Shared ReadOnly Property DataLayer() As IDataLayer
		Get
			If fDataLayer Is Nothing Then
				SyncLock lockObject
					fDataLayer = GetDataLayer()
				End SyncLock
			End If
			Return fDataLayer
		End Get
	End Property

	Private Shared Function GetDataLayer() As IDataLayer
		XpoDefault.Session = Nothing

		Dim ds As New InMemoryDataStore()
		Dim dict As DevExpress.Xpo.Metadata.XPDictionary = New DevExpress.Xpo.Metadata.ReflectionDictionary()
		dict.GetDataStoreSchema(GetType(Article).Assembly)

		Return New ThreadSafeDataLayer(dict, ds)
	End Function

	Private Shared Sub CreateDefaultObjects()
		Const CategoryCount As Integer = 3
		Const ArticleCount As Integer = 5

		Using uow As UnitOfWork = GetNewUnitOfWork()
			For i As Integer = 1 To CategoryCount
				Dim category As New Category(uow)
				category.Name = String.Format("Category {0}", i)
				For j As Integer = 1 To ArticleCount
					Dim article As New Article(uow)
					article.Category = category
					article.Name = String.Format("Article {0}-{1}", i, j)
				Next j
			Next i
			uow.CommitChanges()
		End Using
	End Sub
End Class
