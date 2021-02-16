using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum WallBlocks { baseWallBlock, spikeWallBlock, slimeWallBlock}

[System.Serializable]
public class WallSpawnScenario
{

    [SerializeField] List<WallBlocks> leftWallScenario;
    [SerializeField] List<WallBlocks> rightWallScenario;

    [SerializeField] public int weight = 1;

    public WallSpawnScenario()
    {
        leftWallScenario = new List<WallBlocks>();
        rightWallScenario = new List<WallBlocks>();
    }

    public WallSpawnScenario(int scenariolLength)
    {
        leftWallScenario = new List<WallBlocks>(new WallBlocks[scenariolLength]);
        rightWallScenario = new List<WallBlocks>(new WallBlocks[scenariolLength]);
    }

    public (WallBlocks?, WallBlocks?) GetNextBlocks()
    {
        if (leftWallScenario.Count > 0 && rightWallScenario.Count > 0)
        {
            var leftBlock = leftWallScenario[leftWallScenario.Count - 1];
            var rightBlock = rightWallScenario[rightWallScenario.Count - 1];        
            return (leftBlock, rightBlock);
        }
        return (null, null);
    } 

    public void RemoveLastPairOfBlocks()
    {
        leftWallScenario.RemoveAt(leftWallScenario.Count - 1);
        rightWallScenario.RemoveAt(rightWallScenario.Count - 1);
    }

    /// <summary>
    /// Copys scenario
    /// </summary>
    /// <returns></returns>
    public WallSpawnScenario Copy()
    {
        WallSpawnScenario newScenario = new WallSpawnScenario(leftWallScenario.Count);

        newScenario.leftWallScenario = leftWallScenario.Select(x => x).ToList();
        newScenario.rightWallScenario = rightWallScenario.Select(x => x).ToList();

        return newScenario;
    }
}

[System.Serializable]
public class ScenariosList
{
    [SerializeField] private List<WallSpawnScenario> listOfScenarios;

    private int weightSum;

    public ScenariosList(int scenariolLength)
    {
        listOfScenarios = new List<WallSpawnScenario>
        {
            new WallSpawnScenario(scenariolLength)
        };
    }
    /// <summary>
    /// Rolls new scenario based on wieghts
    /// </summary>
    /// <returns>Copy of scenario</returns>
    public WallSpawnScenario GetRandomScenarioCopy()
    {
        weightSum = listOfScenarios.Aggregate(0, (acc, x) => acc + x.weight);

        System.Random rnd = new System.Random();
        //WallSpawnScenario scenario = new WallSpawnScenario();
        var number = rnd.Next(weightSum);
        foreach( WallSpawnScenario scenario in listOfScenarios)
        {
            number -= scenario.weight;
            if (number < 0)
                return scenario.Copy();
        }
        return listOfScenarios[0];
    }
}