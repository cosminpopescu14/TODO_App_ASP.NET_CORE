# TODO_App_ASP.NET_CORE
O aplicatie web simple folosind care foloseste ASP.NET Core

2.1.2017 - La multi ani ! <br>
29.12.2016 - am adaugat Polymer (https://www.polymer-project.org/1.0/) <br>
4.12.2016 - creare si vizualizare todo-uri insa mai trebuie lucrat la design


Cum pornesc acesta aplicatie ?<br>
 1.Click pe butonul Clone or Download si se alege Download ZIP<br>
 2.In folderul src se deschide Command Prompt si se introduc comenzile:
    2.1 dotnet ef dbcontext list - se afiseaza contextul de baza de date. Ne intereseaza TODO_APP1.Models.TODO_AppContext(este contextul            de baza de date curent al aplicatiei)
    2.2 dotnet ef migrations add "testForGitHub" -c TODO_APP1.Models.TODO_AppContext.
    2.3 dotnet ef database update. Comanda aplica migratia curenta asupra bazei de date. Optional se veriifca daca in baza de date sunt 3         tabele:<br> Users, TODO si UsersTodo.
 3 Se deschide aplicatia si se apasa butonul "Start From Here". 
  
