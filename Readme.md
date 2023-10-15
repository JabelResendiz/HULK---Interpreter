# INTERPRETER OF HULK

## GENERAL
 
Interprete del lenguaje HULK escrito en C# en .NET7.0 , Windows10. Para compilarlo dirigirlo a la carpeta Interpreter.Visual y seguido el comando --dotnet run. 

## GRAMATIC

```
program : statement_list

statement_list : compounds

statement : declarations
          | assignment
          | conditionals
          | functions
          | call_functions  
          | print
          | compounds

declarations : LET + ID (COMMA ID)* + EQUAL + compounds + IN + compounds

assignment : ID + ASSIGN + compounds

conditionals : IF + L_PARENT + compounds + R_PARENT (RETURN)* + compounds + ELSE (RETURN)*+ compounds

functions :FUNCTIONS + ID + L_PARENT + ID (COMMA ID)* + R_PARENT + RETURN + compounds

call_functions:  L_PARENT + compounds (COMMA compounds)* + R_PARENT 

print: PRINT + L_PARENT + compounds + R_PARENT

compounds : comp + ((AND | OR) comp)*

comp : expr + ((SAME | DIFFERENT | LESS | GREATER | LESS_EQUAL | GREATER_EQUAL | NOT ) expr)*

expr : term + ((PLUS | MINUS | AT ) term)*

term : exp + ((MUL | DIV | MOD) exp)*

exp: factor + ((POW) factor)*

factor :statement 
       | PLUS factor
       | MINUS factor
       | NUMBER
       | STRING
       | BOOLEAN
       | PI
       | ID
       | TRUE
       | FALSE
       | L_PARENT compounds R_PARENT


type_data : 
           NUMBER 
          | BOOL 
          | STRING


```

## Operators

- **[OK]** _PLUS (+)_
- **[OK]** _MINUS (-)_
- **[OK]** _MULT (*)_
- **[OK]** _MOD (%)_
- **[OK]** _FLOAT_DIV (/)_
- **[OK]** _ASSIGN (=)_
- **[OK]** _SAME (==)_
- **[OK]** _DIFFERENT (!=)_
- **[OK]** _LESS (<)_
- **[OK]** _GREATER (>)_
- **[OK]** _LESS_EQUAL (<=)_
- **[OK]** _GREATER_EQUAL (>=)_
- **[OK]** _NOT (!)_
- **[OK]** _AND (&)_
- **[OK]** _OR (|)_

## RESERVED KEYWORDS

- **number**
- **string**
- **let**
- **in**
- **True**
- **False**
- **if**
- **print**
- **return**
- **else**
- **function**

## EXAMPLES OF CODE

```
    >let number = 42 in (let text = "The meaning of life is" in (print(text @ number))); 
    >let a = 42 in if (a % 2 == 0) print("Even") else print("odd");
    >function fib(a)=> if(a==1) return 1 else return (fib(a-1)*a);
    >print(23+print(21));
```
