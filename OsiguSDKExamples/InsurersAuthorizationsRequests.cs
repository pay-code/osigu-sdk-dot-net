using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsiguSDK.Clients.Insurers;
using OsiguSDK.Config;
using OsiguSDK.Models.Insurers;
using OsiguSDK.Models.Insurers.Base;
using OsiguSDK.Models.Insurers.Requests;
using OsiguSDK.Models.Insurers.Responses;

namespace OsiguSDKExamples
{
    public class InsurersAuthorizationsRequests
    {
        private IConfiguration _config;

        public InsurersAuthorizationsRequests(IConfiguration config)
        {
            _config = config;
        }

        public void CreateAuthorization()
        {
            var _createAuthRequest = new AuthorizationRequest()
            {
                ReferenceId = "ACJ12398-1233",
                AuthorizationDate = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMonths(1),
                Diagnoses = new List<Diagnosis>()
                {
                    new Diagnosis(){Name = "Gastroenteritis"},
                    new Diagnosis(){Name = "Barrett's Esophagus"}
                },
                DoctorInfo = new DoctorInfo()
                {
                    Name = "Wade Wilson",
                    MedicalLicense = "MG20100211-4567",
                    Specialties = new List<DoctorInfo.DoctorSpecialty>()
                    {
                        new DoctorInfo.DoctorSpecialty()
                        {
                            Name = "Gastroenterologist"
                        }
                    }
                },
                Policy = new Policy()
                {
                    CountryCode = "GT",
                    Number = "55222",
                    Certificate = "0000011255",
                    ExpirationDate = DateTime.Now.AddYears(1),
                    InsuranceCompanyName = "Roble",
                    PolicyHolder = new Policy.PolicyHolderInfo()
                    {
                        Id = "022165654654654",
                        Name = "William Smith",
                        Email = "william.smith@gmail.com",
                        Cellphone = "(734) 555-1212",
                        Owner = true,
                        DateOfBirth = DateTime.Now.AddYears(-15)
                    }
                },
                Items = new List<ItemDetail>()
                {
                    new ItemDetail()
                    {
                        ProductId = "101",
                        Quantity = 1
                    },
                    new ItemDetail()
                    {
                        ProductId = "100",
                        Quantity = 5
                    }
                }                
            };

            var _client = new AuthorizationClient(_config);
            AuthorizationResponse response = _client.CreateAuthorization(_createAuthRequest);
            
        }
    }
}
