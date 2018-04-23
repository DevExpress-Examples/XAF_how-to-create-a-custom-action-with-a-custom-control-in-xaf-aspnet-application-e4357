Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.ComponentModel
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Web.SystemModule
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Web.Templates.ActionContainers.Menu
Imports DevExpress.ExpressApp.Web.Templates.ActionContainers
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Localization
Imports DevExpress.ExpressApp.Web.Templates
Imports Solution28.Module.Web
Imports DevExpress.ExpressApp.Templates

Namespace Solution28.Module.Web

	Public Class ParametrizedRangeAction
		Inherits ParametrizedAction

		Public Sub New()
			MyBase.New()
		End Sub
		Public Sub New(ByVal container As IContainer)
			MyBase.New(container)
		End Sub
		Public Sub New(ByVal owner As Controller, ByVal id As String, ByVal category As String, ByVal valueType As Type)
			MyBase.New(owner, id, category, valueType)
		End Sub
	End Class

	Public Structure Range(Of T)
		Public [From] As T
		Public [To] As T
		Public Overrides Function Equals(ByVal obj As Object) As Boolean
			If TypeOf obj Is Range(Of T) Then
				Return Equals(DirectCast(obj, Range(Of T)).From, Me.From) AndAlso Equals(DirectCast(obj, Range(Of T)).To, Me.To)
			End If
			Return False
		End Function
		Public Overrides Function GetHashCode() As Integer
			Return [From].GetHashCode() Xor [To].GetHashCode()
		End Function
	End Structure
End Namespace

Namespace DevExpress.ExpressApp.Web.Templates.ActionContainers

	Public Class MyActionContainerHolder
		Inherits ActionContainerHolder

		Public Sub New()
			MyBase.New()
		End Sub

		Protected Overrides Function CreateParametrizedActionItem(ByVal parametrizedAction As ParametrizedAction) As MenuActionItemBase
			If TypeOf parametrizedAction Is ParametrizedRangeAction Then
				Return New ParametrizedRangeActionMenuActionItem(DirectCast(parametrizedAction, ParametrizedRangeAction))
			End If
			Return MyBase.CreateParametrizedActionItem(parametrizedAction)
		End Function
	End Class

	Public Class ParametrizedRangeActionMenuActionItem
		Inherits TemplatedMenuActionItem

		Private _orientation As ActionContainerOrientation
		Private isExecuted As Boolean = False
		Private executionLockCount As Integer
		Private clientClickHandler As String
		Private Sub UpdateEditorValue()
			executionLockCount += 1
			Try
				If Control IsNot Nothing Then
					Control.Value = CType(Action, ParametrizedAction).Value
				End If
			Finally
				executionLockCount -= 1
			End Try
		End Sub
		Private Sub action_CurrentValueChanged(ByVal sender As Object, ByVal e As EventArgs)
			UpdateEditorValue()
		End Sub
		Private Sub ExecuteWithCurrentValue()
			If executionLockCount = 0 AndAlso (Not isExecuted) Then
				isExecuted = True
				CType(Action, ParametrizedAction).DoExecute(Control.Value)
			End If
		End Sub
		Protected Overrides Sub SetImage(ByVal imageInfo As ImageInfo)
			If Control IsNot Nothing Then
				Control.SetImage(imageInfo, Action.ShortCaption)
			End If
		End Sub
		Protected Overrides Sub SetCaption(ByVal caption As String)
			If Control IsNot Nothing Then
				Control.Caption = caption
				If Control.Button.Image.IsEmpty Then
					Control.Button.Text = Action.ShortCaption
				End If
				Control.SetNullValuePrompt(Action.NullValuePrompt)
			End If
		End Sub
		Protected Overrides Sub SetPaintStyle(ByVal paintStyle As ActionItemPaintStyle)
			MyBase.SetPaintStyle(paintStyle)
			If Control IsNot Nothing Then
				Control.CaptionVisible = Not Equals(paintStyle, ActionItemPaintStyle.Image)
			End If
		End Sub
		Protected Overrides Sub SetEnabled(ByVal enabled As Boolean)
			If Control IsNot Nothing Then
				Control.ClientEnabled = enabled
			End If
		End Sub
		Protected Overrides Sub SetToolTip(ByVal toolTip As String)
			If Control IsNot Nothing Then
				Control.ToolTip = toolTip
			End If
		End Sub
		Protected Overrides Sub SetConfirmationMessage(ByVal message As String)
		End Sub
		Protected Overrides Function CreateControlCore() As Control
			isExecuted = False
			Dim result As New ParametrizedActionDateRangeControl(Orientation)
			result.ID = WebIdHelper.GetCorrectedActionId(Action)
			result.Value = CType(Action, ParametrizedAction).Value
			result.SetNullValuePrompt(Action.NullValuePrompt)
			result.Button.AutoPostBack = False
			result.Button.ClientSideEvents.Click = clientClickHandler
			Return result
		End Function
		Public Sub New(ByVal action As ParametrizedRangeAction)
			MyBase.New(action)
			AddHandler action.ValueChanged, AddressOf action_CurrentValueChanged
		End Sub
		Public Overrides Sub Dispose()
			If Action IsNot Nothing Then
				RemoveHandler Action.ValueChanged, AddressOf action_CurrentValueChanged
			End If
			MyBase.Dispose()
		End Sub
		Public Overrides Sub ProcessAction()
			ExecuteWithCurrentValue()
		End Sub
		Public Overrides Sub SetClientClickHandler(ByVal callbackManager As XafCallbackManager, ByVal controlID As String)
			Dim clientScript As String = callbackManager.GetScript(controlID, String.Format("'{0}'", MenuItem.IndexPath), Action.GetFormattedConfirmationMessage(), IsPostBackRequired)
			clientClickHandler = "function(s, e) { " & clientScript & "e.processOnServer = false;}"
			If Control IsNot Nothing Then
				Control.Button.ClientSideEvents.Click = clientClickHandler
			End If
		End Sub
		Public Shadows ReadOnly Property Control() As ParametrizedActionDateRangeControl
			Get
				Return CType(MyBase.Control, ParametrizedActionDateRangeControl)
			End Get
		End Property
		Public Shadows ReadOnly Property Action() As ParametrizedAction
			Get
				Return CType(MyBase.Action, ParametrizedAction)
			End Get
		End Property
		Public Property Orientation() As ActionContainerOrientation
			Get
				Return _orientation
			End Get
			Set(ByVal value As ActionContainerOrientation)
				_orientation = value
			End Set
		End Property
	End Class

	Public Class ParametrizedActionDateRangeControl
		Inherits WebControl
		Implements INamingContainer, IDisposableExt

		Private calendarFrom As ASPxDateEdit
		Private calendarTo As ASPxDateEdit
		Private _button As ASPxButton
		Private label As ASPxLabel
		Private labelCell As TableCell
		Private isPrerendered As Boolean
		Private _isDisposed As Boolean
		Private _clientEnabled As Boolean = True
		Private Sub button_Click(ByVal sender As Object, ByVal e As EventArgs)
			OnClick()
		End Sub
		Private Sub UpdateEnabled()
			If Button IsNot Nothing Then
				Button.ClientEnabled = ClientEnabled
			End If
			If calendarFrom IsNot Nothing Then
				calendarFrom.ClientEnabled = ClientEnabled
			End If
			If calendarTo IsNot Nothing Then
				calendarTo.ClientEnabled = ClientEnabled
			End If
		End Sub
		Protected Function GetForceButtonClickScript() As String
			Return "function(s, e) { " & RenderHelper.GetForceButtonClickFunctionName() & "(e, '" & _button.ClientID & "'); }"
		End Function
		Protected Overridable Function GetClientControlClassName() As String
			Return "ParametrizedActionClientControl"
		End Function
		Protected Overrides Sub OnPreRender(ByVal e As EventArgs)
			isPrerendered = True
			MyBase.OnPreRender(e)
		End Sub
		Protected Overrides Sub Render(ByVal writer As HtmlTextWriter)
			If Not isPrerendered Then
				OnPreRender(EventArgs.Empty)
			End If
			MyBase.Render(writer)
			DevExpress.Web.Internal.RenderUtils.WriteScriptHtml(writer, "window." & ClientID & " =  new " & GetClientControlClassName() & "('" & ClientID & "');")
		End Sub
		Protected Overridable Sub OnClick()
			RaiseEvent Click(Me, New EventArgs())
		End Sub
		Public Sub SetConfirmationMessage(ByVal message As String)
			ConfirmationsHelper.SetConfirmationScript(Button, message)
		End Sub
		Public Sub SetImage(ByVal imageInfo As DevExpress.ExpressApp.Utils.ImageInfo, ByVal buttonText As String)
			If Not imageInfo.IsEmpty Then
				ASPxImageHelper.SetImageProperties(Button.Image, imageInfo)
				Button.Text = ""
				CssClass = "ParametrizedActionWithImage"
			Else
				ASPxImageHelper.ClearImageProperties(Button.Image)
				Button.Text = buttonText
				CssClass = "ParametrizedAction"
			End If
		End Sub
		Public Sub New()
			Me.New(ActionContainerOrientation.Horizontal)
		End Sub
		Public Sub New(ByVal orientation As ActionContainerOrientation)
			_button = RenderHelper.CreateASPxButton()
			_button.AutoPostBack = False
			AddHandler _button.Click, AddressOf button_Click
			_button.EnableClientSideAPI = True
			_button.ID = "B"
			Dim editor As Control = CreateEditorBody()
			editor.ID = "Ed"
			label = RenderHelper.CreateASPxLabel()
			label.ID = "L"
			label.Wrap = DevExpress.Utils.DefaultBoolean.False
			Dim table As Table = RenderHelper.CreateTable()
			table.CssClass = "ParametrizedActionControl"
			table.ID = "T"
			labelCell = New TableCell()
			Dim editorCell As New TableCell()
			Dim buttonCell As New TableCell()
			FillTemplateTable(orientation, table, labelCell, editorCell, buttonCell)
			labelCell.Controls.Add(label)
			labelCell.CssClass = "ControlCaption"
			editorCell.Controls.Add(editor)
			editorCell.CssClass = "Label"
			buttonCell.Controls.Add(_button)
			buttonCell.CssClass = "Editor"
			Me.Controls.Add(table)
		End Sub
		Private Function CreateEditorBody() As Control
			calendarFrom = RenderHelper.CreateASPxDateEdit()
			calendarFrom.ID = "EdF"
			calendarTo = RenderHelper.CreateASPxDateEdit()
			calendarTo.ID = "EdT"
			Dim table As Table = RenderHelper.CreateTable()
			Dim trow As New TableRow()
			Dim tcell1 As New TableCell()
			tcell1.Controls.Add(calendarFrom)
			trow.Cells.Add(tcell1)
			Dim tcell2 As New TableCell()
			tcell2.Controls.Add(calendarTo)
			trow.Cells.Add(tcell2)
			table.Rows.Add(trow)
			Return table
		End Function
		Protected Function FillTemplateTable(ByVal orientation As ActionContainerOrientation, ByVal table As Table, ByVal labelCell As TableCell, ByVal editorCell As TableCell, ByVal buttonCell As TableCell) As Table
			If Equals(orientation, ActionContainerOrientation.Horizontal) Then
				Return FillHTemplateTable(table, labelCell, editorCell, buttonCell)
			Else
				Return FillVTemplateTable(table, labelCell, editorCell, buttonCell)
			End If
		End Function
		Protected Overridable Function FillHTemplateTable(ByVal table As Table, ByVal labelCell As TableCell, ByVal editorCell As TableCell, ByVal buttonCell As TableCell) As Table
			table.Rows.Add(New TableRow())
			table.Rows(0).Cells.Add(labelCell)
			table.Rows(0).Cells.Add(editorCell)
			table.Rows(0).Cells.Add(buttonCell)
			Return table
		End Function
		Protected Overridable Function FillVTemplateTable(ByVal table As Table, ByVal labelCell As TableCell, ByVal editorCell As TableCell, ByVal buttonCell As TableCell) As Table
			table.Rows.Add(New TableRow())
			table.Rows(0).Cells.Add(labelCell)
			table.Rows.Add(New TableRow())
			table.Rows(1).Cells.Add(editorCell)
			table.Rows(1).Cells.Add(buttonCell)
			Return table
		End Function
		Public Sub SetNullValuePrompt(ByVal nullValuePrompt As String)
			calendarFrom.NullText = nullValuePrompt
			calendarTo.NullText = nullValuePrompt
		End Sub
		Public Overrides Sub Dispose() Implements System.IDisposable.Dispose
			If _button IsNot Nothing Then
				RemoveHandler _button.Click, AddressOf button_Click
			End If
			MyBase.Dispose()
			_button = Nothing
			_isDisposed = True
		End Sub
		Public Property ClientEnabled() As Boolean
			Get
				Return _clientEnabled
			End Get
			Set(ByVal value As Boolean)
				_clientEnabled = value
				UpdateEnabled()
			End Set
		End Property
		Public Overrides Property ToolTip() As String
			Get
				Return Button.ToolTip
			End Get
			Set(ByVal value As String)
				Button.ToolTip = value
			End Set
		End Property
		Public ReadOnly Property Button() As ASPxButton
			Get
				Return _button
			End Get
		End Property
		Public Property Caption() As String
			Get
				Return label.Text
			End Get
			Set(ByVal value As String)
				label.Text = value
				CaptionVisible = Not String.IsNullOrEmpty(value)
			End Set
		End Property
		Public Property CaptionVisible() As Boolean
			Get
				Return labelCell.Visible
			End Get
			Set(ByVal value As Boolean)
				labelCell.Visible = value
			End Set
		End Property
		Public Overridable Property Value() As Object
			Get
				Return New Range(Of Date)() With {.From = calendarFrom.Date, .To = calendarTo.Date}
			End Get
			Set(ByVal value As Object)
				If TypeOf value Is Range(Of Date) Then
					calendarFrom.Date = DirectCast(value, Range(Of Date)).From
					calendarTo.Date = DirectCast(value, Range(Of Date)).To
				End If
			End Set
		End Property
		Public Event Click As EventHandler
		#Region "IDisposableExt Members"
		Public ReadOnly Property IsDisposed() As Boolean Implements IDisposableExt.IsDisposed
			Get
				Return _isDisposed
			End Get
		End Property
		#End Region
	End Class

End Namespace
