
namespace PlaywrigthDemo
{
    public interface ITestBase
    {
        Task NavigateToUrl();
        Task TakeScreenshotAsync(string fileName);
    }
}