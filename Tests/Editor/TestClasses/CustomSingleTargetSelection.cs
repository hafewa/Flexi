namespace Physalia.AbilityFramework.Tests
{
    public class CustomSingleTargetChoiseContext : ChoiceContext
    {
        public CustomUnit target;
    }

    public class CustomSingleTargetAnswerContext : NodeContext
    {
        public CustomUnit target;
    }

    public class CustomSingleTargetSelection : ProcessNode
    {
        public Outport<CustomUnit> targetPort;

        protected override AbilityState DoLogic()
        {
            Instance.System.TriggerChoice(new CustomSingleTargetChoiseContext());
            return AbilityState.PAUSE;
        }

        public override bool CheckNodeContext(NodeContext nodeContext)
        {
            if (nodeContext is CustomSingleTargetAnswerContext answerContext)
            {
                if (answerContext.target != null)
                {
                    return true;
                }
            }

            return false;
        }

        protected override AbilityState ResumeLogic(NodeContext nodeContext)
        {
            var answerContext = nodeContext as CustomSingleTargetAnswerContext;
            targetPort.SetValue(answerContext.target);
            return AbilityState.RUNNING;
        }
    }
}
