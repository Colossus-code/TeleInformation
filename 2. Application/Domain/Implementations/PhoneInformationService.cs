using Contracts;
using Contracts.Dto;
using Contracts.RepositoryContracts;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Domain class for the logic of the program.
    /// </summary>
    public class PhoneInformationService : IPhoneInformationService
    {
        private readonly IRepositoryPhones _repositoryPhones;
        private readonly IRepositoryCache _repositoryCache;

        public PhoneInformationService(IRepositoryCache repositoryCache, IRepositoryPhones repositoryPhones)
        {
            _repositoryCache = repositoryCache;
            _repositoryPhones = repositoryPhones;
        }
        /// <summary>
        /// Method to get the information and process using the interaction with the infrastructure.
        /// </summary>
        /// <param name="phoneFabricator">The maker of the phone.</param>
        /// <param name="phoneId">Identificator number of the phone.</param>
        /// <param name="phoneName">Aprox. name of the phone.</param>
        /// <returns></returns>
        public async Task<string> GetPhone(string phoneFabricator = "", int phoneId = 0, string phoneName = "")
        {
            var cachedPhones = _repositoryCache.GetCache<Phone>(1); 
            // In that case we are using const number to take cache result, it must to be found by GUID.

            if (cachedPhones.Count > 0)
            {
                var phone = cachedPhones.FirstOrDefault(e => e.PhoneID == phoneId);

                if (phone == null && !string.IsNullOrEmpty(phoneName) && !string.IsNullOrEmpty(phoneFabricator))
                {
                    List<Phone> phones = cachedPhones
                        .Where(e => e.PhoneFabricatorName?.Contains(phoneFabricator) == true).ToList()
                    ?? cachedPhones.Where(e => e.PhoneName.Contains(phoneName)).ToList();

                    return JsonConvert.SerializeObject(phones);
                }
                else
                {
                    return JsonConvert.SerializeObject(phone);
                }

            }
            if (phoneId > 0)
            {
                var responseRetrivedDto =  _repositoryPhones.GetPhoneByID(phoneId);

                Phone phone = new Phone
                {
                    PhoneID = responseRetrivedDto.PhoneID,
                    PhoneName = responseRetrivedDto.PhoneName,
                    PhoneDescription = responseRetrivedDto.PhoneDescription,
                    PhoneFabricatorName = responseRetrivedDto.PhoneFabricatorName,
                    PhoneStatus = responseRetrivedDto.PhoneStatus,
                    FabricationDate = responseRetrivedDto.FabricationDate,
                    LastStatusUpdate = responseRetrivedDto.LastStatusUpdate
                };
                var repositoryCache = _repositoryCache.GetCache<Phone>(1);
                repositoryCache.Add(phone);

                _repositoryCache.SetCache(repositoryCache, 1);

                return JsonConvert.SerializeObject(phone);
            }
            else if(phoneId == 0 && (!string.IsNullOrEmpty(phoneName) || !string.IsNullOrEmpty(phoneFabricator))) 
            {
                List<Phone> phones = new List<Phone>();
                List<PhoneDto> phonesDto = new List<PhoneDto>();
                
                if (string.IsNullOrEmpty(phoneName)) 
                {
                    phonesDto =  _repositoryPhones.GetPhonesByName(phoneName);
                }
                else
                {
                    phonesDto =  _repositoryPhones.GetPhonesByFabricator(phoneFabricator);
                }

                foreach (PhoneDto phoneDto in phonesDto)
                {
                    phones.Add(new Phone
                    {
                        PhoneID = phoneDto.PhoneID,
                        PhoneName = phoneDto.PhoneName,
                        PhoneDescription = phoneDto.PhoneDescription,
                        FabricationDate = phoneDto.FabricationDate,
                        LastStatusUpdate = phoneDto.LastStatusUpdate,
                        PhoneFabricatorName = phoneDto.PhoneFabricatorName,
                        PhoneStatus = phoneDto.PhoneStatus,
                    });
                }

                if(phones.Count > 0)
                {
                    _repositoryCache.SetCache(phones, 1);
                    return JsonConvert.SerializeObject(phones);
                }

                return string.Empty;
            }
            else
            {
                List<Phone> phones = new List<Phone>();
                List<PhoneDto> phonesDto = new List<PhoneDto>();

                phonesDto =  _repositoryPhones.GetListAllPhonesDto();

                foreach (PhoneDto phoneDto in phonesDto)
                {
                    phones.Add(new Phone
                    {
                        PhoneID = phoneDto.PhoneID,
                        PhoneName = phoneDto.PhoneName,
                        PhoneDescription = phoneDto.PhoneDescription,
                        FabricationDate = phoneDto.FabricationDate,
                        LastStatusUpdate = phoneDto.LastStatusUpdate,
                        PhoneFabricatorName = phoneDto.PhoneFabricatorName,
                        PhoneStatus = phoneDto.PhoneStatus,
                    });
                }
                _repositoryCache.SetCache(phones, 1);
                return JsonConvert.SerializeObject(phones);
            }
        }
    }
}
