module TreeParser

type Expression =
    | Number of int
    | Add of Expression * Expression
    | Multiply of Expression * Expression
    | Subtract of Expression * Expression
    | Divide of Expression * Expression
    
type Tree =
    | Tip
    | Node of Expression * Tree * Tree

let rec evaluateTree