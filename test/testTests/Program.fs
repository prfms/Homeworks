module testTests.fs
open NUnit.Framework
open FsUnit
open tasks

[<Test>]
let ``Sum of first less than 10 even fibonacci should return ``() =
    let correct = 10
    let original = fibonacciSum 10
    Assert.AreEqual(correct, original)
    
[<Test>]    
let ``Sum of first less than 34 even fibonacci should return ``() =
    let correct = 44
    let original = fibonacciSum 34
    Assert.AreEqual(correct, original)
    
[<Test>]
let ``Print square with side length 3``() =

    let expectedOutput =
        "***\r\n" +
        "* *\r\n" +
        "***\r\n"

    use sw = new System.IO.StringWriter()
    System.Console.SetOut(sw)
    printSquare 3
    let actualOutput = sw.ToString()
    Assert.AreEqual(expectedOutput, actualOutput)
    
[<Test>]
let ``Print square with side length 1``() =

    let expectedOutput =
        "*" 
    use sw = new System.IO.StringWriter()
    System.Console.SetOut(sw)
    printSquare 1
    let actualOutput = sw.ToString()
    Assert.AreEqual(expectedOutput, actualOutput)
    
[<Test>]
let ``Print square with side length 2``() =

    let expectedOutput =
        "**\r\n" +
        "**\r\n" 
        
    use sw = new System.IO.StringWriter()
    System.Console.SetOut(sw)
    printSquare 2
    let actualOutput = sw.ToString()

    Assert.AreEqual(expectedOutput, actualOutput)