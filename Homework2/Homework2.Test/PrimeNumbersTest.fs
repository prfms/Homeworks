module Homework2.Test.PrimeNumbersTest
open PrimeNumbers
open NUnit.Framework
open FsUnit

[<Test>]
let ``Check first 10 prime numbers`` () = 
    Assert.AreEqual( infiniteSeqPrimeNum 2 |> Seq.take 10 |> Seq.toList,  [2; 3; 5; 7; 11; 13; 17; 19; 23; 29])

