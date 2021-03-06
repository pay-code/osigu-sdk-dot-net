﻿using System;
using System.Collections.Generic;
using OsiguSDK.Core.Config;
using OsiguSDK.Insurers.Clients.v1;
using OsiguSDK.Insurers.Models.v1;
using OsiguSDK.Insurers.Models.Requests.v1;
using OsiguSDK.Insurers.Clients.v1;
using OsiguSDK.Insurers.Models.Requests.v1;
using OsiguSDK.Insurers.Models.v1;

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
            var createAuthRequest = new CreateAuthorizationRequest
            {
                ReferenceId = "ACJ12398-1236",
                AuthorizationDate = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMonths(1),
                Diagnoses = new List<Diagnosis>
                {
                    new Diagnosis {Name = "Gastroenteritis"},
                    new Diagnosis {Name = "Barrett's Esophagus"}
                },
                Doctor = new Doctor
                {
                    CountryCode = "GT",
                    Name = "Wade Wilson",
                    MedicalLicense = "MG20100211-4567",
                    Specialties = new List<Doctor.DoctorSpecialty>
                    {
                        new Doctor.DoctorSpecialty
                        {
                            Name = "Gastroenterologist"
                        }
                    }
                },
                Policy = new Policy
                {
                    CountryCode = "GT",
                    Number = "55222",
                    Certificate = "0000011255",
                    ExpirationDate = DateTime.UtcNow.AddYears(1),
                    InsuranceCompanyName = "Roble",
                    PolicyHolder = new Policy.PolicyHolderInfo
                    {
                        Id = "022165654654654",
                        Name = "William Smith",
                        Email = "william.smith@gmail.com",
                        Cellphone = "(734) 555-1212",
                        Owner = true,
                        DateOfBirth = DateTime.UtcNow.AddYears(-15)
                    }
                },
                Items = new List<CreateAuthorizationRequest.Item>
                {
                    new CreateAuthorizationRequest.Item
                    {
                        ProductId = "QAINSURER1",
                        Quantity = 1
                    },
                    new CreateAuthorizationRequest.Item
                    {
                        ProductId = "QAINSURER2",
                        Quantity = 5
                    }
                }
            };


            var response = _client.Create(createAuthRequest);
            return response;
        }


        public Authorization GetRecentlyCreatedAuthorization(string id)
        {
            var response = _client.GetSingle(id);
            return response;
        }

        public Authorization ModifyTheAuthorization(string id, CreateAuthorizationRequest createAuthorizationObj)
        {
            var response = _client.Modify(id, createAuthorizationObj);
            return response;
        }

        public void VoidTheAuthorization(string id)
        {
            _client.Void(id);            
        }

    }
}
