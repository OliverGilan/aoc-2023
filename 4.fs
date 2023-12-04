module Four
  open System
  open One
  open System.Text.RegularExpressions

  let CardRegex = Regex("\s*(\d+)") 
  let parseCards path = readLines path |> Array.map (
    fun line -> (
      line.Split(":")[1]).Split("|") |> Array.map (fun half -> CardRegex.Matches(half) |> Seq.map (fun m -> int m.Value) |> Seq.toArray)
    )

  let cardPoints (winningNumbers: int[], hadNumbers: int[]) = 
    hadNumbers 
    |> Seq.fold (fun acc num -> if Array.contains num winningNumbers then acc + 1 else acc) -1 
    |> float
    |> ( ** ) 2.0
    |> int

  let sumPoints (cards: int[][][]) =
     cards
     |> Seq.fold (fun acc card -> acc + cardPoints(card.[0], card.[1])) 0

  // Part 2