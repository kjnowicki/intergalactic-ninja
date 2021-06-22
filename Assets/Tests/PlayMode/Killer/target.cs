using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class target
    {
        [UnityTest]
        public IEnumerator a()
        {
            GameObject container = new GameObject("Container");
            var gameManager = container.AddComponent<GameManagerKiller>();
            
            // var gameManager = GameManagerKiller.Instance;
            while (true)
            {
                GameObject.Instantiate()   
                var a = 5;
                yield return new WaitForSeconds(10);
                var target = GameObject.FindObjectOfType<Target>();
                if (target != null)
                    break;
            }
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
