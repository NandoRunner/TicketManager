using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectCSharpToMySQL;
using System.Data;

namespace TicketManager.BusinessRules
{
    public class BaseTicketManager
    {
        public bool GravarTicket(string customerId, string contactId, string ticketIssueSubject, string ticketIssueDescription, string outlookEntryId, ref string msg)
        {
            NameValueCollection nvc = new NameValueCollection();

            nvc["@customerId"] =customerId;
            nvc["@contactId"] = contactId;
            nvc["@ticketIssueSubject"] = ticketIssueSubject;
            nvc["@ticketIssueDescription"] = ticketIssueDescription;
            nvc["@outlookEntryId"] = outlookEntryId;

            string sql = "insert into ticket (CustomerId, ContactId, "
                + "CreateUserid, StatusId, StatusDetailId, TicketDate, "
                + "TicketIssueSubject, TicketIssueDescription, RespUserId, OutlookEntryId) "
                + "VALUES (@customerId, @contactId, 1, 1, 1, SYSDATE(), @ticketIssueSubject, "
                + "@ticketIssueDescription, 1, @outlookEntryId)";

            string lastInsertedID = string.Empty;

            DB.getInstance().msg = string.Empty;

            bool ret = DB.getInstance().execSQL(sql, nvc, ref lastInsertedID);

            msg = DB.getInstance().msg;

            return ret;
        }

        public string[] BuscarPorEmail(string contactEmail)
        {
            string sql = "select c.customerid, co.contactid, c.customerAbrev "
            + " from contact co inner join customer c on c.CustomerId = co.Customerid "
            + " where ContactEmail like '" + contactEmail + "'";

            DataTable ds = new DataTable();
            DB.getInstance().CarregarDS(sql, ref ds);

            string[] ret = new string[3];

            if (ds.Rows.Count != 1) return ret;

            for (int i = 0; i < 3; i++)
                ret[i] = ds.Rows[0][i].ToString();

            return ret;
        }

        static public string GetStatusColor(string status)
        {
            string ret = string.Empty;

            if (status == "1. Novo")
                ret = "status-1";
            else if (status == "2. Andamento")
                ret = "status-2";
            else
                ret = "status-3";
            return ret;
        }

    }
}
