# INTERPRETER OF HULK

## GRAMATIC

```
program : import block export

import : IMPORT globals_variables (COMMA globals_variables)* SEMI

export : EXPORT SEMI

block : MAIN L_KEYS statement_list (SEMI statement_list)* R_KEYS

statement_list : statement
               | statement SEMI statement_list

statement : declarations
          | assignment
          | conditionals
          | cicles
          | funtions
          | empty

functions : INTERNAL L_PARENT factor (COMMA factor)* R_PARENT


declarations : type_data ID (COMMA ID)* SEMI

assignment : variable ASSIGN compounds

cicles : WHILE L_PARENT compounds R_PARENT L_KEYS statement_list R_KEYS

conditionals : IF L_PARENT compounds R_PARENT L_KEYS statement_list R_KEYS

compounds : comp ((AND | OR) comp)*
          | L_PARENT compounds R_PARENT

comp : expr ((SAME | DIFFERENT | LESS | GREATER | LESS_EQUAL | GREATER_EQUAL | NOX) expr)*

expr : term ((PLUS | MINUS) term)*

term : factor ((MUL | DIV | MOD) factor)*

factor : PLUS factor
       | MINUS factor
       | INTEGER 
       | FLOAT 
       | STRING
       | TRUE
       | FALSE
       | L_PARENT expr R_PARENT

type_data : INTEGER 
          | FLOAT 
          | BOOL 
          | STRING

globals_variables : states
                  | models

variable : ID

empty : 
```

## Operators

- **[OK]** _PLUS (+)_
- **[OK]** _MINUS (-)_
- **[OK]** _MULT (*)_
- **[OK]** _MOD (%)_
- **[OK]** _FLOAT_DIV (/)_
- **[OK]** _INTEGER_DIV (//)_
- **[OK]** _ASSIGN (=)_
- **[OK]** _SAME (==)_
- **[OK]** _DIFFERENT (!=)_
- **[OK]** _LESS (<)_
- **[OK]** _GREATER (>)_
- **[OK]** _LESS_EQUAL (<=)_
- **[OK]** _GREATER_EQUAL (>=)_
- **[OK]** _NOX (!)_
- **[OK]** _AND (&&)_
- **[OK]** _OR (||)_

## RESERVED KEYWORDS
- **@Internal**
- **export**
- **int**
- **float**
- **string**
- **True**
- **False**
- **main**
- **if**
- **while**
- **ShowLine**
- **return**

## EXAMPLE OF CODE

```
main{

    int a = 15, b = 20, c;

    if ( b > a) {
        c = a;
        a = b;
        b = a;
    }

    while (a % b != 0) {
        int r = a % b;
        a = b;
        b = r;
    }

    [< This is a comment >]

    ShowLine(a + ' ' + b + ' ' + c);
}
```
