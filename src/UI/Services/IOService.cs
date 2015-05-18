using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetCompare.UI.Services
{
    public interface IIOService
    {
        string OpenFileDialog(string defaultPath);
    }

    public class IOService : IIOService
    {
        public string OpenFileDialog(string defaultPath)
        {
            


        }
    }
}
