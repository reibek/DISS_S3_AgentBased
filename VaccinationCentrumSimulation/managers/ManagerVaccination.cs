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
		public void ProcessFinish(MessageForm message)
        {
            var nurse = ((MyMessage) message).Nurse;

            if (MyAgent.PoolNurses.FreeCount == 0 
                && MyAgent.QuVaccination.Count > 0)
            {
                var messageFromQueue = MyAgent.QuVaccination.Dequeue();
                nurse.AcceptNext(((MyMessage) messageFromQueue).Patient);
                ((MyMessage)messageFromQueue).Nurse = nurse;
				messageFromQueue.Addressee = MyAgent.FindAssistant(SimId.ProcessVaccination);
                StartContinualAssistant(messageFromQueue);

                MyAgent.StatQuVaccinationTime.AddSample(MySim.CurrentTime -
                                                         ((MyMessage)messageFromQueue).Patient
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
                EntityNurse nurse = MyAgent.PoolNurses.Assign(choiceNum, ((MyMessage) message).Patient);
                ((MyMessage) message).Nurse = nurse;
                message.Addressee = MyAgent.FindAssistant(SimId.ProcessVaccination);
				StartContinualAssistant(message);

                MyAgent.StatQuVaccinationTime.AddSample(0);
			}
            else
            {
                ((MyMessage) message).Patient.VaccinationQuStartTime = MySim.CurrentTime;
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

		//meta! userInfo="Generated code: do not modify", tag="begin"
		public void Init()
		{
		}

		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.RequestVaccination:
				ProcessRequestVaccination(message);
			break;

			case Mc.Finish:
				ProcessFinish(message);
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