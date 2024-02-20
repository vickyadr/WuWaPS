using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace GameServer.Controllers.Gacha;
internal class GachaPool
{
	public readonly List<List<int>> Items;

	private int _pullCount;

	private int _pityFour;

	//private bool _guaranteed;

	public int Id { get; }

	public string Name { get; }

	public bool Limited { get; }

	public int SoftPityStart { get; }

	public int HardPityFour { get; }

	public int HardPityFive { get; }

	public float[] Rates { get; }

	public GachaPool(int id, string name, bool limited, int softPityStart, int hardPityFour, int hardPityFive, float[] rates)
	{
		Id = id;
		Name = name;
		Limited = limited;
		SoftPityStart = softPityStart;
		HardPityFour = hardPityFour;
		HardPityFive = hardPityFive;
		Rates = rates;
		List<List<int>> list = new List<List<int>>();
		CollectionsMarshal.SetCount(list, 5);
		Span<List<int>> span = CollectionsMarshal.AsSpan(list);
		int num = 0;
		span[num] = new List<int>();
		num++;
		span[num] = new List<int>();
		num++;
		span[num] = new List<int>();
		num++;
		span[num] = new List<int>();
		num++;
		span[num] = new List<int>();
		num++;
		Items = list;
	}

	public void AddItemsThree(int[] items)
	{
		Items[0].AddRange(items);
	}

	public void AddItemsFour(int[] items)
	{
		Items[1].AddRange(items);
	}

	public void AddItemsFourPromoted(int[] items)
	{
		Items[2].AddRange(items);
	}

	public void AddItemsFive(int[] items)
	{
		Items[3].AddRange(items);
	}

	public void AddItemsFivePromoted(int[] items)
	{
		Items[4].AddRange(items);
	}

	public (int, int) DoPull()
	{
		Random random = new Random(Environment.TickCount);
		float[] prob = (float[])Rates.Clone();
		if (_pullCount >= SoftPityStart)
		{
			prob[0] = 100f - (0.8f + (float)(8 * (_pullCount - SoftPityStart - 1)));
			prob[2] = 0.8f + (float)(8 * (_pullCount - SoftPityStart - 1));
		}
		if (_pityFour + 1 >= HardPityFour)
		{
			prob = new float[3] { 0f, 100f, 0f };
		}
		if (_pullCount + 1 >= HardPityFive)
		{
			prob = new float[3] { 0f, 0f, 100f };
		}
		int roll = random.Next(1000) / 10;
		int rarityResult = 3;
		int itemResult = 1;
		for (int i = 0; i < prob.Length; i++)
		{
			if (prob[i] > (float)roll)
			{
				rarityResult = i + 3;
				break;
			}
		}
		switch (rarityResult)
		{
		case 3:
			itemResult = Items.ElementAt(0).ElementAt(random.Next(Items.ElementAt(0).Count));
			break;
		case 4:
			itemResult = Items.ElementAt(1).ElementAt(random.Next(Items.ElementAt(1).Count));
			break;
		case 5:
			itemResult = Items.ElementAt(3).ElementAt(random.Next(Items.ElementAt(3).Count));
			break;
		}
		Console.WriteLine("PullCount: " + _pullCount);
		Console.WriteLine("Pity: " + _pityFour);
		Console.WriteLine("Rates: " + prob[0] + "\t" + prob[1] + "\t" + prob[2]);
		Console.WriteLine("Result Rarity: " + rarityResult);
		Console.WriteLine("Result Item: " + itemResult);
		_pityFour = ((rarityResult != 4 && _pityFour != 10) ? (_pityFour + 1) : 0);
		_pullCount = ((rarityResult != 5 && _pullCount != 80) ? (_pullCount + 1) : 0);
		return (itemResult, rarityResult);
	}
}
