﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class contact
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public contact()
    {
        this.ticket = new HashSet<ticket>();
    }

    public int ContactId { get; set; }
    public int CustomerId { get; set; }
    public string ContactName { get; set; }
    public string ContactEmail { get; set; }

    public virtual customer customer { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<ticket> ticket { get; set; }
}

public partial class customer
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public customer()
    {
        this.contact = new HashSet<contact>();
        this.ticket = new HashSet<ticket>();
    }

    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerAbrev { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<contact> contact { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<ticket> ticket { get; set; }
}

public partial class status
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public status()
    {
        this.statusdetail = new HashSet<statusdetail>();
        this.ticket = new HashSet<ticket>();
    }

    public int StatusId { get; set; }
    public string StatusName { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<statusdetail> statusdetail { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<ticket> ticket { get; set; }
}

public partial class statusdetail
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public statusdetail()
    {
        this.ticket = new HashSet<ticket>();
    }

    public int StatusDetailId { get; set; }
    public int StatusId { get; set; }
    public string StatusDetailName { get; set; }

    public virtual status status { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<ticket> ticket { get; set; }
}

public partial class ticket
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public ticket()
    {
        this.tickethistory = new HashSet<tickethistory>();
    }

    public int TicketId { get; set; }
    public int CustomerId { get; set; }
    public int ContactId { get; set; }
    public int CreateUserid { get; set; }
    public int StatusId { get; set; }
    public Nullable<int> StatusDetailId { get; set; }
    public System.DateTime TicketDate { get; set; }
    public string TicketIssueSubject { get; set; }
    public string TicketIssueDescription { get; set; }
    public Nullable<int> RespUserId { get; set; }
    public string OutlookEntryId { get; set; }

    public virtual contact contact { get; set; }
    public virtual customer customer { get; set; }
    public virtual status status { get; set; }
    public virtual statusdetail statusdetail { get; set; }
    public virtual user user { get; set; }
    public virtual user user1 { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<tickethistory> tickethistory { get; set; }
}

public partial class tickethistory
{
    public int TicketHistoryId { get; set; }
    public int TicketId { get; set; }
    public int TicketHistoryUserId { get; set; }
    public string TicketHistoryDetail { get; set; }
    public System.DateTime TicketHistoryDate { get; set; }

    public virtual ticket ticket { get; set; }
    public virtual user user { get; set; }
}

public partial class user
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public user()
    {
        this.ticket = new HashSet<ticket>();
        this.ticket1 = new HashSet<ticket>();
        this.tickethistory = new HashSet<tickethistory>();
    }

    public int UserId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string UserAbrev { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<ticket> ticket { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<ticket> ticket1 { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<tickethistory> tickethistory { get; set; }
}