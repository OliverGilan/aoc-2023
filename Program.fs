module AoC2023
  open One
  open Two
  open Three
  open Four

  [<EntryPoint>]
  let main args = 
    let document = normalizedDocument (calibrationDocument "./inputs/1b.txt")
    let calibration = calculateCalibration document
    printfn "Calibration: %d" calibration
    let games = readLines "./inputs/2a.txt" |> Seq.map parseCubeGame
    let sumOfGames = sumValidGames games
    printfn "Sum valid games: %d" sumOfGames
    printfn "Sum min set power: %d" (minSetPowerSum games)
    let schematic = parseSchematic "./inputs/3a.txt"
    let sumOfParts = sumParts(schematic, (adjacencyMatrix schematic isSymbol))
    printfn "Sum of all parts: %d" sumOfParts
    let sumOfGearWeights = sumGearWeights (schematic, (adjacencyMatrix2 schematic), (weightMatrix schematic))
    printfn "Sum of gear weights: %d" sumOfGearWeights
    let cardPoints = sumPoints (parseCards "./inputs/4a.txt")
    printfn "Card points: %d" cardPoints
    let totalCards = sumTotalCardsCollected (parseCards "./inputs/4a.txt")
    printfn "Total Cards collected: %d" totalCards
    0
