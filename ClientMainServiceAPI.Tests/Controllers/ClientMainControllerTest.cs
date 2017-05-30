using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientMainServiceAPI.Controllers;
using ClientMainServiceAPI.Controller.Contracts;
using System.Web.Http.Results;

namespace ClientMainServiceAPI.Tests.Controllers
{
    [TestClass]
    public class ClientMainControllerTest
    {
        private IPersonController _model;

        public ClientMainControllerTest(IPersonController model)
        {
            this._model = model;
        }

        [TestMethod]
        public void GetByID()
        {
            // Arrange
            ClientMainController controller = new ClientMainController(this._model);

            // Act
            //ViewResult  await OkResult = controller.GetByID("1") as ViewResult;

            // Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
