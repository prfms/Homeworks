module PrimeNumbers
let isPrime n =
    let rec check i =
        i > n / 2 || (n % i <> 0 && check (i + 1))
    check 2
let infiniteSeqPrimeNum n =
    (+) n |> Seq.initInfinite |> Seq.filter isPrime 
