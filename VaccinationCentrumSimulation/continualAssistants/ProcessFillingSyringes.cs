using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="80"
	public class ProcessFillingSyringes : Process
	{
		public ProcessFillingSyringes(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentColdStorage", id="81", type="Start"
		public void ProcessStart(MessageForm message)
        {
			var nurse = ((MessageNurse)message).Nurse;
            double fillingTime = 0.0;

            for (int i = 0; i < 20; i++)
            {
                fillingTime += nurse.RandFillSyringeTime.Sample();
            }

            message.Code = Mc.NoticeProcessFillingSyringesEnded;
            
            if (((MySimulation)MySim).EnableLightModel)
                Hold(0, message);
            else
                Hold(fillingTime, message);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
            }
		}

		//meta! sender="AgentColdStorage", id="153", type="Notice"
		public void ProcessNoticeProcessFillingSyringesEnded(MessageForm message)
        {
            MyAgent.VaccinesInPackageLeft -= 20;
            AssistantFinished(message);
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.NoticeProcessFillingSyringesEnded:
				ProcessNoticeProcessFillingSyringesEnded(message);
			break;

			case Mc.Start:
				ProcessStart(message);
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