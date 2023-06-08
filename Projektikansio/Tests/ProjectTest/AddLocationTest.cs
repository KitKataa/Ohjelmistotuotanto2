using Bunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MyApi.Controllers;
using MyApi.Data;
using SharedLib;


namespace ProjectTest
{
    public class AddLocationTest : TestContext
    {
        //matkakohteiden lisäyssivun renderöityminen kirjautuneella käyttäjällä
        [Fact]
        public async void AddLocationRenderWithLoggedUserSuccess()
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

            Services.AddSingleton(new HttpClient{BaseAddress = new System.Uri("http://localhost")});
            Services.AddSingleton<LoginState>(loginState);

            //Act
            var page = RenderComponent<FrontEnd.Pages.CRUDlocations.AddLocation>();

            //kun on kirjauduttu sisään, nähdään sivu
            page.WaitForState(() => loginState.isLoggedIn == true);
            var addlocation = page.Find("#lisaamatkakohde");
            var button = page.Find("#AddButton");

            //Assert
            Assert.NotNull(addlocation);
            Assert.NotNull(button);
        }


        //matkakohteiden lisäyssivun renderöityminen kirjautumattomalla käyttäjällä
        [Fact]
        public async void AddLocationRenderWithUnknownUserSuccess()
        {
            //Arrange
            MydbContext _db = new MydbContext();
            var loginState = new LoginState();
            Services.AddSingleton(new HttpClient { BaseAddress = new System.Uri("http://localhost") });
            Services.AddSingleton<LoginState>(loginState);

            //Act
            var page = RenderComponent<FrontEnd.Pages.CRUDlocations.AddLocation>();

            //koska ei olla kirjauduttu sisään, nähdään vain "Kirjaudu sisään lisätäksesi matkakohteita"  -teksti
            var NotLoggedIn = page.Find("#notloggedin");
            var loading = page.Find("#loading");

            //Assert
            Assert.NotNull(NotLoggedIn);
            Assert.NotNull(loading);

        }


        //Napin painaminen (kirjautunut käyttäjä)
        [Fact]
        public async void ClickButton()
        {
            //Arrange
            MydbContext _db = new MydbContext();
            MatkakohdesController matkakohdesController = new MatkakohdesController(_db);
            MatkaajasController matkaajasController = new MatkaajasController(_db);
            SharedLib.matkakohdeDTO newlocation = new SharedLib.matkakohdeDTO();
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

            newlocation.kuva = "Pariisi.jpg";
            newlocation.kohdenimi = "Tour de France";
            newlocation.paikkakunta = "Pariisi";
            newlocation.maa = "Ranska";
            newlocation.kuvausteksti = "Tour de France!";

            //Act
            var page = RenderComponent<FrontEnd.Pages.CRUDlocations.AddLocation>();

            //kun on kirjauduttu sisään, nähdään sivu
            page.WaitForState(() => loginState.isLoggedIn == true);
            page.Instance.newlocation = newlocation;
            await page.Find("button").ClickAsync(new Microsoft.AspNetCore.Components.Web.MouseEventArgs());

            //Assert, kun nappia painetaan niin isSubmitting arvo vaihtuu todeksi
            Assert.True(page.Instance.isSubmitting == true);
            
        }

        //Matkakohteen lisääminen tietokantaan
        [Fact]
        public async void PostLocationSucceeds()
        {
            //Arrange
            MydbContext _db = new MydbContext();
            MatkakohdesController matkakohdesController = new MatkakohdesController(_db);
            matkakohdeDTO location = new matkakohdeDTO();
            location.kuva = "Pariisi.jpg";
            location.kohdenimi = "Tour de France";
            location.paikkakunta = "Pariisi";
            location.maa = "Ranska";
            location.kuvausteksti = "Tour de France!";

            //Act
            var actionResult = await matkakohdesController.PostLocation(location);
            var actual = GetObjectResultContent<matkakohdeDTO>(actionResult);
            //Assert

            Assert.NotNull(actual);
        }

        //https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt/63217643#63217643
        private static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }
    }
}
