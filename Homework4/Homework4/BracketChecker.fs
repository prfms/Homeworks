module Homework4.BracketChecker

let bracketChecker input =
    let isOpeningBracket c = c = '(' || c = '[' || c = '{'
    let isClosingBracket c = c = ')' || c = ']' || c = '}'
    let matches opening closing =
        (opening = '(' && closing = ')') ||
        (opening = '[' && closing = ']') ||
        (opening = '{' && closing = '}')

    let rec checkBalance chars stack =
        match chars with
        | [] -> List.isEmpty stack
        | c::cs when isOpeningBracket c -> checkBalance cs (c::stack)
        | c::cs when isClosingBracket c ->
            match stack with
            | [] -> false
            | top::rest ->
                if matches top c then
                    checkBalance cs rest
                else
                    false
        | _::cs -> checkBalance cs stack

    checkBalance (input |> Seq.toList) []
        
            
    