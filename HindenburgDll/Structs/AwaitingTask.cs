namespace HindenburgDll.Structs
{
	public struct AwaitingTask
	{
		public IntPtr Thread;
		public int ThreadRef;
		public Task<Action> Task;
	}
}
