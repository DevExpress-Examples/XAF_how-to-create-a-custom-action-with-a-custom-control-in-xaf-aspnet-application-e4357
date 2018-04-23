using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web.Templates.ActionContainers.Menu;
using DevExpress.ExpressApp.Web.Templates.ActionContainers;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Localization;
using DevExpress.ExpressApp.Web.Templates;
using Solution28.Module.Web;
using DevExpress.ExpressApp.Templates;

namespace Solution28.Module.Web {

    public class ParametrizedRangeAction : ParametrizedAction {
        public ParametrizedRangeAction() : base() { }
        public ParametrizedRangeAction(IContainer container) : base(container) { }
        public ParametrizedRangeAction(Controller owner, string id, string category, Type valueType) : base(owner, id, category, valueType) { }
    }

    public struct Range<T> {
        public T From;
        public T To;
        public override bool Equals(object obj) {
            if (obj is Range<T>) {
                return Equals(((Range<T>)obj).From, this.From) && Equals(((Range<T>)obj).To, this.To);
            }
            return false;
        }
        public override int GetHashCode() {
            return From.GetHashCode() ^ To.GetHashCode();
        }
    }
}

namespace DevExpress.ExpressApp.Web.Templates.ActionContainers {

    public class MyActionContainerHolder : ActionContainerHolder {
        public MyActionContainerHolder():base() { }

        protected override MenuActionItemBase CreateParametrizedActionItem(ParametrizedAction parametrizedAction) {
            if (parametrizedAction is ParametrizedRangeAction) {
                return new ParametrizedRangeActionMenuActionItem((ParametrizedRangeAction)parametrizedAction);
            }
            return base.CreateParametrizedActionItem(parametrizedAction);
        }
    }

    public class ParametrizedRangeActionMenuActionItem : TemplatedMenuActionItem {
        private ActionContainerOrientation orientation;
        private bool isExecuted = false;
        private int executionLockCount;
        private string clientClickHandler;
        private void UpdateEditorValue() {
            executionLockCount++;
            try {
                if (Control != null) {
                    Control.Value = ((ParametrizedAction)Action).Value;
                }
            } finally {
                executionLockCount--;
            }
        }
        private void action_CurrentValueChanged(object sender, EventArgs e) {
            UpdateEditorValue();
        }
        private void ExecuteWithCurrentValue() {
            if (executionLockCount == 0 && !isExecuted) {
                isExecuted = true;
                ((ParametrizedAction)Action).DoExecute(Control.Value);
            }
        }
        protected override void SetImage(ImageInfo imageInfo) {
            if (Control != null) {
                Control.SetImage(imageInfo, Action.ShortCaption);
            }
        }
        protected override void SetImageCore(ImageInfo imageInfo) { }
        protected override void SetCaption(string caption) {
            if (Control != null) {
                Control.Caption = caption;
                if (Control.Button.Image.IsEmpty) {
                    Control.Button.Text = Action.ShortCaption;
                }
                Control.SetNullValuePrompt(Action.NullValuePrompt);
            }
        }
        protected override void SetPaintStyle(ActionItemPaintStyle paintStyle) {
            base.SetPaintStyle(paintStyle);
            if (Control != null) {
                Control.CaptionVisible = paintStyle != ActionItemPaintStyle.Image;
            }
        }
        protected override void SetEnabled(bool enabled) {
            if (Control != null) {
                Control.ClientEnabled = enabled;
            }
        }
        protected override void SetToolTip(string toolTip) {
            if (Control != null) {
                Control.ToolTip = toolTip;
            }
        }
        protected override void SetConfirmationMessage(string message) { }
        protected override Control CreateControlCore() {
            isExecuted = false;
            ParametrizedActionDateRangeControl result = new ParametrizedActionDateRangeControl(Orientation);
            result.ID = WebIdHelper.GetCorrectedActionId(Action);
            result.Value = ((ParametrizedAction)Action).Value;
            result.SetNullValuePrompt(Action.NullValuePrompt);
            result.Button.AutoPostBack = false;
            result.Button.ClientSideEvents.Click = clientClickHandler;
            return result;
        }
        public ParametrizedRangeActionMenuActionItem(ParametrizedRangeAction action)
            : base(action) {
            action.ValueChanged += new EventHandler(action_CurrentValueChanged);
        }
        public override void Dispose() {
            if (Action != null) {
                Action.ValueChanged -= new EventHandler(action_CurrentValueChanged);
            }
            base.Dispose();
        }
        public override void ProcessAction() {
            ExecuteWithCurrentValue();
        }
        public override void SetClientClickHandler(XafCallbackManager callbackManager, string controlID) {
            string clientScript = callbackManager.GetScript(controlID, string.Format("'{0}'", MenuItem.IndexPath), Action.GetFormattedConfirmationMessage(), IsPostBackRequired);
            clientClickHandler = "function(s, e) { " + clientScript + "e.processOnServer = false;}";
            if (Control != null) {
                Control.Button.ClientSideEvents.Click = clientClickHandler;
            }
        }
        public new ParametrizedActionDateRangeControl Control {
            get { return (ParametrizedActionDateRangeControl)base.Control; }
        }
        public new ParametrizedAction Action {
            get { return (ParametrizedAction)base.Action; }
        }
        public ActionContainerOrientation Orientation {
            get { return orientation; }
            set { orientation = value; }
        }
    }

    public class ParametrizedActionDateRangeControl : WebControl, INamingContainer, IDisposableExt {
        private ASPxDateEdit calendarFrom;
        private ASPxDateEdit calendarTo;
        private ASPxButton button;
        private ASPxLabel label;
        private TableCell labelCell;
        private bool isPrerendered;
        private Boolean isDisposed;
        private bool clientEnabled = true;
        private void button_Click(object sender, EventArgs e) {
            OnClick();
        }
        private void UpdateEnabled() {
            if (Button != null) {
                Button.ClientEnabled = ClientEnabled;
            }
            if (calendarFrom != null) {
                calendarFrom.ClientEnabled = ClientEnabled;
            }
            if (calendarTo != null) {
                calendarTo.ClientEnabled = ClientEnabled;
            }
        }
        protected string GetForceButtonClickScript() {
            return "function(s, e) { " + RenderHelper.GetForceButtonClickFunctionName() + "(e, '" + button.ClientID + "'); }";
        }
        protected virtual string GetClientControlClassName() {
            return "ParametrizedActionClientControl";
        }
        protected override void OnPreRender(EventArgs e) {
            isPrerendered = true;
            base.OnPreRender(e);
        }
        protected override void Render(HtmlTextWriter writer) {
            if (!isPrerendered) {
                OnPreRender(EventArgs.Empty);
            }
            base.Render(writer);
            DevExpress.Web.ASPxClasses.Internal.RenderUtils.WriteScriptHtml(writer, @"window." + ClientID + @" =  new " + GetClientControlClassName() + "('" + ClientID + "');");
        }
        protected virtual void OnClick() {
            if (Click != null) {
                Click(this, new EventArgs());
            }
        }
        public void SetConfirmationMessage(string message) {
            ConfirmationsHelper.SetConfirmationScript(Button, message);
        }
        public void SetImage(DevExpress.ExpressApp.Utils.ImageInfo imageInfo, string buttonText) {
            if (!imageInfo.IsEmpty) {
                ASPxImageHelper.SetImageProperties(Button.Image, imageInfo);
                Button.Text = "";
                CssClass = "ParametrizedActionWithImage";
            } else {
                ASPxImageHelper.ClearImageProperties(Button.Image);
                Button.Text = buttonText;
                CssClass = "ParametrizedAction";
            }
        }
        public ParametrizedActionDateRangeControl() : this(ActionContainerOrientation.Horizontal) { }
        public ParametrizedActionDateRangeControl(ActionContainerOrientation orientation) {
            button = RenderHelper.CreateASPxButton();
            button.AutoPostBack = false;
            button.Click += new EventHandler(button_Click);
            button.EnableClientSideAPI = true;
            button.ID = "B";
            Control editor = CreateEditorBody();
            editor.ID = "Ed";
            label = RenderHelper.CreateASPxLabel();
            label.ID = "L";
            label.Wrap = DevExpress.Utils.DefaultBoolean.False;
            Table table = RenderHelper.CreateTable();
            table.CssClass = "ParametrizedActionControl";
            table.ID = "T";
            labelCell = new TableCell();
            TableCell editorCell = new TableCell();
            TableCell buttonCell = new TableCell();
            FillTemplateTable(orientation, table, labelCell, editorCell, buttonCell);
            labelCell.Controls.Add(label);
            labelCell.CssClass = "ControlCaption";
            editorCell.Controls.Add(editor);
            editorCell.CssClass = "Label";
            buttonCell.Controls.Add(button);
            buttonCell.CssClass = "Editor";
            this.Controls.Add(table);
        }
        private Control CreateEditorBody() {
            calendarFrom = RenderHelper.CreateASPxDateEdit();
            calendarFrom.ID = "EdF";
            calendarTo = RenderHelper.CreateASPxDateEdit();
            calendarTo.ID = "EdT";
            Table table = RenderHelper.CreateTable();
            TableRow trow = new TableRow();
            TableCell tcell1 = new TableCell();
            tcell1.Controls.Add(calendarFrom);
            trow.Cells.Add(tcell1);
            TableCell tcell2 = new TableCell();
            tcell2.Controls.Add(calendarTo);
            trow.Cells.Add(tcell2);
            table.Rows.Add(trow);
            return table;
        }
        protected Table FillTemplateTable(ActionContainerOrientation orientation, Table table, TableCell labelCell,
            TableCell editorCell, TableCell buttonCell) {
            if (orientation == ActionContainerOrientation.Horizontal) {
                return FillHTemplateTable(table, labelCell, editorCell, buttonCell);
            } else {
                return FillVTemplateTable(table, labelCell, editorCell, buttonCell);
            }
        }
        protected virtual Table FillHTemplateTable(Table table, TableCell labelCell, TableCell editorCell, TableCell buttonCell) {
            table.Rows.Add(new TableRow());
            table.Rows[0].Cells.Add(labelCell);
            table.Rows[0].Cells.Add(editorCell);
            table.Rows[0].Cells.Add(buttonCell);
            return table;
        }
        protected virtual Table FillVTemplateTable(Table table, TableCell labelCell, TableCell editorCell, TableCell buttonCell) {
            table.Rows.Add(new TableRow());
            table.Rows[0].Cells.Add(labelCell);
            table.Rows.Add(new TableRow());
            table.Rows[1].Cells.Add(editorCell);
            table.Rows[1].Cells.Add(buttonCell);
            return table;
        }
        public void SetNullValuePrompt(string nullValuePrompt) {
            calendarFrom.NullText = nullValuePrompt;
            calendarTo.NullText = nullValuePrompt;
        }
        public override void Dispose() {
            if (button != null) {
                button.Click -= new EventHandler(button_Click);
            }
            base.Dispose();
            button = null;
            isDisposed = true;
        }
        public bool ClientEnabled {
            get {
                return clientEnabled;
            }
            set {
                clientEnabled = value;
                UpdateEnabled();
            }
        }
        public override string ToolTip {
            get { return Button.ToolTip; }
            set { Button.ToolTip = value; }
        }
        public ASPxButton Button {
            get {
                return button;
            }
        }
        public string Caption {
            get { return label.Text; }
            set {
                label.Text = value;
                CaptionVisible = !String.IsNullOrEmpty(value);
            }
        }
        public bool CaptionVisible {
            get { return labelCell.Visible; }
            set { labelCell.Visible = value; }
        }
        public virtual object Value {
            get {
                return new Range<DateTime>() { From = calendarFrom.Date, To = calendarTo.Date };
            }
            set {
                if (value is Range<DateTime>) {
                    calendarFrom.Date = ((Range<DateTime>)value).From;
                    calendarTo.Date = ((Range<DateTime>)value).To;
                }
            }
        }
        public event EventHandler Click;
        #region IDisposableExt Members
        public bool IsDisposed {
            get { return isDisposed; }
        }
        #endregion
    }

}
