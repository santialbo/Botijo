
module Botijo.Plugins.Google

open System.IO
open System.Net
open System.Web.Script.Serialization
open System.Collections.Generic
open System.Text.RegularExpressions

open IRCbot.Util

let Download (url: string) =
    try
        let req = WebRequest.Create(url)
        let resp = req.GetResponse()
        let stream = resp.GetResponseStream()
        use reader = new StreamReader(stream)
        reader.ReadToEnd()
    with
    | _ -> ""

let GoogleQuery text =
    try
        let jss = new JavaScriptSerializer();
        let response =
            (sprintf "http://ajax.googleapis.com/ajax/services/search/web?v=1.0&q=%s" text)
            |> Download
            |> jss.DeserializeObject
            :?> Dictionary<string, obj>
        let responseData = response.["responseData"] :?> Dictionary<string, obj>
        let results =
            responseData.["results"] :?> array<obj>
            |> Array.map (fun d ->
                              d
                              :?> Dictionary<string, obj>
                              :> seq<_>
                              |> Seq.map (fun kv -> kv.Key, kv.Value :?> string)    // ütter bullshit
                              |> Map.ofSeq)
            |> Array.toList
        results
    with
    | _ -> []
    
let ImFeelingLucky text =

    let google = Format.BOLD +
                 (Format.COLOR "blue") + "G" +
                 (Format.COLOR "red") + "o" +
                 (Format.COLOR "yellow") + "o" +
                 (Format.COLOR "blue") + "g" +
                 (Format.COLOR "green") + "l" +
                 (Format.COLOR "red") + "e" +
                 Format.BOLD +
                 Format.RESET 

    let removeHTML text =
        let r = new Regex(@"<[^>]+>")
        r.Replace(text, "")
        
    let ResultResponse (result: Map<string,string>) =
        (sprintf "%s: %s: %s (%s)" google (Format.BOLD + result.["titleNoFormatting"] + Format.BOLD) (removeHTML result.["content"]) result.["unescapedUrl"])
        
    let NoResultResponse = fun () ->
        (sprintf "%s: No results :(" google)
    
    match (GoogleQuery text) with
    | first :: _ -> ResultResponse first
    | [] -> NoResultResponse()
    
    
    
     
    
