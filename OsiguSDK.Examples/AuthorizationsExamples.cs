using System;
using System.Collections.Generic;
using OsiguSDK.Core.Config;
using OsiguSDK.Insurers.Clients;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Models.Base;
using OsiguSDK.Insurers.Models.Requests;
using OsiguSDK.Insurers.Models.Responses;

namespace OsiguSDKExamples
{
    public class AuthorizationsExamples
    {
        private readonly AuthorizationsClient _client;

        public AuthorizationsExamples(IConfiguration config)
        {
            _client = new AuthorizationsClient(config);
        }


        /// <summary>
        /// Create authorization method for insurers
        /// </summary>
        public AuthorizationResponse CreateAuthorization()
        {
            var createAuthRequest = new AuthorizationRequest()
            {
                ReferenceId = "ACJ12398-1233",
                AuthorizationDate = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMonths(1),
                Diagnoses = new List<Diagnosis>()
                {
                    new Diagnosis() {Name = "Gastroenteritis"},
                    new Diagnosis() {Name = "Barrett's Esophagus"}
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


            AuthorizationResponse response = _client.CreateAuthorization(createAuthRequest);
            return response;
        }


        public AuthorizationResponse GetRecentlyCreatedAuthorization(string id)
        {
            var response = _client.GetSingleAuthorization(id);
            return response;
        }

        public AuthorizationResponse ModifyTheAuthorization(string id, AuthorizationRequest authorizationObj)
        {
            var response = _client.ModifyAuthorization(id, authorizationObj);
            return response;
        }

        public void VoidTheAuthorization(string id)
        {
            _client.VoidAuthorization(id);            
        }

    }
}
