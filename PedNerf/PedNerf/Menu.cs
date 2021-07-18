using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonUI;
using LemonUI.Menus;
using LemonUI.Elements;
using GTA;

namespace PedNerf
{
    class Menu : Script
    {
        private readonly ObjectPool pool = new ObjectPool();
        private readonly NativeMenu menu = new NativeMenu(Main.modName, "The configuration menu")
        {
            ItemCount = CountVisibility.Always
        };
        private readonly NativeCheckboxItem enableHealthChange =
            new NativeCheckboxItem("Enable Health Change", "Enable to apply the changes of health to Peds in range");
        private readonly LemonUI.Menus.NativeSliderItem healthValue =
            new NativeSliderItem("New Peds Health", "The Peds new health value", 500, Main.pedHealth);
        private readonly NativeCheckboxItem enableAccuracyChange =
            new NativeCheckboxItem("Enable Accuracy Change", "Enable to apply the changes of accuracy to Peds in range");
        private readonly NativeSliderItem accuracyValue =
            new NativeSliderItem("New Peds Accuracy", "The higher, the more likely to hit", 100, Main.pedAccuracy);
        private readonly NativeSliderItem updateRadius =
            new NativeSliderItem("Update Radius", "The max distance from the player the changes will be made to the ped");
        private readonly NativeCheckboxItem enableDebugMode =
            new NativeCheckboxItem("Debug Mode", "Developer debug tool", false);


        public Menu()
        {
            Tick += Menu_Tick;
            KeyUp += Menu_KeyUp;

            enableHealthChange.CheckboxChanged += EnableHealthChange_CheckboxChanged;
            enableAccuracyChange.CheckboxChanged += EnableAccuracyChange_CheckboxChanged;
            healthValue.ValueChanged += HealthValue_ValueChanged;
            accuracyValue.ValueChanged += AccuracyValue_ValueChanged;
            updateRadius.ValueChanged += UpdateRadius_ValueChanged;
            enableDebugMode.CheckboxChanged += EnableDebugMode_CheckboxChanged;

            pool.Add(menu);
            menu.Add(enableHealthChange);
            menu.Add(healthValue);
            menu.Add(enableAccuracyChange);
            menu.Add(accuracyValue);
            menu.Add(enableDebugMode);
        }

        private void UpdateRadius_ValueChanged(object sender, EventArgs e)
        {
            Main.updateRadius = updateRadius.Value;
        }

        private void EnableDebugMode_CheckboxChanged(object sender, EventArgs e)
        {
            Main.valueEnableDebugMode = enableDebugMode.Checked;
        }

        private void AccuracyValue_ValueChanged(object sender, EventArgs e)
        {
            Main.pedAccuracy = accuracyValue.Value;
        }

        private void HealthValue_ValueChanged(object sender, EventArgs e)
        {
            Main.pedHealth = healthValue.Value;
        }

        private void EnableAccuracyChange_CheckboxChanged(object sender, EventArgs e)
        {
            Main.valueEnableAccuracyChanges = enableAccuracyChange.Checked;
            accuracyValue.Enabled = enableAccuracyChange.Checked;
        }

        private void EnableHealthChange_CheckboxChanged(object sender, EventArgs e)
        {
            Main.valueEnableHealthChanges = enableHealthChange.Checked;
            healthValue.Enabled = enableHealthChange.Checked;
        }

        private void Menu_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode.Equals(Main.keyOpenMenu)&&Main.isValid())
            {
                if (!menu.Visible)
                    menu.Visible = true;
                else
                    menu.Visible = false;
            }
        }

        private void Menu_Tick(object sender, EventArgs e)
        {
            pool.Process();
        }
    }
}
