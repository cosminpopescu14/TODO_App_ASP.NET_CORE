# TODO_App_ASP.NET_CORE
O aplicatie web simple folosind care foloseste ASP.NET Core

2.1.2017 - La multi ani ! <br>
29.12.2016 - am adaugat Polymer (https://www.polymer-project.org/1.0/) <br>
4.12.2016 - creare si vizualizare todo-uri insa mai trebuie lucrat la design


Cum pornesc acesta aplicatie ?<br>
 1.Click pe butonul Clone or Download si se alege Download ZIP<br>
 2.In folderul src se deschide Command Prompt si se introduc comenzile:<br>
    2.1 dotnet ef dbcontext list - se afiseaza contextul de baza de date. Ne intereseaza TODO_APP1.Models.TODO_AppContext(este contextul            de baza de date curent al aplicatiei)<br>
    2.2 dotnet ef migrations add "testForGitHub" -c TODO_APP1.Models.TODO_AppContext.<br>
    2.3 dotnet ef database update. Comanda aplica migratia curenta asupra bazei de date. Optional se veriifca daca in baza de date sunt 3         tabele:<br> Users, TODO si UsersTodo.<br>
 3 Se deschide aplicatia si se apasa butonul "Start From Here". <br>
  
Intrucat bd nu are nimic, se creaza un nou user. Dupa acest lucru noul utilizator se autentifica si creaza un todo<br>
Aplicatia este inca in dezvoltare. In tabelul UsersTodos campurile trebuie completate manual

EX:
UserId=1<br>
TodoId=1<br>
--------------<br>
UserId=1<br>
TodoId=2<br>
etc<br>
Acest lucru va determina ca aplicatia sa afiseze doar todo-urile utilizatorului cu id-ul 1
