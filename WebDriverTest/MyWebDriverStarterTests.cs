using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.IE;
using System.Collections;
using OpenQA.Selenium;
using System.Threading;

namespace WebDriverTest
{
    [TestClass]
    public class MyWebDriverStarterTests
    {
        RemoteWebDriver Driver;
        WeightCountPage page;

        [TestInitialize]
        public void TestInitialize()
        {
            if (Driver == null)
            {
                InternetExplorerOptions options = new InternetExplorerOptions();
                options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                options.RequireWindowFocus = true;

                Driver = new InternetExplorerDriver(options);

                
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1000);
                Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(1000);
            }
            Driver.Navigate().GoToUrl(@"http://svyatoslav.biz/testlab/wt/index.php");
            page = new WeightCountPage(Driver);
        }

        [TestMethod]
        public void TestPageOpen()
        {
            try
            {
                var title = Driver.Title;
                Assert.AreEqual("Расчёт веса", title);
            }
            finally
            {
                Driver.Quit();
            }
        }

        [TestMethod]
        public void TestInputEmpty()
        {
            try
            {
                page.clickSubmit();
                Thread.Sleep(1000);
                Assert.IsTrue(page.isTDExists("Не указано имя.\r\nРост должен быть в диапазоне 50-300 см.\r\nВес должен быть в диапазоне 3-500 кг.\r\nНе указан пол."));
            }
            finally
            {
                Driver.Quit();
            }
        }

        [TestMethod]
        public void TestInputName()
        {
            try
            {
                page.typeName("Temp");
                page.clickSubmit();
                Thread.Sleep(1000);
                Assert.IsTrue(page.isTDExists("Рост должен быть в диапазоне 50-300 см.\r\nВес должен быть в диапазоне 3-500 кг.\r\nНе указан пол."));
            }
            finally
            {
                Driver.Quit();
            }
        }

        [TestMethod]
        public void TestInputCorrect()
        {
            try
            {
                page.typeName("T");
                page.typeHeight("170");
                page.typeWeight("65");
                page.clickMGender();
                page.clickSubmit();
                Thread.Sleep(1000);
                Assert.IsTrue(page.isTDExists("масса тела"));
            }
            finally
            {
                Driver.Quit();
            }
        }

        [TestMethod]
        public void TestHeightOutOfRangeHight()
        {
            try
            {
                page.typeName("T");
                page.typeHeight("301");
                page.typeWeight("65");
                page.clickMGender();
                page.clickSubmit();
                Thread.Sleep(1000);
                Assert.IsTrue(page.isTDExists("Рост должен быть в диапазоне 50-300 см."));
            }
            finally
            {
                Driver.Quit();
            }
        }

        [TestMethod]
        public void TestHeightOutOfRangeLow()
        {
            try
            {
                page.typeName("T");
                page.typeHeight("29");
                page.typeWeight("65");
                page.clickMGender();
                page.clickSubmit();
                Thread.Sleep(1000);
                Assert.IsTrue(page.isTDExists("Рост должен быть в диапазоне 50-300 см."));
            }
            finally
            {
                Driver.Quit();
            }
        }

        [TestMethod]
        public void TestWeightOutOfRangeHight()
        {
            try
            {
                page.typeName("T");
                page.typeHeight("170");
                page.typeWeight("501");
                page.clickMGender();
                page.clickSubmit();
                Thread.Sleep(1000);
                Assert.IsTrue(page.isTDExists("Вес должен быть в диапазоне 3-500 кг."));
            }
            finally
            {
                Driver.Quit();
            }
        }

        [TestMethod]
        public void TestWeightOutOfRangeLow()
        {
            try
            {
                page.typeName("T");
                page.typeHeight("170");
                page.typeWeight("2");
                page.clickMGender();
                page.clickSubmit();
                Thread.Sleep(1000);
                Assert.IsTrue(page.isTDExists("Вес должен быть в диапазоне 3-500 кг."));
            }
            finally
            {
                Driver.Quit();
            }
        }
    }
}
