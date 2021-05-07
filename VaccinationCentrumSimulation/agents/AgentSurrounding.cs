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
        public int InPatientsCount { get; set; }
        public int OutPatientsCount { get; set; }
        public int CanceledPatientsNum { get; set; }
        public List<int> CanceledPatientsIds { get; set; }
        public UniformDiscreteRNG RandCanceledPatientsNum { get; set; }
        public UniformDiscreteRNG RandCanceledPatientsIds { get; set; }
        public UniformContinuousRNG RandArrivalDecision { get; set; }
        public UniformContinuousRNG RandEarlyArrivalDecision { get; set; }
        public List<UniformContinuousRNG> RandEarlierTimes { get; set; }

        public AgentSurrounding(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();

            CanceledPatientsIds = new List<int>();
			RandCanceledPatientsNum = new UniformDiscreteRNG(5, 25, ((MySimulation) MySim).RandSeedGenerator);
            RandCanceledPatientsIds = new UniformDiscreteRNG(1, ((MySimulation) MySim).OrderedPatientsNum,
                ((MySimulation) MySim).RandSeedGenerator);
            RandArrivalDecision = new UniformContinuousRNG(0, 1, ((MySimulation) MySim).RandSeedGenerator);
            RandEarlyArrivalDecision = new UniformContinuousRNG(0, 1, ((MySimulation) MySim).RandSeedGenerator);
            
            RandEarlierTimes = new List<UniformContinuousRNG>(4);
            RandEarlierTimes.Add(new UniformContinuousRNG(60, 1200, ((MySimulation)MySim).RandSeedGenerator));
            RandEarlierTimes.Add(new UniformContinuousRNG(1200, 3600, ((MySimulation)MySim).RandSeedGenerator));
            RandEarlierTimes.Add(new UniformContinuousRNG(3600, 4800, ((MySimulation)MySim).RandSeedGenerator));
            RandEarlierTimes.Add(new UniformContinuousRNG(4800, 14400, ((MySimulation)MySim).RandSeedGenerator));
        }

        public override void PrepareReplication()
        {
            base.PrepareReplication();

            InPatientsCount = 0;
            OutPatientsCount = 0;
            CanceledPatientsIds.Clear();
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerSurrounding(SimId.ManagerSurrounding, MySim, this);
			new SchedulerPatientsArrival(SimId.SchedulerPatientsArrival, MySim, this);
			new ActionCancelPatients(SimId.ActionCancelPatients, MySim, this);
			new ActionPatientsWithEarlyArrival(SimId.ActionPatientsWithEarlyArrival, MySim, this);
			AddOwnMessage(Mc.Initialization);
			AddOwnMessage(Mc.NoticePatientGeneratingEnded);
			AddOwnMessage(Mc.NoticePatientLeave);
			AddOwnMessage(Mc.NoticePatientGenerated);
			AddOwnMessage(Mc.NoticePreGeneratedPatientPicked);
			AddOwnMessage(Mc.NoticePreGeneratedPatientHoldEnded);
		}
		//meta! tag="end"
	}
}