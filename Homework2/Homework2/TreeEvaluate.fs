module TreeEvaluate

type treeExpression =
    | Number of float
    | Add of treeExpression * treeExpression
    | Multiply of treeExpression * treeExpression
    | Subtract of treeExpression * treeExpression
    | Divide of treeExpression * treeExpression
    
let rec evaluateTree tree =
    match tree with
    | Number n -> n
    | Add(x,y) -> evaluateTree x + evaluateTree y
    | Subtract(x,y) -> evaluateTree x - evaluateTree y
    | Multiply(x,y) -> evaluateTree x * evaluateTree y
    | Divide(x,y) -> try
                         evaluateTree x / evaluateTree y
                     with
                         | :? System.DivideByZeroException -> "Division by zero"; infinity
                    
    