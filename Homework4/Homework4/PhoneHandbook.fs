module PhoneHandbook
open System.IO

type PhoneRecord =
    {
        Name: string
        Phone: string
    }
    
let addRecord handbook record =
    handbook @ [record]

let findPhoneByName name handbook =
    handbook |> List.filter (fun x -> x.Name = name) |> List.map _.Phone
    
let findNameByPhone phone handbook =
    handbook |> List.filter (fun x -> x.Phone = phone) |> List.map _.Name

let printRecord record =
    printfn $"Name: %s{record.Name}, Phone: %s{record.Phone}"
    
let printHandbook handbook =
    printfn "Dictionary includes:\n"
    handbook |> List.map printRecord

let writeToFile handbook fileName =
    try
        let records = handbook |> List.map (fun record -> $"%s{record.Name} %s{record.Phone}")
        File.WriteAllLines(fileName, records)
    with
    | :? IOException as ex -> printfn $"Error writing to file: %s{ex.Message}"

let readFromFile fileName =
    try
        File.ReadLines(fileName)
        |> Seq.fold (fun handbook record -> 
            let parts = record.Split [|' '|]
            match parts with
            | [| name; phone |] -> addRecord handbook { Name = name; Phone = phone }
            | _ -> failwith "Invalid record format"
        ) []
    with
    | :? IOException as ex -> 
        printfn $"Error reading from file: %s{ex.Message}"
        []   
    
    