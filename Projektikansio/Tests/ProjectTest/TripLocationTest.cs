using Bunit;
using SharedLib;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using Moq;
using MyApi.Data;
using MyApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Bunit.TestDoubles;

namespace ProjectTest
{

    public class TripLocationTest : TestContext
    {
        /*Testataan locations -sivu. Mitä tulee esille jos on kirjautunut / kirjautumaton käyttäjä, sekä pari API testiä*/

        //sivu renderöityy, kirjautunut käyttäjä näkee kaiken
        [Fact]
        public async void LoggedUserSeesWholePage()
        {
            //Arrange
            MydbContext _db = new MydbContext();
            MatkakohdesController matkakohdesController = new MatkakohdesController(_db);
            List<SharedLib.matkakohdeDTO>? locations = new List<SharedLib.matkakohdeDTO>();
            var loginState = new LoginState();

            var loggeduser = new SharedLib.matkaajaDTO();
            loggeduser.idmatkaaja = 3000;
            loggeduser.etunimi = "Maija";
            loggeduser.sukunimi = "Meikäläinen";
            loggeduser.nimimerkki = "MaijaMehiläinen";
            loggeduser.paikkakunta = "Maaninka";
            loggeduser.esittely = "Maaginen";
            loggeduser.kuva = "6.png";
            loggeduser.email = "m@m.com";
            loggeduser.password = "12345";

            
            loginState.loggedUser = loggeduser;
            loginState.isLoggedIn = true;

            Services.AddSingleton(new HttpClient { BaseAddress = new System.Uri("http://localhost") });
            Services.AddSingleton<LoginState>(loginState);

            //Act

            var page = RenderComponent<FrontEnd.Pages.Locations>();
            var otsikko = page.Find("#otsikko");           

            //kun on kirjauduttu sisään, nähdään linkki matkakohteiden lisäystä varten
            page.WaitForState(() => loginState.isLoggedIn == true);
            page.WaitForState(() => page.FindAll("#addlink").Count.Equals(1));
            var addlink = page.Find("#addlink");      

            //kun on matkakohteita ja ollaan kirjauduttu, nähdään loput sivun osat ja toiminnot (edit ja poisto -linkit)

            var actionResult = await matkakohdesController.GetLocations();
            foreach (var location in actionResult.Value)
            {
                locations.Add(location);
            }   
            page.Instance.locations = locations;
            
            var test = page.Find("#locationsnotnull");

            page.WaitForState(() => page.Instance.locations.Count.Equals(11)); //pitäisi tulla 11 matkakohdetta

            var infos = page.FindAll("#info");
            var pictures = page.FindAll("#picture");
            var picturelinks = page.FindAll("#picturelink");
            var places = page.FindAll("#placeandcountry");
            var descriptions = page.FindAll("#description");

            var editlinks = page.FindAll("#editlink");
            var deletelinks = page.FindAll("#deletelink");

            //Assert
            Assert.NotNull(otsikko);
            Assert.NotNull(page.Find("#klikkaa"));
            Assert.NotNull(page.Instance.locations);
            Assert.True(page.Instance.locations.Count.Equals(11));
            Assert.NotNull(test);
            Assert.NotNull(infos);
            Assert.NotNull(pictures);
            Assert.NotNull(places);
            Assert.NotNull(descriptions);
            Assert.NotNull(picturelinks);

            //käyttäjä näkee myös kohteita, jotka voi nähdä vain jos on kirjautunut sisään
            Assert.NotNull(addlink);
            Assert.NotNull(editlinks);
            Assert.NotNull(deletelinks);
            
        }


        //sivu renderöityy, kirjautumaton käyttäjä näkee vain matkakohteet (ei kaikkia linkkejä)
        [Fact]
        public async void UnknownUserSeesLocations()
        {
            //Arrange
            MydbContext _db = new MydbContext();
            MatkakohdesController matkakohdesController = new MatkakohdesController(_db);
            List<SharedLib.matkakohdeDTO>? locations = new List<SharedLib.matkakohdeDTO>();
            Services.AddSingleton(new HttpClient { BaseAddress = new System.Uri("http://localhost") });
            var loginState = new LoginState();
            Services.AddSingleton<LoginState>(loginState);

            //Act

            var page = RenderComponent<FrontEnd.Pages.Locations>();
            var otsikko = page.Find("#otsikko");

            var actionResult = await matkakohdesController.GetLocations();
            foreach (var location in actionResult.Value)
            {
                locations.Add(location);
            }
            page.Instance.locations = locations;

            var test = page.Find("#locationsnotnull");
            page.WaitForState(() => page.Instance.locations.Count.Equals(11)); //pitäisi tulla 11 matkakohdetta

            var infos = page.FindAll("#info");
            var pictures = page.FindAll("#picture");
            var places = page.FindAll("#placeandcountry");
            var descriptions = page.FindAll("#description");
            var picturelinks = page.FindAll("#picturelink");

            var editlinks = page.FindAll("#editlink");
            var deletelinks = page.FindAll("#deletelink");

            //Assert
            Assert.NotNull(otsikko);
            Assert.NotNull(page.Find("#klikkaa"));
            Assert.NotNull(page.Instance.locations);
            Assert.True(page.Instance.locations.Count.Equals(11));
            Assert.NotNull(test);
            Assert.NotNull(infos);
            Assert.NotNull(pictures);
            Assert.NotNull(places);
            Assert.NotNull(descriptions);
            Assert.NotNull(picturelinks);

            //ei olla kirjauduttu sisään, joten ei voida esim. muokata tai poistaa kohteita

            Assert.True(editlinks.Count == 0);
            Assert.True(deletelinks.Count == 0);
        }


        //Haetaan matkakohteet, matkakohteita on 11 kpl
        [Fact]
        public async void GetLocationsSucceeds()
        {
            //Arrange
            MydbContext _db = new MydbContext();
            MatkakohdesController matkakohdesController = new MatkakohdesController(_db);

            //Act
            var actionResult = await matkakohdesController.GetLocations();
            Assert.NotNull(actionResult);
            Assert.True(actionResult.Value.Count == 11);
   
        }


        //Haetaan Pariisin matkakohde id:llä 3
        [Fact]
        public async void GetLocationSucceeds()
        {
            //Arrange
            MydbContext _db = new MydbContext();
            MatkakohdesController matkakohdesController = new MatkakohdesController(_db);
            long id = 3;

            //Act
            var actual = await matkakohdesController.GetLocation(id);
            

            //Assert
            Assert.NotNull(actual.Value);
            Assert.True(actual.Value.paikkakunta.Equals("Pariisi"));

        }


        //Matkakohteen haku ei palauta mitään id:llä 500
        [Fact]
        public async void GetLocationFails()
        {
            //Arrange
            MydbContext _db = new MydbContext();
            MatkakohdesController matkakohdesController = new MatkakohdesController(_db);
            long id = 500;

            //Act
            var actual = await matkakohdesController.GetLocation(id);

            //Assert
            Assert.IsType<NotFoundResult>(actual.Result);
        }


        
    }
}
