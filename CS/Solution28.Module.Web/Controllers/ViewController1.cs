using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;

namespace Solution28.Module.Web {
    public class ViewController1 : ViewController {
        public ViewController1() {
            ParametrizedRangeAction filterAction = new ParametrizedRangeAction(this, "FilterRange", "Unspecified", typeof(DateTime));
            filterAction.Execute += new ParametrizedActionExecuteEventHandler(filterAction_Execute);
        }
        private void filterAction_Execute(object sender, ParametrizedActionExecuteEventArgs e) {
            Range<DateTime> range = (Range<DateTime>)e.ParameterCurrentValue;
            ((ListView)View).CollectionSource.Criteria["By Date"] = CriteriaOperator.Parse("[Date] <= ? AND [Date] >= ?", range.To, range.From);
        }
    }
}
