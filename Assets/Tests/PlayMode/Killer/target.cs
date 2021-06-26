using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class target
    {
        [UnityTest]
        public IEnumerator disappearsAfterLifetimeEnds()
        {
            var mockGameOverObject = new GameObject("MockGameOverObject");
            
            var gameManagerGameObject = new GameObject("GameManager");
            var gameManager = gameManagerGameObject.AddComponent<GameManagerKiller>();
            gameManager.targetTag = "Target";
            gameManager.gameOverObejct = mockGameOverObject;
            
            var targetContainer = new GameObject("TargetContainer");
            Target targetPrefab = Resources.Load<Target>("Minigames/Killer/Prefabs/Target");
            var target = GameObject.Instantiate(targetPrefab, Vector2.zero, Quaternion.identity, targetContainer.transform);
            target.LifeTime = 5;


            // GameObject gameObject = new GameObject("Target");
            // var target = gameObject.AddComponent<Target>();
            // Assert.IsNotNull(target);
            // GameObject.Instantiate(new Target(), Vector2.zero, Quaternion.identity, container.transform);
            yield return new WaitForSeconds(10);
            // var b = 5;
            Assert.IsNull(target);
            // Assert.IsNull(gameObject);
            // var gameManager = GameManagerKiller.Instance;
            // while (true)
            // {
            //     // GameObject.Instantiate()   
            //     var a = 5;
            //     yield return new WaitForSeconds(10);
            //     var target = GameObject.FindObjectOfType<Target>();
            //     if (target != null)
            //         break;
            // }
            Assert.Pass();
            // var a = 5;
            yield return null;
        }
        
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator targetWithEnumeratorPasses()
        {
            
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
