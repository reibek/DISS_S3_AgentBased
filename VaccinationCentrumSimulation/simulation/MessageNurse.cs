using entities;
using OSPABA;
namespace simulation
{
    public class MessageNurse : MessageForm
    {
        public EntityNurse Nurse { get; set; }

        public MessageNurse(Simulation sim) :
            base(sim)
        {
        }

        public MessageNurse(MessageForm original) :
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
            Nurse = original.Nurse;
        }
    }
}