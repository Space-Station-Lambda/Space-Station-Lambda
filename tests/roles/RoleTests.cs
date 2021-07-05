using ssl.Role;
using Xunit;

namespace ssl.Tests.Roles
{
    public class RoleTests
    {
        
        [Fact]
        private void Test_ToString()
        {
            Role.Role role = new RoleAssistant() ;
            Assert.Equal("assistant", role.ToString());
        }
    }
}
