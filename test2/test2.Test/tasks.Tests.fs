module tasks.Tests.fs
open System.Threading
open System.Threading.Tasks
open NUnit.Framework
open FsUnit
open tasks

[<Test>]
let ``Super map of 2 functions should return correct list``() =
    superMap [1.0; 2.0; 3.0] (fun x -> [sin x; cos x]) |> should equal [sin 1.0; cos 1.0; sin 2.0; cos 2.0; sin 3.0; cos 3.0]
[<Test>]
let ``Super map of 1 function should return correct list``() =
    superMap [1.0; 2.0; 3.0] (fun x -> [x + 1.0]) |> should equal [2.0; 3.0; 4.0]
    
[<Test>]
let ``Super map of empty function should return empty list``() =
    superMap [1.0; 2.0; 3.0] (fun _ -> []) |> should equal []
    
[<Test>]
let ``Super map of string list should return correct list``() =
    superMap ["aa"; "bb"] (fun x -> [x + "d"; x + "e"]) |> should equal ["aad"; "aae"; "bbd"; "bbe"]
    
[<Test>]
let ``Filter should return correct value for not empty tree``() =
    let tree =
        Node(5,
            Node(3,
                Node(1, Empty, Empty),
                Node(4, Empty, Empty)),
            Node(8,
                Node(6, Empty, Empty),
                Node(10, Empty, Empty)))

    let isEven x = x % 2 = 0

    filterTree isEven tree |> should equal [4; 8; 6; 10]
    
[<Test>]
let ``Filter for an empty tree should return empty list``() =
    let tree = Empty
    let isEven x = x % 2 = 0
    
    filterTree isEven tree |> should equal []
    
[<Test>]
let ``Adding and deleting the item to the queue should return an empty queue``() =
    let blockingQueue = BlockingQueue<int>()
    let customer = 
        async {
                blockingQueue.Enqueue(1)
                blockingQueue.Dequeue() |> ignore
                do! Async.Sleep(500)
            }

    Async.Start customer

    Thread.Sleep(1000)
    
    Assert.AreEqual(blockingQueue.IsEmpty, true)
    
[<Test>]
let ``Dequeue from empty queue should wait until enqueue``() =
    let queue = BlockingQueue<int>()
    let itemToEnqueue = 1
    let mutable itemFromQueue = 0
    let dequeueTask = Task.Run(fun () ->
        itemFromQueue <- queue.Dequeue()
    )
    
    Thread.Sleep(1000)
    
    Assert.AreEqual(0, itemFromQueue)
   
    let enqueueTask = Task.Run(fun () ->
        queue.Enqueue(itemToEnqueue)
    )

    Task.WaitAll([| dequeueTask; enqueueTask |])

    Assert.AreEqual(itemToEnqueue, itemFromQueue)