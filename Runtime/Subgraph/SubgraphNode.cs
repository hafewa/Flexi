using System.Collections.Generic;

namespace Physalia.AbilityFramework
{
    public sealed class SubgraphNode : ProcessNode
    {
        private enum State { STANDBY, ENTERED, EXITED }

        public string guid;

        private AbilityGraph macroGraph;
        private State state;

        public override FlowNode Next
        {
            get
            {
                if (state == State.ENTERED)
                {
                    return macroGraph.GraphInputNode;
                }
                else if (state == State.EXITED)
                {
                    IReadOnlyList<Port> connections = next.GetConnections();
                    return connections.Count > 0 ? connections[0].Node as FlowNode : null;
                }
                else
                {
                    return null;
                }
            }
        }

        protected override AbilityState DoLogic()
        {
            if (state == State.STANDBY)
            {
                state = State.ENTERED;

                // Get graph
                macroGraph = Instance.System.GetMacroGraph(guid);
                for (var i = 0; i < macroGraph.Nodes.Count; i++)
                {
                    macroGraph.Nodes[i].instance = Instance;
                }

                // Copy inport values
                foreach (Inport inport in Inports)
                {
                    if (inport == previous)
                    {
                        continue;
                    }

                    Outport macroOutport = macroGraph.GraphInputNode.GetOutput(inport.name);
                    macroOutport.SetValueFromInport(inport);
                }

                // Push
                Instance.Graph.PushGraph(macroGraph);
            }
            else if (state == State.ENTERED)
            {
                state = State.EXITED;

                // Copy outport values
                foreach (Outport outport in Outports)
                {
                    if (outport == next)
                    {
                        continue;
                    }

                    Inport macroInport = macroGraph.GraphOutputNode.GetInput(outport.name);
                    outport.SetValueFromInport(macroInport);
                }
            }

            return AbilityState.RUNNING;
        }

        protected internal override void Reset()
        {
            macroGraph = null;
            state = State.STANDBY;
        }
    }
}
