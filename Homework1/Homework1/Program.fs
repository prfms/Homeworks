let factorial x =
    let rec factorialAcc acc x= 
        if x = 1 then acc
        else factorialAcc (acc * x) (x - 1)
    factorialAcc 1 x
    
let fibonacci n =
    let rec fibCounter x y i n =
        if n = 0 then 0
        elif n = 1 then 1
        elif i = n then x + y
        else fibCounter y (x + y) (i + 1) n      
    fibCounter 0 1 2 n
    
let reverse list =
    let rec reverseAcc list acc = 
        match list with
        | h :: t -> reverseAcc t (h :: acc)
        | [] -> acc
    reverseAcc list []
    
let generatePowers n m =
    let rec generatePowersAcc m pow acc = 
        if m = -1 then acc
        else generatePowersAcc (m - 1) (pow * 2.0) (acc @ [pow])
    generatePowersAcc m (2.0 ** n) []
    
let findElement ls value = 
    let rec numPosition ls i = 
        match ls with
        | h :: t -> if h = value then Some(i) else numPosition t (i + 1)
        | [] -> None
    numPosition ls 0

