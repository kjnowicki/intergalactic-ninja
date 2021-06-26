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
        private GameObject targetContainer;
        private Target targetPrefab;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            var mockGameOverObject = new GameObject("MockGameOverObject");
            
            var gameManagerGameObject = new GameObject("GameManager");
            var gameManager = gameManagerGameObject.AddComponent<GameManagerKiller>();
            gameManager.targetTag = "Target";
            gameManager.gameOverObejct = mockGameOverObject;
            
            targetContainer = new GameObject("TargetContainer");
            targetPrefab = Resources.Load<Target>("Minigames/Killer/Prefabs/Target");
        }
        
        [UnityTest]
        public IEnumerator DoesNotDisappearInstantly()
        {
            // Act
            var target = GameObject.Instantiate(targetPrefab, Vector2.zero, Quaternion.identity, targetContainer.transform);

            // Assert
            Assert.IsNotNull(target, "Target should not be null");
            yield return null;
        }

        [UnityTest]
        public IEnumerator DisappearsAfterLifetimeEnds()
        {
            // Arrange
            const int TARGET_LIFE_TIME = 5;
            const float TOLERANCE = 0.5f;

            // Act
            var target = GameObject.Instantiate(targetPrefab, Vector2.zero, Quaternion.identity, targetContainer.transform);
            target.LifeTime = TARGET_LIFE_TIME;
            yield return new WaitForSeconds(TARGET_LIFE_TIME + TOLERANCE);
            
            // Assert
            Assert.IsTrue(target == null, "Target should be null");
            yield return null;
        }
    }
}
