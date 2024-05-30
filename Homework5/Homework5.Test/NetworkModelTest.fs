module NetworkModelTest
open NUnit.Framework
open FsUnit
open Moq
open NetworkModel

let matrix = array2D [ [ false; true; false ]
                       [ true; false; true ]
                       [ false; true; false ] ]

let computers = [ Computer(Windows, 0); Computer(Linux, 1); Computer(MacOS, 2) ]

let network = Network(matrix, computers)

[<Test>]
let ``Virus spreads like BFS when probability is always 1``() =
    let virus = Virus(fun _ -> 1.0)

    let simulator = Simulator(network, virus)

    computers[0].IsInfected <- true

    simulator.Run()

    for computer in computers do
        computer.IsInfected |> should equal true

[<Test>]
let ``No computer gets infected when probability is always 0``() =
    let virus = Virus(fun _ -> 0.0)
    let simulator = Simulator(network, virus)

    computers[0].IsInfected <- true

    simulator.Run()

    computers[0].IsInfected |> should equal true
    for i in 1..computers.Length - 1 do
        computers.[i].IsInfected |> should equal false