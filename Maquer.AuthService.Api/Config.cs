using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maquer.AuthService.Api
{
    public sealed class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
             {
                 new ApiResource("AuthService", "AuthService API"),
                 new ApiResource("UserService", "UserService API"),
                 new ApiResource("CatalogService", "CatalogService API"),
                 new ApiResource("OrderService", "OrderService API")
             };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
             {
                 new Client
                 {
                     ClientId = "AuthServiceClient",
                     AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                     ClientSecrets =
                     {
                         new Secret("AuthServiceClient".Sha256())
                     },
                     AllowedScopes = new List<string> { "AuthService","UserService","CatalogService","OrderService"},
                     AccessTokenLifetime = 60 * 60 * 1
                 }
             };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
             {
                 new TestUser
                 {
                     Username = "paul",
                     Password = "123456",
                     SubjectId = "1"
                 },
                 new TestUser
                 {
                     Username = "pauldeng",
                     Password = "123456",
                     SubjectId = "2"
                 }
             };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>();
        }
    }
}
