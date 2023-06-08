using Bunit;
namespace ProjectTest
{

    public class SignUpTest : TestContext
    {
        [Fact]
        public async void SignInExists()
        {
            //Tarkistetaan ett� rekister�itymissivu on kunnossa, ja ett� rekistre�iryminen on mahdollista.
            var page = RenderComponent<FrontEnd.Pages.Signup>();

            Checkpoint1(page);
            await Task.Delay(500);
            Checkpoint2(page);
            await Task.Delay(500);
            Checkpoint3(page);
            await Task.Delay(500);
            Checkpoint4(page);
            await Task.Delay(500);
            Checkpoint5(page);
            await Task.Delay(500);
            Checkpoint6(page);
            await Task.Delay(500);
            Checkpoint7(page);
            await Task.Delay(500);
            Checkpoint8(page);
            await Task.Delay(500);
            Checkpoint9(page);
        }

        private static void Checkpoint1(IRenderedComponent<FrontEnd.Pages.Signup> page)
        {
            //Etunimen voi syott��
            var firstNameInput = page.Find("#etunimi");
            Assert.NotNull(firstNameInput);
        }

        private static void Checkpoint2(IRenderedComponent<FrontEnd.Pages.Signup> page)
        {
            //Sukunimen voi sy�tt��
            var lastNameInput = page.Find("#sukunimi");
            Assert.NotNull(lastNameInput);
        }

        private static void Checkpoint3(IRenderedComponent<FrontEnd.Pages.Signup> page)
        {
            //nimimerkin voi sy�tt��
            var usernameInput = page.Find("#nimimerkki");
            Assert.NotNull(usernameInput);
        }

        private static void Checkpoint4(IRenderedComponent<FrontEnd.Pages.Signup> page)
        {
            //esittelyn voi sy�tt��
            var bioInput = page.Find("#esittely");
            Assert.NotNull(bioInput);
        }

        private static void Checkpoint5(IRenderedComponent<FrontEnd.Pages.Signup> page)
        {
            //paikkakunnan voi sy�tt��
            var locationInput = page.Find("#paikkakunta");
            Assert.NotNull(locationInput);
        }

        private static void Checkpoint6(IRenderedComponent<FrontEnd.Pages.Signup> page)
        {
            //s�hk�postin voi sy�tt��
            var emailInput = page.Find("#email");
            Assert.NotNull(emailInput);
        }

        private static void Checkpoint7(IRenderedComponent<FrontEnd.Pages.Signup> page)
        {
            //salasanan voi sy�tt��
            var passwordInput = page.Find("#password");
            Assert.NotNull(passwordInput);
        }

        private static void Checkpoint8(IRenderedComponent<FrontEnd.Pages.Signup> page)
        {
            //Rekister�ityminen on mahdollista
            var button = page.Find("button[type='submit']");
            Assert.NotNull(button);
        }

        private static void Checkpoint9(IRenderedComponent<FrontEnd.Pages.Signup> page)
        {
            //Kuvan voi valita
            var radioButton = page.Find("input[type='radio'][name='kuva'][value='1.png'][checked]");
            Assert.NotNull(radioButton);
        }



    }
}
