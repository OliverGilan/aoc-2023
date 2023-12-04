module One
  open System
  open System.Text.RegularExpressions


  // Part 1
  let readLines filePath = System.IO.File.ReadLines(filePath) |> Array.ofSeq
  let calibrationDocument path = readLines path
  let takeFirstLast seq: int = (Seq.head(seq) * 10) + Seq.last seq
  let parseCalibrationValue s = s |> Seq.filter Char.IsDigit |> Seq.map Char.GetNumericValue |> Seq.map int |> takeFirstLast
  let calculateCalibration calibrationDocument = calibrationDocument |> Seq.fold (fun acc line -> acc + parseCalibrationValue line) 0

  // Part 2
  let digitPattern = Regex("one|two|three|four|five|six|seven|eight|nine|zero")
  let replaceNumbers (str: string) = 
    let matchReplace (m: Match) = 
        match m.Value with
        | "one" -> "1"
        | "two" -> "2"
        | "three" -> "3"
        | "four" -> "4"
        | "five" -> "5"
        | "six" -> "6"
        | "seven" -> "7"
        | "eight" -> "8"
        | "nine" -> "9"
        | "zero" -> "0"
        | _ -> ""
    if digitPattern.IsMatch(str) then
      let firstMatch = digitPattern.Matches(str) |> Seq.head
      str.Replace(firstMatch.Value, (matchReplace firstMatch))
    else
      str
  let containsDigit (str: string) = str |> Seq.exists Char.IsDigit
  let replaceForward acc c = 
    match containsDigit(acc + string c) with
      | false -> replaceNumbers(acc + string c)
      | true -> acc + string c
  let replaceBackward c acc =
    match containsDigit(string c + acc) with
      | false -> replaceNumbers(string c + acc)
      | true -> string c + acc
  let normalizedLine str = Seq.foldBack replaceBackward (Seq.fold replaceForward "" str) ""
  let normalizedDocument document = document |> Seq.map normalizedLine
