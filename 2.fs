module Two
  open System
  let redCount = 12
  let greenCount = 13
  let blueCount = 14

  type SetColors = int * int * int
  type CubeGame = {
    id: int
    maxColors: SetColors
  }

  let isValidGame (game: CubeGame) = 
    let (red, green, blue) = game.maxColors
    red <= redCount && green <= greenCount && blue <= blueCount

  let extractNum (str: string) = str |> Seq.filter Char.IsDigit |> String.Concat |> int
  let extractColorCount (set: string) = 
    set.Split(",") |> Seq.fold (fun (r, g, b) (c: string) -> 
      if c.Contains("red") then
        (extractNum c, g, b)
      elif c.Contains("green") then
        (r, extractNum c, b)
      elif c.Contains("blue") then
        (r, g, extractNum c)
      else
        (r, g, b)
    ) (0, 0, 0)

  let foldMaxColors (red, green, blue) (set: string) =
    let (r, g, b) = extractColorCount set
    (Math.Max(red, r), Math.Max(green, g), Math.Max(blue, b))

  let parseCubeGame (game: string) : CubeGame = 
    let parseMaxSet (gameSets: string) = gameSets.Split(";") |> Array.fold foldMaxColors (0, 0, 0)
    {
      id = extractNum(game.Split(":")[0])
      maxColors = parseMaxSet(game.Split(":")[1])
    }
  
  let sumValidGames games = games |> Seq.filter isValidGame |> Seq.sumBy (fun g -> g.id)

  // Part 2
  let setPower (r, g, b) = r * g * b
  let minSetPowerSum (games: CubeGame seq) = games |> Seq.map (fun g -> g.maxColors) |> Seq.map setPower |> Seq.sum
