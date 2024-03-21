module Homework2.Test
open NUnit.Framework
open FsCheck
open EvenNumCount
let eqMapAndFilter (ls:list<int>) = countEvenNumMap ls = countEvenNumFilter ls
let eqMapAndFold (ls:list<int>) = countEvenNumMap ls = countEvenNumFold ls
let eqFoldAndFilter (ls:list<int>) = countEvenNumFold ls = countEvenNumFilter ls
[<Test>]
let ``Equality of EvenNumCountMap and EvenNumCountFilter`` () = Check.Quick eqMapAndFilter
[<Test>]
let ``Equality of EvenNumCountMap and EvenNumCountFold`` () = Check.Quick eqMapAndFold
[<Test>]
let ``Equality of EvenNumCountFold and EvenNumCountFilter`` () = Check.Quick eqFoldAndFilter