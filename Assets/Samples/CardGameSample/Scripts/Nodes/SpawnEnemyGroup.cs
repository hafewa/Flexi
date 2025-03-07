using System.Collections.Generic;

namespace Physalia.Flexi.Samples.CardGame
{
    [NodeCategory("Card Game Sample")]
    public class SpawnEnemyGroup : DefaultProcessNode
    {
        public Inport<Game> gamePort;

        protected override FlowState OnExecute()
        {
            Game game = gamePort.GetValue();
            IReadOnlyList<Unit> units = game.RandomGenerateEnemyGroup();
            EnqueueEvent(new UnitSpawnedEvent { units = units });
            return FlowState.Success;
        }
    }
}
