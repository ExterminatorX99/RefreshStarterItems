using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;

namespace RefreshStarterItems
{
	public class RefreshBag : StartBag
	{
		public static FieldInfo itemsField = typeof(StartBag).GetField("items", BindingFlags.NonPublic | BindingFlags.Instance);

		public List<Item> GetItems() => (List<Item>) itemsField.GetValue(this);

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("{$tModLoader.StartBagItemName}");
			Tooltip.SetDefault("Contains starter items\n{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void RightClick(Player player)
		{
			List<Item> items = GetItems();
			items.Clear();
			items.AddRange(PlayerHooks.SetupStartInventory(player));

			base.RightClick(player);
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(ModContent.ItemType<RefreshBag>());
			recipe.AddRecipe();
		}
	}
}
