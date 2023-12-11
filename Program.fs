module AoC2023
  open System
  open One
  open Two
  open Three
  open Four
  open Five
  open Six

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

    
    // let almanac = readLines "./inputs/5.txt"
    // let almanacSeeds = parseSeedRanges almanac
    // printfn "seeds parsed"
    // printfn "seeds: %A" almanacSeeds
    // let seedToSoil = parseMap almanac "seed" "soil"
    // printfn "seedtosoil: %A" seedToSoil
    // printfn "Soil: %d" (navigateMap seedToSoil almanacSeeds[0])
    // let soilToFertilizer = parseMap almanac "soil" "fertilizer"
    // printfn "fertilizer: %d" (navigateMap soilToFertilizer (navigateMap seedToSoil almanacSeeds[0]))
    // let fertilizerToWater = parseMap almanac "fertilizer" "water"
    // // printfn "fertilizerToWater: %A" fertilizerToWater
    // printfn "water: %d" (navigateMap fertilizerToWater (navigateMap soilToFertilizer (navigateMap seedToSoil almanacSeeds[0])))
    // let waterToLight = parseMap almanac "water" "light"
    // printfn "light: %d" (navigateMap waterToLight (navigateMap fertilizerToWater (navigateMap soilToFertilizer (navigateMap seedToSoil almanacSeeds[0]))))
    // let lightToTemperature = parseMap almanac "light" "temperature"
    // printfn "temperature: %d" (navigateMap lightToTemperature (navigateMap waterToLight (navigateMap fertilizerToWater (navigateMap soilToFertilizer (navigateMap seedToSoil almanacSeeds[0])))))

    // let temperatureToHumidity = parseMap almanac "temperature" "humidity"
    // printfn "humidity: %d" (navigateMap temperatureToHumidity (navigateMap lightToTemperature (navigateMap waterToLight (navigateMap fertilizerToWater (navigateMap soilToFertilizer (navigateMap seedToSoil almanacSeeds[0]))))))

    // let humidityToLocation = parseMap almanac "humidity" "location"
    // printfn "location: %d" (navigateMap humidityToLocation (navigateMap temperatureToHumidity (navigateMap lightToTemperature (navigateMap waterToLight (navigateMap fertilizerToWater (navigateMap soilToFertilizer (navigateMap seedToSoil almanacSeeds[0])))))))
    // printfn "Almanac stage: %A" [seedToSoil; soilToFertilizer; fertilizerToWater; waterToLight; lightToTemperature; temperatureToHumidity; humidityToLocation]
    // let lowestLocations = navigateAlmanac(almanac, almanacSeeds, stages) |> Array.min
    // printfn "lowest locations: %d" lowestLocations 
    // let [|times; records|] = readLines "./inputs/6.txt" |> Array.map raceVector
    let [|time; record|] = readLines "./inputs/6.txt" |> Array.map singleRace
    // let possibilities = generatePossibilities (time + 1L)
    // printfn "winMultiples: %A" (Array.zip times records)
    // printfn "%d %d" possibilities.Length possibilities[0].Length
    // printfn "%A" possibilities[7]
    // let winMultiples = winPossibilities (time, record, possibilities) |> Array.length
    let winOptions = oneRacePossibility time record
    printfn "winMultiples: %d" winOptions
    0
