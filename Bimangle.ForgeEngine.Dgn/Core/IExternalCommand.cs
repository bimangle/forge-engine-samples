using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimangle.ForgeEngine.Dgn.Core
{
    public interface IExternalCommand
    {
        int Execute(string unparsed, ref string message);
    }
}
