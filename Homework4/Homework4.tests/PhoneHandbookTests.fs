module PhoneHandbookTest.fs
open System.IO
open NUnit.Framework
open FsUnit
    
[<Test>]
let ``Add record to an empty handbook and find it by name and phone should return this record's data.``() =
    let record =  { Name = "Anna"; Phone = "8928" }
    let handbook = addRecord [] record
    let findPhone = findPhoneByName "Anna" handbook
    let findName = findNameByPhone "8928" handbook
    Assert.AreEqual(findPhone[0], "8928")
    Assert.AreEqual(findName[0], "Anna")
    
[<Test>]
let ``Write handbook to file and then read it back should return the same data``() =
    let fileName = "C://Users//nasty//RiderProjects//Homeworks//Homework4//Homework4//test.txt"
    let originalHandbook = [{ Name = "Anna"; Phone = "8928" }; { Name = "Nick"; Phone = "000" }]
    writeToFile originalHandbook fileName
    let readHandbook = readFromFile fileName
    Assert.AreEqual(originalHandbook, readHandbook)
    
