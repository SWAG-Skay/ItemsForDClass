using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using Item = Exiled.API.Features.Items.Item;

namespace PureDClassItemsPlugin
{
    public class PureDClassItemsPlugin : Plugin<Config>
    {
        public override string Author => "Skay";
        public override string Name => "DClass Inventory";
        public override string Prefix => "DClass Inventory";

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.ChangingRole += OnChangingRole;
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.ChangingRole -= OnChangingRole;
        }

        private void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.NewRole == RoleTypeId.ClassD)
            {
                Timing.CallDelayed(0.5f, () => GiveDClassItems(ev.Player));
            }
        }

        private void GiveDClassItems(Player player)
        {
            if (player == null || !player.IsAlive) return;
            if (Config.ClearInventory)
                player.ClearInventory();
            foreach (ItemType itemType in Config.ItemsForDClass)
            {
                Item item = player.AddItem(itemType);

            }
        }
    }

    public class Config : Exiled.API.Interfaces.IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public bool ClearInventory { get; set; } = true;
        public List<ItemType> ItemsForDClass { get; set; } = new List<ItemType>
        {
            ItemType.KeycardJanitor,
            ItemType.Flashlight,
            ItemType.Coin
        };
    }
}