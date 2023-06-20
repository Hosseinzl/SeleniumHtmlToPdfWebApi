namespace ContractMakerWebApi.interfaces
{
    public interface ISeleniumService
    {
        Task<byte[]> ChromeDriverConvertor(string html);
    }
}
