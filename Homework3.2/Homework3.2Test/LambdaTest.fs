module LambdaTest
open Lambda
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
    
let I = Abstraction("x", Variable "x")
let K = Abstraction("x", Abstraction("y", Variable "x"))
let S = Abstraction("x", Abstraction("y", Abstraction("z", Application(Application(Variable "x", Variable "z"), Application(Variable "y", Variable "z")))))

let SKK = Application(Application(S, K), K)

[<Test>]
let ``S K K should be equivalent to I``() =
    let reducedExpr = betaReduce SKK
    Assert.AreEqual(I, reducedExpr)
    
[<Test>]
let ``(lambda(y).y x) (lambda(z).z) -> x``() = 
    let inputExpr = Application(Abstraction("y", Application(Variable "y", Variable "x")), Abstraction("z", Variable "z"))
    let reducedExpr = betaReduce inputExpr
    let correctExpr = Variable "x"
    Assert.AreEqual(correctExpr, reducedExpr)
    
[<Test>]
let ``(lambda(x). x y) y -> y y``() =
    let inputExpr = Application(Abstraction("x",Application(Variable "x", Variable "y")), Variable "y")
    let reducedExpr = betaReduce inputExpr
    let correctExpr = Application(Variable "y", Variable "y")
    Assert.AreEqual(correctExpr, reducedExpr)