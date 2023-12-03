module AoC2023
  open One
  open Two

  [<EntryPoint>]
  let main args = 
    let document = normalizedDocument (calibrationDocument "./inputs/1b.txt")
    let calibration = calculateCalibration document
    printfn "Calibration: %d" calibration
    let games = readLines "./inputs/2a.txt" |> Seq.map parseCubeGame
    0
