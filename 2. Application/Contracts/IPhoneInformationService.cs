using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPhoneInformationService
    {
        Task<string> GetPhone(string phoneFabricator = "", int phoneId = 0, string phoneName = "");

    }
}
