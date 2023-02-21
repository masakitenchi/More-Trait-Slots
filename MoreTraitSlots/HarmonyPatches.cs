using System.Reflection;
using HarmonyLib;
using Verse;

namespace MoreTraitSlots;

[StaticConstructorOnStartup]
internal class HarmonyPatches
{
	static HarmonyPatches()
	{
		Harmony harmony = new Harmony("com.rimworld.mod.moretraitslots");
		harmony.PatchAll(Assembly.GetExecutingAssembly());
	}
}
