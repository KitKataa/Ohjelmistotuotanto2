# Ohjelmistotuotanto2

Ryhmätyö: web -pohjainen sovellus
Tehtävänä luoda Kuopion Kulkijat -harrasteryhmälle sosiaalinen alusta, jossa voi jakaa kuvia ja tarinoita toteutuneista matkoista, sekä tallentaa ja muokata matkakohteita.
Kirjautunut käyttäjä pääsee käsiksi ryhmän sisäisiin tietoihin, ja kirjautumattomalle käyttäjälle on rajattu pääsy: esim. tarinoita ei voi nähdä, ellei ole kirjautunut.
Käyttäjä voi muokata vain omia tietojaan, tarinoitaan ja matkojaan. Mikäli matkalla on tarina, sitä ei voi muokata. Mikäli matkakohteeseen on tehty matkoja, sitäkään ei voi muokata.

Oma osuuteni sisälsi matkakohteet -osion, matkojen ja tarinoiden toiminnallisuuden (controllerit ja CRUD-sivut) sekä matkakohteisiin liittyvät testit. Matkakohteiden ulkoasua ja dokumentteja (mm. vaatimus- ja tekninen määrittely) katsottiin yhdessä tiimiläisten kanssa. Matkakohteiden, matkojen ja tarinoiden CRUD-sivut löytyvät kansioista "Pages" kansion alta, matkakohteiden razor -sivut "Pages" kansiosta, controllerit omasta kansiostaan ja AddLocationTest & TripLocationTest ProjectTest:n alta.

Teknologiat: SqLite, ASP.NET / C#, REST API, Bunit
