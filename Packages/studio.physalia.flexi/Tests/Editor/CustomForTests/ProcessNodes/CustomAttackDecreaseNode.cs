using System.Collections.Generic;

namespace Physalia.Flexi.Tests
{
    [NodeCategoryForTests]
    public class CustomAttackDecreaseNode : DefaultProcessNode
    {
        public Inport<List<CustomUnit>> targets;
        public Inport<int> baseValue;

        protected override FlowState OnExecute()
        {
            List<CustomUnit> list = targets.GetValue();
            int damage = baseValue.GetValue();
            for (var i = 0; i < list.Count; i++)
            {
                Stat stat = list[i].GetStat(CustomStats.ATTACK);
                stat.CurrentBase -= damage;
            }

            return FlowState.Success;
        }
    }
}
