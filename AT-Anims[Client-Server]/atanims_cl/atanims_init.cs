using CitizenFX.Core;
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
        public static Dictionary<string, string> walkDict = new Dictionary<string, string>();

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

        internal static void SetupWalksDict()
        {
            foreach (var walk in GetConfig.Config["WalkStyleList"])
            {
                string name = walk["Name"].ToString().ToLower();
                string style = walk["Style"].ToString();

                walkDict.Add(name, style);
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
            TriggerEvent("chat:addSuggestion", "/walk", "Set your walk style",
                new[] {new {name = "style", help = "/walks for a list of valid styles"}});
            TriggerEvent("chat:addSuggestion", "/walks", "List available walk styles");
            
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
            
            API.RegisterCommand("walk", new Action<int, List<object>, string>((source, args, raw) =>
            {
                Funciones.WalkCommand(args);
            }), false);
            
            API.RegisterCommand("walks", new Action<int, List<object>, string>((source, args, raw) =>
            {
                Funciones.WalksOnCommand();
            }), false);
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
