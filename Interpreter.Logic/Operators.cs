using System.Linq.Expressions;
namespace InterpreterDyZ;


// Abstract syntac tree class 
public class AST
{
   /* private Dictionary<string,object>map;
    public void AccederDiccionario(Dictionary<string,object>Scope)
    {
        map= Interpreter.Scope;
        Interpreter.Scope= Scope;
    }
    */
}

public class BinaryOperator : AST
{
    public AST Left, Right;
    public Token Operator;

    public BinaryOperator(AST left, Token op, AST right)
    {
        Left = left;
        Operator = op;
        Right = right;
    }
}

public class UnaryOperator : AST
{
    public Token Operator;
    public AST Expression;

    public UnaryOperator(Token op, AST expr)
    {
        Operator = op;
        Expression = expr;
    }
}

public class Num : AST
{
    public Token Token;
    public object Value;


    public Num(Token token)
    {
        Token = token;
        Value = token.Value;
    }
}

public class Bool : AST
{
    public Token Token;
    public bool Value;

    public Bool(Token token)
    {
        Token = token;
        Value = (bool)token.Value;
    }
}

public class Cadene : AST
{
    public Token Token;
    public string Value;

    public Cadene(Token token)
    {
        Token = token;
        Value = (string)token.Value;
    }
}

public class Type : AST
{
    public Token Token;
    public object Value;

    public Type(Token token)
    {
        Token = token;
        Value = token.Value;
    }
}

public class Instructions : AST
{
    public List<AST>? Commands;

    public Instructions()
    {
        Commands = new List<AST>();
    }
}

public class Declarations : AST
{
    public List<AST>? Commands;
    public AST? instruccion;
    public Dictionary<string,object> Scope;
    public Declarations(AST ins,List<AST> Commands)
    {
        this.Commands = Commands;
        instruccion=ins;
        Scope= new Dictionary<string, object>();
    }

    // una propiedad para llenar el Scope local de cada Declaration
}

public class Assign : AST
{
    public Var Left;
    public Token Operator;
    public AST Right;

    public Assign(Var left, Token op, AST right)
    {
        Left = left;
        Operator = op;
        Right = right;
    }
}

public class Var : AST
{
    public Token Token;
    public object Value; 

    public Var(Token token)
    {
        Token = token;
        Value = token.Value;
    }
}

public class VarDecl : AST
{
    public Var Node;
    public TokenTypes Type;
    public AST Value;
    //public Dictionary<string,object>Scope;

    public VarDecl(Var node, TokenTypes type, AST value)
    {
        Node = node;
        Type = type;
        Value=value;
        //Scope = new Dictionary<string, object>();
    }

    
}

public class Empty : AST
{
    
}

public class Condition : AST
{
    public AST Compound;
    public AST StatementList;
    public AST StatementElse;

    public Condition(AST compound, AST statements,AST statement2)
    {
        Compound = compound;
        StatementList = statements;
        StatementElse=statement2;
    }
}

public class PRINT : AST{
    public AST Compound;// expresion que constituye una nueva lista de instrucciones
    public PRINT(AST compound){
        Compound=compound;
        
    }
}


public class FUNCTIONAL:AST
{
    public string name;
    public Dictionary<string,object>argumentos;
    public AST Statement;
    //public List<AST>arg;

    public FUNCTIONAL(Token names,Dictionary<string,object>argumentos,AST Statement){
        name=(string)names.Value;
        this.argumentos=argumentos;
        this.Statement=Statement;
    }

    
   /* public FUNCTIONAL(Token names,List<AST>arg){
        name=(string)names.Value;
        this.arg=arg;
    }
    */
}
public class CallFUNCTION:AST
{
    public string name;
    public List<AST>arg;
    public CallFUNCTION(Token names,List<AST>arg){
        name= (string)names.Value;
        this.arg=arg;
    }
    
    
    
}
public class Sen:AST
{
    public AST Statement;

    public Sen(AST Statement){
        this.Statement=Statement;
    }
}
public class Cos:AST
{
    public AST Statement;

    public Cos(AST Statement){
        this.Statement=Statement;
    }
}

public class LOG:AST{

    public AST? bases;
    public AST? Statement;
    public LOG(AST bases,AST Statement){
        this.bases=bases;
        this.Statement=Statement;
    }

    public LOG(AST Statement){
        this.Statement=Statement;
    }
}
/*
public class CallFUNCTION:FUNCTIONAL
{
    public string name;
    public List<object>arg;
    public CallFUNCTION(Token names,List<object>arg):base(names,null,null){
        //name=(string)names.Value;
        this.arg=arg;
        this.Statement=Statement;
        int i=0;
     /*   foreach(KeyValuePair<string,object> item in argumentos)
        {
            argumentos[item.Key]=arg[i];
            i+=1;
        }
    
    }

}
*/
public class Cicle : AST
{
    public AST Compound;// expresion a evaluar
    public AST StatementList;// expresion que constituye una nueva lista de instrucciones

    public Cicle(AST compound, AST statements)
    {
        Compound = compound;
        StatementList = statements;
    }
}
