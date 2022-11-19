using System.Text;

public static class TealCodeCases
{
    public static class SmartContract
    {
        public const string ApprovalSrc =
@"#pragma version 4
txn ApplicationID
int 0
==
bnz main_l12
txn OnCompletion
int DeleteApplication
==
bnz main_l11
txn OnCompletion
int UpdateApplication
==
bnz main_l10
txn OnCompletion
int CloseOut
==
bnz main_l9
txn OnCompletion
int OptIn
==
bnz main_l8
txn OnCompletion
int NoOp
==
bnz main_l7
err
main_l7:
int 1
return
main_l8:
int 1
return
main_l9:
int 1
return
main_l10:
txn Sender
global CreatorAddress
==
return
main_l11:
txn Sender
global CreatorAddress
==
return
main_l12:
int 1
return";

        public static readonly byte[] ApprovalBytes = Encoding.UTF8.GetBytes(ApprovalSrc);

        public const string ClearStateSrc =
@"#pragma version 4
int 1
return";

        public static readonly byte[] ClearStateBytes = Encoding.UTF8.GetBytes(ClearStateSrc);
    }

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

        public static readonly byte[] SrcBytes = Encoding.UTF8.GetBytes(Src);

        public const string CompiledResult = "AiAD6AcBuBcmAyD2TmOfueim8Re2ESV2GjKqvwSScfbZtuOlSKmBnd39ZgrW9b1vW9b1vW9bIP56+ztC8srYFMeypK6fn0Qf1ybaZs4NQfeeN2m1/W00MQEiDDEQIxIxCTIDEhAxIDIDEhAQMQcoEi0BKRIQMQcqEjECJA0QERBD";

        public const string CompiledHash = "VSFC4BJGTENSXD6D5XI7RVXVJO5J7ORWODEWK3V7U65CRXBVAWMZI3M6JI";
    }

}
