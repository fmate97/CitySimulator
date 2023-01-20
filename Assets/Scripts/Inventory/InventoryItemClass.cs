using System;
using UnityEngine;

public class InventoryItemClass
{
    public enum ItemName { Strawberry }
    public enum ItemType { food, drink }

    public ItemName name;
    public GameObject itemIcon;
    public ItemType type;
    public int consumptionValue;
    public int maxStackSize;
    public float timeToProduceAUnit;

    public InventoryItemClass(ItemName name, GameObject itemIcon, ItemType type, int consumptionValue, int maxStackSize, float timeToProduceAUnit)
    {
        if (consumptionValue <= 0)
        {
            throw new ArgumentOutOfRangeException(consumptionValue.ToString(), "consumptionValue must be greater than 0");
        }
        if (maxStackSize <= 0)
        {
            throw new ArgumentOutOfRangeException(maxStackSize.ToString(), "maxStackSize must be greater than 0");
        }
        if (timeToProduceAUnit <= 0)
        {
            throw new ArgumentOutOfRangeException(timeToProduceAUnit.ToString(), "timeToProduceAUnit must be greater than 0");
        }

        this.name = name;
        this.itemIcon = itemIcon;
        this.type = type;
        this.consumptionValue = consumptionValue;
        this.maxStackSize = maxStackSize;
        this.timeToProduceAUnit = timeToProduceAUnit;
    }
}
