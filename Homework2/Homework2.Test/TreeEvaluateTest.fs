module Homework2.Test.TreeEvaluateTest
open TreeEvaluate
open NUnit.Framework
open FsUnit

[<Test>]
let ``2 + 2 * 3 should equal 8`` () =
    let expTree = BinaryOperation(Number 2., "+", BinaryOperation(Number 2., "*", Number 3.))
    let result = evaluateTree expTree
    Assert.AreEqual(result, 8.)
    
[<Test>]
let ``2 + 3\0 should return exception`` () =
    let expTree = BinaryOperation(Number 2., "+", BinaryOperation(Number 3., "/", Number 0.))
    let result = evaluateTree expTree
    Assert.AreEqual(result, infinity)