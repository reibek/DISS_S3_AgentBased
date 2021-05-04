using OSPABA;
using simulation;
using managers;
using continualAssistants;
//using instantAssistants;
namespace agents
{
	//meta! id="1"
	public class AgentModel : Agent
	{
		public AgentModel(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();

            var message = new MessagePatient(MySim)
            {
                Addressee = MySim.FindAgent(SimId.AgentSurrounding),
                Code = Mc.Initialization
            };
            MyManager.Call(message);

            var message2 = new MessagePatient(MySim)
            {
                Addressee = MySim.FindAgent(SimId.AgentCentrum),
                Code = Mc.Initialization
            };
            MyManager.Call(message2);
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerModel(SimId.ManagerModel, MySim, this);
			AddOwnMessage(Mc.NoticePatientArrival);
			AddOwnMessage(Mc.NoticePatientLeave);
		}
		//meta! tag="end"
	}
}