using Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RepositoryContracts
{
    public interface IRepositoryPhones
    {
        List<PhoneDto> GetListAllPhonesDto();
        List<PhoneDto> GetPhonesByFabricator(string fabricator);
        List<PhoneDto> GetPhonesByName(string name);
        PhoneDto GetPhoneByID(int id);
    }
}
