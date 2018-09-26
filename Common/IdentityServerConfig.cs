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
        public const string client_1 = "client";
        public const string client_2 = "ro.client";
        public const string MsgApi = "msg_api";
        public const string PhoneApi = "phone_api";
        public const string Secret = "MySecret";


        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(MsgApi, "Msg"),
                new ApiResource(PhoneApi, "Phone")
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
                    ClientId = client_1,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(Secret.Sha256())
                    },
                    AllowedScopes =
                    {
                       MsgApi,
                    }
                },
                // resource owner password grant client
                new Client
                {
                    ClientId = client_2,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret(Secret.Sha256())
                    },
                    AllowedScopes =
                    {
                       PhoneApi
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
