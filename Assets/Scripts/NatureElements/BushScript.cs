using System.Linq;
using UnityEngine;

public class BushScript : MonoBehaviour
{
    [SerializeField] InventoryStackSize inventoryStackSize;
    [SerializeField] GameObject infoPanel;
    [SerializeField] InventoryItemClass.ItemName itemName;

    private int _actualItemNumber = 0, _showedItemNumber = 0;
    private float _deltaTime = 0f;
    private UIBarScript _infoPanelBarScript;
    private InventoryItemClass _itemProperty;

    void Start()
    {
        _infoPanelBarScript = infoPanel.GetComponentsInChildren<Transform>().Where(x => x.name == "FoodLevelBar").First().GetComponent<UIBarScript>();
        _itemProperty = inventoryStackSize.inventoryItems.Where(x => x.name == itemName).First();
    }

    void Update()
    {
        if (_actualItemNumber != _itemProperty.maxStackSize)
        {
            if (_deltaTime >= _itemProperty.timeToProduceAUnit)
            {
                _deltaTime = 0f;
                _actualItemNumber++;
            }
        }

        _deltaTime += Time.deltaTime;
    }

    void OnMouseEnter()
    {
        UpdateShowedItemNumber();
        infoPanel.SetActive(true);
    }

    void OnMouseOver()
    {
        if (_showedItemNumber != _actualItemNumber)
        {
            UpdateShowedItemNumber();
        }
    }

    void UpdateShowedItemNumber()
    {
        _infoPanelBarScript.SetValue(_actualItemNumber, _itemProperty.maxStackSize);
        _showedItemNumber = _actualItemNumber;
    }

    void OnMouseExit()
    {
        infoPanel.SetActive(false);
    }

    public int GetOneUnit()
    {
        if(_actualItemNumber <= 0) { return 0; }

        _actualItemNumber--;
        return _itemProperty.consumptionValue;
    }
}
