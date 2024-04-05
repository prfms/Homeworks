module Homework3.Tests.LambdaTest
open Homework3.Lambda
open NUnit.Framework
open FsUnit

[<Test>]
let ``Free variable of lambda(x).x y is "y"``() =
    let inputExpr = Abstraction("x", Application(Variable "x", Variable "y"))
    let freeVarExpr = freeVar inputExpr
    let correctFreeVar = ["y"]
    Assert.AreEqual(correctFreeVar, freeVarExpr)
[<Test>]
let ``Bound variable of lambda(x).x y is "x"``() =
    let inputExpr = Abstraction("x", Application(Variable "x", Variable "y"))
    let boundVarExpr = boundVar inputExpr
    let correctBoundVar = ["x"]
    Assert.AreEqual(correctBoundVar, boundVarExpr)

[<Test>]
let ``(lambda(y).y x) z -> z x``() = 
    let inputExpr = Application(Abstraction("y", Application(Variable "y", Variable "x")), Variable "z")
    let reducedExpr = betaReduce inputExpr
    let correctExpr = Application(Variable "z", Variable "x")
    Assert.AreEqual(correctExpr, reducedExpr)
[<Test>]
let ``(lambda(y).y x) (lambda(z).z) -> lambda(z).z x``() = 
    let inputExpr = Application(Abstraction("y", Application(Variable "y", Variable "x")), Abstraction("z", Variable "z"))
    let reducedExpr = betaReduce inputExpr
    let correctExpr = Abstraction("z", Application(Variable "z", Variable "x"))
    Assert.AreEqual(correctExpr, reducedExpr)
    
[<Test>]
let ``(lambda(x). x y) y -> *y y``() =
    let inputExpr = Application(Abstraction("x",Application(Variable "x", Variable "y")), Variable "y")
    let reducedExpr = betaReduce inputExpr
    let correctExpr = Application(Variable "*y", Variable "y")
    Assert.AreEqual(correctExpr, reducedExpr)