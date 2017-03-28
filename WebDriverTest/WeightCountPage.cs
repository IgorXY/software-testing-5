using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.IE;
using System.Collections;
using OpenQA.Selenium;

namespace WebDriverTest
{
    public class WeightCountPage
    {
        private RemoteWebDriver driver;

        By nameLocator = By.Name("name");
        By heightLocator = By.Name("height");
        By weightLocator = By.Name("weight");
        By genderMLocator = By.CssSelector("input[value='m']");
        By genderFLocator = By.CssSelector("input[value='f']");
        By submitLocator = By.XPath("//input[@type='submit']");
        By messageLocator = By.TagName("b");
        By tdTag = By.TagName("td");



        public WeightCountPage(RemoteWebDriver driver)
        {
            this.driver = driver;
            if (!"Расчёт веса".Equals(driver.Title))
            {
                throw new Exception("This is not the weight count page");
            }
        }

        public WeightCountPage typeName(String name)
        {
            driver.FindElement(nameLocator).SendKeys(name); 
            return this;
        }

        public WeightCountPage typeHeight(String height)
        {
            driver.FindElement(heightLocator).SendKeys(height);
            return this;
        }

        public WeightCountPage typeWeight(String weight)
        {
            driver.FindElement(weightLocator).SendKeys(weight); 
            return this;
        }

        public WeightCountPage clickMGender()
        {
            driver.FindElement(genderMLocator).Click();
            return this;
        }

        public WeightCountPage clickFGender()
        {
            driver.FindElement(genderFLocator).Click();
            return this;
        }

        public WeightCountPage clickSubmit()
        {
            driver.FindElement(submitLocator).Click();
            return this;
        }

        public string getMessage()
        {
            var el = driver.FindElement(messageLocator);
           return driver.FindElement(messageLocator).Text;
        }

        public bool isTDExists(string value)
        {
            var el = driver.FindElements(tdTag);
            return ((el.Where(x => System.Text.RegularExpressions.Regex.
            IsMatch(x.Text, value, System.Text.RegularExpressions.RegexOptions.IgnoreCase))).Count() > 0);
        }

    }
}
