using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [Header("Selected character info panel settings")]
    [SerializeField] GameObject SelectedCharacterInfoPanel;
    [SerializeField] Text nameTextbox;
    [SerializeField] Text nowDoTextbox;
    [SerializeField] GameObject foodLevelBar;
    [SerializeField] GameObject drinkLevelBar;

    private Camera _mainCamera;
    private Ray _ray;
    private RaycastHit _hit;
    private GameObject _selectedCharacter;
    private CharacterProperties _selectedCharacterProperties;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit, 1000f))
            {
                if (_hit.transform.tag == "Characters")
                {
                    if (_selectedCharacter != null)
                    {
                        _selectedCharacter.GetComponent<CharacterMovement>().UnselectedCharacter();
                    }

                    _selectedCharacter = _hit.transform.gameObject;
                    _selectedCharacter.GetComponent<CharacterMovement>().SelectedCharacter();
                    _selectedCharacterProperties = _selectedCharacter.GetComponent<CharacterProperties>();

                    UpdateSelectedCharacterInfoPanel();
                }
            }
        }

        if (_selectedCharacter != null && _selectedCharacter.activeSelf == false)
        {
            _selectedCharacter = null;
            _selectedCharacterProperties = null;
            SelectedCharacterInfoPanel.SetActive(false);
        }

        if (_selectedCharacter != null)
        {
            if (nowDoTextbox.text != _selectedCharacterProperties.NowDo.ToString())
            {
                nowDoTextbox.text = _selectedCharacterProperties.NowDo.ToString();
            }

            if (foodLevelBar.GetComponent<UIBarScript>().GetValue() != _selectedCharacterProperties.foodLevel)
            {
                foodLevelBar.GetComponent<UIBarScript>().SetValue(_selectedCharacterProperties.foodLevel);
            }

            if (drinkLevelBar.GetComponent<UIBarScript>().GetValue() != _selectedCharacterProperties.drinkLevel)
            {
                drinkLevelBar.GetComponent<UIBarScript>().SetValue(_selectedCharacterProperties.drinkLevel);
            }
        }
    }

    void UpdateSelectedCharacterInfoPanel()
    {
        if (!SelectedCharacterInfoPanel.activeSelf) { SelectedCharacterInfoPanel.SetActive(true); }

        nameTextbox.text = _selectedCharacterProperties.characterName;
        nowDoTextbox.text = _selectedCharacterProperties.NowDo.ToString();
        foodLevelBar.GetComponent<UIBarScript>().SetValue(_selectedCharacterProperties.foodLevel);
        drinkLevelBar.GetComponent<UIBarScript>().SetValue(_selectedCharacterProperties.drinkLevel);
    }
}
