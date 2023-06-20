using ContractMakerWebApi.interfaces;
using OpenQA.Selenium.Chrome;

namespace ContractMakerWebApi.Services
{
    public class SeleniumService : ISeleniumService
    {

        public SeleniumService() { }

        public async Task<byte[]> ChromeDriverConvertor(string html)
        {
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "ApplicationName");
            Directory.CreateDirectory(directory);
            var filePath = Path.Combine(directory, $"{Guid.NewGuid()}.html");
            await File.WriteAllTextAsync(filePath, html);

            var chromeOptions = new ChromeOptions();

            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--disable-gpu");
            chromeOptions.AddExcludedArgument("enable-automation");

            var driver = new ChromeDriver(chromeOptions);
            driver.Navigate().GoToUrl(filePath);

            // Output a PDF of the first page in A4 size at 90% scale
            var printOptions = new Dictionary<string, object>
        {
            { "paperWidth", 210 / 25.4 },
            { "paperHeight", 297 / 25.4 },
            { "scale", 0.9 },
            {"preferCSSPageSize", true }

        };

            var printOutput = driver.ExecuteCdpCommand("Page.printToPDF", printOptions) as Dictionary<string, object>;
            var pdf = Convert.FromBase64String(printOutput["data"] as string);

            return pdf;
        }
    }
}
