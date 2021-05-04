using OSPABA;
using simulation;
using agents;
using Action = OSPABA.Action;

namespace instantAssistants
{
	//meta! id="92"
	public class ActionCancelPatients : Action
	{
		public ActionCancelPatients(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

        public override void Execute(MessageForm message)
		{
            MyAgent.CanceledPatientsNum =
                (int)System.Math.Round(
                    MyAgent.RandCanceledPatientsNum.Sample() * ((double)((MySimulation)MySim).OrderedPatientsNum / 540), 0);

            for (int i = 0; i < MyAgent.CanceledPatientsNum; i++)
            {
                int id = MyAgent.RandCanceledPatientsIds.Sample();
                while (MyAgent.CanceledPatientsIds.Contains(id))
                    id = MyAgent.RandCanceledPatientsIds.Sample();

                MyAgent.CanceledPatientsIds.Add(id);
            }

            MyAgent.CanceledPatientsIds.Sort();
		}
		public new AgentSurrounding MyAgent
		{
			get
			{
				return (AgentSurrounding)base.MyAgent;
			}
		}
	}
}