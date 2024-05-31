module Homework2.Test.TreeMapTest
open TreeMap
open NUnit.Framework
open FsUnit
[<Test>]
let ``MapForTree on empty tree should return empty tree`` () =
    let originalTree = Tip
    let transformedTree = mapForTree (fun x -> x + 1) originalTree
    Assert.AreEqual(Tip, transformedTree)
[<Test>]
let ``MapForTree on not empty tree should return function to all nodes`` () =
    let originalTree =
            Node(1,
                Node(2, Tip, Tip),
                Node(3,
                    Node(4, Tip, Tip),
                    Tip))
    let expectedTree =
        Node(2,
            Node(3, Tip, Tip),
            Node(4,
                Node(5, Tip, Tip),
                Tip))
    let transformedTree = mapForTree (fun x -> x + 1) originalTree
    Assert.AreEqual(expectedTree, transformedTree)