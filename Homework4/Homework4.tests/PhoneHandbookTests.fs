module PhoneHandbookTest.fs
open PhoneHandbook
open NUnit.Framework
open FsUnit
    
exception FileError of string

[<Test>]
let ``Add record to an empty handbook and find it by name and phone should return this record's data.``() =
    let record =  { Name = "Anna"; Phone = "8928" }
    let handbook = addRecord [] record
    let findPhone = findPhoneByName "Anna" handbook
    let findName = findNameByPhone "8928" handbook
    Assert.AreEqual(findPhone[0], "8928")
    Assert.AreEqual(findName[0], "Anna")
    
[<Test>]
let ``Add records with same names and different phones and find them by names``() =
    let record1 =  { Name = "Anna"; Phone = "8928" }
    let record2 =  { Name = "Anna"; Phone = "0000" }
    let handbook = addRecord [] record1
    let handbook = addRecord handbook record2
    let findPhone = findPhoneByName "Anna" handbook
    Assert.AreEqual(findPhone[0], "0000")
    Assert.AreEqual(findPhone[1], "8928")
    
[<Test>]
let ``Write handbook to file and then read it back should return the same data``() =
    let fileName = "C:\\Users\\nasty\\RiderProjects\\Homeworks\\Homework4\\Homework4\\test.txt"
    let originalHandbook = [{ Name = "Anna"; Phone = "8928" }; { Name = "Nick"; Phone = "000" }]
    match writeToFile originalHandbook fileName with
    | None -> raise (FileError("Writing to file error."))
    | Some () ->
        match readFromFile fileName with
        | None -> raise (FileError("Reading from file error."))
        | Some (readHandbook: PhoneRecord list) -> originalHandbook |> should equal readHandbook
            