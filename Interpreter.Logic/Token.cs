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

// palabras reservadas del lenguaje
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
        ("log",TokenTypes.LOG),//si
        ("PI", TokenTypes.PI)//si
    };
    public static Dictionary<string, TokenTypes> Keyword{get;set;}= tuplas.ToDictionary(t=>t.Item1,t=>t.Item2);
    
}