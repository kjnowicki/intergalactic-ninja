using System.IO;
using UnityEngine;

public class SlimeGenerator : MonoBehaviour
{
    private const string SLIME_EFFECT_PREFAB_PATH = "Minigames/WallJumping/Prefabs/SlimeEffect";
    private GameObject slimeEffectPrefab;
    private RectTransform gameAreaRectTransform;
    private GameObject slimeEffect;
    private bool isSlimeActive;

    public void Initialize(RectTransform gameAreaRectTransform)
    {
        slimeEffectPrefab = Resources.Load(SLIME_EFFECT_PREFAB_PATH) as GameObject;
        this.gameAreaRectTransform = gameAreaRectTransform;
        InstantiateSlimeEffect();
        RescaleSlimeEffect();
    }

    public void Slime()
    {
        slimeEffect.SetActive(true);
        isSlimeActive = true;
    }

    private void InstantiateSlimeEffect()
    {
        slimeEffect = Instantiate(slimeEffectPrefab, transform);
        slimeEffect.transform.localPosition = new Vector2(gameAreaRectTransform.sizeDelta.x / 2, gameAreaRectTransform.sizeDelta.y / 2);
        slimeEffect.SetActive(false);
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
}
