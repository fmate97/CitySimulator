using UnityEngine;

public class CharacterProperties : MonoBehaviour
{
    public enum DoList { Idle, Walking, Gathering };
    [HideInInspector] public DoList NowDo;

    public string characterName;
    [Range(0f, 10f)] public float walkingSpeed;
    [Range(0f, 100)] public int foodLevel;
    [Range(0f, 100)] public int drinkLevel;
    [Range(0f, 10f)] public float gatheringSpeed;

    private float deltaTime = 0f;

    void Update()
    {
        deltaTime += Time.deltaTime;

        if (deltaTime >= 2.5f)
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
