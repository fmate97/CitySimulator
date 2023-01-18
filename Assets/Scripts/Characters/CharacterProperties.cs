using UnityEngine;

public class CharacterProperties : MonoBehaviour
{
    public enum DoList { Idle, Walking };
    [HideInInspector] public DoList NowDo;

    public string characterName;
    [Range(0f, 10f)] public float walkingSpeed;
    [Range(0f, 100)] public int foodLevel;
    [Range(0f, 100)] public int drinkLevel;

    private float deltaTime = 0f;

    void Update()
    {
        deltaTime += Time.deltaTime;

        if (deltaTime >= 1f)
        {
            deltaTime = 0f;

            foodLevel--;
            drinkLevel--;
        }

        if (foodLevel == 0 || drinkLevel == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
