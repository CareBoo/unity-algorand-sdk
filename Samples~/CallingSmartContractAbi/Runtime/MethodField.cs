using Algorand.Unity.Experimental.Abi;
using UnityEngine.UIElements;

namespace Algorand.Unity.Samples.CallingSmartContractAbi
{
    public class MethodField : VisualElement
    {
        private readonly ArgField[] argumentFields;

        public MethodField(Method method)
        {
            argumentFields = new ArgField[method.Arguments.Length];

            var argFoldout = new Foldout { text = "Arguments" };
            Add(argFoldout);

            for (var i = 0; i < method.Arguments.Length; i++)
            {
                var arg = method.Arguments[i];
                var argField = ArgField.Create(arg.Name, arg.Type);
                argumentFields[i] = argField;
                argFoldout.contentContainer.Add(argField);
            }
        }

        public IAbiValue[] GetAbiValues()
        {
            var abiValues = new IAbiValue[argumentFields.Length];
            for (var i = 0; i < abiValues.Length; i++)
            {
                abiValues[i] = argumentFields[i].Value;
            }

            return abiValues;
        }
    }
}