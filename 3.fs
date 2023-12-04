module Three
  open System
  open One
  let parseSchematic path = readLines path |> Seq.map (fun line -> Seq.toArray line) |> Seq.toArray
  let isSymbol c = c <> '.' && not(Char.IsDigit(c))
  let adjacencyMatrix schematic isNode =
    let width = schematic |> Seq.head |> Array.length
    let height = schematic |> Array.length
    let adj = Array.init width (fun _ -> Array.init height (fun _ -> false))
    for row in 0 .. height - 1 do
      for col in 0 .. width - 1 do
        if isNode schematic.[row].[col] then
          for r in row - 1 .. row + 1 do
            for c in col - 1 .. col + 1 do
              if r >= 0 && r < height && c >= 0 && c < width then
                adj[r][c] <- true
    adj
  let sumRow (row: char[], adj: bool[]) = 
    let mutable firstIndex = -1
    let mutable sum = 0
    for i in 0 .. row.Length - 1 do
      if Char.IsDigit row[i] && firstIndex < 0 then
        firstIndex <- i
      elif not(Char.IsDigit row[i]) && firstIndex >= 0 then
        if adj[firstIndex .. i - 1] |> Array.contains true then
          sum <- sum + int (String.Concat(row.[firstIndex .. i - 1]))
        firstIndex <- -1
    if firstIndex >= 0 && adj[firstIndex .. row.Length - 1] |> Array.contains true then
      sum <- sum + int (String.Concat(row.[firstIndex .. row.Length - 1]))
    sum
  
  let sumParts (schematic: char[][], adj: bool[][]) = schematic |> Seq.mapi (fun i row -> sumRow(row, adj[i])) |> Seq.sum

  // Part 2
  let isGear c = 
    match c with
    | '*' -> true
    | _ -> false
  let adjacencyMatrix2 schematic =
    let width = schematic |> Seq.head |> Array.length
    let height = schematic |> Array.length
    let adj = Array.init width (fun _ -> Array.init height (fun _ -> 0))
    for row in 0 .. height - 1 do
      let mutable startIndex = -1
      for col in 0 .. width - 1 do
        if Char.IsDigit schematic.[row].[col] && startIndex = -1 then
          startIndex <- col
        elif (not(Char.IsDigit schematic.[row].[col]) && startIndex >= 0) || (Char.IsDigit schematic.[row].[col] && col = width - 1 && startIndex >= 0) then
          // let endIndex = if Char.IsDigit schematic.[row].[col] then col else col - 1
          for r in row - 1 .. row + 1 do
            for c in startIndex-1 .. col do
              if r >= 0 && r < height && c >= 0 && c < width && not(Char.IsDigit(schematic[r][c])) then
                adj[r][c] <- adj[r][c] + 1
          startIndex <- -1
    adj
  
  let weightMatrix schematic =
    let width = schematic |> Seq.head |> Array.length
    let height = schematic |> Array.length
    let weights = Array.init width (fun _ -> Array.init<int64> height (fun _ -> 1))
    for row in 0 .. height - 1 do
      let mutable firstIndex = -1
      for col in 0 .. width - 1 do
        let isDigit = Char.IsDigit schematic.[row].[col]
        if isDigit && firstIndex = -1 then
          firstIndex <- col
        elif (not(isDigit) && firstIndex >= 0) || (isDigit && col = width - 1 && firstIndex >= 0) then
          let endIndex = if isDigit then col else col - 1
          let weight = int64 (String.Concat(schematic.[row].[firstIndex .. endIndex]))
          for r in row - 1 .. row + 1 do
            for c in firstIndex - 1 .. col do
              if r >= 0 && r < height && c >= 0 && c < width then
                weights.[r].[c] <- weights.[r].[c] * weight
          firstIndex <- -1
    weights
  
  let sumGearWeights (schematic: char[][], adj: int[][], weights: int64[][]) =
    schematic |> Seq.mapi (fun i row -> Seq.mapi (fun j c -> if isGear c && adj[i][j] = 2 then weights[i][j] else 0) row |> Seq.sum) |> Seq.sum