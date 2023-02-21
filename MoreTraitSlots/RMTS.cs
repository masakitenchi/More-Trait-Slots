using UnityEngine;
using Verse;

namespace MoreTraitSlots;

internal class RMTS : Mod
{
	public static Settings Settings;

	public override string SettingsCategory()
	{
		return "RMTS.RMTS".Translate();
	}

	public override void DoSettingsWindowContents(Rect canvas)
	{
		Settings.DoWindowContents(canvas);
	}

	public RMTS(ModContentPack content)
		: base(content)
	{
		Settings = GetSettings<Settings>();
	}
}
