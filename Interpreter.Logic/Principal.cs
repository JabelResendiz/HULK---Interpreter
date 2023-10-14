using System.Reflection.Emit;
using InterpreterDyZ;


    //Dictionary<string,AST>functional= new Dictionary<string, AST>();
    public class Principal{
        
        Lexer lexer;
        Parser parser;
        string Text;
        Interpreter interpreter;
        public static Dictionary<string,AST> Functiones= new Dictionary<string, AST>();
        public Principal(){
            
           
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
                            Console.WriteLine(".Try again please.");
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

        
     
        }
    }