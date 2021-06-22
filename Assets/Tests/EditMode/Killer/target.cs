using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class target
    {
        [Test]
        public void targetSimplePasses()
        {
            Assert.AreEqual(1, 1);
        }
    }
}
