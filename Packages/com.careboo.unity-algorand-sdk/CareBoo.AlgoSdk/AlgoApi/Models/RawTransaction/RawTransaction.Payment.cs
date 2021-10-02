namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiField(null, "rcv")]
        public Address Receiver
        {
            get => PaymentParams.Receiver;
            set => PaymentParams.Receiver = value;
        }

        [AlgoApiField(null, "amt")]
        public ulong Amount
        {
            get => PaymentParams.Amount;
            set => PaymentParams.Amount = value;
        }

        [AlgoApiField(null, "close")]
        public Address CloseRemainderTo
        {
            get => PaymentParams.CloseRemainderTo;
            set => PaymentParams.CloseRemainderTo = value;
        }
    }
}
