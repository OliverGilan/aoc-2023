module OneB =
  open System

  let replaceNumbers (str: string) = str.ToLower().Replace("one", "1").Replace("two", "2").Replace("three", "3").Replace("four", "4").Replace("five", "5").Replace("six", "6").Replace("seven", "7").Replace("eight", "8").Replace("nine", "9").Replace("zero", "0")

  let normalizedLine (str: string) = str |> Seq.fold (fun acc c -> replaceNumbers(acc + string c)) ""
      


