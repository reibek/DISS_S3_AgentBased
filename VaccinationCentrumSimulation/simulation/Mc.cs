using OSPABA;
namespace simulation
{
	public class Mc : IdList
    {
		//meta! userInfo="Generated code: do not modify", tag="begin"
		public const int NoticePatientLeave = 1008;
		public const int NoticePatientArrival = 1001;
		public const int NoticeNewPatient = 1002;
		public const int RequestRegistration = 1003;
		public const int RequestExamination = 1004;
		public const int RequestVaccination = 1005;
		public const int RequestWaitingRoom = 1006;
		public const int Initialization = 1010;
		public const int RequestFillSyringes = 1011;
		public const int RequestAdminWorkerBreak = 1013;
		public const int RequestDoctorBreak = 1014;
		public const int RequestNurseBreak = 1015;
		public const int RequestEmployeeLunch = 1018;
		//meta! tag="end"

		// 1..1000 range reserved for user
        public const int NoticePatientGenerated = 10;
        public const int ProcessRegistrationEnded = 11;
        public const int ProcessExaminationEnded = 12;
        public const int ProcessVaccinationEnded = 13;
        public const int ProcessWaitingRoomEnded = 14;
        public const int ProcessFillingSyringesEnded = 15;
        public const int ProcessMovingRegToExaEnded = 16;
        public const int ProcessMovingExaToVacEnded = 17;
        public const int ProcessMovingVacToWaiEnded = 18;
        public const int ProcessMovingToFromCanEnded = 19;
        public const int ProcessMovingToFromColdStorEnded = 20;
	}
}