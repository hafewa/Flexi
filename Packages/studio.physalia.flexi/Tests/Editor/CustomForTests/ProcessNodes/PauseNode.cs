namespace Physalia.Flexi.Tests
{
    [NodeCategoryForTests]
    public class PauseNode : DefaultProcessNode
    {
        protected override FlowState OnExecute()
        {
            return FlowState.Pause;
        }

        protected internal override bool CanResume(IResumeContext resumeContext)
        {
            return true;
        }

        protected override FlowState OnResume(IResumeContext resumeContext)
        {
            return FlowState.Success;
        }
    }
}
