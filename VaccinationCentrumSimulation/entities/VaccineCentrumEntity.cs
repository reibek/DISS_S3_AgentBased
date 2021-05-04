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
        /// Indicator, if entity is busy.
        /// </summary>
        public bool IsBusy { get; set; }

        /// <summary>
        /// Indicator, if entity had break.
        /// </summary>
        public bool HadBreak { get; set; }

        /// <summary>
        /// Time, when entity started break.
        /// </summary>
        public double BreakStarted { get; set; }

        /// <summary>
        /// Time, when entity ended break.
        /// </summary>
        public double BreakEnded { get; set; }

        /// <summary>
        /// Duration of break.
        /// </summary>
        public double BreakDuration { get; set; }

        /// <summary>
        /// Uniform random variable for registration time of entity.
        /// </summary>
        public double StatUtilizationLastChange { get; set; }

        /// <summary>
        /// Working time time of entity.
        /// </summary>
        public double WorkingTime { get; set; }

        public double AverageUtilization => WorkingTime / (MySim.CurrentTime - BreakDuration);

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
            IsBusy = false;
            HadBreak = false;
            StatUtilizationLastChange = 0;
            WorkingTime = 0;
            BreakStarted = -1;
            BreakEnded = -1;
            BreakDuration = 0;
            Patient = null;
            State = EntityState.Free;
        }

        public void Assign(EntityPatient patient)
        {
            #region STATISTICS

            StatUtilizationLastChange = MySim.CurrentTime;

            #endregion

            Patient = patient;
            IsBusy = true;
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
            BreakStarted = -1;
            BreakEnded = -1;
            BreakDuration = 0;
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
