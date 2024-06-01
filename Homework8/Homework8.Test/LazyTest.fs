module LazyTest
open NUnit.Framework
open FsUnit
open Lazy

let mutable counter = 0

let AllLazyCases =
    [ (fun f -> SingleThreadedLazy f :> ILazy<obj>)
      (fun f -> MultiThreadedLazy f :> ILazy<obj>)
      (fun f -> LockFreeLazy f :> ILazy<obj>) ]
    |> List.map (fun f -> TestCaseData(f))

let MultiThreadedLazyCases =
    [ (fun f -> MultiThreadedLazy f :> ILazy<obj>)
      (fun f -> LockFreeLazy f :> ILazy<obj>) ]
    |> List.map (fun f -> TestCaseData(f))

[<TestCaseSource(nameof(AllLazyCases))>]
let ``Value should be computed only once`` (lazyConstructor: (unit -> obj) -> ILazy<obj>) =
    let supplier () =
        counter <- counter + 1
        obj ()

    counter <- 0

    let lazyObject = lazyConstructor supplier
    let result1 = lazyObject.Get()
    let result2 = lazyObject.Get()
    
    result1 |> should equal result2
    counter |> should equal 1

[<TestCaseSource(nameof(AllLazyCases))>]
let ``Lazy should return the same value on each call`` (lazyConstructor: (unit -> obj) -> ILazy<obj>) =
    let supplier () = obj ()

    let lazyObject = lazyConstructor supplier
    let result1 = lazyObject.Get()
    let result2 = lazyObject.Get()

    result1 |> should equal result2

[<TestCaseSource(nameof(MultiThreadedLazyCases))>]
let ``Lazy should return the same value in each thread`` (lazyConstructor: (unit -> obj) -> ILazy<obj>) =
    let supplier () = obj ()

    let lazyObject = MultiThreadedLazy supplier :> obj ILazy

    Seq.initInfinite (fun _ -> async { return lazyObject.Get() })
    |> Seq.take 8
    |> Async.Parallel
    |> Async.RunSynchronously
    |> Seq.distinct
    |> Seq.length
    |> should equal 1


[<Test>]
let ``Lazy should compute value only once`` () =
    let counter = ref 0

    let supplier () =
        System.Threading.Interlocked.Increment(counter) |> ignore
        obj ()

    let lazyObject = MultiThreadedLazy supplier :> obj ILazy

    Seq.initInfinite (fun _ -> async { return lazyObject.Get() })
    |> Seq.take 12
    |> Async.Parallel
    |> Async.RunSynchronously
    |> Seq.distinct
    |> Seq.length
    |> should equal 1

    counter.Value |> should equal 1