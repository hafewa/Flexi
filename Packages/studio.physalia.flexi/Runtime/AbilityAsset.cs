using System;
using System.Collections.Generic;
using UnityEngine;

namespace Physalia.Flexi
{
    /// <summary>
    /// An AbilityAsset preserves an ability data. This asset is only used for Unity serialization.
    /// It can create <see cref="AbilityData"/> for runtime usage.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAbilityAsset", menuName = "Flexi/Ability Asset", order = 151)]
    public sealed class AbilityAsset : GraphAsset
    {
        [SerializeField]
        private List<BlackboardVariable> blackboard = new();
        [HideInInspector]
        [SerializeField]
        private List<AbilityGraphGroup> graphGroups = new();

        [NonSerialized]
        private AbilityData abilityData;

        internal List<BlackboardVariable> Blackboard
        {
            get
            {
                return blackboard;
            }
            set
            {
                blackboard.Clear();
                if (value != null)
                {
                    // Clone each variable to prevent modify the source
                    for (var i = 0; i < value.Count; i++)
                    {
                        blackboard.Add(value[i].Clone());
                    }
                }
            }
        }

        internal List<AbilityGraphGroup> GraphGroups => graphGroups;

        public AbilityData Data
        {
            get
            {
                abilityData ??= CreateInstance();
                return abilityData;
            }
        }

        private AbilityData CreateInstance()
        {
            var abilityData = new AbilityData { name = name };
            for (var i = 0; i < blackboard.Count; i++)
            {
                abilityData.blackboard.Add(blackboard[i].Clone());
            }

            for (var i = 0; i < graphGroups.Count; i++)
            {
                abilityData.graphGroups.Add(graphGroups[i].Clone());
            }

            return abilityData;
        }
    }
}
