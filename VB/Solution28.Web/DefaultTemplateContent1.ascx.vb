Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports DevExpress.ExpressApp.Web.Controls
Imports DevExpress.ExpressApp.Web.Templates.ActionContainers
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Web.Templates
Imports DevExpress.ExpressApp.Templates

Namespace Solution28.Web
	Partial Public Class DefaultTemplateContent1
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
			Dim window As WebWindow = DirectCast(sender, WebWindow)
			Dim isLeftPanelVisible As String = (VTC.HasActiveActions() OrElse DAC.HasActiveActions()).ToString().ToLower()
			window.RegisterStartupScript("OnLoadCore", String.Format("Init(""{1}"", ""DefaultCS"");OnLoadCore(""LPcell"", ""separatorCell"", ""separatorImage"", {0}, true);", isLeftPanelVisible, BaseXafPage.CurrentTheme), True)
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
