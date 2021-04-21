using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="16"
	public class SchedulerPatientsArrival : Scheduler
	{
		public SchedulerPatientsArrival(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
        }

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentSurrounding", id="17", type="Start"
		public void ProcessStart(MessageForm message)
        {
            message.Code = Mc.NoticePatientGenerated;
            message.Addressee = MyAgent;
            Hold(60, message);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
				case Mc.NoticePatientGenerated:
                    Notice(message);
                    break;
			}
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.Start:
				ProcessStart(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AgentSurrounding MyAgent
		{
			get
			{
				return (AgentSurrounding)base.MyAgent;
			}
		}
	}
}