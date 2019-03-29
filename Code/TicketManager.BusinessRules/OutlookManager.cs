using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace TicketManager.BusinessRules
{
    public class OutlookManager
    {

        public bool AtualizarMailItem(string categories, string entryId)
        {
            bool ret = false;

            try
            {
                Outlook.Application myApp = new Outlook.Application();
                Outlook._NameSpace mapiNameSpace = myApp.GetNamespace("MAPI");

                Outlook.MAPIFolder fd = mapiNameSpace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);

                string storeId = fd.StoreID;

                Outlook.MailItem mailItem = mapiNameSpace.GetItemFromID(entryId, storeId) as Outlook.MailItem;

                mailItem.Categories = categories;
                mailItem.Close(Outlook.OlInspectorClose.olSave);

                myApp.Quit();

                ret = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return ret;
        }

    }
}
