using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace MoreTraitSlots;

internal class Settings : ModSettings
{
	public int MAX_TRAITS = -1;

	public float traitsMin = 2f;

	public float traitsMax = 3f;

	public bool smallFont = false;

	private string minBuffer;

	private string maxBuffer;

	public Settings()
	{
		SetMinBuffer();
		SetMaxBuffer();
	}

	public void DoWindowContents(Rect canvas)
	{
		if (MAX_TRAITS == -1)
		{
			MAX_TRAITS = DefDatabase<TraitDef>.AllDefsListForReading.Count();
		}
		float xMin = canvas.xMin;
		float yMin = canvas.yMin;
		float width = canvas.width * 0.5f;
		Widgets.Label(new Rect(xMin, yMin, canvas.width, 30f), "RMTS.traitsMin".Translate());
		yMin += 32f;
		float num = traitsMin;
		traitsMin = Widgets.HorizontalSlider_NewTemp(new Rect(xMin + 10f, yMin, width, 32f), traitsMin, 0f, 8.25f);
		if (num != traitsMin)
		{
			SetMinBuffer();
		}
		yMin += 32f;
		string value = minBuffer;
		minBuffer = Widgets.TextField(new Rect(xMin + 10f, yMin, 50f, 32f), minBuffer);
		if (!minBuffer.Equals(value))
		{
			ParseInput(minBuffer, traitsMin, out traitsMin);
		}
		if (traitsMin > traitsMax)
		{
			traitsMax = traitsMin;
			SetMaxBuffer();
		}
		yMin += 60f;
		Widgets.Label(new Rect(xMin, yMin, canvas.width, 30f), "RMTS.traitsMax".Translate());
		yMin += 32f;
		num = traitsMax;
		traitsMax = Widgets.HorizontalSlider_NewTemp(new Rect(xMin + 10f, yMin, width, 32f), traitsMax, 0f, 8.25f);
		if (num != traitsMax)
		{
			SetMaxBuffer();
		}
		yMin += 32f;
		value = maxBuffer;
		maxBuffer = Widgets.TextField(new Rect(xMin + 10f, yMin, 50f, 32f), maxBuffer);
		if (!maxBuffer.Equals(value))
		{
			ParseInput(maxBuffer, traitsMax, out traitsMax);
		}
		if (traitsMax < traitsMin)
		{
			traitsMin = traitsMax;
			SetMinBuffer();
		}
		yMin += 60f;
		Widgets.Label(new Rect(xMin, yMin, canvas.width, 100f), "RMTS.traitsNotes".Translate());
		if (traitsMin > (float)MAX_TRAITS)
		{
			traitsMin = MAX_TRAITS;
			SetMinBuffer();
		}
		if (traitsMax > (float)MAX_TRAITS)
		{
			traitsMax = MAX_TRAITS;
			SetMaxBuffer();
		}
	}

	private void ParseInput(string buffer, float origValue, out float newValue)
	{
		if (!float.TryParse(buffer, out newValue))
		{
			newValue = origValue;
		}
		if (newValue < 0f)
		{
			newValue = origValue;
		}
	}

	public override void ExposeData()
	{
		base.ExposeData();
		Scribe_Values.Look(ref traitsMin, "traitsMin", 2f);
		if (traitsMax < traitsMin)
		{
			traitsMax = traitsMin;
		}
		Scribe_Values.Look(ref traitsMax, "traitsMax", 3f);
		if (Scribe.mode != LoadSaveMode.Saving)
		{
			SetMinBuffer();
			SetMaxBuffer();
		}
	}

	private void SetMinBuffer()
	{
		minBuffer = checked((int)traitsMin).ToString();
	}

	private void SetMaxBuffer()
	{
		maxBuffer = checked((int)traitsMax).ToString();
	}
}
