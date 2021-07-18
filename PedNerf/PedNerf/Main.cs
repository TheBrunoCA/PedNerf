using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTA;

namespace PedNerf
{
    public class Main : Script
    {
        public static readonly string modName = "PedNerf";

        public static bool valueEnableHealthChanges
            , valueEnableAccuracyChanges
            , valueEnableDebugMode;

        public static Keys keyOpenMenu;

        private readonly string pathToSettings = @"scripts\"+modName+@"\Settings.ini";

        ScriptSettings settings;

        private readonly string catGeneral = "General"
            , catValues = "Values"
            , catKeys = "Keys";

        private readonly string descEnableHealthChanges = "Enable_Health_Changes"
            , descEnableAccuracyChanges = "Enable_Accuracy_Changes"
            , descEnableDebugMode = "Enable_Debug_Mode"
            , descHealthValue = "Ped_Health_Value"
            , descAccuracyValue = "Ped_Accuracy_Value"
            , descUpdateRadius = "Mod_Update_Radius"
            , descOpenMenu = "Open_Menu_Key";
        public static int pedHealth, pedAccuracy, updateRadius;

        public Main()
        {
            Tick += Main_Tick;
            KeyUp += Main_KeyUp;
            settings = ScriptSettings.Load(pathToSettings);
            valueEnableHealthChanges = settings.GetValue<bool>(catGeneral, descEnableHealthChanges, true);
            valueEnableAccuracyChanges = settings.GetValue<bool>(catGeneral, descEnableAccuracyChanges, true);
            valueEnableDebugMode = settings.GetValue<bool>(catGeneral, descEnableDebugMode, false);
            updateRadius = settings.GetValue<int>(catGeneral, descUpdateRadius, 100);
            pedHealth = settings.GetValue<int>(catValues, descHealthValue, 150);
            pedAccuracy = settings.GetValue<int>(catValues, descAccuracyValue, 10);
            keyOpenMenu = settings.GetValue<Keys>(catKeys, descOpenMenu, Keys.F9);

            Menu menu = new Menu();

        }

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void Main_Tick(object sender, EventArgs e)
        {
            if ((valueEnableHealthChanges || valueEnableAccuracyChanges) && isValid())
            {
                Ped playerPed = Game.Player.Character;
                Ped[] peds = World.GetNearbyPeds(playerPed, updateRadius);
                foreach (var p in peds)
                {
                    if(p.MaxHealth>pedHealth&&valueEnableHealthChanges)
                    {
                        p.MaxHealth = pedHealth;
                    }
                    if(p.Accuracy>pedAccuracy&&valueEnableAccuracyChanges)
                    {
                        p.Accuracy = pedAccuracy;
                    }
                }
            }
        }

        public static bool isValid()
        {
            return Game.Player.Character.IsAlive && !Game.IsLoading
                            && !Game.IsPaused;
        }
    }
}
