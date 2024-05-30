module StringCalculation

/// <summary>
/// Type representing string calculation if strings represent int numbers
/// Returns None, otherwise 
/// </summary>
type StringCalcBuilder() =
    member this.Bind(x : string, f) =
        match System.Int32.TryParse(x) with
        | true, value -> f value
        | false, _ -> None        
    member this.Return(x) = Some x
       