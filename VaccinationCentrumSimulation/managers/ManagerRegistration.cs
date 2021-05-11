using OSPABA;
using simulation;
using agents;
using continualAssistants;
using entities;

//using instantAssistants;
namespace managers
{
	//meta! id="4"
	public class ManagerRegistration : Manager
	{
		public ManagerRegistration(int id, Simulation mySim, Agent myAgent) :
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

		//meta! sender="ProcessRegistration", id="20", type="Finish"
		public void ProcessFinishProcessRegistration(MessageForm message)
        {
            var adminWorker = ((MessagePatient) message).AdminWorker;

            if (MyAgent.PoolAdminWorkers.IsBreakTime
                && MyAgent.PoolAdminWorkers.OnBreakCount < MyAgent.PoolAdminWorkers.Count / 2
                && !adminWorker.HadBreak)
            {
                MyAgent.PoolAdminWorkers.Release(adminWorker);
				MyAgent.PoolAdminWorkers.GetBreak(adminWorker);
                MyAgent.PoolAdminWorkers.OnBreakCount++;
                
                adminWorker.BreakStarted = MySim.CurrentTime;

                var breakMessage = new MessageBreak(MySim)
                {
                    Entity = adminWorker,
                    Addressee = MySim.FindAgent(SimId.AgentCentrum),
                    Code = Mc.RequestAdminWorkerBreak
                };

                Request(breakMessage);
			}
            else if (MyAgent.PoolAdminWorkers.FreeCount == 0
                    && MyAgent.QuRegistration.Count > 0)
            {
                var messageFromQueue = MyAgent.QuRegistration.Dequeue();
                adminWorker.AcceptNext(((MessagePatient) messageFromQueue).Patient);
                ((MessagePatient)messageFromQueue).AdminWorker = adminWorker;
				messageFromQueue.Addressee = MyAgent.FindAssistant(SimId.ProcessRegistration);
				StartContinualAssistant(messageFromQueue);

                MyAgent.StatQuRegistrationTime.AddSample(MySim.CurrentTime -
                                                         ((MessagePatient) messageFromQueue).Patient
                                                         .RegistrationQuStartTime);
				MyAgent.StatQuRegistrationSize.AddSample(MyAgent.QuRegistration.Size);
			} 
            else if (MyAgent.QuRegistration.IsEmpty())
            {
				MyAgent.PoolAdminWorkers.Release(adminWorker);
            }

			message.Addressee = MySim.FindAgent(SimId.AgentCentrum);
            message.Code = Mc.RequestRegistration;
			Response(message);
        }

		//meta! sender="AgentCentrum", id="32", type="Request"
		public void ProcessRequestRegistration(MessageForm message)
		{
            if (MyAgent.PoolAdminWorkers.FreeCount > 0
                && MyAgent.QuRegistration.IsEmpty())
            {
                int choiceNum = MyAgent.RandAdminWorkerChoice[MyAgent.PoolAdminWorkers.FreeCount - 1].Sample();
                EntityAdminWorker adminWorker =
                    MyAgent.PoolAdminWorkers.Assign(choiceNum, ((MessagePatient) message).Patient);
                ((MessagePatient) message).AdminWorker = adminWorker;
                message.Addressee = MyAgent.FindAssistant(SimId.ProcessRegistration);
				StartContinualAssistant(message);

				MyAgent.StatQuRegistrationTime.AddSample(0);
            }
            else
            {
                ((MessagePatient) message).Patient.RegistrationQuStartTime = MySim.CurrentTime;
				MyAgent.QuRegistration.Enqueue(message);
				
                MyAgent.StatQuRegistrationSize.AddSample(MyAgent.QuRegistration.Size);
            }
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
			}
		}

		//meta! sender="AgentCentrum", id="52", type="Response"
		public void ProcessRequestAdminWorkerBreak(MessageForm message)
        {
            MyAgent.PoolAdminWorkers.OnBreakCount--;
            MyAgent.PoolAdminWorkers.BreaksCompleteCount++;

            EntityAdminWorker adminWorker = (EntityAdminWorker)((MessageBreak)message).Entity;
            adminWorker.BreakEnded = MySim.CurrentTime;
            adminWorker.BreakDuration = adminWorker.BreakEnded - adminWorker.BreakStarted;

            if (MyAgent.PoolAdminWorkers.BreaksCompleteCount < MyAgent.PoolAdminWorkers.Count)
            {
                foreach (var aw in MyAgent.PoolAdminWorkers.Entities)
                {
                    if (aw.State == EntityState.Free && !aw.HadBreak)
                    {
                        MyAgent.PoolAdminWorkers.GetBreak(aw);
                        MyAgent.PoolAdminWorkers.OnBreakCount++;

                        aw.BreakStarted = MySim.CurrentTime;

                        var breakMessage = new MessageBreak(message);
                        breakMessage.Entity = aw;
                        breakMessage.Addressee = MySim.FindAgent(SimId.AgentCentrum);
                        breakMessage.Code = Mc.RequestAdminWorkerBreak;
                        Request(breakMessage);

                        break;
                    }
                }
            }

            if (MyAgent.PoolAdminWorkers.FreeCount == 0
                     && MyAgent.QuRegistration.Count > 0)
            {
                var messageFromQueue = MyAgent.QuRegistration.Dequeue();
                adminWorker.Assign(((MessagePatient)messageFromQueue).Patient);
                ((MessagePatient)messageFromQueue).AdminWorker = adminWorker;
                messageFromQueue.Addressee = MyAgent.FindAssistant(SimId.ProcessRegistration);
                StartContinualAssistant(messageFromQueue);

                MyAgent.StatQuRegistrationTime.AddSample(MySim.CurrentTime -
                                                         ((MessagePatient)messageFromQueue).Patient
                                                         .RegistrationQuStartTime);
                MyAgent.StatQuRegistrationSize.AddSample(MyAgent.QuRegistration.Size);
            }
            else if (MyAgent.QuRegistration.IsEmpty())
            {
                MyAgent.PoolAdminWorkers.Release(adminWorker);
            }
        }

		//meta! sender="SchedulerAdminWorkerBreak", id="60", type="Finish"
		public void ProcessFinishSchedulerAdminWorkerBreak(MessageForm message)
        {
            MyAgent.PoolAdminWorkers.IsBreakTime = true;
            int halfCount = MyAgent.PoolAdminWorkers.Count / 2;
            int pickedCount = 0;

            for (int i = 0; i < halfCount; i++)
            {
                foreach (var aw in MyAgent.PoolAdminWorkers.Entities)
                {
                    if (aw.State == EntityState.Free && !aw.HadBreak)
                    {
                        MyAgent.PoolAdminWorkers.GetBreak(aw);
                        MyAgent.PoolAdminWorkers.OnBreakCount++;
                        pickedCount++;

                        aw.BreakStarted = MySim.CurrentTime;

                        var breakMessage = new MessageBreak(MySim);
                        breakMessage.Entity = aw;
                        breakMessage.Addressee = MySim.FindAgent(SimId.AgentCentrum);
                        breakMessage.Code = Mc.RequestAdminWorkerBreak;
                        Request(breakMessage);

                        break;
                    }
                }
            }
        }

		//meta! sender="AgentCentrum", id="107", type="Call"
		public void ProcessInitialization(MessageForm message)
        {
            message.Addressee = MyAgent.FindAssistant(SimId.SchedulerAdminWorkerBreak);
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
				case SimId.ProcessRegistration:
					ProcessFinishProcessRegistration(message);
				break;

				case SimId.SchedulerAdminWorkerBreak:
					ProcessFinishSchedulerAdminWorkerBreak(message);
				break;
				}
			break;

			case Mc.Initialization:
				ProcessInitialization(message);
			break;

			case Mc.RequestAdminWorkerBreak:
				ProcessRequestAdminWorkerBreak(message);
			break;

			case Mc.RequestRegistration:
				ProcessRequestRegistration(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AgentRegistration MyAgent
		{
			get
			{
				return (AgentRegistration)base.MyAgent;
			}
		}
	}
}