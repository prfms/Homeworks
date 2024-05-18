module PhoneHandbook
open System.IO

type PhoneRecord =
    {
        Name: string
        Phone: string
    }
    
let addRecord handbook record =
    record :: handbook

let findPhoneByName name handbook =
    handbook |> List.filter (fun x -> x.Name = name) |> List.map _.Phone
    
let findNameByPhone phone handbook =
    handbook |> List.filter (fun x -> x.Phone = phone) |> List.map _.Name
    
let printHandbook handbook =
    handbook |> Seq.map (fun record -> $"Name: %s{record.Name}, Phone: %s{record.Phone}")
        |> String.concat "\n"

let writeToFile handbook fileName =
    try
        let records = handbook |> List.map (fun record -> $"%s{record.Name} %s{record.Phone}")
        File.WriteAllLines(fileName, records)
        Some ()
    with
    | :? IOException -> None
     
let readFromFile fileName =
    try
        let handbook =
            File.ReadLines(fileName)
            |> Seq.fold (fun handbook record -> 
                let parts = record.Split [|' '|]
                match parts with
                | [| name; phone |] -> addRecord handbook { Name = name; Phone = phone }
                | _ -> failwith "Invalid record format"
            ) []
        Some handbook
    with
    | :? IOException -> None