using UnityEngine;
using System.Collections.Generic;
using static InventoryItemClass;

public class InventoryStackSize : MonoBehaviour
{
    [Header("Strawberry")]
    [SerializeField] ItemName strawberryName;
    [SerializeField] GameObject strawberryIcon;
    [SerializeField] ItemType strawberryType;
    [SerializeField] int strawberryConsumptionValue;
    [SerializeField] int strawberryStackSize;
    [SerializeField] float timeToProduceAStrawberry;

    public List<InventoryItemClass> inventoryItems = new List<InventoryItemClass>();

    void Awake()
    {
        inventoryItems.Add(new InventoryItemClass(strawberryName, strawberryIcon, strawberryType, strawberryConsumptionValue, strawberryStackSize, timeToProduceAStrawberry));
    }
}
