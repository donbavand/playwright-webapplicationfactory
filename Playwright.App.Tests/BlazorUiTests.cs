using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using Playwright.App.Tests.Infrastructure;
using Xunit;

namespace Playwright.App.Tests;

public class BlazorUiTests : PageTest, IClassFixture<CustomWebApplicationFactory>
{
    private readonly string _serverAddress;

    public BlazorUiTests(CustomWebApplicationFactory fixture)
    {
        _serverAddress = fixture.ServerAddress;
    }

    [Fact]
    public async Task Navigate_to_counter_ensure_current_counter_increases_on_click()
    {
        //Arrange
        using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync();
        var page = await browser.NewPageAsync();

        //Act
        await page.GotoAsync(_serverAddress);
        await page.ClickAsync("[class='nav-link']");
        await page.ClickAsync("[class='btn btn-primary']");

        //Assert
        await Expect(page.Locator("[role='status']")).ToHaveTextAsync("Current count: 1");
    }
}