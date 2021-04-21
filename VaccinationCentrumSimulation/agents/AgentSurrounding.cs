using System;
using System.Collections.Generic;
using OSPABA;
using simulation;
using managers;
using continualAssistants;
using OSPRNG;

//using instantAssistants;

namespace agents
{
	//meta! id="2"
	public class AgentSurrounding : Agent
    {
        public int OrderedPatientsNum { get; set; }
        public int ArrivedPatientsCount { get; set; }
        public int CanceledPatientsNum { get; set; }
        public List<int> CanceledPatientsIds { get; set; }
        public UniformDiscreteRNG RandCanceledPatientsNum { get; set; }
        public UniformDiscreteRNG RandCanceledPatientsIds { get; set; }

        public AgentSurrounding(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
            OrderedPatientsNum = 540;

            CanceledPatientsIds = new List<int>();
			RandCanceledPatientsNum = new UniformDiscreteRNG(5, 25, ((MySimulation) MySim).RandSeedGenerator);
			RandCanceledPatientsIds = new UniformDiscreteRNG(1, OrderedPatientsNum, ((MySimulation)MySim).RandSeedGenerator);
        }

        public override void PrepareReplication()
        {
            base.PrepareReplication();

            ArrivedPatientsCount = 0;

            CanceledPatientsIds.Clear();
            CanceledPatientsNum =
                (int) Math.Round(RandCanceledPatientsNum.Sample() * ((double) OrderedPatientsNum / 540), 0);

            for (int i = 0; i < CanceledPatientsNum; i++)
            {
                int id = RandCanceledPatientsIds.Sample();
                while (CanceledPatientsIds.Contains(id))
                    id = RandCanceledPatientsIds.Sample();

                CanceledPatientsIds.Add(id);
            }

            CanceledPatientsIds.Sort();
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerSurrounding(SimId.ManagerSurrounding, MySim, this);
			new SchedulerPatientsArrival(SimId.SchedulerPatientsArrival, MySim, this);
			AddOwnMessage(Mc.Initialization);
			AddOwnMessage(Mc.NoticePatientLeave);
			AddOwnMessage(Mc.NoticePatientGenerated);
		}
		//meta! tag="end"
	}
}