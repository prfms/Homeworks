module Homework3.Lambda

type LambdaExpression =
    | Variable of string
    | Abstraction of string * LambdaExpression
    | Application of LambdaExpression * LambdaExpression

let rec substitute expr varName subExpr =
    match expr with
    | Variable name -> if name = varName then subExpr else expr
    | Abstraction (name, body) -> 
        if name = varName then expr 
        else 
            Abstraction(name, substitute body varName subExpr)
    | Application (func, arg) -> 
        Application(substitute func varName subExpr, substitute arg varName subExpr)

let rec betaReduce expr =
    match expr with
    | Application (Abstraction (param, body), arg) -> 
        let renamedBody = substitute body param arg
        betaReduce renamedBody
    | Application (func, arg) -> 
        let reducedFunc = betaReduce func
        let reducedArg = betaReduce arg
        Application(reducedFunc, reducedArg)
    | Abstraction (param, body) -> 
        let reducedBody = betaReduce body
        Abstraction(param, reducedBody)
    | _ -> expr

let rec printLambda (expr: LambdaExpression): string =
    match expr with
    | Variable name -> name
    | Abstraction (param, body) -> 
        sprintf "lambda %s.%s" param (printLambda body)
    | Application (func, arg) -> 
        sprintf "(%s) (%s)" (printLambda func) (printLambda arg)
