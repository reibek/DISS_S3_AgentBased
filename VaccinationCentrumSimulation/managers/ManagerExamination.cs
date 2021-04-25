using OSPABA;
using simulation;
using agents;
using continualAssistants;
using entities;

//using instantAssistants;
namespace managers
{
	//meta! id="5"
	public class ManagerExamination : Manager
	{
		public ManagerExamination(int id, Simulation mySim, Agent myAgent) :
			base(id, mySim, myAgent)
		{
			Init();
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication

			if (PetriNet != null)
			{
				PetriNet.Clear();
			}
		}

		//meta! sender="AgentCentrum", id="33", type="Request"
		public void ProcessRequestExamination(MessageForm message)
		{
            if (MyAgent.PoolDoctors.FreeCount > 0
                && MyAgent.QuExamination.IsEmpty())
            {
                int choiceNum = MyAgent.RandDoctorChoice[MyAgent.PoolDoctors.FreeCount - 1].Sample();
                EntityDoctor doctor = MyAgent.PoolDoctors.Assign(choiceNum, ((MyMessage) message).Patient);
                ((MyMessage)message).Doctor = doctor;
                message.Addressee = MyAgent.FindAssistant(SimId.ProcessExamination);
				StartContinualAssistant(message);

                MyAgent.StatQuExaminationTime.AddSample(0);
			}
            else
            {
				((MyMessage)message).Patient.ExaminationQuStartTime = MySim.CurrentTime;
                MyAgent.QuExamination.Enqueue(message);

                MyAgent.StatQuExaminationSize.AddSample(MyAgent.QuExamination.Size);
			}
		}

		//meta! sender="ProcessExamination", id="23", type="Finish"
		public void ProcessFinish(MessageForm message)
        {
            var doctor = ((MyMessage) message).Doctor;

            if (MyAgent.PoolDoctors.FreeCount == 0
                && MyAgent.QuExamination.Count > 0)
            {
                var messageFromQueue = MyAgent.QuExamination.Dequeue();
                doctor.AcceptNext(((MyMessage) messageFromQueue).Patient);
                ((MyMessage)messageFromQueue).Doctor = doctor;
				messageFromQueue.Addressee = MyAgent.FindAssistant(SimId.ProcessExamination);
                StartContinualAssistant(messageFromQueue);

                MyAgent.StatQuExaminationTime.AddSample(MySim.CurrentTime -
                                                         ((MyMessage)messageFromQueue).Patient
                                                         .ExaminationQuStartTime);
                MyAgent.StatQuExaminationSize.AddSample(MyAgent.QuExamination.Size);

			}
			else if (MyAgent.QuExamination.IsEmpty())
            {
				MyAgent.PoolDoctors.Release(doctor);
            }

            message.Addressee = MySim.FindAgent(SimId.AgentCentrum);
            message.Code = Mc.RequestExamination;
			Response(message);
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

        public override void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.RequestExamination:
				ProcessRequestExamination(message);
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
		public new AgentExamination MyAgent
		{
			get
			{
				return (AgentExamination)base.MyAgent;
			}
		}
	}
}