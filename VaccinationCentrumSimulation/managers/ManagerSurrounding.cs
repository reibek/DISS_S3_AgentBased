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
            }
		}

		//meta! sender="AgentModel", id="42", type="Call"
		public void ProcessInitialization(MessageForm message)
        {
            if (((MySimulation)MySim).EnableEarlyArrivals)
            {
                message.Addressee = MyAgent.FindAssistant(SimId.ActionPatientsWithEarlyArrival);
				Execute(message);
            }
            message.Addressee = MyAgent.FindAssistant(SimId.ActionCancelPatients);
			Execute(message);

            message.Addressee = MyAgent.FindAssistant(SimId.SchedulerPatientsArrival);
            ((MessagePatient) message).IsFirst = true;
			StartContinualAssistant(message);
		}

		//meta! sender="AgentModel", id="85", type="Notice"
		public void ProcessNoticePatientLeave(MessageForm message)
        {
            MyAgent.OutPatientsCount++;

            if (MyAgent.InPatientsCount == MyAgent.OutPatientsCount
                && MySim.CurrentTime > 32400.0)
            {
                ((MySimulation)MySim).FinalUpdateStatistics();
                ((MySimulation)MySim).CurrentReplicationDuration = MySim.CurrentTime;
                MySim.StopReplication();
            }
		}

		//meta! sender="SchedulerPatientsArrival", id="133", type="Notice"
		public void ProcessNoticePatientGenerated(MessageForm message)
		{
            ((MessagePatient)message).IsFirst = false;
            MyAgent.InPatientsCount++;
            var patient = new EntityPatient(MyAgent.InPatientsCount, MySim);
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

            if (MyAgent.InPatientsCount == ((MySimulation)MySim).OrderedPatientsNum) return;

            message.Addressee = MyAgent.FindAssistant(SimId.SchedulerPatientsArrival);
            StartContinualAssistant(message);
		}

		//meta! sender="SchedulerPatientsArrival", id="134", type="Notice"
		public void ProcessNoticePreGeneratedPatientPicked(MessageForm message)
		{
            MyAgent.InPatientsCount++;
            var patient = ((MySimulation)MySim).PreGeneratedPatients.Dequeue();
            if (MyAgent.CanceledPatientsIds.Count > 0
                && MyAgent.CanceledPatientsIds.Contains(patient.Id))
            {
                message.Code = Mc.NoticePatientLeave;
                Notice(new MessagePatient(message));
            }
            else
            {
                ((MessagePatient)message).Patient = patient;
                message.Addressee = MySim.FindAgent(SimId.AgentModel);
                message.Code = Mc.NoticePatientArrival;
                Notice(new MessagePatient(message));
            }

            if (MyAgent.InPatientsCount == ((MySimulation)MySim).OrderedPatientsNum) return;

            message.Addressee = MyAgent.FindAssistant(SimId.SchedulerPatientsArrival);
            StartContinualAssistant(message);
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		public void Init()
		{
		}

		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.Initialization:
				ProcessInitialization(message);
			break;

			case Mc.Finish:
				ProcessFinish(message);
			break;

			case Mc.NoticePatientGenerated:
				ProcessNoticePatientGenerated(message);
			break;

			case Mc.NoticePreGeneratedPatientPicked:
				ProcessNoticePreGeneratedPatientPicked(message);
			break;

			case Mc.NoticePatientLeave:
				ProcessNoticePatientLeave(message);
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