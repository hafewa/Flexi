namespace Physalia.Flexi.Tests
{
    [NodeCategoryForTests]
    public class CustomActivationNode : DefaultEntryNode<CustomActivationNode.Context>
    {
        public class Context : IEventContext
        {
            public CustomUnit activator;
        }

        public Outport<CustomUnit> activatorPort;

        protected internal override bool CanExecute(Context context)
        {
            if (context.activator != null)
            {
                return true;
            }

            return false;
        }

        protected override FlowState OnExecute(Context context)
        {
            activatorPort.SetValue(context.activator);
            return FlowState.Success;
        }
    }
}
