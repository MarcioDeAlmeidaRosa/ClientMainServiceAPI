using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientMainServiceAPI.Controllers;

namespace ClientMainServiceAPI.Tests.Controllers
{
    [TestClass]
    public class ClientMainControllerTest
    {
        [TestMethod]
        public void GetByID()
        {
            // Arrange
            ClientMainController controller = new ClientMainController();

            // Act
            ViewResult result = controller.GetByID("1") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
