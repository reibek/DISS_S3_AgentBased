using OSPABA;
using simulation;
using managers;
using continualAssistants;
//using instantAssistants;
namespace agents
{
	//meta! id="7"
	public class AgentWaitingRoom : Agent
	{
        public int WaitingPatientsCount { get; set; }
		public AgentWaitingRoom(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();

            AddOwnMessage(Mc.ProcessWaitingRoomEnded);
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();

            WaitingPatientsCount = 0;
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerWaitingRoom(SimId.ManagerWaitingRoom, MySim, this);
			new ProcessWaitingRoom(SimId.ProcessWaitingRoom, MySim, this);
			AddOwnMessage(Mc.RequestWaitingRoom);
		}
		//meta! tag="end"
	}
}