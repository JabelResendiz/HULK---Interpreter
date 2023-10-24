using static System.Console;
namespace InterpreterDyZ;

    public class Game
{
    public void Start()
    {
        MainMenu();
        
        //WriteLine("Press any key to exit");

        ReadKey(true);
    }
    
    // metodo para definir el primer menu
    private void MainMenu()
    {
        ForegroundColor= ConsoleColor.Green;

        string prompt =@"
                       ██░ ██        █    ██        ██▓           ██ ▄█▀
                      ▓██░ ██▒       ██  ▓██▒      ▓██▒           ██▄█▒ 
                      ▒██▀▀██░      ▓██  ▒██░      ▒██░          ▓███▄░ 
                      ░▓█ ░██       ▓▓█  ░██░      ▒██░          ▓██ █▄ 
                      ░▓█▒░██▓      ▒▒█████▓       ░██████▒      ▒██▒ █▄
                       ▒ ░░▒░▒      ░▒▓▒ ▒ ▒       ░ ▒░▓  ░      ▒ ▒▒ ▓▒
                       ▒ ░▒░ ░      ░░▒░ ░ ░       ░ ░ ▒  ░      ░ ░▒ ▒░
                       ░  ░░ ░       ░░░ ░ ░         ░ ░         ░ ░░ ░ 
                       ░  ░  ░         ░               ░  ░      ░  ░   
                                                  

Welcome to the HULK lenguage. What would you like to do?
(Use the arrows keys to cycle throught options and press enter to select a option.)";
        string[] options= {"Play","About","Exit"};

        Menu mainMenu= new Menu(prompt,options);

        int SelectedIndex= mainMenu.Run();
        ResetColor();

        switch(SelectedIndex)
        {
            case 0:
                RunFirstChoice();
                break;
            
            case 1:
                DisplayAboutInfo();
                break;
            
            case 2:
                ExitGame();
                break;

            default:
                break;
        }
    }

    // metodo de EXIT que cierra la app 
    private void ExitGame()
    {
        WriteLine("Hope you enjoyed :)");
        WriteLine("Press any key to exit ...");
        
        ReadKey(true);
        Environment.Exit(0);
    }


    // metodo para ABOUT , informaciones generales del autor.
    // se regresa al menu inicial
    private void DisplayAboutInfo()
    {
        Clear();

        WriteLine("HULK LENGUAGE @ AUTHOR: JABEL RESENDIZ AGUIRRE");
        WriteLine("HAVANA UNIVERSITY - MATCOM");
        WriteLine("GITHUB @JabelResendiz");
        
        ReadKey(true);
        MainMenu();
    }

    // opcion de PLAY, se otorga un menu de opciones para color y esta listo para empezar el compilador
    private void RunFirstChoice(){
        
        string prompt= "What color paint would you like to watch dry?";
        string[]options= {"Red","Green","Blue"};
        Menu colorMenu= new Menu(prompt,options);
        int selectedIndex=colorMenu.Run();

        BackgroundColor = ConsoleColor.White;
        switch(selectedIndex)
        {
            case 0:
                ForegroundColor = ConsoleColor.Red;
                break;
            case 1:
                ForegroundColor = ConsoleColor.Green;
                break;
            case 2:
                ForegroundColor = ConsoleColor.Blue;
                break;
        }
        BackgroundColor=ConsoleColor.Black;
        //ResetColor();
        //ExitGame();
    }
}

