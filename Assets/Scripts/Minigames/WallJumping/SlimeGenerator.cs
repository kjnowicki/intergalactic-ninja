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
}
