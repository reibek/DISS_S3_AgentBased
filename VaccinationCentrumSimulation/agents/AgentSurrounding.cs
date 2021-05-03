using System;
using System.Collections.Generic;
using OSPABA;
using simulation;
using managers;
using continualAssistants;
using OSPRNG;
using instantAssistants;

namespace agents
{
	//meta! id="2"
	public class AgentSurrounding : Agent
    {
        public int PatientsCount { get; set; }
        public int CanceledPatientsNum { get; set; }
        public List<int> CanceledPatientsIds { get; set; }
        public UniformDiscreteRNG RandCanceledPatientsNum { get; set; }
        public UniformDiscreteRNG RandCanceledPatientsIds { get; set; }

        public AgentSurrounding(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();

            AddOwnMessage(Mc.NoticePatientGenerated);

            CanceledPatientsIds = new List<int>();
			RandCanceledPatientsNum = new UniformDiscreteRNG(5, 25, ((MySimulation) MySim).RandSeedGenerator);
            RandCanceledPatientsIds = new UniformDiscreteRNG(1, ((MySimulation) MySim).OrderedPatientsNum,
                ((MySimulation) MySim).RandSeedGenerator);
        }

        public override void PrepareReplication()
        {
            base.PrepareReplication();

            PatientsCount = 0;
            CanceledPatientsIds.Clear();
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerSurrounding(SimId.ManagerSurrounding, MySim, this);
			new ActionCancelPatients(SimId.ActionCancelPatients, MySim, this);
			new SchedulerPatientsArrival(SimId.SchedulerPatientsArrival, MySim, this);
			AddOwnMessage(Mc.Initialization);
			AddOwnMessage(Mc.NoticePatientLeave);
		}
		//meta! tag="end"
	}
}