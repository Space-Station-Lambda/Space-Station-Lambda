using SSL.Role;
using Xunit;

namespace SSL.Tests.Roles
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
