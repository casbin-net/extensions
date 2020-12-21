using System.Collections.Generic;
using Xunit;

namespace Casbin.Extension.Frontend.Tests
{
    using Helpers;

    public class ExtensionsTest
    {
        private const string ACL = "acl";
        private readonly EnforcerBuilder _enforcerBuilder;

        public ExtensionsTest()
        {
            _enforcerBuilder = new EnforcerBuilder();
        }

        [Fact]
        public void PolicyForAcl__SchouldBe__Right()
        {
            var enforcer = _enforcerBuilder.GetEnforcer(ACL);
            var permissions = enforcer.CasbinJsGetPermissionForUser("alice");

            Assert.Equal(new List<string> { "data1" }, permissions["read"]);
            Assert.Equal(new List<string> { "data1", "data2" }, permissions["write"]);
        }
    }
}
