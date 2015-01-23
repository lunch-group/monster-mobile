using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// List of prices for Buyable objects.
public class CommerceManager : MonoBehaviour
{
	[System.Serializable]
	public struct Prices
	{
		public string name;
		public int price;
	}
	public Prices[] PriceList;

	// This doubles the memory needed but makes lookups much easier.
	private Dictionary<string, int> mPriceDictionary = new Dictionary<string, int>();

	void Start ()
	{
		for (int i = 0; i < PriceList.Length; ++i)
		{
			mPriceDictionary.Add(PriceList[i].name, PriceList[i].price);
		}
	}

	public int GetPrice(string name)
	{
		if (mPriceDictionary.ContainsKey(name))
		{
			return mPriceDictionary[name];
		}

		return -1;
	}

	public bool IsAffordable(string name)
	{
		if (mPriceDictionary.ContainsKey(name))
		{
			return (Player.gold >= mPriceDictionary[name]);
		}

		return false;
	}
	
	public bool Purchase(string name)
	{
		bool success = false;

		// Check if the item exists and can be bought.
		if (IsAffordable(name))
		{
			Player.gold -= mPriceDictionary[name];
			success = true;
			
			Debug.Log("COMMERCE: Purchased " + name + " for " + mPriceDictionary[name] + " gold.");
        }
        
        return success;
	}
}
