using UnityEngine;

public class SlimeGenerator : MonoBehaviour
{
    private const string SLIME_EFFECT_PREFAB_PATH = "Minigames/WallJumping/Prefabs/SlimeEffect";
    private GameObject slimeEffectPrefab;

    public void Initialize()
    {
        slimeEffectPrefab = Resources.Load(SLIME_EFFECT_PREFAB_PATH) as GameObject;
    }

    public void Slime()
    {
        Instantiate(slimeEffectPrefab);
    }
}
