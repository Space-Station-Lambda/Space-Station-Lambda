using SSL_Core.roles;
using Xunit;

namespace SSL_Core_Tests.roles
{
    public class RoleTests
    {
        
        [Fact]
        private void Test_ToString()
        {
            Role role = new RoleAssistant() ;
            Assert.Equal("assistant", role.ToString());
        }
    }
}