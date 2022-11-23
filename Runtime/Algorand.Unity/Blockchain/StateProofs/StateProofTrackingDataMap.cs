using System;

namespace Algorand.Unity
{
    [AlgoApiObject]
    [Serializable]
    public partial struct StateProofTrackingDataMap
        : IEquatable<StateProofTrackingDataMap>
    {
        [AlgoApiField("0")]
        public StateProofTrackingData StateProofBasicData;

        public StateProofTrackingData this[StateProofType type]
        {
            get
            {
                switch (type)
                {
                    case StateProofType.StateProofBasic:
                        return StateProofBasicData;
                    default:
                        throw new System.NotSupportedException(type.ToString());
                }
            }
            set
            {
                switch (type)
                {
                    case StateProofType.StateProofBasic:
                        StateProofBasicData = value;
                        break;
                    default:
                        throw new System.NotSupportedException(type.ToString());
                }
            }
        }

        public bool Equals(StateProofTrackingDataMap other)
        {
            return StateProofBasicData.Equals(other.StateProofBasicData);
        }
    }
}
