using Bunit;
using FrontEnd.Pages;
using FrontEnd.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MyApi.Controllers;
using MyApi.Data;
using SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTest
{
    public class GroupTrips : TestContext
    {
        //Tämä facta pittää olla että näkkyyy tuolla test explorelilla <-- Tiedoksi tyhmälle Kapatille
        [Fact]
        public async Task TestGrouptripsPage()
        {
            Services.AddSingleton(new HttpClient { BaseAddress = new System.Uri("http://localhost") });
            Services.AddSingleton<LoginState>();
            var loginState = Services.GetRequiredService<LoginState>();
            loginState.SetLogin(false, null);

            var navMenuComponent = RenderComponent<NavMenu>();

            var kirjauduButton = navMenuComponent.Find("#kirjaudu");
            kirjauduButton.Click();

            await Task.Delay(500);
           
        }

        //Testataan, että sivu renderöityy kirjautuneelle käyttäjälle
        [Fact]
        public async void GroupTripsRenderWithLoggedUserSuccess()
        {
            //Arrange
            MydbContext _db = new MydbContext();
            MatkaajasController matkaajasController = new MatkaajasController(_db);
            var loginState = new LoginState();
            matkaajaDTO loggedUser = new matkaajaDTO();

            long id = 3;
            var actionResult = await matkaajasController.GetMatkaaja(id);

            loggedUser.idmatkaaja = actionResult.Value.Idmatkaaja;
            loggedUser.etunimi = actionResult.Value.Etunimi;
            loggedUser.sukunimi = actionResult.Value.Sukunimi;
            loggedUser.nimimerkki = actionResult.Value.Nimimerkki;
            loggedUser.paikkakunta = actionResult.Value.Paikkakunta;
            loggedUser.esittely = actionResult.Value.Esittely;
            loggedUser.kuva = actionResult.Value.Kuva;
            loggedUser.email = actionResult.Value.Email;
            loggedUser.password = actionResult.Value.Password;

            loginState.loggedUser = loggedUser;
            loginState.isLoggedIn = true;

            Services.AddSingleton(new HttpClient { BaseAddress = new System.Uri("http://localhost") });
            Services.AddSingleton<LoginState>(loginState);

            //Act
            var page = RenderComponent<FrontEnd.Pages.Grouptrips>();

            //kun on kirjauduttu sisään, nähdään sivu
            page.WaitForState(() => loginState.isLoggedIn == true);
            var addlocation = page.Find("#AloitusOtsikko");

            //Assert
            Assert.NotNull(addlocation);
        }

        [Fact]
        public async Task MatkaController_Get_Returns()
        {

            MydbContext dbContext = new MydbContext();
            MatkasController matkasController = new MatkasController(dbContext);

            //Act 
            var result = matkasController.GetMatkas();

            //Assert
            Assert.NotNull(result);


        }

        [Fact]
        public async void MatkasController_GetIdMatka_Returns()
        {
            //Arrange
            MydbContext dbContext = new MydbContext();
            MatkasController matkasController = new MatkasController(dbContext);
            long matkaId = 1;

            //Act
            var actionResult = await matkasController.GetMatka(matkaId);
            //Assert.IsType<OkObjectResult>(actionResult.Result);
            //var actual = GetObjectResultContent<Matka>(actionResult);

            //Assert
            Assert.NotNull(actionResult.Value);

        }


        //Vaatii toimiakseen
        //https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt/63217643#63217643
        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }

    }
}