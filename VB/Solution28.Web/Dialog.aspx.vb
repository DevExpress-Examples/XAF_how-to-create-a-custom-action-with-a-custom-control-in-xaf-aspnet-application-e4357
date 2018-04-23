Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Web.Templates

Partial Public Class DialogPage
	Inherits BaseXafPage
	Implements ILookupPopupFrameTemplate
	Private Sub window_PagePreRender(ByVal sender As Object, ByVal e As EventArgs)
		RemoveHandler (CType(sender, WebWindow)).PagePreRender, AddressOf window_PagePreRender
		If SearchActionContainer.HasActiveActions() AndAlso IsSearchEnabled Then
			TableCell1.Style("padding-bottom") = "30px"
		End If
	End Sub
	Protected Overrides Sub OnUnload(ByVal e As EventArgs)
		If WebWindow.CurrentRequestWindow IsNot Nothing Then
			RemoveHandler WebWindow.CurrentRequestWindow.PagePreRender, AddressOf window_PagePreRender
		End If
		MyBase.OnUnload(e)
	End Sub
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		ViewSiteControl = viewSiteControl
		WebApplication.Instance.CreateControls(Me)
		Dim window As WebWindow = WebWindow.CurrentRequestWindow
		If window IsNot Nothing Then
			Dim clientScript As String = String.Format(" " & ControlChars.CrLf & "            var activePopupControl = GetActivePopupControl(window.parent);" & ControlChars.CrLf & "            if (activePopupControl != null){{" & ControlChars.CrLf & "                var viewImageControl = document.getElementById('{0}');" & ControlChars.CrLf & "                if (viewImageControl && viewImageControl.src != ''){{" & ControlChars.CrLf & "                    activePopupControl.SetHeaderImageUrl(viewImageControl.src);" & ControlChars.CrLf & "                }}" & ControlChars.CrLf & "                var viewCaptionControl = document.getElementById('{1}');" & ControlChars.CrLf & "                if (viewCaptionControl){{" & ControlChars.CrLf & "                    var headerText = viewCaptionControl.innerText;" & ControlChars.CrLf & "                    if(__aspxFirefox && viewCaptionControl.textContent) {{" & ControlChars.CrLf & "                        headerText = viewCaptionControl.textContent;" & ControlChars.CrLf & "                    }}" & ControlChars.CrLf & "                    activePopupControl.SetHeaderText(headerText);" & ControlChars.CrLf & "                }}" & ControlChars.CrLf & "            }}", viewImageControl.Control.ClientID, viewCaptionControl.Control.ClientID)
			window.RegisterStartupScript("UpdatePopupControlHeader", clientScript, True)
			AddHandler window.PagePreRender, AddressOf window_PagePreRender
		End If
	End Sub
	Public Sub New()
	End Sub
	#Region "ILookupPopupFrameTemplate Members"

	Public Property IsSearchEnabled() As Boolean Implements ILookupPopupFrameTemplate.IsSearchEnabled
		Get
			Return SearchActionContainer.Visible
		End Get
		Set(ByVal value As Boolean)
			SearchActionContainer.Visible = value
		End Set
	End Property

	Public Sub SetStartSearchString(ByVal searchString As String) Implements ILookupPopupFrameTemplate.SetStartSearchString
	End Sub

	Public Sub FocusFindEditor() Implements ILookupPopupFrameTemplate.FocusFindEditor
	End Sub
	#End Region
End Class
