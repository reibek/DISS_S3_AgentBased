using OSPABA;
using simulation;
using agents;
using entities;

namespace instantAssistants
{
	//meta! id="100"
	public class ActionPatientsWithEarlyArrival : Action
	{
		public ActionPatientsWithEarlyArrival(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		public override void Execute(MessageForm message)
        {
            var mySimulation = (MySimulation) MySim;
            for (int i = 0; i < mySimulation.OrderedPatientsNum; i++)
            {
                var arrivalTime = (32400.0 / mySimulation.OrderedPatientsNum) * i;

                var randArrivalDecision = MyAgent.RandArrivalDecision.Sample();
                var randEarlyArrivalDecision = MyAgent.RandEarlyArrivalDecision.Sample();

                if (randArrivalDecision > 0.1)
                {
                    if (randEarlyArrivalDecision <= 0.3)
                        arrivalTime -= MyAgent.RandEarlierTimes[0].Sample();
                    else if (randEarlyArrivalDecision > 0.3 && randEarlyArrivalDecision <= 0.7)
                        arrivalTime -= MyAgent.RandEarlierTimes[1].Sample();
                    else if (randEarlyArrivalDecision > 0.7 && randEarlyArrivalDecision <= 0.9)
                        arrivalTime -= MyAgent.RandEarlierTimes[2].Sample();
                    else
                        arrivalTime -= MyAgent.RandEarlierTimes[3].Sample();

                    if (arrivalTime < 0)
                        arrivalTime = 0;
                }

                var patient = new EntityPatient(i + 1, MySim, arrivalTime);

                mySimulation.PreGeneratedPatients.Enqueue(patient, (float) patient.ArrivalTime);
			}
		}

		public new AgentSurrounding MyAgent
		{
			get
			{
				return (AgentSurrounding)base.MyAgent;
			}
		}
	}
}
