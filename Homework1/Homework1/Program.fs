let factorial x =
    let rec factorialAcc acc x= 
        if x = 1 then acc
        else factorialAcc (acc * x) (x - 1)
    factorialAcc 1 x
    
let fib n =
    let rec fibCounter x y i n =
        if n = 0 then 0
        elif n = 1 then 1
        elif i = n then x + y
        else fibCounter y (x + y) (i + 1) n      
    fibCounter 0 1 2 n
    
let reverse list =
    let rec reverse_acc list acc = 
        match list with
        | h :: t -> reverse_acc t (h::acc)
        | [] -> acc
    reverse_acc list []
    
let generatePowers n m =
    let rec generatePowersAcc m pow acc = 
        if m = -1.0 then acc
        else generatePowersAcc (m - 1.0) (pow * 2.0) (acc @ [pow])
    generatePowersAcc m (2.0 ** n) []
    
let findElement ls value = 
    let rec numPosition ls i = 
        match ls with
        | h :: t when h = value -> i
        | h :: t when h <> value -> numPosition t (i + 1)
        | [] -> -1
    numPosition ls 0

