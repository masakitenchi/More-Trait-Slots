using HarmonyLib;
using RimWorld;
using Verse;

namespace MoreTraitSlots;

[HarmonyPatch(typeof(PawnGenerator), "GenerateTraits")]
internal static class PawnGenerator_GenerateTraits
{
	public static int curTraitCount = -1;

	private static void Postfix(Pawn pawn, PawnGenerationRequest request)
	{
        if (pawn.story == null || request.AllowedDevelopmentalStages.Newborn())
        {
            return;
        }
        checked
		{
			curTraitCount = Rand.RangeInclusive((int)RMTS.Settings.traitsMin, (int)RMTS.Settings.traitsMax);
			int num = 0;
			while (curTraitCount > pawn.story.traits.allTraits.Count && num < 500)
			{
				num++;
				Trait trait = PawnGenerator.GenerateTraitsFor(pawn, 1, request, growthMomentTrait: true).FirstOrFallback();
				if (trait != null)
				{
					num = 0;
					pawn.story.traits.GainTrait(trait);
				}
			}
			curTraitCount = -1;
		}
	}
}
