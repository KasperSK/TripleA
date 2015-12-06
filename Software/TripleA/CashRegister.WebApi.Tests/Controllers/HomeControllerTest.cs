﻿using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashRegister.WebApi;
using CashRegister.WebApi.Controllers;

namespace CashRegister.WebApi.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }

        [TestMethod]
        public void Settings()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Settings() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Settings", result.ViewBag.Title);
        }
    }
}
