using OSPABA;
using simulation;
using agents;
using continualAssistants;
using entities;
//using instantAssistants;

namespace managers
{
	//meta! id="6"
	public class ManagerVaccination : Manager
	{
		public ManagerVaccination(int id, Simulation mySim, Agent myAgent) :
			base(id, mySim, myAgent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication

			if (PetriNet != null)
			{
				PetriNet.Clear();
			}
		}

		//meta! sender="ProcessVaccination", id="26", type="Finish"
		public void ProcessFinishProcessVaccination(MessageForm message)
        {
            var nurse = ((MessagePatient) message).Nurse;
            nurse.SyringesFullCount--;

            if (MyAgent.PoolNurses.FreeCount == 0 
                && MyAgent.QuVaccination.Count > 0)
            {
                var messageFromQueue = MyAgent.QuVaccination.Dequeue();
                nurse.AcceptNext(((MessagePatient) messageFromQueue).Patient);
                ((MessagePatient)messageFromQueue).Nurse = nurse;
				messageFromQueue.Addressee = MyAgent.FindAssistant(SimId.ProcessVaccination);
                StartContinualAssistant(messageFromQueue);

                MyAgent.StatQuVaccinationTime.AddSample(MySim.CurrentTime -
                                                         ((MessagePatient)messageFromQueue).Patient
                                                         .VaccinationQuStartTime);
                MyAgent.StatQuVaccinationSize.AddSample(MyAgent.QuVaccination.Size);
			}
			else if (MyAgent.QuVaccination.IsEmpty())
            {
                MyAgent.PoolNurses.Release(nurse);
            }

			message.Addressee = MySim.FindAgent(SimId.AgentCentrum);
            message.Code = Mc.RequestVaccination;
			Response(message);
        }

		//meta! sender="AgentCentrum", id="34", type="Request"
		public void ProcessRequestVaccination(MessageForm message)
		{
            if (MyAgent.PoolNurses.FreeCount > 0
                && MyAgent.QuVaccination.IsEmpty())
            {
                int choiceNum = MyAgent.RandNurseChoice[MyAgent.PoolNurses.FreeCount - 1].Sample();
                EntityNurse nurse = MyAgent.PoolNurses.Assign(choiceNum, ((MessagePatient) message).Patient);
                ((MessagePatient) message).Nurse = nurse;
                message.Addressee = MyAgent.FindAssistant(SimId.ProcessVaccination);
				StartContinualAssistant(message);

                MyAgent.StatQuVaccinationTime.AddSample(0);
			}
            else
            {
                ((MessagePatient) message).Patient.VaccinationQuStartTime = MySim.CurrentTime;
                MyAgent.QuVaccination.Enqueue(message);

                MyAgent.StatQuVaccinationSize.AddSample(MyAgent.QuVaccination.Size);
			}
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
			}
		}

		//meta! sender="AgentCentrum", id="54", type="Response"
		public void ProcessRequestNurseBreak(MessageForm message)
		{
		}

		//meta! sender="SchedulerNurseBreak", id="66", type="Finish"
		public void ProcessFinishSchedulerNurseBreak(MessageForm message)
		{
		}

		//meta! sender="AgentColdStorage", id="50", type="Response"
		public void ProcessRequestFillSyringes(MessageForm message)
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
			case Mc.RequestNurseBreak:
				ProcessRequestNurseBreak(message);
			break;

			case Mc.RequestVaccination:
				ProcessRequestVaccination(message);
			break;

			case Mc.Finish:
				switch (message.Sender.Id)
				{
				case SimId.SchedulerNurseBreak:
					ProcessFinishSchedulerNurseBreak(message);
				break;

				case SimId.ProcessVaccination:
					ProcessFinishProcessVaccination(message);
				break;
				}
			break;

			case Mc.RequestFillSyringes:
				ProcessRequestFillSyringes(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AgentVaccination MyAgent
		{
			get
			{
				return (AgentVaccination)base.MyAgent;
			}
		}
	}
}