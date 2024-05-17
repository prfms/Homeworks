module tasks
open System.Collections.Generic
open System.Threading

/// <summary>
/// Method to apply function to each element of the list. 
/// </summary>
/// <param name="ls">The list.</param>
/// <param name="func">The function.</param>
let superMap ls func =
    ls |> List.collect func
    
/// <summary>
/// Discriminated Union to implement the binary tree
/// </summary>
type BinaryTree<'a> =
    | Empty
    | Node of 'a * BinaryTree<'a> * BinaryTree<'a>
    
/// <summary>
/// Method to get elements of the bin tree that satisfy the predicate.
/// </summary>
/// <param name="predicate">The filter function.</param>
/// <param name="tree">The binary tree.</param>
let filterTree predicate tree =
    let rec filterTreeHelper predicate tree =
        match tree with
        | Empty -> []
        | Node (value, left, right) ->
            let leftFiltered = filterTreeHelper predicate left
            let rightFiltered = filterTreeHelper predicate right
            if predicate value then
                value :: (leftFiltered @ rightFiltered)
            else
                leftFiltered @ rightFiltered
    filterTreeHelper predicate tree

/// <summary>
/// Class representing blocking queue
/// </summary>
type BlockingQueue<'T>() =
    let queue = Queue<'T>()
    let lockObj = obj()
    
    /// <summary>
    /// Method to add item to queue
    /// </summary>
    member this.Enqueue(item: 'T) =
        lock lockObj (fun () ->
            queue.Enqueue(item)
            Monitor.Pulse(lockObj))  
    
    /// <summary>
    /// Method to delete the item from the queue.
    /// </summary>
    member this.Dequeue() : 'T =
        lock lockObj (fun () ->
            while queue.Count = 0 do
                Monitor.Wait(lockObj)  
            queue.Dequeue())
    
    /// <summary>
    /// Method to check if the queue is empty.
    /// </summary>
    member this.IsEmpty =
        lock lockObj (fun () ->
            queue.Count = 0)