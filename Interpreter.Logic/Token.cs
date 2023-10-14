namespace InterpreterDyZ;

public class Token
{
    public TokenTypes? Type;
    public object Value; 

    public Token(TokenTypes type, object value)
    {
        this.Type = type;
        this.Value = value;
    }

    public Token(Token other)
    {
        Type = other.Type;
        Value = other.Value;
    }


    public string Show()
    {
        return $"Token({Type}, {Value})";
    }
}

public static class ReservateKeywords
{
    public static List<(string,TokenTypes)>tuplas= new List<(string, TokenTypes)>{
        ("let",TokenTypes.LET),// bien 
        ("in",TokenTypes.IN),// no
        ("if", TokenTypes.IF),// si
        ("else",TokenTypes.ELSE),//no
        ("True", TokenTypes.TRUE),//no
        ("False", TokenTypes.FALSE),//no
        ("while", TokenTypes.WHILE),//no
        ("print", TokenTypes.PRINT),//si
        ("return", TokenTypes.RETURN),//no
        ("function",TokenTypes.FUNCTION),//no
        ("sen",TokenTypes.SEN),//si
        ("cos",TokenTypes.COS),//si
        ("tan",TokenTypes.TAN),//si
        ("log",TokenTypes.LOG),//si
        ("PI", TokenTypes.PI)//si
    };
    public static Dictionary<string, TokenTypes> Keyword{get;set;}= tuplas.ToDictionary(t=>t.Item1,t=>t.Item2);
    
    //public Dictionary<string, AST> Function;

/*
    public ReservateKeywords()
    {
        Keyword = new Dictionary<string, TokenTypes>();
        //Function = new Dictionary<string, AST>();

        //Keyword.Add("main", TokenTypes.MAIN);
        Keyword.Add("let",TokenTypes.LET);
        Keyword.Add("in",TokenTypes.IN);
        //Keyword.Add("int", TokenTypes.INTEGER);
        //Keyword.Add("float", TokenTypes.FLOAT);
        //Keyword.Add("bool", TokenTypes.BOOLEAN);
        //Keyword.Add("string", TokenTypes.STRING);
        Keyword.Add("if", TokenTypes.IF);
        Keyword.Add("else",TokenTypes.ELSE);
        Keyword.Add("True", TokenTypes.TRUE);
        Keyword.Add("False", TokenTypes.FALSE);
        Keyword.Add("while", TokenTypes.WHILE);
        Keyword.Add("print", TokenTypes.PRINT);
        Keyword.Add("return", TokenTypes.RETURN);
        Keyword.Add("function",TokenTypes.FUNCTION);
    }
    */
}