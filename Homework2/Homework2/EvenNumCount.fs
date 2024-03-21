module EvenNumCount
let countEvenNumMap ls =
    ls |> List.map (fun x -> if x % 2 = 0 then 1 else 0) |> List.sum

let countEvenNumFilter ls =
    ls |> List.filter (fun x -> x % 2 = 0) |> List.length
    
let countEvenNumFold ls =
    (0, ls) ||> List.fold (fun x y -> if y % 2 = 0 then (x + 1) else x)
