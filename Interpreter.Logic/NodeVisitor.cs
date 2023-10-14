namespace InterpreterDyZ;

public abstract class NodeVisitor // las clases hija pueden modificar sus metodos abstractos
{
    //public NodeVisitor():base(){}
    protected delegate int DelegateMethod(AST node);// metodo protegido

    protected object Visit(AST node,Dictionary<string,object> some)// identifica que clase es
    {
        if(node is CallFUNCTION)
            return VisitCallFunction((CallFUNCTION)node,some);

        if(node is Sen)
            return VisitSen((Sen)node,some);
        
        if(node is Cos)
            return VisitCos((Cos)node,some);
    
        if(node is LOG)
            return VisitLOG((LOG)node,some);
            
        if(node is FUNCTIONAL)
            return VisitFunctional((FUNCTIONAL)node,some);

        if(node is PRINT){
            return VisitShowLine((PRINT)node,some);
        }
        if (node is BinaryOperator)

            return VisitBinaryOperator((BinaryOperator)node,some);
        
        else if (node is UnaryOperator)

            return VisitUnaryOperator((UnaryOperator)node,some);
        
        else if (node is Num)

            return VisitNum((Num)node);
        
        else if (node is Cadene)

            return VisitCadene((Cadene)node);

        else if (node is Instructions)

            return VisitInstructions((Instructions)node,some);

        else if (node is Declarations)

            return VisitDeclarations((Declarations)node,some);

        else if (node is Assign)

            return VisitAssign((Assign)node,some);

        else if (node is Condition)

            return VisitCondition((Condition)node,some);

        else if (node is Cicle)

            return VisitCicle((Cicle)node);

        else if (node is Var)

            return VisitVar((Var)node,some);

        else if (node is VarDecl)

            return VisitVarDecl((VarDecl)node,some);

        else if (node is Bool)

            return VisitBool((Bool)node,some);

        else if (node is Type)

            return VisitType((Type)node);

        else if (node is Empty)

            return VisitEmpty((Empty)node);

        return null;
    }
    public abstract object VisitCallFunction(CallFUNCTION node,Dictionary<string,object>Scope);
    public abstract object VisitFunctional(FUNCTIONAL node,Dictionary<string,object>Scope);
    public abstract object VisitShowLine(PRINT node,Dictionary<string,object>Scope);
    public abstract object VisitBinaryOperator(BinaryOperator node,Dictionary<string,object>Scope);
    public abstract object VisitUnaryOperator(UnaryOperator node,Dictionary<string,object>Scope);
    public abstract object VisitInstructions(Instructions node,Dictionary<string,object>Scope);
    public abstract object VisitDeclarations(Declarations node,Dictionary<string,object>Scope);
    public abstract object VisitAssign(Assign node,Dictionary<string,object>Scope);
    public abstract object VisitCondition(Condition node,Dictionary<string,object>Scope);
    public abstract object VisitCicle(Cicle node);
    public abstract object VisitVar(Var node,Dictionary<string,object>Scope);
    public abstract object VisitVarDecl(VarDecl node,Dictionary<string,object>Scope);
    public abstract object VisitNum(Num node);
    public abstract object VisitBool(Bool node,Dictionary<string,object>Scope);
    public abstract object VisitCadene(Cadene node);
    public abstract object VisitType(Type node);
    public abstract object VisitEmpty(Empty node);
    public abstract object VisitSen(Sen node,Dictionary<string,object>Scope);
    public abstract object VisitCos(Cos node,Dictionary<string,object>Scope);

    public abstract object VisitLOG(LOG node,Dictionary<string,object>Scope);
}