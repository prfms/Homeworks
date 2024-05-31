module Homework3.Tests.LambdaTest
open Homework3.Lambda
open NUnit.Framework
open FsUnit
[<Test>]
let ``(lambda(y).y x) z -> z x``() = 
    let inputExpr = Application(Abstraction("y", Application(Variable "y", Variable "x")), Variable "z")
    let reducedExpr = betaReduce inputExpr
    let correctExpr = Application(Variable "z", Variable "x")
    Assert.AreEqual(correctExpr, reducedExpr )
