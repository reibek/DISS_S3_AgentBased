using System.Linq;
using OSPABA;
using simulation;
using agents;
using continualAssistants;
using entities;

//using instantAssistants;

namespace managers
{
	//meta! id="2"
	public class ManagerSurrounding : Manager
	{
		public ManagerSurrounding(int id, Simulation mySim, Agent myAgent) :
			base(id, mySim, myAgent)
		{
			Init();
		}

        public override void PrepareReplication()
        {
            base.PrepareReplication();
        }

		//meta! sender="SchedulerPatientsArrival", id="17", type="Finish"
		public void ProcessFinish(MessageForm message)
        {
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
				case Mc.NoticePatientGenerated:
					ProcessNoticePatientGenerated(message);
                    break;
			}
		}

		//meta! sender="AgentModel", id="42", type="Call"
		public void ProcessInitialization(MessageForm message)
        {
            message.Addressee = MyAgent.FindAssistant(SimId.SchedulerPatientsArrival);
			StartContinualAssistant(message);
		}

		//meta! userInfo="Removed from model"
		public void ProcessNoticePatientGenerated(MessageForm message)
        {
            MyAgent.PatientsCount++;
            var patient = new EntityPatient(MyAgent.PatientsCount, MySim);
            if (MyAgent.CanceledPatientsIds.Count > 0 
                && patient.Id == MyAgent.CanceledPatientsIds.First())
            {
				message.Code = Mc.NoticePatientLeave;
				MyAgent.CanceledPatientsIds.RemoveAt(0);
                Notice(new MessagePatient(message));
            } 
            else
            {
                ((MessagePatient)message).Patient = patient;
                message.Addressee = MySim.FindAgent(SimId.AgentModel);
                message.Code = Mc.NoticePatientArrival;
                Notice(new MessagePatient(message));
            }

            if (MyAgent.PatientsCount == ((MySimulation)MySim).OrderedPatientsNum) return;

            message.Addressee = MyAgent.FindAssistant(SimId.SchedulerPatientsArrival);
            StartContinualAssistant(message);
        }

		//meta! sender="AgentModel", id="85", type="Notice"
		public void ProcessNoticePatientLeave(MessageForm message)
		{
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		public void Init()
		{
		}

		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.Finish:
				ProcessFinish(message);
			break;

			case Mc.NoticePatientLeave:
				ProcessNoticePatientLeave(message);
			break;

			case Mc.Initialization:
				ProcessInitialization(message);
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