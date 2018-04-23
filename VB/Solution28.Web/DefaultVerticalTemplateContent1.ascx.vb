Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Web.Templates
Imports System.Web.UI
Imports DevExpress.ExpressApp.Web.Templates.ActionContainers
Imports DevExpress.ExpressApp.Templates
Imports DevExpress.ExpressApp.Web.Controls

Namespace Solution28.Web
	Partial Public Class DefaultVerticalTemplateContent1
		Inherits TemplateContent
		Implements IHeaderImageControlContainer, IXafPopupWindowControlContainer

		Protected Overrides Sub OnInit(ByVal e As EventArgs)
			MyBase.OnInit(e)
			WebApplication.Instance.ClientInfo.SetInfo(ClientParams)
		End Sub
		Protected Overrides Sub OnLoad(ByVal e As EventArgs)
			MyBase.OnLoad(e)
			If WebWindow.CurrentRequestWindow IsNot Nothing Then
				AddHandler WebWindow.CurrentRequestWindow.PagePreRender, AddressOf CurrentRequestWindow_PagePreRender
			End If
		End Sub
		Protected Overrides Sub OnUnload(ByVal e As EventArgs)
			If WebWindow.CurrentRequestWindow IsNot Nothing Then
				RemoveHandler WebWindow.CurrentRequestWindow.PagePreRender, AddressOf CurrentRequestWindow_PagePreRender
			End If
			MyBase.OnUnload(e)
		End Sub
		Private Sub CurrentRequestWindow_PagePreRender(ByVal sender As Object, ByVal e As EventArgs)
			WebWindow.CurrentRequestWindow.RegisterStartupScript("OnLoadCore", String.Format("Init(""{0}"", ""VerticalCS"");OnLoadCore(""LPcell"", ""separatorCell"", ""separatorImage"", true, true);", BaseXafPage.CurrentTheme), True)
			UpdateTRPVisibility()
		End Sub
		Private Sub UpdateTRPVisibility()
			Dim isVisible As Boolean = False
			For Each control As Control In TRP.Controls
				If TypeOf control Is ActionContainerHolder Then
					If CType(control, ActionContainerHolder).HasActiveActions() Then
						isVisible = True
						Exit For
					End If
				End If
			Next control
			TRP.Visible = isVisible
		End Sub
		Public Overrides ReadOnly Property DefaultContainer() As IActionContainer
			Get
				If TB IsNot Nothing Then
					Return TB.FindActionContainerById("View")
				End If
				Return Nothing
			End Get
		End Property
		Public Overrides Sub SetStatus(ByVal statusMessages As ICollection(Of String))
			InfoMessagesPanel.Text = String.Join("<br>", (New List(Of String)(statusMessages)).ToArray())
		End Sub
		Public Overrides ReadOnly Property ViewSiteControl() As Object
			Get
				Return VSC
			End Get
		End Property
		Public ReadOnly Property LeftToolsActionContainer() As ActionContainerHolder
			Get
				Return VTC
			End Get
		End Property
		Public ReadOnly Property DiagnosticActionContainer() As ActionContainerHolder
			Get
				Return DAC
			End Get
		End Property
		Public ReadOnly Property MainToolBarActionContainer() As ActionContainerHolder
			Get
				Return TB
			End Get
		End Property
		Public ReadOnly Property SecurityActionContainer() As ActionContainerHolder
			Get
				Return SAC
			End Get
		End Property
		Public ReadOnly Property TopToolsActionContainer() As ActionContainerHolder
			Get
				Return SHC
			End Get
		End Property

		Public ReadOnly Property XafPopupWindowControl() As XafPopupWindowControl Implements IXafPopupWindowControlContainer.XafPopupWindowControl
			Get
				Return PopupWindowControl
			End Get
		End Property
		Public ReadOnly Property HeaderImageControl() As ThemedImageControl Implements IHeaderImageControlContainer.HeaderImageControl
			Get
				Return TIC
			End Get
		End Property
	End Class
End Namespace
