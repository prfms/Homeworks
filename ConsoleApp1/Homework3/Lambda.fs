module Homework3.Lambda

type LambdaExpression =
    | Variable of string
    | Abstraction of string * LambdaExpression
    | Application of LambdaExpression * LambdaExpression

let boundVar expr =
    let rec boundVarAcc expr acc =
        match expr with
        | Variable(x) -> acc
        | Application(func, arg) -> boundVarAcc func acc @ boundVarAcc arg acc
        | Abstraction(param, body) -> boundVarAcc body (param :: acc)
    boundVarAcc expr [] |> List.distinct    
let freeVar expr =
    let rec freeVarAcc expr acc =
        match expr with
        | Variable(x) -> (x::acc)
        | Application(func, arg) -> freeVarAcc func acc @ freeVarAcc arg acc
        | Abstraction(param, body) -> freeVarAcc body acc |> List.filter (fun x -> x <> param)
    freeVarAcc expr [] |> List.distinct
let rec alphaConvert var newVar expression =
    match expression with
    | Variable(x) when x = var -> Variable(newVar)
    | Abstraction(param, body) when param = var -> Abstraction(newVar, alphaConvert var newVar body)
    | Abstraction(param, body) -> Abstraction(param, alphaConvert var newVar body)
    | Application(func, arg) -> Application(alphaConvert var newVar func, alphaConvert var newVar arg)
    | _ -> expression
let rec substitute expr varName subExpr =
    match expr with
    | Variable name -> if name = varName then subExpr else expr
    | Application (func, arg) -> 
        Application (substitute func varName subExpr, substitute arg varName subExpr)
    | Abstraction (param, body) when param = varName -> expr
    | Abstraction (param, body) when not (freeVar body |> List.exists(fun x -> x = varName)) || not(freeVar subExpr |> List.exists(fun x -> x = param)) ->
        Abstraction(param, substitute body varName subExpr)
    | Abstraction(param, body) ->
        let newParam = "*" + param
        Abstraction(newParam, substitute (alphaConvert param newParam body) varName subExpr)    
       
let rec betaReduce expr =
    match expr with
    | Application (Abstraction (param, body), arg) -> 
        betaReduce (substitute body param arg)
    | Application (func, arg) -> 
        Application(betaReduce func, betaReduce arg)
    | Abstraction (param, body) -> 
        Abstraction(param, betaReduce body)
    | _ -> expr

let rec printLambda (expr: LambdaExpression): string =
    match expr with
    | Variable name -> name
    | Abstraction (param, body) -> 
        sprintf "lambda %s.%s" param (printLambda body)
    | Application (func, arg) -> 
        sprintf "(%s) (%s)" (printLambda func) (printLambda arg)
