using System.Reflection.Emit;
using InterpreterDyZ;
using static System.Console;

    //Dictionary<string,AST>functional= new Dictionary<string, AST>();
    public class Principal{
        
        Lexer lexer;
        Parser parser;
        string Text;
        Interpreter interpreter;
        public static Dictionary<string,AST> Functiones= new Dictionary<string, AST>();
        public Principal(){
            
        
           WriteLine("PRESS ENTER OR ESCAPE FOR SOME FUNCTIONALITY");
            while(true)
            {
                
                if(KeyAvailable)
                {   
                    
                    var key= ReadKey(true).Key;

                    if(key==ConsoleKey.Enter)
                    {   

                        Write(">>");
                        Text=ReadLine();
                        try{
                            Method();
                        }
                        catch(StackOverflowException ex){
                            WriteLine("RecursionError: maximum recursion depth exceeded");
                        }
                        catch(Exception ex){
                            var g= ForegroundColor;
                            ForegroundColor=ConsoleColor.Yellow;
                            WriteLine(".Try again please.");
                            ForegroundColor=g;
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

            //Console.ForegroundColor = ConsoleColor.Green;

            interpreter.Interpret();

        
     
        }
    }
    