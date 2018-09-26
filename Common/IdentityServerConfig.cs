using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;

namespace Common
{
    public class IdentityServerConfig
    {
        public const string Scheme = "Bearer";
        public const string Authority = "http://localhost:10086/ids4";
        public const string Client_Msg = "msg";
        public const string Client_Phone = "phone";
        public const string Api_Msg = "msg_api";
        public const string Api_Phone = "phone_api";
        public const string Secret = "MySecret";


        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(Api_Msg, "Msg"),
                new ApiResource(Api_Phone, "Phone")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = Client_Msg,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(Secret.Sha256())
                    },
                    AllowedScopes =
                    {
                       Api_Msg,
                    }
                },
                // resource owner password grant client
                new Client
                {
                    ClientId = Client_Phone,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret(Secret.Sha256())
                    },
                    AllowedScopes =
                    {
                       Api_Phone
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "admin",
                    Password = "123"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "root",
                    Password = "321"
                }
            };
        }
    }

}
