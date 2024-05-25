module Program
open System
open System.IO
open PhoneHandbook
let greetMessage = "Hi, it's a dictionary. That's what I can do:
    0. Exit.
    1. Add the record (name and phone).
    2. Find the phone by name.
    3. Find the name by phone.
    4. Print out the dictionary.
    5. Save the data in the file.
    6. Read the data from the file."
    
let handbook = []
Console.WriteLine(greetMessage)    
let addUserRecord () =
    printfn "Enter name:"
    let name = Console.ReadLine()
    printfn "Enter phone:"
    let phone = Console.ReadLine()
    let newRecord = { Name = name; Phone = phone }
    let newHandbook = addRecord handbook newRecord
    printfn "Record added successfully."
let findPhoneByName () =
    printfn "Enter name:"
    let name = Console.ReadLine()
    let phones = findPhoneByName name handbook
    phones |> List.iter (printfn "Phone: %s")

let findNameByPhone () =
    printfn "Enter phone:"
    let phone = Console.ReadLine()
    let names = findNameByPhone phone handbook
    names |> List.iter (printfn "Name: %s")

let printHandbook () =
    printfn $"%s{printHandbook handbook}"

let saveToFile () =
    printfn "Enter file name:"
    let fileName = Console.ReadLine()
    match writeToFile handbook fileName with
    | None -> failwith "Writing to the file error"

let readFromFile () =
    printfn "Enter file name:"
    let fileName = Console.ReadLine()
    let newHandbook = readFromFile fileName
    printfn "Data read from file successfully." 

let parseUserInput userInput =
    match userInput with
    | "0" -> exit 0
    | "1" -> addUserRecord()
    | "2" -> findPhoneByName()
    | "3" -> findNameByPhone()
    | "4" -> printHandbook()
    | "5" -> saveToFile()
    | "6" -> readFromFile()
    | _ -> printfn "Invalid option. Please select a valid option."



