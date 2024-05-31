module Homework4.tests.PointFree_Test

open Homework4.PointFree
open NUnit.Framework
open FsUnit
open FsCheck

[<Test>]
let ``PointFree function should be equal actual``() =
    let equality x l = func'4 x l = func x l
    Check.Quick equality
