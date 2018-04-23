using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Xpo;
using DevExpress.Web.ASPxGridView;
using PersistentObjects;

namespace XpoWebApplication {
    public partial class _Default : System.Web.UI.Page {

        Session XpoSession;

        protected void Page_Init(object sender, EventArgs e) {
            XpoSession = XpoHelper.GetNewSession();
            XpoDataSource1.Session = XpoSession;
        }

        protected void ASPxGridView1_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e) {
            Customer customer = XpoSession.GetObjectByKey<Customer>(e.EditingKeyValue);
            Session["Customer_LockValue"] = customer.GetMemberValue("OptimisticLockField");
        }

        protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) {
            object lockOldValue = Session["Customer_LockValue"];
            if(lockOldValue == null)
                throw new Exception("Cannot ensure optimistic lock.");

            Customer customerSaved = XpoSession.GetObjectByKey<Customer>(e.Keys[0]);
            object lockCurrentValue = customerSaved.GetMemberValue("OptimisticLockField");
            if(!lockOldValue.Equals(lockCurrentValue))
                throw new Exception("This data record has been edited by someone else.");
        }
    }
}