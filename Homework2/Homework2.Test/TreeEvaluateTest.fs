module Homework2.Test.TreeEvaluateTest
open TreeEvaluate
open NUnit.Framework
open FsUnit

[<Test>]
let ``2 + 2 * 3 should equal 8`` () =
    let expTree = Add(Number 2., Multiply(Number 2., Number 3.))
    let result = evaluateTree expTree
    Assert.AreEqual(result, 8.)
[<Test>]
let ``2 + 3\0 should return exception`` () =
    let expTree = Add(Number 2., Divide(Number 3.,Number 0.))
    let result = evaluateTree expTree
    Assert.AreEqual(result, infinity)
    