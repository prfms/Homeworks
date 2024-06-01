module LazyTest
open NUnit.Framework
open FsUnit
open System.Threading
open Lazy
let testSupplier () = 
    1

let testSupplierWithCounter (computationCount: byref<int>) () =
    computationCount <- computationCount + 1
    1

let singleThreadedLazySupplier supplier = SingleThreadedLazy<int>(supplier) :> ILazy<int>
let multiThreadedLazySupplier supplier = MultiThreadedLazy<int>(supplier) :> ILazy<int>
let lockFreeLazySupplier supplier = LockFreeLazy<int>(supplier) :> ILazy<int>

let lazyImplementations =
    [
        TestCaseData(singleThreadedLazySupplier).SetName("SingleThreadedLazy")
        TestCaseData(multiThreadedLazySupplier).SetName("MultiThreadedLazy")
        TestCaseData(lockFreeLazySupplier).SetName("LockFreeLazy")
    ]

[<TestCaseSource("lazyImplementations")>]
let ``Lazy implementation should return correct value`` (createLazyInstance: (unit -> int) -> ILazy<int>) =
    let lazyValue = createLazyInstance testSupplier
    let result1 = lazyValue.Get()
    let result2 = lazyValue.Get()
    Assert.AreEqual(1, result1)
    Assert.AreEqual(1, result2)

[<TestCaseSource("lazyImplementations")>]
let ``Lazy implementation should compute value only once`` (createLazyInstance: (unit -> obj) -> ILazy<obj>) =
    let counter = ref 0
    let supplier = fun _ -> Interlocked.Increment(counter) |> ignore
                            obj()
    let lazyValue = createLazyInstance supplier
    let result1 = lazyValue.Get()
    let result2 = lazyValue.Get()
    result1 |> should equal result2
    counter |> should equal 1


[<TestCaseSource("lazyImplementations")>]
let ``Lazy implementation should be thread-safe`` (createLazyInstance: (unit -> 'a) -> ILazy<'a>) =
    let mutable computationCount = 0
    let supplier = 
        fun () -> 
            computationCount <- computationCount + 1
            Thread.Sleep(100)
            1
    let lazyValue = createLazyInstance supplier
    let results = 
        [for _ in 1..10 -> async { return lazyValue.Get() }]
        |> Async.Parallel
        |> Async.RunSynchronously
    Assert.AreEqual(results |> Array.forall (fun v -> v = 1), true)
    Assert.AreEqual(1, computationCount)