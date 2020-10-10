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

    public void Initialize(RectTransform gameAreaRectTransform, float slimeEffectDuration)
    {
        slimeEffectPrefab = Resources.Load(SLIME_EFFECT_PREFAB_PATH) as GameObject;
        this.gameAreaRectTransform = gameAreaRectTransform;
        this.slimeEffectDuration = slimeEffectDuration;
        InstantiateSlimeEffect();
        RescaleSlimeEffect();
        DisableSlimeEffect();
    }

    public void GenerateSlimeEffect()
    {
        EnableSlimeEffect();
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

    private void InstantiateSlimeEffect()
    {
        slimeEffect = Instantiate(slimeEffectPrefab, transform);
        slimeEffect.transform.localPosition = new Vector2(gameAreaRectTransform.sizeDelta.x / 2, gameAreaRectTransform.sizeDelta.y / 2);
    }

    private void RescaleSlimeEffect()
    {
        float gameAreaWidth = gameAreaRectTransform.sizeDelta.x;
        float gameAreaHeight = gameAreaRectTransform.sizeDelta.y;
        Vector2 slimeEffectSize = slimeEffectPrefab.GetComponent<SpriteRenderer>().bounds.size;
        float slimeEffectScaleX = gameAreaWidth / slimeEffectSize.x;
        float slimeEffectScaleY = gameAreaHeight / slimeEffectSize.y;
        slimeEffect.transform.localScale = new Vector2(slimeEffectScaleX, slimeEffectScaleY);
    }

    private void EnableSlimeEffect()
    {
        slimeEffect.SetActive(true);
        isSlimeActive = true;
        remainingSlimeTime = slimeEffectDuration;
    }

    private void DisableSlimeEffect()
    {
        slimeEffect.SetActive(false);
        isSlimeActive = false;
    }
}
