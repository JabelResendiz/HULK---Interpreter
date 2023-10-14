using System.Reflection.Emit;
using InterpreterDyZ;

/*Puntos importantes 
1.Declara una variable incluye darle valor
2.Se desconoce la naturaleza de la variable hasta analizar su valor
3. Ninguna palabra reservada puede ser usada como nombre
4. El else falta
5. let a = 12, b= 23 in print(a+b);(in actua como un final de la linea)
6. Problemas con reconocer en los print los boolean
7. Usar parentesis para separar varios statement
8. Realizar una funcion que busque el proximo token(principalmente el = )



9. Poner errores en el codigo 
10.Modificar los argumentos de las funciones
11. Codear las funciones seno, coseno y logaritmo
*/

    //Dictionary<string,AST>functional= new Dictionary<string, AST>();
    public class Principal{
        
        Lexer lexer;
        Parser parser;
        string Text;
        Interpreter interpreter;
        public static Dictionary<string,AST> Functiones= new Dictionary<string, AST>();
        public Principal(){
            
           Text="8578^2;";
                    
           Method();

            //Text= "print(sum(3)+10^2-12)";
            //Method();
           Console.WriteLine("PRESIONA ENTER O ESC PARA ALGUNA FUNCION");
            while(true)
            {
                
                if(Console.KeyAvailable)
                {   
                    
                    var key= Console.ReadKey(true).Key;

                    if(key==ConsoleKey.Enter)
                    {   

                        Console.Write(">");
                        Text=Console.ReadLine();
                        try{
                            Method();
                        }
                        catch(Exception ex){
                            Console.WriteLine(". Try again please.");
                        }
                    }

                    else if(key==ConsoleKey.Escape)
                    {
                        break;
                    }
                }
            }
            

        } 
           
        
        public void Method()
        {
    
 
            lexer = new Lexer(Text);
            parser = new Parser(lexer);
            interpreter = new Interpreter(parser);

            Console.ForegroundColor = ConsoleColor.Green;

            interpreter.Interpret();

        
     
            //Console.WriteLine("CHAU");
            /*foreach(KeyValuePair<string,AST> item in Functiones){
                Console.WriteLine(item.Key);
            }
            */
            //Function=interpreter.Function;
        }
    }