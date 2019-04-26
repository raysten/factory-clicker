public interface ISpecialEventHandler
{
	void HandleEvent(EventData eventData);
	void ProcessOutcome(EventData.EventOutcome outcome);
	void EndEvent();
}
