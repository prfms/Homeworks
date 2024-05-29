module Homework4.tests.BracketChecker_Test

open Homework4.BracketChecker
open NUnit.Framework
open FsUnit

[<Test>]
let ``Simple two brackets should be correct``() =
    let string = "()"
    bracketChecker string |> should equal true

[<Test>]
let ``String with other symbols should be correct``() =
    let string = "{[{(a)}]}"
    bracketChecker string |> should equal true
    
[<Test>]
let ``String without brackets should be correct``() =
    let string = "aaa"
    bracketChecker string |> should equal true
    
[<Test>]
let ``String with incorrect brackets should be incorrect``() =
    let string = "([{]})"
    bracketChecker string |> should equal false

[<Test>]
let ``Empty string should be correct``() =
    let string = ""
    bracketChecker string |> should equal true
    
[<Test>]
let ``String with one opening bracket should be incorrect``() =
    let string = "("
    bracketChecker string |> should equal false
    
[<Test>]
let ``String with one closing bracket should be incorrect``() =
    let string = ")"
    bracketChecker string |> should equal false
    
    