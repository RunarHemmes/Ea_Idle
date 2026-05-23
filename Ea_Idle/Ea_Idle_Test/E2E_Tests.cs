using Microsoft.Playwright;

namespace Ea_Idle_Test
{
    [TestClass]
    public class E2E_Tests : PageTest
    {
        [TestMethod]
        public async Task Login()
        {
            await Page.GotoAsync("https://localhost:7020/");

            await Page.Locator("#Username_Input").FillAsync("John");
            await Page.Locator("#Password_Input").FillAsync("passwordJohn");
            await Page.Locator("#Login_Btn").ClickAsync();

            Thread.Sleep(3000);

            Assert.AreEqual<string>("https://localhost:7020/Mining", Page.Url);
        }
    }
}
