Imports System
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.Persistent.Base
Imports DevExpress.Data.Filtering

Namespace Solution28.Module.Web
	Public Class ViewController1
		Inherits ViewController

		Public Sub New()
			Dim filterAction As New ParametrizedRangeAction(Me, "FilterRange", "Unspecified", GetType(Date))
			AddHandler filterAction.Execute, AddressOf filterAction_Execute
		End Sub
		Private Sub filterAction_Execute(ByVal sender As Object, ByVal e As ParametrizedActionExecuteEventArgs)
			Dim range As Range(Of Date) = DirectCast(e.ParameterCurrentValue, Range(Of Date))
			CType(View, ListView).CollectionSource.Criteria("By Date") = CriteriaOperator.Parse("[Date] <= ? AND [Date] >= ?", range.To, range.From)
		End Sub
	End Class
End Namespace
