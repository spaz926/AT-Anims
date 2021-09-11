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
            API.RegisterCommand("emotes", new Action<int, List<object>, string>((source, args, raw) =>
            {
                Menus.MainMenu.GetMenu().OpenMenu(); 
            }), false);

            API.RegisterCommand("e", new Action<int, List<object>, string>((source, args, raw) =>
            {
                Funciones.EmoteCommand(args);
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
