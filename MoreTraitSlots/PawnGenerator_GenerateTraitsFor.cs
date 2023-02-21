using HarmonyLib;
using Verse;

namespace MoreTraitSlots;

[HarmonyPatch(typeof(PawnGenerator), "GenerateTraitsFor")]
internal static class PawnGenerator_GenerateTraitsFor
{
	private static void Prefix(Pawn pawn, ref int traitCount, PawnGenerationRequest? req = null, bool growthMomentTrait = false)
	{
		if (PawnGenerator_GenerateTraits.curTraitCount != -1 && pawn.story.traits.allTraits.Count >= PawnGenerator_GenerateTraits.curTraitCount)
		{
			traitCount = 0;
		}
	}
}
