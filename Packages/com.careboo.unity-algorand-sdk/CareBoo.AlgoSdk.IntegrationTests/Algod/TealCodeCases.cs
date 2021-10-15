public static class TealCodeCases
{
    public static class AtomicSwap
    {
        public const string Src =
@"#pragma version 2
txn Fee
int 1000
<
txn TypeEnum
int pay
==


txn CloseRemainderTo
global ZeroAddress
==
&&
txn RekeyTo
global ZeroAddress
==
&&
&&
txn Receiver
addr 6ZHGHH5Z5CTPCF5WCESXMGRSVK7QJETR63M3NY5FJCUYDHO57VTCMJOBGY
==
arg 0
sha256
byte base32(2323232323232323)
==
&&
txn Receiver
addr 7Z5PWO2C6LFNQFGHWKSK5H47IQP5OJW2M3HA2QPXTY3WTNP5NU2MHBW27M
==
txn FirstValid
int 3000
>
&&
||
&&
return";

        public const string CompiledResult = "AiAD6AcBuBcmAyD2TmOfueim8Re2ESV2GjKqvwSScfbZtuOlSKmBnd39ZgrW9b1vW9b1vW9bIP56+ztC8srYFMeypK6fn0Qf1ybaZs4NQfeeN2m1/W00MQEiDDEQIxIxCTIDEhAxIDIDEhAQMQcoEi0BKRIQMQcqEjECJA0QERBD";

        public const string CompiledHash = "VSFC4BJGTENSXD6D5XI7RVXVJO5J7ORWODEWK3V7U65CRXBVAWMZI3M6JI";
    }

}
