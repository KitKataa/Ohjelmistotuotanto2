using Bunit;
using FrontEnd.Pages;
using FrontEnd.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using SharedLib;
using System.Net.Http;
using System.Threading.Tasks;
using Topshelf.Hosts;
using Topshelf.Runtime;
using Xunit;

namespace ProjectTest
{
    public class LoginTest : TestContext
    {


        [Fact]
        public async Task TestLogin()
        {

            Services.AddSingleton(new HttpClient { BaseAddress = new System.Uri("http://localhost") });
            Services.AddSingleton<LoginState>();
            var loginState = Services.GetRequiredService<LoginState>();
            loginState.SetLogin(false, null);


            var navMenuComponent = RenderComponent<NavMenu>();


            var kirjauduButton = navMenuComponent.Find("#kirjaudu");
            kirjauduButton.Click();
            var navigationManager = Services.GetRequiredService<NavigationManager>();
            Assert.Equal("http://localhost/login", navigationManager.Uri);
            await Task.Delay(500);


            var loginComponent = RenderComponent<Login>();


            //var emailInput = loginComponent.Find("#email");
            //var passwordInput = loginComponent.Find("#password");
            //emailInput.Change("sebastian@halonen.com");
            //passwordInput.Change("12345");

            //var submitButton = loginComponent.Find("button[type='submit']");
            //submitButton.Click();

            

            //    Assert.Equal("http://localhost/user/2", navigationManager.Uri);

            //    var loggedUser = loginState.loggedUser;
            //    Assert.NotNull(loggedUser);
            //    Assert.Equal(2, loggedUser.idmatkaaja);
            //    Assert.True(loginState.isLoggedIn);
        }


        [Fact]
        public async Task TestRegistry()
        {

            Services.AddSingleton(new HttpClient { BaseAddress = new System.Uri("http://localhost") });
            Services.AddSingleton<LoginState>();
            var loginState = Services.GetRequiredService<LoginState>();
            loginState.SetLogin(false, null);


            var navMenuComponent = RenderComponent<NavMenu>();


            var RekisteroidyButton = navMenuComponent.Find("#rekisteroidy");
            RekisteroidyButton.Click();
            var navigationManager = Services.GetRequiredService<NavigationManager>();
            Assert.Equal("http://localhost/signup", navigationManager.Uri);
            await Task.Delay(500);


           
        }

    }
}

