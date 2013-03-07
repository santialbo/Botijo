
module Botijo.Plugins.Lolcats

open System
open System.Text.RegularExpressions
open Botijo.Plugins.Util

let RandomLolcat() =
    let lolcats =
        "http://www.reddit.com/r/lolcats/.json"
        |> Download
        |> (new Regex @"""url"": ""([^""]+.jpe?g)""").Matches
        |> Seq.cast
        |> Seq.map (fun (m:Match) -> m.Groups.[1].Value)
    in lolcats |> Seq.nth ((new Random()).Next (Seq.length lolcats))
    