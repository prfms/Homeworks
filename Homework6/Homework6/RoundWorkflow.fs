module RoundWorkflow

/// <summary>
/// Type representing calculation with given precision
/// </summary>
type RoundBuilder(precision : int) =
    member this.Bind(x : float, f) =
        let roundedValue = System.Math.Round(x, precision)
        f roundedValue
    member this.Return(x : float) =
        let roundedValue = System.Math.Round(x, precision)
        roundedValue