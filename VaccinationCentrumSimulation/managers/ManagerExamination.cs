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
                EntityDoctor doctor = MyAgent.PoolDoctors.Assign(choiceNum, ((MessagePatient) message).Patient);
                ((MessagePatient)message).Doctor = doctor;
                message.Addressee = MyAgent.FindAssistant(SimId.ProcessExamination);
				StartContinualAssistant(message);

                MyAgent.StatQuExaminationTime.AddSample(0);
			}
            else
            {
				((MessagePatient)message).Patient.ExaminationQuStartTime = MySim.CurrentTime;
                MyAgent.QuExamination.Enqueue(message);

                MyAgent.StatQuExaminationSize.AddSample(MyAgent.QuExamination.Size);
			}
		}

		//meta! sender="ProcessExamination", id="23", type="Finish"
		public void ProcessFinishProcessExamination(MessageForm message)
        {
            var doctor = ((MessagePatient) message).Doctor;

			if (MyAgent.PoolDoctors.IsBreakTime
                && MyAgent.PoolDoctors.OnBreakCount < MyAgent.PoolDoctors.Count / 2
                && !doctor.HadBreak)
            {
                MyAgent.PoolDoctors.Release(doctor);
                MyAgent.PoolDoctors.GetBreak(doctor);
                MyAgent.PoolDoctors.OnBreakCount++;

                doctor.BreakStarted = MySim.CurrentTime;

                var breakMessage = new MessageBreak(MySim)
                {
                    Entity = doctor,
                    Addressee = MySim.FindAgent(SimId.AgentCentrum),
                    Code = Mc.RequestDoctorBreak
                };

                Request(breakMessage);
            }
            else if (MyAgent.PoolDoctors.FreeCount == 0
                     && MyAgent.QuExamination.Count > 0)
            {
                var messageFromQueue = MyAgent.QuExamination.Dequeue();
                doctor.AcceptNext(((MessagePatient) messageFromQueue).Patient);
                ((MessagePatient)messageFromQueue).Doctor = doctor;
				messageFromQueue.Addressee = MyAgent.FindAssistant(SimId.ProcessExamination);
                StartContinualAssistant(messageFromQueue);

                MyAgent.StatQuExaminationTime.AddSample(MySim.CurrentTime -
                                                         ((MessagePatient)messageFromQueue).Patient
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

		//meta! sender="SchedulerDoctorBreak", id="63", type="Finish"
		public void ProcessFinishSchedulerDoctorBreak(MessageForm message)
		{
            MyAgent.PoolDoctors.IsBreakTime = true;
            int halfCount = MyAgent.PoolDoctors.Count / 2;
            int pickedCount = 0;

            for (int i = 0; i < halfCount; i++)
            {
                foreach (var d in MyAgent.PoolDoctors.Entities)
                {
                    if (d.State == EntityState.Free && !d.HadBreak)
                    {
                        MyAgent.PoolDoctors.GetBreak(d);
                        MyAgent.PoolDoctors.OnBreakCount++;
                        pickedCount++;

                        d.BreakStarted = MySim.CurrentTime;

                        var breakMessage = new MessageBreak(MySim);
                        breakMessage.Entity = d;
                        breakMessage.Addressee = MySim.FindAgent(SimId.AgentCentrum);
                        breakMessage.Code = Mc.RequestDoctorBreak;
                        Request(breakMessage);

                        break;
                    }
                }
            }
		}

		//meta! sender="AgentCentrum", id="53", type="Response"
		public void ProcessRequestDoctorBreak(MessageForm message)
		{
			MyAgent.PoolDoctors.OnBreakCount--;
			MyAgent.PoolDoctors.BreaksCompleteCount++;

			EntityDoctor doctor = (EntityDoctor)((MessageBreak)message).Entity;
			doctor.BreakEnded = MySim.CurrentTime;
			doctor.BreakDuration = doctor.BreakEnded - doctor.BreakStarted;

			if (MyAgent.PoolDoctors.BreaksCompleteCount < MyAgent.PoolDoctors.Count)
			{
				foreach (var d in MyAgent.PoolDoctors.Entities)
				{
					if (d.State == EntityState.Free && !d.HadBreak)
					{
						MyAgent.PoolDoctors.GetBreak(d);
						MyAgent.PoolDoctors.OnBreakCount++;

						d.BreakStarted = MySim.CurrentTime;

						var breakMessage = new MessageBreak(message);
						breakMessage.Entity = d;
						breakMessage.Addressee = MySim.FindAgent(SimId.AgentCentrum);
						breakMessage.Code = Mc.RequestDoctorBreak;
						Request(breakMessage);

						break;
					}
				}
			}

            if (MyAgent.PoolDoctors.FreeCount == 0
                && MyAgent.QuExamination.Count > 0)
            {
                var messageFromQueue = MyAgent.QuExamination.Dequeue();
                doctor.Assign(((MessagePatient)messageFromQueue).Patient);
                ((MessagePatient)messageFromQueue).Doctor = doctor;
                messageFromQueue.Addressee = MyAgent.FindAssistant(SimId.ProcessExamination);
                StartContinualAssistant(messageFromQueue);

                MyAgent.StatQuExaminationTime.AddSample(MySim.CurrentTime -
                                                        ((MessagePatient)messageFromQueue).Patient
                                                        .ExaminationQuStartTime);
                MyAgent.StatQuExaminationSize.AddSample(MyAgent.QuExamination.Size);

            }
            else if (MyAgent.QuExamination.IsEmpty())
            {
                MyAgent.PoolDoctors.Release(doctor);
            }
        }

		//meta! sender="AgentCentrum", id="111", type="Call"
		public void ProcessInitialization(MessageForm message)
		{
            message.Addressee = MyAgent.FindAssistant(SimId.SchedulerDoctorBreak);
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
			case Mc.Finish:
				switch (message.Sender.Id)
				{
				case SimId.ProcessExamination:
					ProcessFinishProcessExamination(message);
				break;

				case SimId.SchedulerDoctorBreak:
					ProcessFinishSchedulerDoctorBreak(message);
				break;
				}
			break;

			case Mc.RequestExamination:
				ProcessRequestExamination(message);
			break;

			case Mc.Initialization:
				ProcessInitialization(message);
			break;

			case Mc.RequestDoctorBreak:
				ProcessRequestDoctorBreak(message);
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