using System;
using System.Collections.Generic;

using Intersect.Client.Core;
using Intersect.Client.Core.Controls;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Localization;

namespace Intersect.Client.Interface.Game.Hotbar
{

    public class ComboBarWindow
    {

        //Controls
        public ImagePanel ComboWindow;

        //Item List
        public List<CombobarItem> Items = new List<CombobarItem>();

        //Init
        public ComboBarWindow(Canvas gameCanvas)
        {
            ComboWindow = new ImagePanel(gameCanvas, "ComboWindow");
            ComboWindow.ShouldCacheToTexture = true;
            InitCombobarItems();
            ComboWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].EquipPanel.Texture == null)
                {
                    Items[i].EquipPanel.Texture = Graphics.Renderer.GetWhiteTexture();
                }
            }
        }

        private void InitCombobarItems()
        {
            var x = 12;
            for (var i = 0; i < Options.MaxHotbar; i++)
            {
                Items.Add(new CombobarItem((byte)i, ComboWindow));
                Items[i].Pnl = new ImagePanel(ComboWindow, "HotbarContainer" + i);
                Items[i].Setup();
                Items[i].KeyLabel = new Label(Items[i].Pnl, "HotbarLabel" + i);
                Items[i]
                    .KeyLabel.SetText(
                        Strings.Keys.keydict[
                            Enum.GetName(typeof(Keys), Controls.ActiveControls.ControlMapping[Control.Hotkey1 + i].Key1)
                                .ToLower()]
                    );
            }
        }

        public void Update()
        {
            if (Globals.Me == null)
            {
                return;
            }

            for (var i = 0; i < Options.MaxHotbar; i++)
            {
                Items[i].Update();
            }
        }

        public FloatRect RenderBounds()
        {
            var rect = new FloatRect()
            {
                /*X = ComboWindow.LocalPosToCanvas(new Point(0, 0)).X,
                Y = ComboWindow.LocalPosToCanvas(new Point(0, 0)).Y,*/
                X = ComboWindow.LocalPosToCanvas(new Point(100, 100)).X,
                Y = ComboWindow.LocalPosToCanvas(new Point(100, 100)).Y,

                Width = ComboWindow.Width,
                Height = ComboWindow.Height
            };

            return rect;
        }

    }

}
