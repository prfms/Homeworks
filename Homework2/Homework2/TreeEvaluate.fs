module TreeEvaluate

type treeExpression =
    | Number of float
    | BinaryOperation of treeExpression * string * treeExpression

let rec evaluateTree tree =
    match tree with
    | Number n -> n
    | BinaryOperation (left, operation, right) ->
        match operation with
        | "+" -> evaluateTree left + evaluateTree right
        | "-" -> evaluateTree left - evaluateTree right
        | "*" -> evaluateTree left * evaluateTree right
        | "/" -> try
                         evaluateTree left / evaluateTree right
                 with
                         | :? System.DivideByZeroException -> "Division by zero"; infinity
        
                    
        | _ -> failwith "Unknown operation"