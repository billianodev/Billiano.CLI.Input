namespace Billiano.CLI.Input;

/// <summary>
/// 
/// </summary>
public class CLInputOptions
{
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
}
