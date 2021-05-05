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
	//meta! id="6"
	public class AgentVaccination : Agent
	{
        public DataStructures.Queue<MessageForm> QuVaccination;
        public WStat StatQuVaccinationSize { get; set; }
        public Stat StatQuVaccinationTime { get; set; }
		public Pool<EntityNurse> PoolNurses { get; set; }
        public UniformContinuousRNG RandMovingToFromColdStorTime { get; set; }
        public List<UniformDiscreteRNG> RandNurseChoice { get; set; }
		public AgentVaccination(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();

            AddOwnMessage(Mc.ProcessVaccinationEnded);
			AddOwnMessage(Mc.ProcessMovingToFromColdStorEnded);
            AddOwnMessage(Mc.TimeForBreak);

			QuVaccination = new DataStructures.Queue<MessageForm>();
            StatQuVaccinationSize = new WStat(MySim);
            StatQuVaccinationTime = new Stat();
			RandMovingToFromColdStorTime = new UniformContinuousRNG(10, 18, ((MySimulation)MySim).RandSeedGenerator);
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();

            var nursesCount = ((MySimulation)MySim).ResNursesCount;

            QuVaccination.Clear();
            StatQuVaccinationSize.Clear();
            StatQuVaccinationTime.Clear();

			PoolNurses = new Pool<EntityNurse>(nursesCount);
            RandNurseChoice = new List<UniformDiscreteRNG>(nursesCount);
            for (int i = 0; i < nursesCount; i++)
            {
                PoolNurses.Add(new EntityNurse(i + 1, MySim, ((MySimulation)MySim).RandSeedGenerator));
                RandNurseChoice.Add(new UniformDiscreteRNG(0, i, ((MySimulation)MySim).RandSeedGenerator));
            }
		}

        //meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerVaccination(SimId.ManagerVaccination, MySim, this);
			new ProcessVaccination(SimId.ProcessVaccination, MySim, this);
			new SchedulerNurseBreak(SimId.SchedulerNurseBreak, MySim, this);
			new ProcessMovingToFromColdStor(SimId.ProcessMovingToFromColdStor, MySim, this);
			AddOwnMessage(Mc.Initialization);
			AddOwnMessage(Mc.RequestNurseBreak);
			AddOwnMessage(Mc.RequestFillSyringes);
			AddOwnMessage(Mc.RequestVaccination);
		}
		//meta! tag="end"

        public void FinalUpdateStatistics()
        {
            StatQuVaccinationSize.AddSample(QuVaccination.Size);
        }
	}
}