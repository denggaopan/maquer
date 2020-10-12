using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace Maquer.IdentityServer
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
                 new ApiResource("OrderService", "OrderService API"),
                 new ApiResource("PaymentService", "PaymentService API")
             };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
             {
                 new Client
                 {
                     AllowedGrantTypes = GrantTypes.ClientCredentials,
                     ClientId = "AuthServiceClient",
                     ClientSecrets =
                     {
                         new Secret("AuthServiceClient".Sha256())
                     },
                     AllowedScopes = new List<string> { "AuthService","UserService","CatalogService","OrderService","PaymentService"},
                     AccessTokenLifetime = 60 * 60 * 1
                 },
                 new Client
                 {
                     AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                     ClientId = "AuthServiceClient2",
                     ClientSecrets =
                     {
                         new Secret("AuthServiceClient2".Sha256())
                     },
                     AllowedScopes = new List<string> { "AuthService","UserService"},
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
                 },
                 new TestUser
                 {
                     Username = "test2",
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
