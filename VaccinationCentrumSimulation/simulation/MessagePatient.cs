using entities;
using OSPABA;
namespace simulation
{
	public class MessagePatient : MessageForm
	{
		public EntityPatient Patient { get; set; }
		public EntityAdminWorker AdminWorker { get; set; }
		public EntityDoctor Doctor { get; set; }
		public EntityNurse Nurse { get; set; }

		public MessagePatient(Simulation sim) :
			base(sim)
		{
		}

		public MessagePatient(MessageForm original) :
			base(original)
		{
			// copy() is called in superclass
        }

        public override MessageForm CreateCopy()
		{
			return new MessagePatient(this);
		}

        protected override void Copy(MessageForm message)
		{
			base.Copy(message);
			MessagePatient original = (MessagePatient)message;
			
            // Copy attributes
            Patient = original.Patient;
            AdminWorker = original.AdminWorker;
            Doctor = original.Doctor;
            Nurse = original.Nurse;
		}
	}
}