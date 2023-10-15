namespace InterpreterDyZ;

public class Interpreter : NodeVisitor
{
    private Parser? Parser;
    public Dictionary<string, object> Scope;// variables declaradas con let (no puede ser global en todo el programa)
    //public Dictionary<string,AST>Function= new Dictionary<string, AST>();

    
    //public ReservateKeywords reserved= new ReservateKeywords();
    //public Principal principal= new Principal();
    public Interpreter(Parser parser)
    {
        Parser = parser;
        Scope = new Dictionary<string, object>();// esto es una variable local
        //Function = new Dictionary<string,AST>();
        
    }

    # region 1 Semantic Error
    private void SemanticError(string error)
    {   
        Console.WriteLine("!SEMANTIC ERROR: "+error);
        throw new Exception();
    }

    #endregion
    
    # region 2 Evaluador de cada nodo del AST
    //evaluador de una funcion logaritmo

    public override object VisitLOG(LOG node, Dictionary<string, object> Scope)
    {
        if(node.bases is null && node.Statement is null){
            SemanticError("The log function takes at least one arg.");
        }
        
        object tree= Visit(node.Statement,Scope);
            if(tree is string || tree is bool){
                SemanticError("The args of logarithm function is an double variable");
            }

            if(Convert.ToSingle(tree)<=0){
                SemanticError("The arg of the logarithm function is less than 0");
        }

        if(!(node.bases is null))
        {   
            object tree2=Visit(node.bases,Scope);
             if(!(tree2 is double)){
                SemanticError("The base of logarithm is an double variable");
            }

            if(Convert.ToSingle(tree2)<=0 || Convert.ToSingle(tree2)==1 ){
                SemanticError("Logarithm to base less 0 or 1 is not defined");
            }

            return Math.Log(Convert.ToSingle(tree))/Math.Log(Convert.ToSingle(tree2));
        }
        return Math.Log(Convert.ToSingle(tree));
    }
    public override object VisitSen(Sen node, Dictionary<string,object> Scope)
    {
        if(node.Statement is null){
            SemanticError("The function \"sen\" take one arg");
        }
        return Math.Sin(Convert.ToSingle(Visit(node.Statement,Scope)));
    }


    // evaluador de una funcion coseno
    public override object VisitCos(Cos node, Dictionary<string, object> Scope)
    {
        if(node.Statement is null){
            SemanticError("The function \"cos\" take one arg");
        }
        //Console.WriteLine(Math.Cos(Convert.ToSingle(Visit(node.Statement,Scope))));
        return Math.Cos(Convert.ToSingle(Visit(node.Statement,Scope)));   
    }
    public override object VisitFunctional(FUNCTIONAL node,Dictionary<string,object>Scope)
    {
        //ReservateKeywords.Keyword.Add($"node.name",TokenTypes.CALL);
        Principal.Functiones[node.name]= node;
        //Principal.Function.Add(node.name}",node);
        return 0;
    }

// metodo evaluador para llamadas de funciones
    public override object VisitCallFunction(CallFUNCTION node,Dictionary<string,object>Scope)
    {
          if(Principal.Functiones.ContainsKey(node.name)){
            int i=0;
            
            Dictionary<string,object> local = new Dictionary<string, object>(((FUNCTIONAL)Principal.Functiones[node.name]).argumentos);
            // se crea un diccionario local para modificar los valores de local sin cambiar los de la variable estatica
            if(local.Count!= node.arg.Count){
                SemanticError($"Function \" {node.name}\" receives {local.Count} argument(s), but {node.arg.Count} were gives");
            }
            
            foreach(KeyValuePair<string,object> item in local)
            {
                local[item.Key]=Visit(node.arg[i],Scope);
                i+=1;
                
            }

            
            object tree= Visit(((FUNCTIONAL)Principal.Functiones[node.name]).Statement,local);
           
            return tree;
        }

        
        return 0;
    }
    
    // metodo evaluador para la funcion print
    public override object VisitShowLine(PRINT node,Dictionary<string,object>Scope)
    {

        object tree=Visit(node.Compound,Scope);
        
        return tree;
    }
    // metodo para trabajar con operador binarios
    public override object VisitBinaryOperator(BinaryOperator node,Dictionary<string,object>Scope)
    {
        object result = 0;// puede ser lo mismo un int,float,bool

        object left = Visit(node.Left,Scope);
        object right = Visit(node.Right,Scope);

        // si los object left and right no son del mismo tipo salta una excepcion
       

        switch (node.Operator.Type)
        {
            case TokenTypes.AT:

                if(left is bool || right is bool){
                    SemanticError($"Operator \" @ \" cannot be used between a \"bool\" ");
                }
                result =((left is string)?(string)left: (Convert.ToDouble(left)).ToString()) + ((right is string)?(string)right: (Convert.ToDouble(right)).ToString());
                break;
            
            case TokenTypes.PLUS:

                if (left is string && right is string)

                    result = (string)left + (string)right;
                else if(left is bool  || right is bool)
                    SemanticError($"Operator \" + \" cannot be used between a \"bool\" ");
                else{
                    if(left is string || right is string){
                        SemanticError($"Operator \" + \" cannot be used between \"{left.GetType()}\" and \"{right.GetType()}\"");
                    }
                    else{
                        result = Convert.ToDouble(left) + Convert.ToDouble(right);
                    }
                }
                break;

            case TokenTypes.MINUS:
                
                if(left is string || left is bool || right is string || right is bool){
                    SemanticError($"Operator \"- \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
                }

                result =Convert.ToDouble(left) - Convert.ToDouble(right);
                
                break;
            
            case TokenTypes.MULT:

                if(left is string || left is bool || right is string || right is bool){
                    SemanticError($"Operator \"* \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
                }

                result =Convert.ToDouble(left) * Convert.ToDouble(right);
                
                break;
            
            case TokenTypes.FLOAT_DIV:

                if(left is string || left is bool || right is string || right is bool){
                    SemanticError($"Operator \"/ \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
                }
                if(Convert.ToDouble(right)==0){
                    SemanticError("La divsion por 0 no esta definida");
                }
                result = Convert.ToDouble(left) / Convert.ToDouble(right);
                
                break;

            
            case TokenTypes.MOD:

            {
                if(left is string || left is bool || right is string || right is bool){
                    SemanticError($"Operator \"% \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
                }

                result = Convert.ToDouble(left)% Convert.ToDouble(right);
                
                break;
            }

            case TokenTypes.POW:
                if(left is string || left is bool || right is string || right is bool){
                    SemanticError($"Operator \"^ \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
                }

                result =Math.Pow(Convert.ToDouble(left),Convert.ToDouble(right));
                
                break;

            case TokenTypes.SAME:
                
                if (left is string && right is string)

                    result = (string)left == (string)right;
                else if(left is bool  && right is bool)
                    result = Convert.ToSingle(left) == Convert.ToSingle (right);
                else{
                    if(left is string || left is bool){
                        SemanticError($"Operator \"== \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
                    }
                    else{
                        result =Convert.ToDouble(left) == Convert.ToDouble(right);
                    }
                }
                break;
            
            case TokenTypes.DIFFERENT:
            
                if (left is string && right is string)

                    result = (string)left != (string)right;
                else if(left is bool  && right is bool)
                    result = Convert.ToSingle(left) != Convert.ToSingle (right);
                else{
                    if(left is string || left is bool){
                        SemanticError($"Operator \"!= \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
                    }
                    else{
                        result = Convert.ToDouble(left) != Convert.ToDouble(right);
                    }
                }
                break;
            
            case TokenTypes.LESS:

                if(left is string || left is bool || right is string || right is bool){
                    SemanticError($"Operator \"< \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
                }
                result = Convert.ToDouble(left) < Convert.ToDouble(right);
                
                break;

            case TokenTypes.GREATER:

                if(left is string || left is bool || right is string || right is bool){
                    SemanticError($"Operator \"> \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
                }
                result = Convert.ToDouble(left) > Convert.ToDouble(right);
                
                break;
            
            case TokenTypes.LESS_EQUAL:

                if(left is string || left is bool || right is string || right is bool){
                    SemanticError($"Operator \"<= \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
                }
                result = Convert.ToDouble(left) <= Convert.ToDouble(right);
                
                break;
            
            case TokenTypes.GREATER_EQUAL:
                if(left is string || left is bool || right is string || right is bool){
                    SemanticError($"Operator \">= \" cannot be used between not \"{left.GetType()}\" and \"{right.GetType()}\"");
                }

                result = Convert.ToDouble(left) >= Convert.ToDouble(right);
                
                break;
            
            case TokenTypes.AND:

                result = (bool)Visit(node.Left,Scope) && (bool)Visit(node.Right,Scope);
                
                break;
            
            case TokenTypes.OR:

                result = (bool)Visit(node.Left,Scope) || (bool)Visit(node.Right,Scope);
                
                break;
            
        }

        return result;
    }


    // metodo para evaluar operadores unarios
    public override object VisitUnaryOperator(UnaryOperator node,Dictionary<string,object>Scope)
    {
        int result = 0;

        object exp= Visit(node.Expression,Scope);

        if(!(exp is double))
        {
            SemanticError($"Unary Operator \"+ \" cannot be used between if not is \"double\"");
        
        }


        switch (node.Operator.Type)
        {
            case TokenTypes.PLUS:

                result = +(int)exp;

                break;
            
            case TokenTypes.MINUS:

                result = -(int)exp;

                break;
        }

        return result;
    }

    // metodo para evaluar cada instruccion del codigo
    public override object VisitInstructions(Instructions node,Dictionary<string,object>Scope)
    {
        foreach (var item in node.Commands)
        {
            object output =Visit(item,Scope);
            Console.WriteLine((output is string)?(string)output: (output is bool)? (bool)output : Convert.ToDouble(output));
            Scope.Clear();

        }

        return 0;
    }

    public override object VisitDeclarations(Declarations node,Dictionary<string,object>Scope)
    {   

        node.Scope= new Dictionary<string, object>(Scope);
        
        foreach (var item in node.Commands)
        {
            Visit(item,node.Scope);
        }
        
        object f = Visit(node.instruccion,node.Scope);
        node.Scope.Clear();
        return (f is string)?(string)f:(f is bool)?(bool)f:Convert.ToSingle(f);
    }
    
    public override object VisitAssign(Assign node,Dictionary<string,object>Scope)
    {
        string name = (string)node.Left.Value;
        if(!Scope.ContainsKey(name)){
            SemanticError($"Varible \"{name}\" was not found");
        }
        Scope[name] = Visit(node.Right,Scope);

        return Scope[name];
    }
    // metodo para evaluar expressiones condicionales
    public override object VisitCondition(Condition node,Dictionary<string,object>Scope)
    {
        object condition= Visit(node.Compound,Scope);

        if(!(condition is bool )){
            SemanticError("A \"boolean\" expression was not detected in the expression conditional");

        }

        if ((bool)Visit(node.Compound,Scope))

            return Visit(node.StatementList,Scope);
        

        return Visit(node.StatementElse,Scope);
    }
    
    // metodo para evaluar expression en ciclo(aunque no es expression para este interprete)
    public override object VisitCicle(Cicle node)
    {
        
        while ((bool)Visit(node.Compound,Scope))
        {
            Visit(node.StatementList,Scope);
        }

        return 0;
    }
    

    // metodo que retorna el valor de una variable local
    public override object VisitVar(Var node,Dictionary<string,object>Scope)
    {
        
        if(node.Token.Type==TokenTypes.PI)
            return Math.PI ;
        
        string name = (string)node.Value;
        object value=null;

        if(Scope.ContainsKey(name))
        {
            value = Scope[name];
        }
        if (value is null)

            SemanticError($"Variable \"{name}\" was not found");
        
        return value;
    }


    // metodo que retorna el valor que contiene una clase Num
    public override object VisitNum(Num node)
    {
        return node.Value;
    }
    

    // metodo que retorna e valor de un bool
    public override object VisitBool(Bool node,Dictionary<string,object>Scope)
    {
        return node.Value;
    }


    // metodo que retorna el valor de una clase Cadene
    public override object VisitCadene(Cadene node)
    {
        return node.Value;
    }

    // metodo que guarda el valor de una variable en Scope local
    public override object VisitVarDecl(VarDecl node,Dictionary<string,object>Scope) 
    {
        
        if(node.Type == TokenTypes.LET)
        {   
            // si la variable ya existia se sobreescribe entonces
            if(Scope.ContainsKey((string)node.Node.Value)){
                Scope.Remove((string)node.Node.Value);
            }
            Scope.Add((string)node.Node.Value,Visit(((Assign)node.Value).Right,Scope));
        }
        return 0; 
    }
    
    public override object VisitType(Type node) { return 0; }

    public override object VisitEmpty(Empty node) { return 0; }

// se va a crear el AST y se va a dar a llenar el diccionario Scope
    public object Interpret()
    {
        AST tree = Parser.Parse();

        if (tree == null)

            return -1;
        
        return Visit(tree,Scope);
    }
    #endregion
}