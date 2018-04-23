Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Base

Namespace Solution28.Module.BusinessObjects
	<DefaultClassOptions>
	Public Class DomainObject1
		Inherits BaseObject

		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Private _Name As String
		Public Property Name() As String
			Get
				Return _Name
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Name", _Name, value)
			End Set
		End Property
		Private _Date As Date
		Public Property [Date]() As Date
			Get
				Return _Date
			End Get
			Set(ByVal value As Date)
				SetPropertyValue("Date", _Date, value)
			End Set
		End Property
	End Class

End Namespace
