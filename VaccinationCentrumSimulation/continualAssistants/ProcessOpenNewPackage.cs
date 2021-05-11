using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="161"
	public class ProcessOpenNewPackage : Process
	{
		public ProcessOpenNewPackage(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentColdStorage", id="162", type="Start"
		public void ProcessStart(MessageForm message)
        {
            message.Code = Mc.NoticeProcessOpenNewPackageEnded;
			Hold(MyAgent.RandOpenPackage.Sample(), message);
        }

		//meta! sender="AgentColdStorage", id="163", type="Notice"
		public void ProcessNoticeProcessOpenNewPackageEnded(MessageForm message)
        {
            MyAgent.VaccinesInPackageLeft = 400;
			AssistantFinished(message);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
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

			case Mc.NoticeProcessOpenNewPackageEnded:
				ProcessNoticeProcessOpenNewPackageEnded(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AgentColdStorage MyAgent
		{
			get
			{
				return (AgentColdStorage)base.MyAgent;
			}
		}
	}
}
