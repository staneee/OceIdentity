using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;

namespace Common
{
    public class IdentityServerConfig
    {
        public const string Scheme = "Bearer";
        public const string Authority = "http://localhost:10089";
        public const string Client_Msg = "msg";
        public const string Client_Phone = "phone";
        public const string Api_Msg = "msg_api";
        public const string Api_Phone = "phone_api";
        public const string Secret = "MySecret";

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(), //必须要添加，否则报无效的 scope 错误               
                new IdentityResources.Profile()
            };
        }

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
                    AccessTokenLifetime = 5,
                    ClientSecrets =
                    {
                        new Secret(Secret.Sha256())
                    },
                    AllowedScopes =
                    {
                        Api_Msg
                    }
                },
                // resource owner password grant client
                new Client
                {
                    ClientId = Client_Phone,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AccessTokenLifetime = 5,
                    ClientSecrets =
                    {
                        new Secret(Secret.Sha256())
                    },
                    AllowedScopes =
                    {
                        Api_Phone,
                        IdentityServerConstants.StandardScopes.OpenId, //必须要添加，否则报 forbidden 错误
                        IdentityServerConstants.StandardScopes.Profile
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
