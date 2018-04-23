Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Xpo
Imports DevExpress.Web.ASPxGridView
Imports PersistentObjects

Namespace XpoWebApplication
	Partial Public Class _Default
		Inherits System.Web.UI.Page

		Private XpoSession As Session

		Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
			XpoSession = XpoHelper.GetNewSession()
			XpoDataSource1.Session = XpoSession
		End Sub

		Protected Sub ASPxGridView1_StartRowEditing(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs)
			Dim customer As Customer = XpoSession.GetObjectByKey(Of Customer)(e.EditingKeyValue)
			Session("Customer_LockValue") = customer.GetMemberValue("OptimisticLockField")
		End Sub

		Protected Sub ASPxGridView1_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
			Dim lockOldValue As Object = Session("Customer_LockValue")
			If lockOldValue Is Nothing Then
				Throw New Exception("Cannot ensure optimistic lock.")
			End If

			Dim customerSaved As Customer = XpoSession.GetObjectByKey(Of Customer)(e.Keys(0))
			Dim lockCurrentValue As Object = customerSaved.GetMemberValue("OptimisticLockField")
			If (Not lockOldValue.Equals(lockCurrentValue)) Then
				Throw New Exception("This data record has been edited by someone else.")
			End If
		End Sub
	End Class
End Namespace