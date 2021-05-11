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
            if (((MySimulation) MySim).EnableEarlyArrivals)
            {
                message.Code = Mc.NoticePreGeneratedPatientHoldEnded;
                double holdTime = ((MySimulation) MySim).PreGeneratedPatients.First.ArrivalTime - MySim.CurrentTime < 0
                    ? 0
                    : ((MySimulation) MySim).PreGeneratedPatients.First.ArrivalTime - MySim.CurrentTime;

				Hold(holdTime, message);
            }
            else
            {
                message.Code = Mc.NoticePatientGeneratingEnded;
                if (!((MessagePatient)message).IsFirst)
                    Hold(32400.0 / ((MySimulation)MySim).OrderedPatientsNum, message);
                else
                    Hold(0, message);
			}
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
            }
		}

		//meta! sender="AgentSurrounding", id="128", type="Notice"
		public void ProcessNoticePatientGeneratingEnded(MessageForm message)
        {
            message.Code = Mc.NoticePatientGenerated;
            Notice(message);
		}

		//meta! sender="AgentSurrounding", id="132", type="Notice"
		public void ProcessNoticePreGeneratedPatientHoldEnded(MessageForm message)
		{
            message.Code = Mc.NoticePreGeneratedPatientPicked;
            Notice(message);
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.NoticePatientGeneratingEnded:
				ProcessNoticePatientGeneratingEnded(message);
			break;

			case Mc.Start:
				ProcessStart(message);
			break;

			case Mc.NoticePreGeneratedPatientHoldEnded:
				ProcessNoticePreGeneratedPatientHoldEnded(message);
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