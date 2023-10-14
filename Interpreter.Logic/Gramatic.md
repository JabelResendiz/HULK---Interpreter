expr : term ((PLUS | MINUS) term)*

term : factor ((MUL | DIV) factor)*

factor : PLUS factor
       | MINUS factor
       | INTEGER factor
       | L_PARENT expr R_PARENT