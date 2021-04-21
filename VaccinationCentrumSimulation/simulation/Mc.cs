using OSPABA;
namespace simulation
{
	public class Mc : IdList
    {
		//meta! userInfo="Generated code: do not modify", tag="begin"
		public const int RequestRegistration = 1003;
		public const int RequestExamination = 1004;
		public const int RequestVaccination = 1005;
		public const int RequestWaitingRoom = 1006;
		public const int NoticePatientLeave = 1008;
		public const int Initialization = 1010;
		public const int NoticePatientGenerated = 1011;
		public const int NoticePatientArrival = 1001;
		public const int NoticeNewPatient = 1002;
		//meta! tag="end"

		// 1..1000 range reserved for user
	}
}