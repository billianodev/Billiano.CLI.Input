using System.Text;

namespace Billiano.CLI.Input;

/// <summary>
/// 
/// </summary>
/// <param name="options"></param>
public class CLInput(CLInputOptions options)
{
	private CancellationTokenSource? cts;
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="onInputReceived"></param>
	public CLInput(EventHandler<string> onInputReceived) : this(new CLInputOptions(onInputReceived))
	{
	}

	/// <summary>
	/// 
	/// </summary>
	/// <exception cref="InvalidOperationException"></exception>
	public void Initalize()
	{
		if (cts != null)
		{
			throw new InvalidOperationException();
		}

		cts = new CancellationTokenSource();

		Thread thread = new Thread(InitalizeInternal);
		thread.IsBackground = true;
		thread.Start();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <exception cref="InvalidOperationException"></exception>
	public void Stop()
	{
		if (cts == null)
		{
			throw new InvalidOperationException();
		}

		cts.Cancel();
		cts = null;
	}

	private void InitalizeInternal()
	{
		if (cts == null)
		{
			throw new InvalidOperationException();
		}

		Console.CursorVisible = false;

		while (!cts.IsCancellationRequested)
		{
			int pos = 0;
			StringBuilder str = new StringBuilder();
			Write();
			while (!cts.IsCancellationRequested)
			{
				ConsoleKeyInfo key = Console.ReadKey(true);
				switch (key.Key)
				{
					case ConsoleKey.Backspace:
						if (str.Length > 0)
						{
							if (key.Modifiers == ConsoleModifiers.Control)
							{
								int tpos = pos;
								int space;
								do
								{
									space = str.ToString().LastIndexOf(' ', pos - 1) + 1;
								}
								while (space == pos--);
								pos = tpos;
								str.Remove(space, pos - space);
								pos = space;
							}
							else
							{
								if (pos > 0)
								{
									str.Remove(--pos, 1);
								}
							}
							Write();
						}
						break;
					case ConsoleKey.Enter:
						goto _break;
					case ConsoleKey.LeftArrow:
						if (pos > 0)
						{
							pos--;
							Write();
						}
						break;
					case ConsoleKey.RightArrow:
						if (pos < str.Length)
						{
							pos++;
							Write();
						}
						break;
					default:
						if (key.KeyChar != default)
						{
							str.Insert(pos++, key.KeyChar);
							Write();
						}
						break;
				}
			}
		_break:
			string s = str.ToString();
			if (s.Length > 0)
			{
				options.WriteInput(this, s);
			}

			void Write()
			{
				lock (Console.Out)
				{
					int tleft = Console.CursorLeft;
					int ttop = Console.CursorTop;
					ConsoleColor tbcolor = Console.BackgroundColor;
					ConsoleColor tfcolor = Console.ForegroundColor;

					Console.SetCursorPosition(0, Console.BufferHeight - 1);
					if (options.BackColor.HasValue) Console.BackgroundColor = options.BackColor.Value;
					if (options.ForeColor.HasValue) Console.ForegroundColor = options.ForeColor.Value;

					string s = str.ToString().Insert(pos, options.Caret.ToString());

					int espace = Console.BufferWidth - options.Prompt.Length;
					int tspace = espace - s.Length;

					if (str.Length < espace)
					{
						Console.Write(options.Prompt);
						Console.Write(s);
						Console.Write(new string(' ', tspace));
					}
					else
					{
						int start = 0;
						int end = espace;
						if (pos > end)
						{
							start = pos - espace + 1;
						}
						Console.Write(options.Prompt);
						Console.Write(s.Substring(start, espace));
					}

					Console.BackgroundColor = tbcolor;
					Console.ForegroundColor = tfcolor;
					Console.SetCursorPosition(tleft, ttop);
				}
			}
		}
	}
}
