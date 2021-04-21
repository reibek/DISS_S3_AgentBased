using System;
using OSPABA;
using agents;

namespace simulation
{
	public class MySimulation : Simulation
    {
        public Random RandSeedGenerator { get; private set; }

		public MySimulation()
		{
            RandSeedGenerator = new Random(); RandSeedGenerator = new Random();
            Init();
		}

        protected override void PrepareSimulation()
        {
            base.PrepareSimulation();
        }

        protected override void PrepareReplication()
        {
            base.PrepareReplication();
        }

        protected override void ReplicationFinished()
        {
            base.ReplicationFinished();

            Console.WriteLine("R" + CurrentReplication + ": Ordered patients: " + AgentSurrounding.OrderedPatientsNum);
            Console.WriteLine("R" + CurrentReplication + ": Arrived patients: " + AgentSurrounding.ArrivedPatientsCount);
            Console.WriteLine("R" + CurrentReplication + ": Canceled patients: " + AgentSurrounding.CanceledPatientsNum);
		}

        protected override void SimulationFinished()
        {
            base.SimulationFinished();
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			AgentModel = new AgentModel(SimId.AgentModel, this, null);
			AgentSurrounding = new AgentSurrounding(SimId.AgentSurrounding, this, AgentModel);
			AgentCentrum = new AgentCentrum(SimId.AgentCentrum, this, AgentModel);
			AgentRegistration = new AgentRegistration(SimId.AgentRegistration, this, AgentCentrum);
			AgentExamination = new AgentExamination(SimId.AgentExamination, this, AgentCentrum);
			AgentVaccination = new AgentVaccination(SimId.AgentVaccination, this, AgentCentrum);
			AgentWaitingRoom = new AgentWaitingRoom(SimId.AgentWaitingRoom, this, AgentCentrum);
		}
		public AgentModel AgentModel
		{ get; set; }
		public AgentSurrounding AgentSurrounding
		{ get; set; }
		public AgentCentrum AgentCentrum
		{ get; set; }
		public AgentRegistration AgentRegistration
		{ get; set; }
		public AgentExamination AgentExamination
		{ get; set; }
		public AgentVaccination AgentVaccination
		{ get; set; }
		public AgentWaitingRoom AgentWaitingRoom
		{ get; set; }
		//meta! tag="end"
	}
}