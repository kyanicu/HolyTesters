using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    /*
     Changed by Nick Tang
     5/14/19
     */

    private List<string> items;
    public string equipped;

    public string getItemName(int index)
    {
        return items[index];
    }

    public void addItem(string itemName)
    {
        items.Add(itemName);
    }

    public void setEquipped(string itemName)
    {
        if (items.Contains(itemName))
        {
            Swap<string>(items, items.IndexOf(itemName), 0);
        }
    }

    public string getEquippedName()
    {
        return equipped;
    }

    public static void Swap<T>(IList<T> list, int indexA, int indexB)
    {
        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
    }

    void Start()
    {
        items = new List<string>();
    }

    void Update()
    {
        if (items.Count != 0)
        {
            equipped = items[0];
        }
    }
}

