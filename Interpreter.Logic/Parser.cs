using System.Reflection;
using System.Reflection.Emit;
namespace InterpreterDyZ;

public class Parser
{
    private Lexer Lexer;
    private Token CurrentToken;
    //ReservateKeywords reserved;

    //public Declarations declarations;

    // constructor de la clase Parser
    public Parser(Lexer lexer)
    {
        Lexer = lexer;
        CurrentToken = lexer.GetNextToken();// CurrentToken es un objeto Token
        //reserved= new ReservateKeywords();
        //declarations = new Declarations(null);
    }
    #region SyntaxError
    public void SyntaxError(string error)
    {
        Console.WriteLine("! SYNATX ERROR: " +error);
        throw new Exception();
        //Environment.Exit(1);
    }

    #endregion

    #region Parse
  // metodo que creara el node final del bloque del codigo 
  // se crea una instancia de AST que sera el arbol
    public AST Parse()
    {
       
        AST node = StatementList();

        if (CurrentToken.Type != TokenTypes.EOF)
            
            SyntaxError("Expected token EOF");

        return node;
    }
    #endregion

    
    // mostrara el token en pantalla y lee el siguiente token de la cadena
    // realcionado con el metodo GetNextToken() de la clase Lexer
    private void Process(TokenTypes type, string msg)
    {
        //Console.WriteLine($"{CurrentToken.Show()} {type}");

        if(CurrentToken.Type == type)

            CurrentToken = Lexer.GetNextToken();
        
        else

            SyntaxError($"Unexpected token {CurrentToken.Value}.Missing "+ $" {msg}");
    }

    #region Gramatica (Compounds,Comparer,Expression, Termine,Exponenciation,Factor)

        
// Gramatica para BooleanOperator (and , not , or)
    private AST Compounds()
    {
        AST node = Comparer();
        Token token = new Token(CurrentToken);
        while (CurrentToken.Type == TokenTypes.AND || CurrentToken.Type == TokenTypes.OR
        || CurrentToken.Type==TokenTypes.NOT
            )
        {
            token = new Token(CurrentToken);
            if  (token.Type == TokenTypes.AND)
            
                Process(TokenTypes.AND,"and");

            else if(token.Type == TokenTypes.OR)
            
                Process(TokenTypes.OR,"or");
            
            else if (token.Type == TokenTypes.NOT)

                {
                    Process(TokenTypes.NOT,"not");
                    node=new UnaryOperator(token,Comparer());
                    continue;
                }
                
            node = new BinaryOperator(node, token, Comparer());
            
            
        }
        
        return node;
    }
// metodo para identificar operadores 
// es un auxiliar de Comparer()
    private bool IsBooleanOperator()
    {
        Token type = CurrentToken;

        return type.Type == TokenTypes.SAME || type.Type == TokenTypes.DIFFERENT ||
                type.Type == TokenTypes.LESS || type.Type == TokenTypes.GREATER ||
                type.Type == TokenTypes.LESS_EQUAL || type.Type == TokenTypes.GREATER_EQUAL;
                
    }

// Gramatica para Comparadores 
    private AST Comparer()
    {
        AST node = Expression();
        Token token = new Token(CurrentToken);

        while (IsBooleanOperator())
        {
            token = new Token(CurrentToken);

            if (token.Type == TokenTypes.SAME)
                
                Process(TokenTypes.SAME,"same");
            
            else if (token.Type == TokenTypes.DIFFERENT)
                
                Process(TokenTypes.DIFFERENT,"different");
            
            else if (token.Type == TokenTypes.LESS)
                
                Process(TokenTypes.LESS,"less");
            
            else if (token.Type == TokenTypes.LESS_EQUAL)
                
                Process(TokenTypes.LESS_EQUAL,"less equal");
            
            else if (token.Type == TokenTypes.GREATER)
                
                Process(TokenTypes.GREATER,"greater");
            
            else if (token.Type == TokenTypes.GREATER_EQUAL)
                
                Process(TokenTypes.GREATER_EQUAL,"greater equal");
            
            

            node = new BinaryOperator(node, token, Expression());
        }


        return node;
    }

// Gramatica para Expresion(incluye suma,resta y concatenacion)
    private AST Expression()
    {
        AST node = Termine();
        Token token = new Token(CurrentToken);

        while (CurrentToken.Type==TokenTypes.AT || CurrentToken.Type == TokenTypes.PLUS || CurrentToken.Type == TokenTypes.MINUS)
        {
            token = new Token(CurrentToken);// esta innecesario

            if (token.Type == TokenTypes.PLUS)
                
                Process(TokenTypes.PLUS,"plus");
            
            else if (token.Type == TokenTypes.MINUS)
                
                Process(TokenTypes.MINUS,"minus");

            else if(token.Type== TokenTypes.AT)
                Process(TokenTypes.AT,"at");
        
            node = new BinaryOperator(node, token, Termine());
        }


        return node;
    }

    // Gramatica de Termine(incluye multiplicacion,division y modulo)
     private AST Termine()
    {
        AST node = Exponentiation();
        Token token = new Token(CurrentToken);
        
        while (CurrentToken.Type == TokenTypes.MULT || CurrentToken.Type == TokenTypes.FLOAT_DIV || CurrentToken.Type == TokenTypes.MOD)
        {
            token = new Token(CurrentToken);

            if (token.Type == TokenTypes.MULT)
                
                Process(TokenTypes.MULT,"mult");
            
            else if (token.Type == TokenTypes.INTEGER_DIV)
                
                Process(TokenTypes.INTEGER_DIV,"div");
            
            else if (token.Type == TokenTypes.FLOAT_DIV)
                
                Process(TokenTypes.FLOAT_DIV,"div");
            
            else if (token.Type == TokenTypes.MOD)
                
                Process(TokenTypes.MOD,"mod");

            node = new BinaryOperator(node, token, Exponentiation());
        }


        return node;
    }

    // reconocer el simbolos de potencia
    private AST Exponentiation(){
        AST node = Factor();
        Token token= new Token(CurrentToken);

        while(CurrentToken.Type== TokenTypes.POW){
            //token= new Token(CurrentToken);
            Process(TokenTypes.POW,"Pow");
            node= new BinaryOperator(node,token,Factor());
        }

        return node;
    }
    //metodo para reconocer simbolos terminales
    private AST Factor()
    {
        AST node = new AST();
        Token token = new Token(CurrentToken);
        if((ReservateKeywords.Keyword.ContainsValue((TokenTypes)CurrentToken.Type) && (CurrentToken.Type!=TokenTypes.PI) )||
            token.Type is TokenTypes.CALL || (CurrentToken.Type == TokenTypes.ID && Lexer.SeeNextTokenEgual())){
            node = Statement();
            return node;
        }

        switch (token.Type)
        {   
            
            case TokenTypes.ID:

                Process(TokenTypes.ID,"ID");
                
                node = new Var(token);

                break;
            
            case TokenTypes.PI:

                Process(TokenTypes.PI,"PI");
                node=new Var(token);
                break;

            case TokenTypes.PLUS:

                Process(TokenTypes.PLUS,"Plus");
                
                node = new UnaryOperator(token, Factor());

                break;

            case TokenTypes.MINUS:

                Process(TokenTypes.MINUS,"Minus");
                
                node = new UnaryOperator(token, Factor());
            
                break;

            case TokenTypes.NUMBER:

                Process(TokenTypes.NUMBER,"number");
                
                node = new Num(token);
            
                break;
            
            
            case TokenTypes.BOOLEAN:

                Process(TokenTypes.BOOLEAN,"boolean");
                
                node = new Bool(token);
            
                break;
            
            case TokenTypes.STRING:

                Process(TokenTypes.STRING,"string");
                
                node = new Cadene(token);
            
                break;

            case TokenTypes.TRUE:

                Process(TokenTypes.TRUE,"true");
                
                node = new Bool(new Token(TokenTypes.TRUE, true));
            
                break;

            case TokenTypes.FALSE:

                Process(TokenTypes.FALSE,"false");
                
                node = new Bool(new Token(TokenTypes.FALSE, false));
            
                break;
            
            case TokenTypes.L_PARENT:

                Process(TokenTypes.L_PARENT,"L_Parent");
                
                node = Compounds();
                
                Process(TokenTypes.R_PARENT,$" in col {Lexer.Pos}.");

                break;
        }
       
        return node;
    }


#endregion
  
    #region StatementList and Statement (mayor rango)

    // AST que contendra las listas de instrucciones de todo tipo
    private AST StatementList()
    {
        Instructions instructions = new Instructions();// se ha creado una lista Commands de AST
        instructions.Commands.Add(Compounds());
        
        if (CurrentToken.Type != TokenTypes.SEMI)
        {
            //if(CurrentToken.Type == TokenTypes.EOF);
            //Process(TokenTypes.SEMI);// se buscara el proximo token despues del punto y coma
            instructions.Commands.Add(Compounds());// se agregara a la lista
            
        }
      
        Process(TokenTypes.SEMI,$"semicolon. Token \" ; \" must end each line. Col {Lexer.Pos}");
        return instructions;// se retornara despues de finalizado el trabajo
    }

    // identifica la instruccion de cada linea
     private AST Statement()
    {
        AST node = new AST();

        if ( CurrentToken.Type==TokenTypes.LET)// comprueba si el token actual es un let

            node = Declaration();

        else if (CurrentToken.Type == TokenTypes.ID && Lexer.SeeNextTokenEgual())//&& Lexer.GetNextToken().Type== TokenTypes.ASSIGN)

            
            node= Assignment();
            
        
        else if (CurrentToken.Type == TokenTypes.IF)

            node = Conditional();
        
        //else if (CurrentToken.Type == TokenTypes.WHILE)

            //node = Cicle();
        
        else if (CurrentToken.Type== TokenTypes.PRINT)

            node= PRINT();

        else if(CurrentToken.Type==TokenTypes.FUNCTION)

            node= Function();

        else if(CurrentToken.Type==TokenTypes.CALL)

            node= CallFunction();
        
        else if(CurrentToken.Type == TokenTypes.SEN)
            node = Sen();

        else if(CurrentToken.Type == TokenTypes.COS)
            node = Cos();
        
        else if(CurrentToken.Type == TokenTypes.LOG)
            node= Log();

        // estas palabras reservadas no devuelven un valor por tanto no inician una instruccion
        else if(CurrentToken.Type== TokenTypes.IN || CurrentToken.Type== TokenTypes.ELSE ||
        CurrentToken.Type== TokenTypes.TRUE || CurrentToken.Type== TokenTypes.FALSE ||
        CurrentToken.Type== TokenTypes.RETURN )
           
            SyntaxError($"\"{CurrentToken.Value}\" Invalid Token. Col {Lexer.Pos- CurrentToken.Value.ToString().Length}");
            
        else

            node=Compounds();

        return node;
    }
#endregion


    #region Metodos para funciones de las KeyWords
    
    // En cada metodo se creara una instancia de AST

    // Metodo para funcion seno 
    private AST Sen(){
        AST node = new AST();
        
        Process(TokenTypes.SEN,"Sen");
        Process(TokenTypes.L_PARENT,$" open parenthesis before \"sen\" function args .Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        node = new Sen(Compounds());
        Process(TokenTypes.R_PARENT,$" closed parenthesis after \"sen\" function args .Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        return node;
    }
    // MEtodo para funcion coseno
    private AST Cos(){
        AST node = new AST();
        
        Process(TokenTypes.COS,"Cos");
        Process(TokenTypes.L_PARENT,$" open parenthesis before \"cos\" function args. Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        node = new Cos(Compounds());
        Process(TokenTypes.R_PARENT,$" closed parenthesis after \"cos\" function args . Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        return node;
    }

    // Metodo para funcion logaritmo (puede tomar hasta dos argumentos y al menos uno)
    // Si toma dos , el primero es la base del logaritmo y el segundo es el argumento
    // Si toma solo uno, pues el argumento, entendiendo que la base es E 
    private AST Log(){
        AST node = new AST();

        Process(TokenTypes.LOG,"LOG");
        Process(TokenTypes.L_PARENT,$"open parenthesis before \"log\" function args .Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        AST bases = Compounds();
        // si el usuario quiere trabajar con el logaritmo natural
        if(CurrentToken.Type==TokenTypes.COMMA){
            Process(TokenTypes.COMMA,$"comma separating args");
            AST Statement= Compounds();
            node= new LOG(bases,Statement);
        }
        else{
            node= new LOG(bases);
        }
        Process(TokenTypes.R_PARENT,$"closed parenthesis after \"log\" function args .Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        return node;
    }
    
    // MEtodo para cuando se reconoce un llamado de funcion
    private AST CallFunction(){
        AST node = new AST();
        Token token= CurrentToken;
        Process(TokenTypes.CALL,"Call");
        Process(TokenTypes.L_PARENT,$"open parenthesis before \"{token.Value}\" function args .Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        // esta es la lista de argumentos que tiene la funcion
        List<AST> arguments= new List<AST>();
        AST tree= Compounds();
        // Se agrega cada argumento
        arguments.Add(tree);
        
        while (CurrentToken.Type == TokenTypes.COMMA)
        {
            Process(TokenTypes.COMMA,$"Function args must be separated by commas.Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
           
            AST tree2= Compounds();
            arguments.Add(tree2);
            
        }
        node = new CallFUNCTION(token,arguments);
        Process(TokenTypes.R_PARENT,$"closed parenthesis after \"{token.Value}\" function args .Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        return node ;
    }
    
    // metodo para cuando se reconoce la declaracion de function
    private AST Function(){
        AST node= new AST();
        Dictionary<string,object> arguments= new Dictionary<string, object>();
        Process(TokenTypes.FUNCTION,"Function");
        Token token= CurrentToken;
        //var g= (string)token.Value;
        Process(TokenTypes.ID,$" function name after its declaration (Surely there is a function with the same name). Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        //ReservateKeywords.Keyword.Add((string)token.Value,TokenTypes.CALL);
        Principal.Functiones.Add((string)token.Value,node);
        Process(TokenTypes.L_PARENT,$"open parenthesis before \"{token.Value}\" function args .Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        arguments.Add((string)CurrentToken.Value,0);
        Process(TokenTypes.ID,$"variable name.Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        // no es obligado el uso de arguemtnos
        while (CurrentToken.Type == TokenTypes.COMMA)
        {
            Process(TokenTypes.COMMA,"Function args must be separated by commas");
            //var j= new Var(CurrentToken);
             arguments.Add((string)CurrentToken.Value,0);
            Process(TokenTypes.ID,$"variable name.Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        }

        Process(TokenTypes.R_PARENT,$"closed parenthesis after \"{token.Value}\" function args .Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        Process(TokenTypes.RETURN,$" return before function body.Col {Lexer.Pos- CurrentToken.Value.ToString().Length}");
        
        node = new FUNCTIONAL(token,arguments,Compounds());

        return node;
    }
    
    // metodo para la funcion print
    private AST PRINT(){
        AST node = new AST();
        Process(TokenTypes.PRINT,"Print");
        Process(TokenTypes.L_PARENT,$"open parenthesis before \"print\" function arg .Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        //ReservateKeywords reserved= new ReservateKeywords();
        node= new PRINT(Compounds());
        Process(TokenTypes.R_PARENT,$"closed parenthesis after \"print\" function args .Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");

        return node;
    }
    //Metodo para declaraciones de variables
    private AST Declaration()
    {
        
        List<AST> node= new List<AST>();
        TokenTypes type = (TokenTypes)(TypeData());

        var f= new Var(CurrentToken);
        Process(TokenTypes.ID,$"variable name.Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        node.Add(new VarDecl(f,type,AssignmentDecl()));
        
        // el bucle es para leer varias declaraciones de variables
        while (CurrentToken.Type == TokenTypes.COMMA)
        {
            Process(TokenTypes.COMMA,"Comma");
            var j= new Var(CurrentToken);
            Process(TokenTypes.ID,$"variable name");
            
            node.Add(new VarDecl(j, type,AssignmentDecl()));

        }

        Process(TokenTypes.IN,$"\"in\" keyword in expression let-in");
        Declarations arbol = new Declarations(Compounds(),node);
        return arbol;
    }
// metodo para la lectura de variables
    private AST Variable()
    {
        AST node = new Var(CurrentToken);
        //Process(TokenTypes.ID);

        return node;
    }

// metodo para la asignacion de variables
    private AST AssignmentDecl()
    {
        
        AST node = Variable();
        Token token = new Token(CurrentToken);

        Process(TokenTypes.ASSIGN,$" \"equal\" sign to declare variable. Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");

        return new Assign((Var)node, token, Compounds());
    }
    private AST Assignment(){
        AST node = Variable();
        Token token = new Token(CurrentToken);
        Process(TokenTypes.ID,$"variable name .Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        if(CurrentToken.Type == TokenTypes.ASSIGN)
            Process(TokenTypes.ASSIGN,$" \"equal\" sign to declare variable. Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        else if(CurrentToken.Type ==TokenTypes.ASSIGN_DIV || CurrentToken.Type ==TokenTypes.ASSIGN_PLUS || 
        CurrentToken.Type ==TokenTypes.ASSIGN_MUL || CurrentToken.Type ==TokenTypes.ASSIGN_MINUS || CurrentToken.Type ==TokenTypes.ASSIGN_MOD)
        { 
            
            if(CurrentToken.Type==TokenTypes.ASSIGN_DIV){
                Process((TokenTypes)CurrentToken.Type,$" \" equal\" sign to declare variable. Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
                AST sumando=new BinaryOperator(node,new Token(TokenTypes.FLOAT_DIV,"/"),Compounds());
                return new Assign((Var)node,token,sumando);
            }
            
            if(CurrentToken.Type==TokenTypes.ASSIGN_MUL){
                Process((TokenTypes)CurrentToken.Type,$" \" equal\" sign to declare variable. Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
                AST sumando=new BinaryOperator((Var)node,new Token(TokenTypes.MULT,"*"),Compounds());
                return new Assign((Var)node,token,sumando);
            }
            
            if(CurrentToken.Type==TokenTypes.ASSIGN_PLUS){
                Process((TokenTypes)CurrentToken.Type,$" \" equal\" sign to declare variable. Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
                AST sumando=new BinaryOperator((Var)node,new Token(TokenTypes.PLUS,"+"),Compounds());
                return new Assign((Var)node,token,sumando);
            }

            if(CurrentToken.Type==TokenTypes.ASSIGN_MINUS){
                Process((TokenTypes)CurrentToken.Type,$" \" equal\" sign to declare variable. Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
                AST sumando=new BinaryOperator((Var)node,new Token(TokenTypes.MINUS,"-"),Compounds());
                return new Assign((Var)node,token,sumando);
            }
            
            if(CurrentToken.Type==TokenTypes.ASSIGN_MOD){
                Process((TokenTypes)CurrentToken.Type,$" \" equal\" sign to declare variable. Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
                AST sumando=new BinaryOperator((Var)node,new Token(TokenTypes.MOD,"%"),Compounds());
                return new Assign((Var)node,token,sumando);
            }
            
            
        }
        
        
        return new Assign((Var)node, token, Compounds());
    }
// metodo para condicionales 
    private AST Conditional()
    {
        Process(TokenTypes.IF,"IF" );
        Process(TokenTypes.L_PARENT,$"open parenthesis before \"if\" function args .Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");

        AST node = Compounds();

        Process(TokenTypes.R_PARENT,$"closed parenthesis after \"if\" function args .Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
        //Process(TokenTypes.L_KEYS);
        if(CurrentToken.Type==TokenTypes.RETURN){
            Process(TokenTypes.RETURN,"Return");
        }
        AST node2= Compounds();

        Process(TokenTypes.ELSE,$" token \" else \" in conditional expression. Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");

        if(CurrentToken.Type==TokenTypes.RETURN){
            Process(TokenTypes.RETURN,"Return ");
        }
        node= new Condition(node,node2,Compounds());
        
        //Process(TokenTypes.R_KEYS);

        return node;
    }

// metodo para ciclos (en HULK no es necesario)
  /*  private AST Cicle()
    {
        Process(TokenTypes.WHILE);
        Process(TokenTypes.L_PARENT);

        AST node = Statement();

        Process(TokenTypes.R_PARENT);
        Process(TokenTypes.L_KEYS);
        
        node = new Cicle(node, StatementList());
        
        Process(TokenTypes.R_KEYS);

        return node;
    }
*/
    


   
// metodo para buscar datos sencillos tales como num,string,bool
    private TokenTypes TypeData()
    {
        TokenTypes token = TokenTypes.NUMBER;// por defecto

        if (CurrentToken.Type == TokenTypes.LET)
        {
            Process(TokenTypes.LET,$"let keyword in expression let-in. Col {Lexer.Pos-CurrentToken.Value.ToString().Length}");
            token = TokenTypes.LET;
        }
        return token;
    }
#endregion

}