namespace Physalia.Flexi
{
    [NodeCategory(BuiltInCategory.Entry)]
    internal class OnCollectModifierNode : EntryNode<AbilityContainer, OnCollectModifierNode.Context>
    {
        public sealed class Context : IEventContext
        {
            public static Context Instance { get; } = new Context();

            // Empty Content
        }

        public Variable<int> order;

        protected internal override bool CanExecute(Context context)
        {
            return true;
        }

        protected override FlowState OnExecute(Context context)
        {
            return FlowState.Success;
        }
    }
}
