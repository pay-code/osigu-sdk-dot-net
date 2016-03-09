using System;
using System.Collections.Generic;
using OsiguSDK.Core.Config;
using OsiguSDK.Insurers.Clients;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Models.Requests;

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
        public Authorization CreateAuthorization()
        {
            var createAuthRequest = new CreateAuthorizationRequest()
            {
                ReferenceId = "ACJ12398-1233",
                AuthorizationDate = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMonths(1),
                Diagnoses = new List<Diagnosis>()
                {
                    new Diagnosis() {Name = "Gastroenteritis"},
                    new Diagnosis() {Name = "Barrett's Esophagus"}
                },
                Doctor = new Doctor()
                {
                    CountryCode = "GT",
                    Name = "Wade Wilson",
                    MedicalLicense = "MG20100211-4567",
                    Specialties = new List<Doctor.DoctorSpecialty>()
                    {
                        new Doctor.DoctorSpecialty()
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
                Items = new List<CreateAuthorizationRequest.Item>()
                {
                    new CreateAuthorizationRequest.Item()
                    {
                        ProductId = "101",
                        Quantity = 1
                    },
                    new CreateAuthorizationRequest.Item()
                    {
                        ProductId = "100",
                        Quantity = 5
                    }
                }
            };


            AuthorizationResponse response = _client.CreateAuthorization(createAuthRequest);
            return response;
        }


        public Authorization GetRecentlyCreatedAuthorization(string id)
        {
            var response = _client.GetSingleAuthorization(id);
            return response;
        }

        public Authorization ModifyTheAuthorization(string id, CreateAuthorizationRequest createAuthorizationObj)
        {
            var response = _client.ModifyAuthorization(id, createAuthorizationObj);
            return response;
        }

        public void VoidTheAuthorization(string id)
        {
            _client.VoidAuthorization(id);            
        }

    }
}
