using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Reports.Models.Contracts;

namespace Dealership.XmlFilesProcessing.Writers.Contracts
{
    public interface IReportWriter
    {
        void Write();
    }
}
