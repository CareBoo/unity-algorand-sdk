using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlgoSdk.Serialization
{
    public enum JsonReadError
    {
        None,
        UnknownError,
        ParseError,
        IncorrectFormat,
        IncorrectType,
    }
}
