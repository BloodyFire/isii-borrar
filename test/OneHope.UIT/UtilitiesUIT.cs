﻿using Microsoft.VisualStudio.TestPlatform.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneHope.UIT
{
    public class UtilitiesUIT
    {
        private static bool _pipeline = false;
        //private static string _browser = "Chrome";
        //private static string _browser = "Firefox";
        private static string _browser = "Edge";
        public static string URIforUIT
        {
            get
            {
                return "https://localhost:7214/";

            }
        }

        public static void SetUp_UIT(out IWebDriver _driver, out string _URI)
        {
            switch (_browser)
            {
                case "Firefox":
                    SetUp_FireFox4UIT(out _driver);
                    break;
                case "Edge":
                    SetUp_EdgeFor4UIT(out _driver);
                    break;
                default:
                    //by default Chrome will be used
                    SetUp_Chrome4UIT(out _driver);
                    break;
            }
            //Added to make _Driver wait when an element is not found.
            //It will wait for a maximum of 50 seconds.


            _URI = URIforUIT;
            _driver.Manage().Window.Maximize();



        }

        public static void SetUp_Chrome4UIT(out IWebDriver _driver)
        {
            var optionsc = new ChromeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal,
                AcceptInsecureCertificates = true
            };
            //For pipelines use this option for hiding the browser
            if (_pipeline) optionsc.AddArgument("--headless");

            _driver = new ChromeDriver(optionsc);

        }

        public static void SetUp_FireFox4UIT(out IWebDriver _driver)
        {
            var optionsff = new FirefoxOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal,
                AcceptInsecureCertificates = true
            };
            //For pipelines use this option for hiding the browser
            if (_pipeline) optionsff.AddArgument("--headless");

            _driver = new FirefoxDriver(optionsff);

        }

        public static void SetUp_EdgeFor4UIT(out IWebDriver _driver)
        {
            //var edgeDriverService = Microsoft.Edge.SeleniumTools.EdgeDriverService.CreateChromiumService();
            //var edgeOptions = new Microsoft.Edge.SeleniumTools.EdgeOptions();
            //edgeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
            //edgeOptions.UseChromium = true;
            //if (_pipeline) edgeOptions.AddArguments("--headless");

            //_driver = new Microsoft.Edge.SeleniumTools.EdgeDriver(edgeDriverService, edgeOptions);

            var optionsEdge = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal,
                AcceptInsecureCertificates = true
            };

            //For pipelines use this option for hiding the browser
            if (_pipeline) optionsEdge.AddArgument("--headless");

            _driver = new EdgeDriver(optionsEdge);

        }


        public static void WaitForBeingVisible(IWebDriver _driver, By IdElement)
        {
            //used whenever the webelement needs a delay for being clickable
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 10, 0));


            wait.IgnoreExceptionTypes(typeof(NoSuchElementException),
                typeof(WebDriverTimeoutException),
                typeof(UnhandledAlertException),
                typeof(ElementClickInterceptedException));
            bool notFoundButton = true;
            while (notFoundButton)
            {
                try
                {
                    wait.Until(ExpectedConditions.ElementIsVisible(IdElement));
                    notFoundButton = false;
                }
                catch (ElementClickInterceptedException ex)
                {

                }
            }
        }
    }
}
