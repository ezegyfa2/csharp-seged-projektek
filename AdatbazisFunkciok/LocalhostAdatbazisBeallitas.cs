using AdatbazisFunkciok;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaravelProjectCreator
{
    public class LocalhostAdatbazisBeallitas : AdatbazisBeallitas
    {
        public LocalhostAdatbazisBeallitas(string databaseName): base("127.0.0.1", "3306", databaseName, "root", "", "")
        {
        }
    }
}
