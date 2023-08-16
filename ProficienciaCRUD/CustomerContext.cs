using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;


public class CustomerContext : DbContext
{
    public CustomerContext() : base(@"Data Source=CONFITECSPNB291\DBLEANDRO;Initial Catalog=dbCustomers;Integrated Security=True")
    {
    }

    public DbSet<Customer> Customer { get; set; }
}
