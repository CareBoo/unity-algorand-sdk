using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

[InitializeOnLoad]
public class ConditionalIgnores
{
    static ConditionalIgnores()
    {
        ConditionalIgnoreAttribute.AddConditionalIgnoreMapping(nameof(Application.isBatchMode), Application.isBatchMode);
    }
}
