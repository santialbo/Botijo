
module Botijo.Plugins.Util

open System.IO
open System.Net

let Download (url: string) =
    try
        let req = WebRequest.Create(url)
        let resp = req.GetResponse()
        let stream = resp.GetResponseStream()
        use reader = new StreamReader(stream)
        reader.ReadToEnd()
    with
    | _ -> ""

