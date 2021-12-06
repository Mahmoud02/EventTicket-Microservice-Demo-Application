// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace EventTicket.Services.Identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("eventcatalog.fullaccess"),
                new ApiScope("eventcatalog.read"),
                new ApiScope("eventcatalog.write"),
                new ApiScope("shoppingbasket.fullaccess"),
                new ApiScope("discount.fullaccess"),
                new ApiScope("eventticketgateway.fullaccess")
            };

        public static IEnumerable<ApiResource>ApiResources =>
            new ApiResource[]
            {
                new ApiResource("eventcatalog", "Event catalog API")
                {
                    Scopes = { "eventcatalog.read", "eventcatalog.write" }
                },
                new ApiResource("shoppingbasket", "Shopping basket API")
                {
                    Scopes = { "shoppingbasket.fullaccess" }
                },
                new ApiResource("discount", "Discount API")
                {
                    Scopes = { "discount.fullaccess" }
                },
                new ApiResource("eventticketgateway", "Eventticket Gateway")
                {
                    Scopes = { "eventticketgateway.fullaccess" }
                }
            };
     
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
           
                 new Client
                {
                    ClientName = "Event Ticket Client",
                    ClientId = "eventticket",
                    ClientSecrets = { new Secret("ce766e16-df99-411d-8d31-0f5bbc6b8eba".Sha256()) },
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RedirectUris = new []{
                         "https://localhost:5000/signin-oidc",
                    },
                    PostLogoutRedirectUris = { "https://localhost:5000/signout-callback-oidc" },
                    RequireConsent = false,
                    AllowedScopes = { "openid", "profile",
                         "eventticketgateway.fullaccess",
                         "shoppingbasket.fullaccess", 
                         "eventcatalog.read", "eventcatalog.write" }
                }

            };
    }
}