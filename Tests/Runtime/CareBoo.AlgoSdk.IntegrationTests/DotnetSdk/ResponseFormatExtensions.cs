using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Algorand;
using Algorand.Algod;
using AlgoSdk;
using System.Linq;

public static class DotnetExtensions
{
    public static Algorand.Algod.Model.Format? ToDotNetFormat(this ResponseFormat responseFormat)
    {
        switch (responseFormat)
        {
            case ResponseFormat.Json:
                return Algorand.Algod.Model.Format.Json;
            case ResponseFormat.MessagePack:
                return Algorand.Algod.Model.Format.Msgpack;
            default:
                return null;
        }
    }

    public static MsgPackConvert<T> Convert<T>(this T from)
    {
        return new MsgPackConvert<T>(from);
    }
}

public readonly struct MsgPackConvert<T>
{
    readonly T value;

    public MsgPackConvert(T value)
    {
        this.value = value;
    }

    public U To<U>()
    {
        var msgpack = AlgoApiSerializer.SerializeMessagePack(value);
        return Algorand.Utils.Encoder.DecodeFromMsgPack<U>(msgpack);
    }
}
