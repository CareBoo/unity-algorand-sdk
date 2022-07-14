using System;
using System.Collections.Generic;
using AlgoSdk.Experimental.Abi;
using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;

public class ContractField : VisualElement
{
#if UNITY_RUNTIME_UI_ELEMENTS
    private readonly DropdownField selectedMethod;
#endif
    private readonly Button callMethod;
    private readonly MethodField[] methodFields;
    private readonly Label returnLabel;

    private readonly Func<int, IAbiValue[], UniTask<string>> onMethodCall;

    public ContractField(Contract contract, Func<int, IAbiValue[], UniTask<string>> onMethodCall)
    {
        this.onMethodCall = onMethodCall;
        var methodChoices = new List<string>();
        methodFields = new MethodField[contract.Methods.Length];
        for (var i = 0; i < methodFields.Length; i++)
        {
            methodChoices.Add(contract.Methods[i].Name);
            methodFields[i] = new MethodField(contract.Methods[i]);
            methodFields[i].style.display = i > 0 ? DisplayStyle.None : DisplayStyle.Flex;
        }

#if UNITY_RUNTIME_UI_ELEMENTS
        selectedMethod = new DropdownField("Method", methodChoices, 0);
        selectedMethod.RegisterValueChangedCallback(evt =>
        {
            for (var i = 0; i < methodFields.Length; i++)
            {
                if (evt.newValue == contract.Methods[i].Name)
                {
                    methodFields[i].style.display = DisplayStyle.Flex;
                }
                else
                {
                    methodFields[i].style.display = DisplayStyle.None;
                }
            }
        });
        Add(selectedMethod);
#endif

        callMethod = new Button(CallMethod)
        { text = "Call Method" };

        returnLabel = new Label("returned: void");

        foreach (var methodField in methodFields)
        {
            Add(methodField);
        }
        Add(callMethod);
        Add(returnLabel);
    }

    private void CallMethod()
    {
        CallMethodAsync().Forget();
    }

    private async UniTaskVoid CallMethodAsync()
    {
        returnLabel.text = "Calling method...";
        var index = selectedMethod.index;
        var abiValues = methodFields[index].GetAbiValues();
        returnLabel.text = await onMethodCall.Invoke(index, abiValues);
    }
}
