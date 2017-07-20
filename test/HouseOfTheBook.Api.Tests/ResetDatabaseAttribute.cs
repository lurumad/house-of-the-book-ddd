using System.Reflection;
using Xunit.Sdk;

namespace HouseOfTheBook.Api.Tests
{
    public class ResetDatabaseAttribute : BeforeAfterTestAttribute
    {
        public override void Before(MethodInfo methodUnderTest)
        {
            ContainerFixture.ResetCheckpoint();
        }
    }
}
