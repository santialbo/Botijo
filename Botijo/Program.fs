
module Botijo.Main

open IRCbot.Bot
open Botijo.Bot

let server = "irc.quakenet.org"
let port = 6667
let channels = ["#holaquetalsoycolosal"]

[<EntryPoint>]
let main args = 
    let botijo = new SimpleBot(server, port, nick, channels, msgHandler)
    botijo.Start()
    0
