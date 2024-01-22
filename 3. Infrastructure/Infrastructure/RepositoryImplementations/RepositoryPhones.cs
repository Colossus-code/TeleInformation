using Contracts.Dto;
using Contracts.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RepositoryImplementations
{
    /// <summary>
    /// This class interacts with the api endpoints we need, db or others... 
    /// In this case the information to read it's stored as a Json in a local file.
    /// </summary>
    public class RepositoryPhones : IRepositoryPhones
    {
        private readonly string _pathFile;

        public RepositoryPhones()
        {
            _pathFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RepositoryResponseMock", "Response.json");
        }
        #region LocalCalls
        public List<PhoneDto> GetListAllPhonesDto() => RepositoryHelper.GetListLocal<PhoneDto>(_pathFile);

        public PhoneDto GetPhoneByID(int id) => RepositoryHelper.GetListLocal<PhoneDto>(_pathFile)
            .FirstOrDefault(e => e.PhoneID == id);
      
        
        public List<PhoneDto> GetPhonesByFabricator(string fabricator)  => RepositoryHelper.GetListLocal<PhoneDto>(_pathFile).Where(e => e.PhoneFabricatorName == fabricator).ToList();
       

        public List<PhoneDto>GetPhonesByName(string name) => RepositoryHelper.GetListLocal<PhoneDto>(_pathFile).Where(e => e.PhoneName.ToLower().Contains(name.ToLower())).ToList();
        #endregion

        // If we want to include other types of calls, such as to an API or database, we can use a generic class to organize the code.

    }
}
