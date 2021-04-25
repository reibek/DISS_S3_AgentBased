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
        public List<UniformDiscreteRNG> RandNurseChoice { get; set; }
		public AgentVaccination(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();

            AddOwnMessage(Mc.ProcessVaccinationEnded);

            QuVaccination = new DataStructures.Queue<MessageForm>();
            StatQuVaccinationSize = new WStat(MySim);
            StatQuVaccinationTime = new Stat();
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
			AddOwnMessage(Mc.RequestVaccination);
		}
		//meta! tag="end"
	}
}