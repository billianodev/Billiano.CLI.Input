#if DEBUG
using Billiano.CLI.Input;

internal class Test
{
	private static void Main()
	{
		CLInputOptions options = new CLInputOptions()
		{
			BackColor = ConsoleColor.Blue
		};
		options.OnInputReceived += Options_OnInputReceived;

		CLInput input = new CLInput(options);
		input.Initalize();

		// Do other stuff
		CancellationToken.None.WaitHandle.WaitOne();
	}

	private static void Options_OnInputReceived(object sender, string e)
	{

	}
}
#endif