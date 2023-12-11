module Six
  open System
  open System.Text.RegularExpressions
  let raceVector (str: string) = Regex.Split(str, "\\s+")[1..] |> Array.map int
  let generatePossibilities maxTime =
    Array.init maxTime (fun i -> 
      Array.init maxTime (fun j -> 
        Math.Max (j * (i - j), 0)
      )
    )
  
  let winPossibilities (t, r, allDistances: int[][]) = allDistances[t] |> Array.filter (fun d -> d > r)

  // Part 2
  let singleRace (str: string) = Regex.Split(str, "\\s+")[1..] |> String.concat "" |> int64
  let oneRacePossibility time record =
    let rec walk t s acc =
      match s with
      | 0L -> acc
      | _ -> 
        let winner = if record < (t-s) * s then 1L else 0L
        walk t (s - 1L) (acc + winner)
    walk time time 0

    