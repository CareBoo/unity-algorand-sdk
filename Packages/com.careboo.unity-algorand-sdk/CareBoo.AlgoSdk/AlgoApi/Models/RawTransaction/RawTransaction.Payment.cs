namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiKey("rcv")]
        public Address Receiver
        {
            get => PaymentParams.Receiver;
            set => PaymentParams.Receiver = value;
        }

        [AlgoApiKey("amt")]
        public ulong Amount
        {
            get => PaymentParams.Amount;
            set => PaymentParams.Amount = value;
        }

        [AlgoApiKey("close")]
        public Address CloseRemainderTo
        {
            get => PaymentParams.CloseRemainderTo;
            set => PaymentParams.CloseRemainderTo = value;
        }
    }
}
