module Homework2.Test.EvenNumCountTest.fs
open NUnit.Framework
open FsCheck
open FsUnit
open EvenNumCount

let equalityMapFilterFold (ls: list<int>) = 
    countEvenNumMap ls = countEvenNumFilter ls && countEvenNumFilter ls = countEvenNumFold ls

[<Test>]
let ``Equality of EvenNumCountMap and EvenNumCountFilter`` () = Check.Quick equalityMapFilterFold

[<Test>]
let ``Filter function should return 3 for [2, 1, 0, 4]`` () = 
    Assert.AreEqual(3, countEvenNumFilter [2; 1; 0; 4])
