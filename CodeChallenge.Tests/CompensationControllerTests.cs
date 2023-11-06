
using System;
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var compensation = new Compensation()
            {
                EmployeeId = "employeeId",
                EffectiveDate = DateTime.Now,
                CompensationId = Guid.NewGuid().ToString(),
                Salary = 20000
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation/createcompensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.CompensationId);
            Assert.AreEqual(compensation.EmployeeId, newCompensation.EmployeeId);
            Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
            Assert.AreEqual(compensation.Salary, newCompensation.Salary);
        }

        [TestMethod]
        public void GetCompensationById_Returns_Ok()
        {
            // Arrange
            var expectedCompensationId = "a8f52eb2-e441-40ea-a114-b6720eb50162";
            var expectedEmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedSalary = 50000;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/getcompensationbyid/{expectedCompensationId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(expectedCompensationId, compensation.CompensationId);
            Assert.AreEqual(expectedEmployeeId, compensation.EmployeeId);
            Assert.AreEqual(expectedSalary, compensation.Salary);
        }

        [TestMethod]
        public void GetCompensationByEmployeeId_Returns_Ok()
        {
            // Arrange
            var expectedCompensationId = "a8f52eb2-e441-40ea-a114-b6720eb50162";
            var expectedEmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedSalary = 50000;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/getcompensationbyemployeeid/{expectedEmployeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(expectedCompensationId, compensation.CompensationId);
            Assert.AreEqual(expectedEmployeeId, compensation.EmployeeId);
            Assert.AreEqual(expectedSalary, compensation.Salary);
        }
    }
}
