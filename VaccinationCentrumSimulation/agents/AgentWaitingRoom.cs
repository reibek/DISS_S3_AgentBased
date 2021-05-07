using OSPABA;
using simulation;
using managers;
using continualAssistants;
using OSPStat;

//using instantAssistants;
namespace agents
{
	//meta! id="7"
	public class AgentWaitingRoom : Agent
	{
        public int WaitingPatientsCount { get; set; }
        public WStat StatWaitingPatientsCount { get; set; }
		public AgentWaitingRoom(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();

            StatWaitingPatientsCount = new WStat(MySim);
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();

            WaitingPatientsCount = 0;
			StatWaitingPatientsCount.Clear();
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerWaitingRoom(SimId.ManagerWaitingRoom, MySim, this);
			new ProcessWaitingRoom(SimId.ProcessWaitingRoom, MySim, this);
			AddOwnMessage(Mc.RequestWaitingRoom);
			AddOwnMessage(Mc.NoticeProcessWaitingRoomEnded);
		}
		//meta! tag="end"

        public void FinalUpdateStatistics()
        {
            StatWaitingPatientsCount.AddSample(WaitingPatientsCount);
        }
	}
}