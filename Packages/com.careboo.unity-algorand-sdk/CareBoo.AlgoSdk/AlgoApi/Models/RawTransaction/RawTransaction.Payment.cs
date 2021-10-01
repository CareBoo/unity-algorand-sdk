namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiKey(null, "rcv")]
        public Address Receiver
        {
            get => PaymentParams.Receiver;
            set => PaymentParams.Receiver = value;
        }

        [AlgoApiKey(null, "amt")]
        public ulong Amount
        {
            get => PaymentParams.Amount;
            set => PaymentParams.Amount = value;
        }

        [AlgoApiKey(null, "close")]
        public Address CloseRemainderTo
        {
            get => PaymentParams.CloseRemainderTo;
            set => PaymentParams.CloseRemainderTo = value;
        }
    }
}
