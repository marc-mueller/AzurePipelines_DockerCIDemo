using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DevFun.Deplyoment.Verification.Tests
{
    [TestClass]
    public class DeploymentVerificationTests : TestBase
    {
        private const string baseApiUrl = "api/V1.0/jokes";

        [TestMethod]
        public async Task DevFun_GetRandomJoke_ResultOk()
        {
            // Arrange
            using var client = this.CreateHttpClient();

            // Act
            var response = await client.GetAsync($"{baseApiUrl}/random").ConfigureAwait(false);

            // Assert
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task DevFun_GetStatus_StatusOk()
        {
            // Arrange
            using var client = this.CreateHttpClient();

            // Act
            var response = await client.GetAsync($"api/status").ConfigureAwait(false);

            // Assert
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}