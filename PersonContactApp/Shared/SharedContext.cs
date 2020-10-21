using PersonContactApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonContactApp.Shared
{
    static class SharedContext
    {
        static public Emails Email {get; set; }
        static public PhoneNumbers  PhoneNumber {get; set; }
        static public bool IsRefreshed { get; set; }
    }
}
