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
                     ClientId = "Client-1",
                     AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                     ClientSecrets =
                     {
                         new Secret("Client-1".Sha256())
                     },
                     AllowedScopes = new List<string> {"UserService","CatalogService"},
                     AccessTokenLifetime = 60 * 60 * 1
                 },
                 new Client
                 {
                     ClientId = "Client-2",
                     AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                     ClientSecrets =
                     {
                         new Secret("Client-2".Sha256())
                     },
                     AllowedScopes = new List<string> {"CatalogService"},
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
                     Username = "test",
                     Password = "123456",
                     SubjectId = "1"
                 }
             };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>();
        }
    }
}
