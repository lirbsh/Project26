namespace Project26.Models
{
    internal class AlarmSetting(bool cancelAlarm, int secondToStart, string messageToSend)
    {
        public bool CancelAlarm { get; set; } = cancelAlarm;
        public int SecondToStart {  get; set; } = secondToStart;
        public string MessageToSend {  get; set; } = messageToSend;
    }
}
