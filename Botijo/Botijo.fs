
module Botijo.Bot

open IRCbot.Util

let nick = "Botijo"

let msgHandler (line: string) (write: string -> unit) =

    let say channel text = write (sprintf "PRIVMSG %s :%s" channel text)
    let ping what = write (sprintf "PONG :%s" what)
    
    match line with
    | Message (PING(what)) -> ping what
    | Message (JOIN(user, channel)) when user = nick -> say channel "Hello world!"
    | _ -> ()