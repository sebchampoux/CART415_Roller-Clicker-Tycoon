using System;

public interface IRide : IUpdatesDaily
{
    float ContributionToAdmissionFee { get; }
    int NumberOfGuestsToSpawn { get; }
}