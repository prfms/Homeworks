module StringCalculation

type StringCalcBuilder() =
    member this.Bind(x : string, f) =
        match System.Int32.TryParse(x) with
        | true, value -> Some value
        | false, _ -> None
        |> f
    member this.Return(x : string) : int option =
        match System.Int32.TryParse(x) with
        | true, value -> Some value
        | false, _ -> None
        
let calculateFlow = StringCalcBuilder()
    
       