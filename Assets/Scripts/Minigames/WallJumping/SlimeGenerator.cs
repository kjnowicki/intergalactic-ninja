using UnityEngine;

public class SlimeGenerator : MonoBehaviour
{
    private const string SLIME_EFFECT_PREFAB_PATH = "Minigames/WallJumping/Prefabs/SlimeEffect";

    private GameObject slimeEffectPrefab;
    private RectTransform gameAreaRectTransform;
    private GameObject slimeEffect;
    private bool isSlimeActive;
    private float remainingSlimeTime;
    private float slimeEffectDuration;

    /// <summary>
    /// Initializes SlimeGenerator.
    /// </summary>
    /// <param name="gameAreaRectTransform"></param>
    /// <param name="slimeEffectDuration"></param>
    public void Initialize(RectTransform gameAreaRectTransform, float slimeEffectDuration)
    {
        slimeEffectPrefab = Resources.Load(SLIME_EFFECT_PREFAB_PATH) as GameObject;
        this.gameAreaRectTransform = gameAreaRectTransform;
        this.slimeEffectDuration = slimeEffectDuration;
        InstantiateSlimeEffect();
        RescaleSlimeEffect();
        DisableSlimeEffect();
    }

    void Update()
    {
        if (isSlimeActive)
        {
            remainingSlimeTime -= Time.deltaTime;
            if (remainingSlimeTime <= 0f)
            {
                DisableSlimeEffect();
            }
        }
    }

    /// <summary>
    /// Instantiates SlimeEffect object.
    /// </summary>
    private void InstantiateSlimeEffect()
    {
        slimeEffect = Instantiate(slimeEffectPrefab, transform);
        slimeEffect.transform.localPosition = new Vector2(gameAreaRectTransform.sizeDelta.x / 2, gameAreaRectTransform.sizeDelta.y / 2);
    }

    /// <summary>
    /// Rescales SlimeEffect object to match Game Area.
    /// </summary>
    private void RescaleSlimeEffect()
    {
        float gameAreaWidth = gameAreaRectTransform.sizeDelta.x;
        float gameAreaHeight = gameAreaRectTransform.sizeDelta.y;
        Vector2 slimeEffectSize = slimeEffectPrefab.GetComponent<SpriteRenderer>().bounds.size;
        float slimeEffectScaleX = gameAreaWidth / slimeEffectSize.x;
        float slimeEffectScaleY = gameAreaHeight / slimeEffectSize.y;
        slimeEffect.transform.localScale = new Vector2(slimeEffectScaleX, slimeEffectScaleY);
    }

    /// <summary>
    /// Enables slime effect.
    /// </summary>
    public void EnableSlimeEffect()
    {
        slimeEffect.SetActive(true);
        isSlimeActive = true;
        remainingSlimeTime = slimeEffectDuration;
    }

    /// <summary>
    /// Disables slime effect.
    /// </summary>
    private void DisableSlimeEffect()
    {
        slimeEffect.SetActive(false);
        isSlimeActive = false;
    }
}
