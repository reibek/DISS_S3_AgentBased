using System.Collections.Generic;
using OSPABA;
using simulation;
using managers;
using continualAssistants;
using entities;
using OSPRNG;
using OSPStat;
using VaccineCentrum;

//using instantAssistants;
namespace agents
{
	//meta! id="4"
	public class AgentRegistration : Agent
	{
        public DataStructures.Queue<MessageForm> QuRegistration { get; set; }
		public WStat StatQuRegistrationSize { get; set; }
        public Stat StatQuRegistrationTime { get; set; }
		public Pool<EntityAdminWorker> PoolAdminWorkers { get; set; }
		public List<UniformDiscreteRNG> RandAdminWorkerChoice { get; set; }

		public AgentRegistration(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();

            AddOwnMessage(Mc.ProcessRegistrationEnded);
            AddOwnMessage(Mc.TimeForBreak);

			QuRegistration = new DataStructures.Queue<MessageForm>();
			StatQuRegistrationSize = new WStat(MySim);
			StatQuRegistrationTime = new Stat();
        }

		public override void PrepareReplication()
		{
			base.PrepareReplication();

            var adminWorkersCount = ((MySimulation) MySim).ResAdminWorkersCount;

			QuRegistration.Clear();
			StatQuRegistrationSize.Clear();
			StatQuRegistrationTime.Clear();

            PoolAdminWorkers = new Pool<EntityAdminWorker>(adminWorkersCount);
			RandAdminWorkerChoice = new List<UniformDiscreteRNG>(adminWorkersCount);
            for (int i = 0; i < adminWorkersCount; i++)
            {
                PoolAdminWorkers.Add(new EntityAdminWorker(i + 1, MySim, ((MySimulation) MySim).RandSeedGenerator));
                RandAdminWorkerChoice.Add(new UniformDiscreteRNG(0, i, ((MySimulation) MySim).RandSeedGenerator));
            }
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerRegistration(SimId.ManagerRegistration, MySim, this);
			new ProcessRegistration(SimId.ProcessRegistration, MySim, this);
			new SchedulerAdminWorkerBreak(SimId.SchedulerAdminWorkerBreak, MySim, this);
			AddOwnMessage(Mc.Initialization);
			AddOwnMessage(Mc.RequestAdminWorkerBreak);
			AddOwnMessage(Mc.RequestRegistration);
		}
		//meta! tag="end"
        public void FinalUpdateStatistics()
        {
            StatQuRegistrationSize.AddSample(QuRegistration.Size);
        }
    }
}