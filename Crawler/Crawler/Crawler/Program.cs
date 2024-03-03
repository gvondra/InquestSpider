using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Collections.ObjectModel;

namespace InquestSpider.Crawler
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            FirefoxOptions options = new FirefoxOptions();
            // options.AddArgument("-headless");
            using FirefoxDriver firefoxDriver = new FirefoxDriver(options);
            firefoxDriver.Url = "http://example.com";
            Console.WriteLine(firefoxDriver.Title);
            ReadOnlyCollection<IWebElement> elements = firefoxDriver.FindElements(By.CssSelector("body"));
            firefoxDriver.Close();
        }
    }
}
