using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;

namespace RefreshStarterItems;

public class RefreshBag : StartBag
{
	public static FieldInfo itemsField = typeof(StartBag).GetField("items", BindingFlags.NonPublic | BindingFlags.Instance);

	public override string Texture => "ModLoader/StartBag";

	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("{$tModLoader.StartBagItemName}");
		Tooltip.SetDefault("Contains starter items\n{$CommonItemTooltip.RightClickToOpen}");
	}

	public override void RightClick(Player player)
	{
		List<Item> startingItems = PlayerLoader.GetStartingItems(player, Enumerable.Empty<Item>());
		itemsField.SetValue(this, startingItems);

		base.RightClick(player);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddTile(TileID.DemonAltar)
			.Register();
	}
}
