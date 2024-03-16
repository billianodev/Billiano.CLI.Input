namespace Billiano.CLI.Input;

/// <summary>
/// 
/// </summary>
public class CLInputOptions
{
	/// <summary>
	/// 
	/// </summary>
	public event EventHandler<string>? OnInputReceived;

	/// <summary>
	/// 
	/// </summary>
	public ConsoleColor? ForeColor { get; set; } 
	
	/// <summary>
	/// 
	/// </summary>
	public ConsoleColor? BackColor { get; set; }
	
	/// <summary>
	/// 
	/// </summary>
	public string Prompt { get; set; } = ">>> ";
	
	/// <summary>
	/// 
	/// </summary>
	public char Caret {  get; set; } = '_';

	/// <summary>
	/// 
	/// </summary>
	public CLInputOptions()
	{
	}

	internal CLInputOptions(EventHandler<string> onInputReceived)
	{
		OnInputReceived += onInputReceived;
	}

	internal void WriteInput(object obj, string e) => OnInputReceived?.Invoke(obj, e);
}
