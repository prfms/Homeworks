module NetworkModel
open System

type OperatingSystem = 
    | Windows 
    | Linux 
    | MacOS

type Computer(os: OperatingSystem, id: int) =
    member val OS = os with get
    member val ID = id with get
    member val IsInfected = false with get, set

type Network(matrix: bool[,], computers: Computer list) =
    member val AdjacencyMatrix = matrix with get
    member val Computers = computers with get

    member this.GetNeighbors (id: int) = 
        [ for i in 0..Array2D.length1 matrix - 1 do
            if matrix[id, i] then yield this.Computers[i] ]

type Virus(probability: OperatingSystem -> float) =
    let rand = Random()
    member this.TryInfect(os: OperatingSystem) =
        rand.NextDouble() <= probability(os)

type Simulator(network: Network, virus: Virus) =
    let mutable step = 0
    let mutable changed = true

    member this.Step() =
        if not changed then
            printfn "No changes in the last step. Simulation ends."
        else
            let newlyInfected = 
                [ for computer in network.Computers do
                    if not computer.IsInfected then
                        let neighbors = network.GetNeighbors computer.ID
                        if neighbors |> List.exists (_.IsInfected) && virus.TryInfect computer.OS then
                            yield computer ]
            if newlyInfected.IsEmpty then
                changed <- false
            else
                newlyInfected |> List.iter (fun c -> c.IsInfected <- true)
                step <- step + 1
                printfn $"Step %d{step}: %A{[ for c in network.Computers -> c.ID, c.IsInfected ]}"

    member this.Run() =
        while changed do
            this.Step()