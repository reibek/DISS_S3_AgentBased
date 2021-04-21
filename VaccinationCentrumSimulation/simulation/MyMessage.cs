using entities;
using OSPABA;
namespace simulation
{
	public class MyMessage : MessageForm
	{
		public EntityPatient Patient { get; set; }

		public MyMessage(Simulation sim) :
			base(sim)
		{
		}

		public MyMessage(MessageForm original) :
			base(original)
		{
			// copy() is called in superclass
        }

        public override MessageForm CreateCopy()
		{
			return new MyMessage(this);
		}

        protected override void Copy(MessageForm message)
		{
			base.Copy(message);
			MyMessage original = (MyMessage)message;
			// Copy attributes
            Patient = original.Patient;
		}
	}
}