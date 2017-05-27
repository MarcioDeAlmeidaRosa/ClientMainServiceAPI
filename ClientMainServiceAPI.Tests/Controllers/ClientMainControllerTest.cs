using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientMainServiceAPI.Controllers;
using ClientMainServiceAPI.Model.Contracts;

namespace ClientMainServiceAPI.Tests.Controllers
{
    [TestClass]
    public class ClientMainControllerTest
    {
        private IPersonModel _model;

        public ClientMainControllerTest(IPersonModel model)
        {
            this._model = model;
        }

        [TestMethod]
        public void GetByID()
        {
            // Arrange
            ClientMainController controller = new ClientMainController(this._model);

            // Act
            ViewResult result = controller.GetByID("1") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
