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

    public static class BoxApp
    {
        public const string Approval = @"
#pragma version 8
txn ApplicationID
bz end
txn ApplicationArgs 0   // [arg[0]] // fails if no args && app already exists
byte ""create""			// [arg[0], ""create""] // create box named arg[1]
==                      // [arg[0]=?=""create""]
bz del                  // ""create"" ? continue : goto del
int 24                  // [24]
txn NumAppArgs          // [24, NumAppArgs]
int 2                   // [24, NumAppArgs, 2]
==                      // [24, NumAppArgs=?=2]
bnz default             // THIS IS POORLY BEHAVED WHEN ""create"" && NumAppArgs != 2
pop						// get rid of 24 // NumAppArgs != 2
txn ApplicationArgs 2   // [arg[2]]
btoi                    // [btoi(arg[2])]
default:                    // [24] // NumAppArgs == 2
txn ApplicationArgs 1   // [24, arg[1]]
swap
box_create              // [] // boxes: arg[1] -> [24]byte
assert
b end
del:						// delete box arg[1]
txn ApplicationArgs 0   // [arg[0]]
byte ""delete""           // [arg[0], ""delete""]
==                      // [arg[0]=?=""delete""]
bz set                  // ""delete"" ? continue : goto set
txn ApplicationArgs 1   // [arg[1]]
box_del                 // del boxes[arg[1]]
assert
b end
set:						// put arg[1] at start of box arg[0] ... so actually a _partial_ ""set""
txn ApplicationArgs 0   // [arg[0]]
byte ""set""              // [arg[0], ""set""]
==                      // [arg[0]=?=""set""]
bz test                 // ""set"" ? continue : goto test
txn ApplicationArgs 1   // [arg[1]]
int 0                   // [arg[1], 0]
txn ApplicationArgs 2   // [arg[1], 0, arg[2]]
box_replace             // [] // boxes: arg[1] -> replace(boxes[arg[1]], 0, arg[2])
b end
test:						// fail unless arg[2] is the prefix of box arg[1]
txn ApplicationArgs 0   // [arg[0]]
byte ""check""            // [arg[0], ""check""]
==                      // [arg[0]=?=""check""]
bz bad                  // ""check"" ? continue : goto bad
txn ApplicationArgs 1   // [arg[1]]
int 0                   // [arg[1], 0]
txn ApplicationArgs 2   // [arg[1], 0, arg[2]]
len                     // [arg[1], 0, len(arg[2])]
box_extract             // [ boxes[arg[1]][0:len(arg[2])] ]
txn ApplicationArgs 2   // [ boxes[arg[1]][0:len(arg[2])], arg[2] ]
==                      // [ boxes[arg[1]][0:len(arg[2])]=?=arg[2] ]
assert                  // boxes[arg[1]].startwith(arg[2]) ? pop : ERROR
b end
bad:
err
end:
int 1";

        public const string ClearState = @"
#pragma version 8
int 8
";

        public static readonly byte[] ApprovalBytes = Encoding.UTF8.GetBytes(Approval);
        public static readonly byte[] ClearBytes = Encoding.UTF8.GetBytes(ClearState);
    }

}
