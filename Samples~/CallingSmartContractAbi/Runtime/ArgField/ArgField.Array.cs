using System.Collections.Generic;
using System.Linq;
using AlgoSdk.Experimental.Abi;
using UnityEngine.UIElements;

public partial class ArgField
{
    public sealed class Array : ArgField
    {
        private readonly ListView list;

        private readonly List<ArgField> elements;

        public override IAbiValue Value => new Array<IAbiValue>(elements.Select(a => a.Value).ToArray());

        public Array(string label, VariableArrayType type)
        {
            elements = new List<ArgField>();
            list = new ListView(
                elements,
                makeItem: () => ArgField.Create(null, type.ElementType),
                bindItem: (argField, index) =>
                {
                    while (index >= elements.Count)
                    {
                        elements.Add(null);
                    }
                    elements[index] = (ArgField)argField;
                })
            {
                headerTitle = label,
                showFoldoutHeader = true,
                showBoundCollectionSize = true,
                showAddRemoveFooter = false,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
            };
            contentContainer.Add(list);
        }
    }
}
