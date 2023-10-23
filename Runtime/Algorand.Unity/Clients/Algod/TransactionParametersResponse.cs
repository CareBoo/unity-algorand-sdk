namespace Algorand.Unity.Algod
{
    public partial struct TransactionParametersResponse
    {
        public static implicit operator TransactionParams(TransactionParametersResponse response)
        {
            return new TransactionParams
            {
                ConsensusVersion = response.ConsensusVersion,
                Fee = response.Fee,
                GenesisHash = response.GenesisHash,
                GenesisId = response.GenesisId,
                LastRound = response.LastRound,
                MinFee = response.MinFee,
            };
        }
    }
}
