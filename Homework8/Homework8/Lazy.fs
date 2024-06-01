module Lazy

open System.Threading

type ILazy<'a> =
    abstract member Get: unit -> 'a
    
type SingleThreadedLazy<'a>(supplier: unit -> 'a) =
    let mutable value = None
    interface ILazy<'a> with
        member this.Get() =
            match value with
            | Some v -> v
            | None ->
                let result = supplier()
                value <- Some result
                result
                
type MultiThreadedLazy<'a>(supplier: unit -> 'a) =
    let mutable value = None
    let lockObj = obj()
    interface ILazy<'a> with
        member this.Get() =
            lock lockObj (fun () ->
                match value with
                | Some v -> v
                | None ->
                    let result = supplier()
                    value <- Some result
                    result
            )
            
type LockFreeLazy<'a>(supplier: unit -> 'a) =
    let mutable value = None
    interface ILazy<'a> with
        member this.Get() : 'a =
            match value with
            | Some v -> v
            | None ->
                let result = supplier ()
                match Interlocked.CompareExchange(&value, Some result, None) with
                | Some v -> v
                | None -> result             