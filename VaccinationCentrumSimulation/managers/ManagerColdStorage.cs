using OSPABA;
using simulation;
using agents;
using continualAssistants;
//using instantAssistants;

namespace managers
{
	//meta! id="44"
	public class ManagerColdStorage : Manager
	{
		public ManagerColdStorage(int id, Simulation mySim, Agent myAgent) :
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

		//meta! sender="ProcessFillingSyringes", id="81", type="Finish"
		public void ProcessFinish(MessageForm message)
        {
            var nurse = ((MessageNurse) message).Nurse;
            nurse.SyringesFullCount = 20;

            MyAgent.PreparingNursesCount--;

            if (!MyAgent.QuNurses.IsEmpty())
            {
                var messageFromQueue = MyAgent.QuNurses.Dequeue();
                MyAgent.PreparingNursesCount++;
                messageFromQueue.Addressee = MyAgent.FindAssistant(SimId.ProcessFillingSyringes);
                StartContinualAssistant(messageFromQueue);

                MyAgent.StatQuNursesSize.AddSample(MyAgent.QuNurses.Size);
			}

            message.Addressee = MySim.FindAgent(SimId.AgentVaccination);
            message.Code = Mc.RequestFillSyringes;
            Response(message);
		}

		//meta! sender="AgentVaccination", id="50", type="Request"
		public void ProcessRequestFillSyringes(MessageForm message)
		{
            if (MyAgent.PreparingNursesCount < 2)
            {
                MyAgent.PreparingNursesCount++;
                message.Addressee = MyAgent.FindAssistant(SimId.ProcessFillingSyringes);
				StartContinualAssistant(message);
            }
            else
            {
				MyAgent.QuNurses.Enqueue(message);

                MyAgent.StatQuNursesSize.AddSample(MyAgent.QuNurses.Size);
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
			case Mc.RequestFillSyringes:
				ProcessRequestFillSyringes(message);
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
		public new AgentColdStorage MyAgent
		{
			get
			{
				return (AgentColdStorage)base.MyAgent;
			}
		}
	}
}