using OSPABA;
using simulation;
using managers;
using continualAssistants;
using OSPRNG;

//using instantAssistants;

namespace agents
{
	//meta! id="47"
	public class AgentCanteen : Agent
	{
        public int EatingEmployeesCount { get; set; }
        public TriangularRNG RandEatingTime { get; set; }
		public AgentCanteen(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();

            RandEatingTime = new TriangularRNG(300, 900, 1800, ((MySimulation)MySim).RandSeedGenerator);
        }

		override public void PrepareReplication()
		{
			base.PrepareReplication();

            EatingEmployeesCount = 0;
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerCanteen(SimId.ManagerCanteen, MySim, this);
			new ProcessEating(SimId.ProcessEating, MySim, this);
			AddOwnMessage(Mc.NoticeProcessEatingEnded);
			AddOwnMessage(Mc.RequestEmployeeLunch);
		}
		//meta! tag="end"
	}
}