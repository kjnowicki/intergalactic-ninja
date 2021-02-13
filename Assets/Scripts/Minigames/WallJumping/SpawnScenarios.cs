using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnScenarios : MonoBehaviour
{
    private const int defaultScenarioSize = 4;
    [SerializeField] ScenariosList scenarios = new ScenariosList(defaultScenarioSize);

    public WallSpawnScenario GetRandomScenario()
    {
        return scenarios.GetRandomScenario();
    }
}
