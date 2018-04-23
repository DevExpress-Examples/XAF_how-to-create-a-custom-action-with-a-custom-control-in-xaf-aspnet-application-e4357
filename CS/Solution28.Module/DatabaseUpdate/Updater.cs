using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using Solution28.Module.BusinessObjects;

namespace Solution28.Module.DatabaseUpdate {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

            if (ObjectSpace.FindObject<DomainObject1>(null) == null) {
                DateTime dt = DateTime.Today.AddDays(-10);
                for (int i = 1; i < 30; i++) {
                    DomainObject1 obj = ObjectSpace.CreateObject<DomainObject1>();
                    obj.Name = string.Format("sample{0:d3}", i);
                    obj.Date = dt.AddDays(i);
                }
            }
        }
    }
}
