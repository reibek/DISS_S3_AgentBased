using entities;
using OSPABA;
namespace simulation
{
    public class MessageBreak : MessageForm
    {
        public VaccineCentrumEntity Entity { get; set; }

        public MessageBreak(Simulation sim) :
            base(sim)
        {
        }

        public MessageBreak(MessageForm original) :
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
            MessageBreak original = (MessageBreak)message;

            // Copy attributes
            Entity = original.Entity;
        }
    }
}