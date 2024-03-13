using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityMonitor
{
    public record Item(string Path, string NewValue, string OldValue)
    {
    }
}
