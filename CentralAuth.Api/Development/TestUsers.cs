using System.Collections.Generic;
using System.Security.Claims;
using Duende.IdentityServer.Test;

namespace CentralAuth.Api.Development
{
    public static class TestUsers
    {
        public static List<TestUser> Users =>
            [
                new TestUser
                {
                    SubjectId = "1",
                    Username = "zca200",
                    Password = "HeyZCA200!2024"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "zca210",
                    Password = "HeyZCA210!2024"
                }
            ];
    }
}
