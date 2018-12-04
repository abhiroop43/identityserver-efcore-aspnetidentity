using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace maqta.identityserver {
    public class Config {
        public static IEnumerable<IdentityResource> GetIdentityResources () {
            return new List<IdentityResource> {
                new IdentityResources.OpenId (),
                new IdentityResources.Profile (),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources () {
            return new List<ApiResource> {
                new ApiResource ("maqta.ae", "Maqta Gateway API")
            };
        }

        public static IEnumerable<Client> GetClients () {
            return new List<Client> {
                new Client {
                    ClientId = "ro.client",
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                        ClientSecrets = {
                            new Secret ("secret".Sha256 ())
                            },
                            AllowedScopes = { "maqta.ae" },
                            AllowOfflineAccess = true
                            }
            };
        }

    }
}