using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;


public class TeacherContext : DbContext
{
    public TeacherContext() : base("Data Source=CONFITECSPNB291\\DBLEANDRO;Initial Catalog=dbteacherManagement;Integrated Security=True")
    {
    }

    public DbSet<Teacher> Teacher { get; set; }
}