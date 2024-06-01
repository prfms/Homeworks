module Lambda

type LambdaExpression =
    | Variable of string
    | Abstraction of string * LambdaExpression
    | Application of LambdaExpression * LambdaExpression

let boundVar expr =
    let rec boundVarAcc expr acc =
        match expr with
        | Variable _ -> acc
        | Application(func, arg) -> boundVarAcc func acc @ boundVarAcc arg acc
        | Abstraction(param, body) -> boundVarAcc body (param :: acc)
    boundVarAcc expr [] |> List.distinct

let freeVar expr =
    let rec freeVarAcc expr acc =
        match expr with
        | Variable(x) -> x::acc
        | Application(func, arg) -> freeVarAcc func acc @ freeVarAcc arg acc
        | Abstraction(param, body) -> freeVarAcc body acc |> List.filter (fun x -> x <> param)
    freeVarAcc expr [] |> List.distinct

let newVar s t =
    let rec newVarRec s t var =
        if not (List.contains var (freeVar s @ freeVar t)) then
            var
        else
            newVarRec s t (var + "'")
    newVarRec s t "a"

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
    | Abstraction (param, body) when not (List.contains varName (freeVar body)) || not (List.contains param (freeVar subExpr)) ->
        Abstraction(param, substitute body varName subExpr)
    | Abstraction(param, body) ->
        let newParam = newVar body subExpr
        Abstraction(newParam, substitute (substitute body param (Variable(newParam))) varName subExpr)

let rec betaReduce expr =
    match expr with
    | Application (Abstraction(param, body), Application(func, arg)) -> 
        Application ((substitute body param func), arg)
    | Application (Abstraction (param, body), arg) -> 
        betaReduce (substitute body param arg)
    | Abstraction (param, body) -> 
        Abstraction (param, betaReduce body)
    | _ -> expr
   