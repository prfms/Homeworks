module RoundWorkflowTest
open NUnit.Framework
open FsUnit
open RoundWorkflow

[<Test>]
let ``Expression should be calculated with given precision``() =
    let roundFlow = RoundBuilder(3)

    let roundCalculate = roundFlow {
        let! a = 2.0 / 12.0
        let! b = 3.5
        return a / b
    } 
    
    let expectedValue = 0.048
    
    let tolerance = 0.001

    roundCalculate |> should (equalWithin tolerance) expectedValue
    
[<Test>]
let ``Single value should be rounded correctly``() =
    let roundFlow = RoundBuilder(2)
    let roundCalculate = roundFlow {
        let! a = 2.1234
        return a
    }
    
    let expectedValue = 2.12
    
    let tolerance = 0.001

    roundCalculate |> should (equalWithin tolerance) expectedValue

[<Test>]
let ``Expression with zero precision should round to integer``() =
    let roundFlow = RoundBuilder(0)
    let roundCalculate = roundFlow {
        let! a = 1.7
        let! b = 2.8
        return a + b
    }
  
    let expectedValue = 5.0
    
    let tolerance = 0.0

    roundCalculate |> should (equalWithin tolerance) expectedValue    
    