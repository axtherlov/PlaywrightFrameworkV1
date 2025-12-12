namespace PlaywrigthDemo.Tests
{
    public interface ITestBase
    {
        Task NavigateToUrl();
        Task TakeScreenshotAsync(string fileName);
    }
}