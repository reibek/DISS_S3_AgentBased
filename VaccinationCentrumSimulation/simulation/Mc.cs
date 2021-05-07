using OSPABA;
namespace simulation
{
	public class Mc : IdList
    {
		//meta! userInfo="Generated code: do not modify", tag="begin"
		public const int NoticePatientGeneratingEnded = 1019;
		public const int NoticePreGeneratedPatientHoldEnded = 1021;
		public const int NoticePatientGenerated = 1020;
		public const int NoticePreGeneratedPatientPicked = 1023;
		public const int NoticeProcessMovingRegToExaEnded = 1025;
		public const int NoticeProcessMovingExaToVacEnded = 1026;
		public const int NoticeProcessMovingVacToWaiEnded = 1027;
		public const int NoticeProcessMovingToFromCanEnded = 1028;
		public const int NoticeTimeForBreak = 1029;
		public const int NoticeProcessRegistrationEnded = 1030;
		public const int NoticeProcessExaminationEnded = 1032;
		public const int NoticeProcessVaccinationEnded = 1034;
		public const int NoticeProcessMovingToFromColdStorEnded = 1035;
		public const int NoticeProcessWaitingRoomEnded = 1036;
		public const int NoticeProcessFillingSyringesEnded = 1037;
		public const int NoticeProcessEatingEnded = 1038;
		public const int NoticePatientArrival = 1001;
		public const int NoticeNewPatient = 1002;
		public const int RequestRegistration = 1003;
		public const int RequestExamination = 1004;
		public const int RequestVaccination = 1005;
		public const int RequestWaitingRoom = 1006;
		public const int NoticePatientLeave = 1008;
		public const int Initialization = 1010;
		public const int RequestFillSyringes = 1011;
		public const int RequestAdminWorkerBreak = 1013;
		public const int RequestDoctorBreak = 1014;
		public const int RequestNurseBreak = 1015;
		public const int RequestEmployeeLunch = 1018;
		//meta! tag="end"

		// 1..1000 range reserved for user
    }
}