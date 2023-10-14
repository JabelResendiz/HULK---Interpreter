namespace InterpreterDyZ;


// enum de todos los tokens permitidos por la gramatica
public enum TokenTypes {

    #region Types of Datas

    NUMBER,
    BOOLEAN,
    STRING,

    #endregion

    #region Binarys Operators

    PLUS,
    MINUS,
    MULT,
    FLOAT_DIV,
    INTEGER_DIV,
    MOD,
    ASSIGN_PLUS,
    ASSIGN_MINUS,
    ASSIGN_MUL,
    ASSIGN_MOD,
    ASSIGN_DIV,
    POW,

    #endregion

    #region Compare Operators

    SAME,
    DIFFERENT,
    LESS,
    GREATER,
    LESS_EQUAL,
    GREATER_EQUAL,
    NOT,
    AND,
    OR,

    #endregion

    #region Symbols

    L_PARENT,
    R_PARENT,
    AT,

    PI,
    #endregion

    #region Reserved Keywords
    LET,
    IN,
    IF,
    ELSE,
    TRUE,
    FALSE,
    WHILE,
    PRINT,
    RETURN,

    FUNCTION,

    CALL,
    SEN,
    COS,
    TAN,
    LOG,
    
    #endregion

    #region Auxiliars Tokens

    ID,
    COMMA,
    SEMI,
    ASSIGN,
    L_COMMENT,
    R_COMMENT,
    EOF

    #endregion
}