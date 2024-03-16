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

		CLInput input = new CLInput(options, (obj, s) =>
		{
			Console.WriteLine(s);
		});
		input.Initalize();

		// Do other stuff
		CancellationToken.None.WaitHandle.WaitOne();
	}
}
#endif