﻿using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuAPI;

namespace atanims_cl
{
    class atanims_init : BaseScript
    {
        public static uint KeyToOpen = 0;
        public static Dictionary<string, int> emoteDict = new Dictionary<string, int>();

        public atanims_init()
        {
            Tick += OpenMenu;
        }

        internal static void SetupEmoteDict()
        {
            foreach (var emote in GetConfig.Config["EmotesList"])
            {
                string name = emote["Command"].ToString();
                int hash = emote["Emote"].ToObject<int>();
                
                emoteDict.Add(name, hash);
            }
        }
        public static void SetupCommands()
        {
            TriggerEvent("chat:addSuggestion", "/e", "Play an emote",
                new[] {new {name = "emotename", help = "greet, nod, flex or any valid emote."}});
            TriggerEvent("chat:addSuggestion", "/emote", "Play an emote",
                new[] {new {name = "emotename", help = "greet, nod, flex or any valid emote"}});
            TriggerEvent("chat:addSuggestion", "/emotemenu", "Open emotes menu");
            TriggerEvent("chat:addSuggestion", "/emotes", "List available emotes");
            
            API.RegisterCommand("emotemenu", new Action<int, List<object>, string>((source, args, raw) =>
            {
                Menus.MainMenu.GetMenu().OpenMenu(); 
            }), false);

            API.RegisterCommand("e", new Action<int, List<object>, string>((source, args, raw) =>
            {
                Funciones.EmoteCommand(args);
            }), false);

            API.RegisterCommand("emote", new Action<int, List<object>, string>((source, args, raw) =>
            {
                Funciones.EmoteCommand(args);
            }), false);
            
            API.RegisterCommand("emotes", new Action<int, List<object>, string>((source, args, raw) =>
            {
                Funciones.EmotesOnCommand();
            }), false);
            
            // TODO: add walk/walks commands
        }

        public static async Task OpenMenu()
        {
            if (API.IsControlJustPressed(0, KeyToOpen))
            {
                Menus.MainMenu.GetMenu().OpenMenu();
            }
        }
    }
}
