module Five
  open System.Text.RegularExpressions

  let stages = ["seed"; "soil"; "fertilizer"; "water"; "light"; "temperature"; "humidity"; "location"]
  let parseSeeds (almanac: string[]) = 
    (almanac[0].Split("seeds: ")[1]).Split(" ") |> Seq.map int64 |> Seq.toArray
  let parseMap almanac source destination =
    let regex = Regex(sprintf "%s-to-%s map:\n( *\d+\n*)*" source destination)
    let mapping = regex.Match(almanac |> String.concat("\n")).Value.Split("\n") |> Array.filter (fun x -> x.Trim() <> "") 
    mapping[1..mapping.Length - 1]
      |> Seq.map (fun x -> x.Split(" ") |> Seq.map int64 |> Seq.toArray) 
      |> Seq.map (fun range -> [| range[0] - range[1]; range[1]; range[1] + range[2] |])
      |> Seq.toArray
  
  let navigateMap (map: int64[][]) value =
    match map |> Seq.tryFind (fun range -> range[1] <= value && value < range[2]) with
    | Some range -> value + range[0]
    | None -> value

  let mapSeeds seeds map =
    seeds |> Array.map (navigateMap map)

  let navigateAlmanac (almanac: string[], seeds: int64[], stages: string list): int64[] =
    let rec navigate (st: string list, s2: int64[]): int64[] =
      match st with
      | [] -> s2
      | [_] -> s2
      | source::destination::[] -> mapSeeds s2 (parseMap almanac source destination)
      | source::destination::rest -> navigate (destination::rest, ((parseMap almanac source destination) |> mapSeeds s2) )
    navigate (stages, seeds)

// Part 2
  // let seedRegex = Regex("seeds:(( \d+ \d+)*)")
  // let parseSeedRanges almanac = 
  //   seedRegex.Match(almanac |> String.concat("\n")).Groups[1].Value.Split(" ") 
  //     |> Array.filter (fun x -> x.Trim() <> "") 
  //     |> Seq.map int64 |> Seq.toArray |> Array.chunkBySize 2
  //     |> Array.map (fun x -> [x[0]..x[0]+x[1]] |> Seq.toArray) 
  //     |> Array.reduce Array.append
  let parseSeedRanges almanac =
    parseSeeds almanac |> Array.chunkBySize 2 |> Array.map (fun x -> [x[0]..x[0]+x[1]] |> Seq.toArray) |> Array.reduce Array.append