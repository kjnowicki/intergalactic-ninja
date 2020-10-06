using UnityEngine;

public class SlimeGenerator : MonoBehaviour
{
    private const string SLIME_EFFECT_PREFAB_PATH = "Minigames/WallJumping/Prefabs/SlimeEffect";
    private GameObject slimeEffectPrefab;
    private RectTransform gameAreaRectTransform;

    public void Initialize(RectTransform gameAreaRectTransform)
    {
        slimeEffectPrefab = Resources.Load(SLIME_EFFECT_PREFAB_PATH) as GameObject;
        this.gameAreaRectTransform = gameAreaRectTransform;
        RescaleSlimeEffect();
        //slimeEffectPrefab.transform.localScale.x = 
        //slimeEffectPosition = new Ve
        //slimeEffectPrefab.transform.localPosition = Vector2.zero;
        //slimeEffectPrefab/
    }

    public void Slime()
    {
        var a = Instantiate(slimeEffectPrefab, transform);
        a.transform.localPosition = new Vector2(gameAreaRectTransform.sizeDelta.x / 2, gameAreaRectTransform.sizeDelta.y / 2);
        
        //Instantiate(slimeEffectPrefab, transform);
    }

    private void RescaleSlimeEffect()
    {
        float gameAreaWidth = gameAreaRectTransform.sizeDelta.x;
        float gameAreaHeight = gameAreaRectTransform.sizeDelta.y;
        Vector2 slimeEffectSize = slimeEffectPrefab.GetComponent<SpriteRenderer>().bounds.size;
        float slimeEffectScaleX = gameAreaWidth / slimeEffectSize.x;
        float slimeEffectScaleY = gameAreaHeight / slimeEffectSize.y;
        slimeEffectPrefab.transform.localScale = new Vector2(slimeEffectScaleX, slimeEffectScaleY);
    } 
}
