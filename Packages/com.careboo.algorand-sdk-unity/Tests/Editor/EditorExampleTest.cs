using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace CareBoo.Algorand.SDK.Unity.Editor.Tests
{
    class EditorExampleTest
    {
        [Test]
        public void EditorSampleTestSimplePasses()
        {
            var expected = 4;
            var actual = 2 + 2;
            Assert.AreEqual(expected, actual);
        }

        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [UnityTest]
        public IEnumerator EditorSampleTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // yield to skip a frame
            yield return null;
        }
    }
}
