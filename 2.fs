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

  let foldMaxColors (red, green, blue) (c: string) =
    if c.Contains("red") then
      (Math.Max(red, extractNum c), green, blue)
    elif c.Contains("green") then
      (red, Math.Max(green, extractNum c), blue)
    elif c.Contains("blue") then
      (red, green, Math.Max(blue, extractNum c))
    else
      (red, green, blue)

  let parseCubeGame (str: string) : CubeGame = 
    let parseMaxSet (str: string) = str.Split(";") |> Array.fold foldMaxColors (0, 0, 0)
    {
      id = extractNum(str.Split(":")[0])
      maxColors = parseMaxSet(str.Split(":")[1])
    }
  
  let sumValidGames games = games |> Seq.filter isValidGame |> Seq.sumBy (fun g -> g.id)

