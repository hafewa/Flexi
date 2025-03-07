using System.Collections.Generic;

namespace Physalia.Flexi.Tests
{
    [NodeCategoryForTests]
    public class CustomDamageNode : DefaultProcessNode
    {
        public Inport<CustomUnit> instigatorPort;
        public Inport<List<CustomUnit>> targets;
        public Inport<int> baseValue;

        protected override FlowState OnExecute()
        {
            List<CustomUnit> list = targets.GetValue();
            int damage = baseValue.GetValue();
            for (var i = 0; i < list.Count; i++)
            {
                Stat stat = list[i].GetStat(CustomStats.HEALTH);
                stat.CurrentBase -= damage;

                EnqueueEvent(new CustomDamageEventNode.Context
                {
                    instigator = instigatorPort.GetValue(),
                    target = list[i],
                });
            }

            return FlowState.Success;
        }
    }
}
