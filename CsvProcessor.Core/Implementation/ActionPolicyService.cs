using System;
using CsvProcessor.Core.Enum;
using CsvProcessor.Core.Interfaces;
using CsvProcessor.Core.Models;

namespace CsvProcessor.Core.Implementation;

public class ActionPolicyService : IActionPolicy
{
    public Actions ApplyAction(double similarity, ActionPolicy actionPolicy)
    {
        foreach (var tier in actionPolicy.Tiers)
        {
            if (similarity > tier.MinThreshold)
            {
                return tier.Action;
            }
        }
        return Actions.Remove;

    }
}
