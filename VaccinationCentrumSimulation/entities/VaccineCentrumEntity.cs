using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSPABA;

namespace entities
{
    public class VaccineCentrumEntity : Entity
    {
        /// <summary>
        /// Indicator, if nurse is busy.
        /// </summary>
        public bool IsBusy { get; set; }

        /// <summary>
        /// Indicator, if nurse is busy.
        /// </summary>
        public bool HadBreak { get; set; }

        /// <summary>
        /// Uniform random variable for registration time of entity.
        /// </summary>
        public double StatUtilizationLastChange { get; set; }

        /// <summary>
        /// Working time time of entity.
        /// </summary>
        public double WorkingTime { get; set; }

        public double AverageUtilization => WorkingTime / MySim.CurrentTime;

        /// <summary>
        /// Reference for patient.
        /// </summary>
        public EntityPatient Patient { get; set; }

        /// <summary>
        /// Reference for patient.
        /// </summary>
        public EntityState State { get; set; }

        public VaccineCentrumEntity(int id, Simulation simRef) : base(id, simRef)
        {
            StatUtilizationLastChange = 0;
            WorkingTime = 0;
            State = EntityState.Free;
        }

        public void Assign(EntityPatient patient)
        {
            #region STATISTICS

            StatUtilizationLastChange = MySim.CurrentTime;

            #endregion

            Patient = patient;
            IsBusy = true;
            HadBreak = false;
            State = EntityState.Working;
        }

        public void AcceptNext(EntityPatient patient)
        {
            #region STATISTICS

            WorkingTime += MySim.CurrentTime - StatUtilizationLastChange;
            StatUtilizationLastChange = MySim.CurrentTime;

            #endregion

            Patient = patient;
            State = EntityState.Working;
        }

        public void Release()
        {
            #region STATISTICS

            WorkingTime += MySim.CurrentTime - StatUtilizationLastChange;
            StatUtilizationLastChange = MySim.CurrentTime;


            #endregion

            Patient = null;
            IsBusy = false;
            State = EntityState.Free;
        }

        public void GetBreak()
        {
            Patient = null;
            IsBusy = false;
            State = EntityState.Break;
        }

        public virtual void Reset()
        {
            IsBusy = false;
            HadBreak = false;
            StatUtilizationLastChange = 0;
            WorkingTime = 0;
            Patient = null;
            State = EntityState.Free;
        }
    }

    public enum EntityState
    {
        Free,
        Working,
        Moving,
        Preparing,
        Break,
        Eating
    }
}
