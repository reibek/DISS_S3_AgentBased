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

        public VaccineCentrumEntity(int id, Simulation simRef) : base(id, simRef)
        {
            StatUtilizationLastChange = 0;
            WorkingTime = 0;
        }

        public void Assign(EntityPatient patient)
        {
            #region STATISTICS

            StatUtilizationLastChange = MySim.CurrentTime;

            #endregion

            Patient = patient;
            IsBusy = true;
        }

        public void AcceptNext(EntityPatient patient)
        {
            #region STATISTICS

            WorkingTime += MySim.CurrentTime - StatUtilizationLastChange;
            StatUtilizationLastChange = MySim.CurrentTime;

            #endregion

            Patient = patient;
        }

        public void Release()
        {
            #region STATISTICS

            WorkingTime += MySim.CurrentTime - StatUtilizationLastChange;
            StatUtilizationLastChange = MySim.CurrentTime;


            #endregion

            Patient = null;
            IsBusy = false;
        }

        public virtual void Reset()
        {
            IsBusy = false;
            StatUtilizationLastChange = 0;
            WorkingTime = 0;
            Patient = null;
        }
    }
}
