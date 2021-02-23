using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnScenarios : MonoBehaviour
{
    private const int DEFAULT_SCENARIOS_SIZE = 6;
    [SerializeField] private ScenariosList scenarios = new ScenariosList(DEFAULT_SCENARIOS_SIZE);

    public void Awake()
    {
        for (int _ = 0; _ < scenarios.GetScenariosCount(); _++)
        {

        }
    }
    public WallSpawnScenario GetRandomScenario()
    {
        return scenarios.GetRandomScenarioCopy();
    }
}
