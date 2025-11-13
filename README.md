Docker-kuvan rakentamiseksi lisäsin DockerFile- ja .dockerignore-tiedostot, 
joihin lisäsin tarvittavat tiedot, jotka osoittivat selvästi, että tiedosto tulisi avata portin 5001 kautta. 
Samat tiedot lisättiin Program.cs-tiedostoon käyttämällä rivinmuodostajaa builder.WebHost.UseUrls("http://0.0.0.0:5001");

Seuraavaksi rakensin sovelluksen terminaalissa komennolla 
                    docker build -t saisui-app.
Sovellus käynnistetään komennolla 
                    docker run -d -p 5001:8080 saisui-app.
