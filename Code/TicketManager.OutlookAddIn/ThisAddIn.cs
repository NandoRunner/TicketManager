using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Windows.Forms;
using System.Data;
using TicketManager.BusinessRules;

namespace OutlookAddIn1
{
    public partial class ThisAddIn
    {
        Outlook.Inspectors inspectors;

        BaseTicketManager ticket;

        public string contactId { get; set; }
        public string customerId { get; set; }
        public string customerAbrev { get; set; }
        public string emailSender { get; set; }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            ticket = new BaseTicketManager();
            inspectors = this.Application.Inspectors;
            inspectors.NewInspector +=
            new Microsoft.Office.Interop.Outlook.InspectorsEvents_NewInspectorEventHandler(Inspectors_NewInspector);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see http://go.microsoft.com/fwlink/?LinkId=506785
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        void Inspectors_NewInspector(Microsoft.Office.Interop.Outlook.Inspector Inspector)
        {
            Outlook.MailItem mailItem = Inspector.CurrentItem as Outlook.MailItem;

            try
            {
                if ((mailItem == null) || (mailItem.EntryID == null))   return;

                Outlook.Folder parentFolder = mailItem.Parent as Outlook.Folder;
                if (parentFolder.Name != "Caixa de Entrada") return;

                if (mailItem.Categories == null || !mailItem.Categories.Contains("#")) return;
                
                if (!this.BuscarPorEmail(getSenderEmailAddress(mailItem)))
                {
                    MessageBox.Show("Email de contato de cliente não econtrado!"
                        + "\n\nTicket não pode ser cadastrado!", "Ticket.OutlookAddIn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Contato: " + emailSender
                    + "\n\nCliente: " + customerAbrev 
                    + "\n\nCriar Ticket #?", "Ticket.OutlookAddIn", MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                Outlook.MAPIFolder inBox = (Outlook.MAPIFolder)this.Application.
                    ActiveExplorer().Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);

                Outlook.MAPIFolder destFolder = inBox.Folders["_Tickets"];

                //mailItem.Body += "\n\n# Ticket";
                mailItem.Categories = "#; Novo; " + customerAbrev;
                mailItem.Save();
                mailItem = mailItem.Move(destFolder);

                string msg = string.Empty;

                this.GravarTicket(ref mailItem, ref msg);

                if (msg != string.Empty)
                {
                    MessageBox.Show("Erro:\n\n" + msg, "Ticket.OutlookAddIn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            #endregion
        }
        
        private bool GravarTicket(ref Outlook.MailItem  mi, ref string msg)
        {
            int posFim = mi.Body.IndexOf("Atenciosamente");
            if (posFim <= 0) posFim = mi.Body.IndexOf("Grato");
            if (posFim <= 0) posFim = mi.Body.IndexOf("Esta mensagem pode conter informação");
            if ((posFim <= 0) || (posFim > 800))  posFim = 800;
            if ((posFim == 800) && (mi.Body.Length < 800))  posFim = mi.Body.Length;
        
            bool ret = ticket.GravarTicket(customerId, contactId, mi.Subject, mi.Body.Substring(0, posFim), mi.EntryID, ref msg);

            return ret;
        }

        private string getSenderEmailAddress(Outlook.MailItem mail)
        {
            Outlook.AddressEntry sender = mail.Sender;
            emailSender = "";

            if (sender.AddressEntryUserType == Outlook.OlAddressEntryUserType.olExchangeUserAddressEntry || sender.AddressEntryUserType == Outlook.OlAddressEntryUserType.olExchangeRemoteUserAddressEntry)
            {
                Outlook.ExchangeUser exchUser = sender.GetExchangeUser();
                if (exchUser != null)
                {
                    emailSender = exchUser.PrimarySmtpAddress;
                }
            }
            else
            {
                emailSender = mail.SenderEmailAddress;
            }

            return emailSender;
        }


        private bool BuscarPorEmail(string contactEmail)
        {
            string[] ret = ticket.BuscarPorEmail(contactEmail);

            if (ret[0] == null) return false;

            customerId = ret[0];
            contactId = ret[1];
            customerAbrev = ret[2];

            return true;
        }
    }
}
