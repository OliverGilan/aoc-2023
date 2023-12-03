module AoC2023
  open One
  open Two

  [<EntryPoint>]
  let main args = 
    let document = normalizedDocument (calibrationDocument "./inputs/1b.txt")
    let calibration = calculateCalibration document
    printfn "Calibration: %d" calibration
    let games = readLines "./inputs/2a.txt" |> Seq.map parseCubeGame
    let sum = sumValidGames games
    printfn "Sum valid games: %d" sum
    printfn "Sum min set power: %d" (minSetPowerSum games)
    0
