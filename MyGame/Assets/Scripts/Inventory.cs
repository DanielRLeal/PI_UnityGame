using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public static Inventory instance;

    #region Singleton
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }
    #endregion

    public int space = 20;

    public List<Item> items = new List<Item>();
	
    // Use this for initialization
	public bool Add(Item item) {
        if (!item.isDefaultItem) {
            if(items.Count >= space)
            {
                Debug.Log("Not enough space.");
                return false;
            }
            items.Add(item);
        }
        return true;
    }
	
	// Update is called once per frame
	public void Remove (Item item) {
        items.Remove(item);
	}
}
