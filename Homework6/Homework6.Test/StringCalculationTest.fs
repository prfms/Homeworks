module StringCalculationTest

open NUnit.Framework
open FsUnit
open StringCalculation

[<Test>]
let ``Result of two strings representing int numbers should return int result``() =
    let calculateFlow = StringCalcBuilder()
    let result = calculateFlow {
            let! x = "1"
            let! y = "5"
            let z = x + y
            return z
    }    
    result |> should equal (Some 6)

[<Test>]
let ``Result of int and string should return None``() =
    let calculateFlow = StringCalcBuilder()
    let result = calculateFlow {
            let! x = "1"
            let! y = "a"
            let z = x + y
            return z
        }    
    result |> should equal None
    
[<Test>]
let ``Result of three strings representing int numbers should return int result``() =
    let calculateFlow = StringCalcBuilder()
    let result = calculateFlow {
            let! x = "1"
            let! y = "2"
            let! z = "3"
            let w = x + y + z
            return w
    }    
    result |> should equal (Some 6)