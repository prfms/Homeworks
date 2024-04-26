module tasks

let fibonacciSum limit =
    let rec fibonacciSumAcc acc x y =
        if (x + y) > limit then acc
        elif ((x + y) % 2 = 0) then fibonacciSumAcc (acc + x + y) y (x + y) 
        else fibonacciSumAcc acc y (x + y)      
    fibonacciSumAcc 0 0 1

let printSquare n =
    let rec printLine n =
        if n = 0 then printfn ""
        else printf "*"; printLine (n-1)

    let rec printMiddleLine n count =
        let rec printMiddlePart i n =
            if i = 0 || i = n - 1 then printf "*"
            else printf " "
            if i = n - 1 then printfn ""
            else printMiddlePart (i + 1) n
        printMiddlePart 0 n
        if count > 1 then printMiddleLine n (count - 1)
    
    if (n > 2) then 
        printLine n
        printMiddleLine n (n - 2)
        printLine n
    elif n = 1 then printf "*"
    else
        printLine n
        printLine n 
        





