using System.Dynamic;
using OSPABA;
using simulation;
using managers;
using continualAssistants;
using OSPRNG;
using OSPStat;

//using instantAssistants;

namespace agents
{
	//meta! id="44"
	public class AgentColdStorage : Agent
	{
        public DataStructures.Queue<MessageForm> QuNurses { get; set; }
        public int PreparingNursesCount { get; set; }
        public WStat StatQuNursesSize { get; set; }
        public int VaccinesInPackageLeft { get; set; }

        public UniformContinuousRNG RandOpenPackage { get; set; }

		public AgentColdStorage(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();

            QuNurses = new DataStructures.Queue<MessageForm>();
			StatQuNursesSize = new WStat(MySim);
			RandOpenPackage = new UniformContinuousRNG(30, 140, ((MySimulation)MySim).RandSeedGenerator);
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();

            QuNurses.Clear();
            PreparingNursesCount = 0;
            VaccinesInPackageLeft = 400;
            StatQuNursesSize.Clear();
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerColdStorage(SimId.ManagerColdStorage, MySim, this);
			new ProcessOpenNewPackage(SimId.ProcessOpenNewPackage, MySim, this);
			new ProcessFillingSyringes(SimId.ProcessFillingSyringes, MySim, this);
			AddOwnMessage(Mc.NoticeProcessFillingSyringesEnded);
			AddOwnMessage(Mc.RequestFillSyringes);
			AddOwnMessage(Mc.NoticeProcessOpenNewPackageEnded);
		}
		//meta! tag="end"

        public void FinalUpdateStatistics()
        {
            StatQuNursesSize.AddSample(QuNurses.Size);
        }
	}
}