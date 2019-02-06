using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPricer;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace scratch_c_sharp
{
	public class Node
	{
		public int Value;
		public Node Next;

		public Node(int value, Node next)
		{
			Value = value;
			Next = next;
		}
	}

	public class LinkedList
	{

		public Node Head;

		public LinkedList(int val)
		{
			Head = new Node(val, null);
		}

		public Node Push(ref Node node, int value)
		{
			return (node.Next != null) ? Push(ref node.Next, value) : node.Next = new Node(value, null);
		}

		public Node Pop(Node node, int value)
		{
			Node popped = null;

			if (node.Value == value)
			{
				popped = Head;
				Head = node.Next;
			}
			else if (node.Next != null)
			{
				if (node.Next.Value == value)
				{
					popped = node.Next;
					node.Next = node.Next.Next;
				}
				else if (node.Next.Next != null) return Pop(node.Next, value);
			}
			return popped;
		}

		public Node Push(int value)
		{
			return Push(ref Head, value);
		}

		public Node Search(Node node, int value)
		{
			if (node.Value == value) return node;
			else if (node.Next != null) return Search(node.Next, value);
			else return null;
		}
	}

	class Program
	{
		public enum Menu
		{
			LINKED_LIST,
			REMOVE_CHARS,
			REVERSE_WORDS,
			ROTATE_IMAGE,
			VALID_BRACES,
			FINAL_PRICE,
			CAR_PRICER,
			CONVERT_UTF8_TO_ISO88591,
			CLEAR_CONSOLE,
			EXIT
		}

		public enum ConversionMenu
		{
			ASCII,
			BIG_ENDIAN_UNICODE,
			DEFAULT,
			ISO_8859_1,
			UNICODE,
			UTF32,
			UTF_7,
			UTF_8,
			RETURN_TO_MAIN_MENU
		}

		static void Main(string[] args)
		{
			int menuChoice = -1;
			bool clearConsole = true;

			while (menuChoice != (int)Menu.EXIT)
			{
				WriteMenu(clearConsole);

				string selection = Console.ReadLine();

				menuChoice = -1;
				if (Int32.TryParse(selection, out menuChoice))
				{
					if (menuChoice >= 0 && menuChoice <= (int)Menu.EXIT)
					{
						switch (menuChoice)
						{
							case (int)Menu.LINKED_LIST: LinkedListRun(); break;
							case (int)Menu.REMOVE_CHARS: RemoveChars(); break;
							case (int)Menu.REVERSE_WORDS: ReverseWords(); break;
							case (int)Menu.ROTATE_IMAGE:
								int[][] a = new int[3][];
								a[0] = new int[] { 1, 2, 3 };
								a[1] = new int[] { 4, 5, 6 };
								a[2] = new int[] { 7, 8, 9 };

								rotateImage(a);
								break;
							case (int)Menu.VALID_BRACES:
								string[] values = new string[]
								{
									"6",
									"}][}}(}][))]",
									"[](){()}",
									"()",
									"({}([][]))[]()",
									"{)[](}]}]}))}(())(",
									"([[)",
									"((((())))){{{{{3}}}}}[[[[[((((()))))]]]]]",
									"]]]]]]]]]]]]]]}}}}}}}}}}}}",
									"[)[)[)",
									"[{{{{{]}}}}}"
								};

								string[] valid = braces(values);

								foreach (string v in valid)
								{
									Console.WriteLine("Validated String Value: " + v);
								}
								break;
							case (int)Menu.FINAL_PRICE:
								finalPrice(new List<int>() { 1,1,1,1,1,1,1,1,1,2,1,1,1,1,1,1000001 });
								break;
							case (int)Menu.CAR_PRICER:
								/*
									5-1=4
									1=5
									3-2=6
									3-2=7
									2=9
									5=14
								*/

								Car car = new Car()
								{
									AgeInMonths = 11 * 12,
									NumberOfCollisions = 7,
									NumberOfMiles = 253572,
									NumberOfPreviousOwners = 3,
									PurchaseValue = 35000
								};

								PriceDeterminator price = new PriceDeterminator();

								Console.WriteLine("$" + price.DetermineCarPrice(car));
							   
								break;
							case (int)Menu.CONVERT_UTF8_TO_ISO88591:
								ConvertFile();
								break;
							case (int)Menu.CLEAR_CONSOLE:
								clearConsole = true;
								Console.ForegroundColor = ConsoleColor.White;
								if (clearConsole)
									Console.WriteLine("Console will now clear each run.");
								else
									Console.WriteLine("Console text will stay each run.");
								break;
							case (int)Menu.EXIT: break;
							default: break;
						}
					}
					else
						WriteToConsole(ConsoleColor.Yellow, null, "Menu Input Is Invalid: '{0}'.\r\nPlease use a number between 0 and {1}", selection, Menu.EXIT.ToString());
				}
				else
					WriteToConsole(ConsoleColor.Yellow, null, "Menu Input Is Invalid: '{0}'.\r\nPlease use a number between 0 and {1}", selection, Menu.EXIT.ToString());

				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Press 'Enter' To Continue...");
				Console.ReadLine();
			}
		}

		static void ConvertFile(bool clearConsole = true)
		{
			WriteTextEncodingMenu("Select Source Encoding:", clearConsole);
			
			// TODO

			// Show menu and get the source file info from the user
			Console.WriteLine("Text Converstion Menu:\r\nOnly supports UTF-8 to ISO-8859-1 currently.\r\n\r\nInput a path to a file to convert:");
			string filename = "";
			string path = Console.ReadLine();

			if (String.IsNullOrEmpty(path))
			{
				path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				WriteToConsole(null, null, "Using default file path: '{0}'", path);
			}

			if (String.IsNullOrEmpty(new FileInfo(path).Extension))
			{
				Console.WriteLine("Input a file name to convert:");
				filename = Console.ReadLine();

				if (String.IsNullOrEmpty(filename))
				{
					filename = "2019-02-01-test-page.markdown";
					WriteToConsole(null, null, "Using default file name: '{0}'", filename);
				}
			}

			string fullPath = Path.Combine(path, filename);

			// Get the destination file info from the user
			Console.WriteLine("Input a path to a path you would like to write to:");
			string newFilename = "";
			string newPath = Console.ReadLine();

			if (String.IsNullOrEmpty(newPath))
			{
				//Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
				newPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Default");
				WriteToConsole(null, null, "Using default file path: '{0}'", newPath);
			}

			if (String.IsNullOrEmpty(new FileInfo(newPath).Extension))
			{
				Console.WriteLine("Input a filename to write to:");
				newFilename = Console.ReadLine();

				if (String.IsNullOrEmpty(newFilename))
				{
					newFilename = "2019-02-01-test-page.markdown";
					WriteToConsole(null, null, "Using default file name: '{0}'", newFilename);
				}
			}
			else
			{
				WriteToConsole(ConsoleColor.Yellow, null, "Found full file path in string: '{0}'\r\nSplitting path and filename...", newPath);

				int lastSlashIndex = newPath.LastIndexOf('\\') - 1;
				newFilename = newPath.Substring(lastSlashIndex, newPath.Length - 1 - lastSlashIndex);
				WriteToConsole(ConsoleColor.Yellow, null, "New filename: '{0}'", newFilename);

				newPath = newPath.Substring(0, lastSlashIndex);
				WriteToConsole(ConsoleColor.Yellow, null, "New path: '{0}'", newPath);
			}

			// Get text from file
			string text = ReadTextFromFileAsUTF8(fullPath);

			if (!String.IsNullOrEmpty(text))
			{
				// Convert the file to a byte array
				byte[] conversion = ConvertUTF8ToISO88591(text);

				if (conversion.Length > 1)
				{
					// Write byte array to file
					SaveBytesToFile(conversion, newPath, newFilename);
				}
			}
		}

		static byte[] ConvertUTF8ToISO88591 (string message)
		{
			try
			{
				WriteToConsole(ConsoleColor.White, null, "Encoding String...");
				Encoding UTF = Encoding.UTF8;
				Encoding ISO88591 = Encoding.GetEncoding("ISO-8859-1");
				byte[] utfBytes = UTF.GetBytes(message);
				byte[] isoBytes = Encoding.Convert(UTF, ISO88591, utfBytes);
				WriteToConsole(ConsoleColor.Green, null, "Done!\r\n");

				return isoBytes;
			}
			catch (Exception e)
			{
				WriteErrorToConsole(e);
				return new byte[] { 0 };
			}
		}

		static string ReadTextFromFileAsUTF8(string path)
		{
			if (!File.Exists(path))
			{
				WriteToConsole(ConsoleColor.Red, null, "Error! File does not exist at: '{0}'", path);
				return String.Empty;
			}

			string value = String.Empty;

			WriteToConsole(ConsoleColor.White, null, "Reading file from: '{0}'", path);
			using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				try
				{
					byte[] bytes = new byte[stream.Length];
					new Random().NextBytes(bytes);

					char block = (char)220;
					string bar = String.Empty;
					float percent = 0;

					Console.ForegroundColor = ConsoleColor.Green;

					for (int i = 0; i < stream.Length; i++)
					{ 
						percent = (float)i / (float)bytes.Length;
						bar = String.Empty;

						for (int j = 0; j <= percent; j += 7)
							bar += block;
						
						Console.Write(String.Format("{0} {1}%",bar, percent.ToString()));
						Console.SetCursorPosition(0, Console.CursorTop);
						bytes[i] = (byte)stream.ReadByte();
					}

					Encoding utf8 = Encoding.UTF8;
					byte[] utfBytes = Encoding.Convert(Encoding.Default, utf8, bytes.ToArray<byte>());
					value = utf8.GetString(utfBytes);
				}
				catch (Exception e)
				{
					WriteErrorToConsole(e);
				}
			}

			return value;
		}

		static void SaveBytesToFile(byte[] message, string path, string filename)
		{
			// Check path
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
				WriteToConsole(ConsoleColor.Yellow, null, "Created new directory at: '{0}'\r\n", path);
			}

			if (GrantAccess(path))
			{
				string fullpath = Path.Combine(path, filename);
				WriteToConsole(ConsoleColor.Yellow, null, "Checking file: '{0}'", fullpath);

				// Delete the file if it exists
				if (File.Exists(fullpath))
				{
					File.Delete(fullpath);
					WriteToConsole(ConsoleColor.Yellow, null, "Found file at: '{0}'\r\nDeleted file...\r\n", fullpath);
				}

				try
				{
					using (FileStream stream = new FileStream(fullpath, FileMode.Create, FileAccess.ReadWrite))
					{
						// Write message to the file
						for (int i = 0; i < message.Length; i++)
						{
							stream.WriteByte(message[i]);
						}

						// Verify the write bytes
						stream.Seek(0, SeekOrigin.Begin);

						for (int j = 0; j < stream.Length; j++)
						{
							try
							{
								if (stream.ReadByte() != message[j])
								{
									WriteToConsole(ConsoleColor.Red, null, "Error! Write stream failed to validate properly!");
								}
							}
							catch (Exception e)
							{
								WriteErrorToConsole(e, "An Error Occurred During Validation:\r\n\r\nMessage:\r\n{0}\r\n\r\nStack Trace:\r\n{1}\r\n");
							}
						}
					}
				}
				catch (Exception e)
				{
					WriteErrorToConsole(e);
				}
			}
		}

		static bool GrantAccess(string path)
		{
			DirectoryInfo di = new DirectoryInfo(path);
			var ds = di.GetAccessControl();

			try
			{
				DirectorySecurity security = Directory.GetAccessControl(path);
				WriteToConsole(ConsoleColor.Green, null, "Console has access to: '{0}'", path);
			}
			catch (Exception)
			{
				try
				{
					WriteToConsole(ConsoleColor.Yellow, null, "Console requesting access to: '{0}'", path);
					ds.AddAccessRule(new FileSystemAccessRule(
						new SecurityIdentifier(WellKnownSidType.SelfSid, null),
						FileSystemRights.FullControl,
						InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
						PropagationFlags.NoPropagateInherit,
						AccessControlType.Allow));
					di.SetAccessControl(ds);
					WriteToConsole(ConsoleColor.Green, null, "Done!", null);
				}
				catch(Exception ex)
				{
					WriteErrorToConsole(ex, "Error requesting access to: '{0}'");
					return false;
				}
			}

			return true;
		}
		
		static void WriteToConsole(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string message, params string[] args)
		{
			if (args == null)
				args = new string[] { "" };

			string Text = String.Format(message, args);

			if (foregroundColor != null)
				Console.ForegroundColor = (ConsoleColor)foregroundColor;

			if (backgroundColor != null)
				Console.BackgroundColor = (ConsoleColor)backgroundColor;

			Console.WriteLine(Text);
			
			// reset console colors
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Black;
		}

		static void WriteErrorToConsole(Exception e)
		{
			WriteToConsole(ConsoleColor.Red, null, "An Error Occurred:\r\n\r\nMessage:\r\n{0}\r\n\r\nStack Trace:\r\n{1}\r\n", e.Message, e.StackTrace);
		}

		static void WriteErrorToConsole(Exception e, string message)
		{
			string m = message;
			if (String.IsNullOrEmpty(message))
				m = "An Error Occurred:\r\n\r\nMessage:\r\n{0}\r\n\r\nStack Trace:\r\n{1}\r\n";

			WriteToConsole(ConsoleColor.Red, null, m, e.Message, e.StackTrace);
		}

		static void WriteMenu(bool clearConsole = true)
		{
			WriteMenu(
				new string[] {
				"0. Linked List\r\n",
				"1. Remove Characters\r\n",
				"2. Reverse Words\r\n",
				"3. Rotate Image (Multidementional Array)\r\n",
				"4. Validate Braces\r\n",
				"5. Final Price\r\n",
				"6. Car Pricer\r\n",
				"7. Convert UTF-8 File To ISO-8859-1\r\n",
				"\r\nApplication Settings:",
				"8. Toggle Clear Console\r\n",
				"9. Exit"
				},
				"Main Menu:\r\n",
				clearConsole);
		}

		static void WriteMenu(string[] args, string headline, bool clearConsole = true)
		{
			if (clearConsole)
				Console.Clear();

			string formatting = headline;

			for (int i = 0; i < args.Length; i++)
			{
				formatting += "{" + i + "}";
			}

			WriteToConsole(Console.ForegroundColor = ConsoleColor.Cyan, null, formatting, args);
		}

		static void WriteTextEncodingMenu(string SourceDestHeadline, bool clearConsole = true)
		{
			WriteMenu(new string[] {
				SourceDestHeadline + "\r\n",
				"0. ASCII\r\n",
				"1. Big Endian Unicode\r\n",
				"2. Default\r\n",
				"3. ISO-8859-1\r\n",
				"4. Unicode\r\n",
				"5. UTF-32\r\n",
				"6. UTF-7\r\n",
				"7. UTF-8\r\n",
				"\r\nSettings:\r\n",
				"8. Return To Main Menu"
			},
			"Text Encoding Menu:\r\n\r\n",
			clearConsole);
		}

		static void LinkedListRun()
		{
			Console.WriteLine("How bout some a linked list? LESDOIT!");

			Console.WriteLine("Let's create some elements in a linked list.  How about 15 items...");

			Random rand = new Random();
			LinkedList list = new LinkedList(rand.Next());
			Console.Write(list.Head.Value);

			for (int i = 1; i < 15; i++)
			{
				Console.Write(", " + list.Push(rand.Next()).Value);
			}

			// search
			Console.WriteLine("\r\nDone...coolio.\r\nHey, type a search number!");

			int search = -1;
			Int32.TryParse(Console.ReadLine(), out search);

			Console.WriteLine("\r\nLet's search for that value");
			Node found = list.Search(list.Head, search);

			Console.Write("I found...");
			if (found == null) Console.Write("NOTHING");
			else Console.Write(found.Value);

			// pop
			Console.WriteLine("\r\nDone...coolio.\r\nHey, type another number!");

			int pop = -1;
			Int32.TryParse(Console.ReadLine(), out pop);

			Console.WriteLine("\r\nLet's search for that value too...muah ha ha");
			Node popped = list.Pop(list.Head, pop);

			Console.Write("Hey, guess what...");
			if (found == null) Console.Write("uhh...nvm");
			else Console.Write(popped.Value + "just got deleted lol");
		}

		static void RemoveChars()
		{
			// create a hash table with the remove chars
			// create a output array of the same len as the original string
			// loop through the array
			// either add each char to the output array or don't
			// keep track of index of the output array for the last char added

			string input = "hi, i am a string and I am going to have some chars deleted...yay.";
			string deleteChars = "aeiou,";

			Console.WriteLine(String.Format("Here is the original string!\r\n\"{0}\"", input));

			HashSet<char> dChars = new HashSet<char>();

			for (int i = 0; i < deleteChars.Length; i++)
			{
				dChars.Add(deleteChars[i]);
			}

			StringBuilder sb = new StringBuilder();

			for (int j = 0; j < input.Length; j++)
			{
				if (!dChars.Contains(input[j]))
				{
					sb.Append(input[j]);
				}
			}

			Console.WriteLine(String.Format("Here is the new string!\r\n\"{0}\"", sb.ToString()));
		}

		static void ReverseWords()
		{
			// create a end index
			// scan through the string for whitespace
			// if the current space is whitespace, loop back throught he string to 'end' and copy chars to a string builder
			// insert whitespace in the string builder and update end index

			string input = "are these Yoda, of Words";
			char delimeter = ' ';

			int end = input.Length;
			StringBuilder sb = new StringBuilder();

			for (int i = (input.Length - 1); i >= 0; i--)
			{
				if ((input[i] == delimeter && (end - i > 1)))
				{
					for (int j = i + 1; j < end; j++)
					{
						sb.Append(input[j]);
					}
					
					sb.Append(delimeter);

					end = i;
				}
				else if (i == 0 && (end - i > 1))
				{
					for (int j = i; j < end; j++)
					{
						sb.Append(input[j]);
					}
				}
			}

			Console.WriteLine(String.Format("OG: {0} \r\nNew: {1}", input, sb.ToString()));
		}

		public static int[][] rotateImage(int[][] a)
		{
			int temp = 0;
			int iX;
			int iY;
			for (int y = 0; y < Math.Floor((double)a.Length / 2); y++)
			{
				for (int x = 0; x <= a[y].Length - 1; x++)
				{
					if (x < y)
					{
						continue;
					}

					Console.Write("a=" + a[y][x] + ",x=" + x + ",y=" + y + " - ");

					//first swap
					temp = a[y][x];
					iX = a[y].Length - 1 - y;
					iY = x;
					Console.Write("Swap1: " + a[y][x] + " with " + a[iY][iX] + " - ");
					a[y][x] = a[iY][iX];
					a[iY][iX] = temp;

					//second swap
					temp = a[y][x];
					iX = a[y].Length - 1 - x;
					iY = a.Length - 1 - y;
					Console.Write("Swap2: " + a[y][x] + " with " + a[iY][iX] + " - ");
					a[y][x] = a[iY][iX];
					a[iY][iX] = temp;

					//third swap
					temp = a[y][x];
					iX = y;
					iY = a.Length - 1 - x;
					Console.Write("Swap3: " + a[y][x] + " with " + a[iY][iX] + " - \r\n");
					a[y][x] = a[iY][iX];
					a[iY][iX] = temp;
				}
			}

			return a;
		}

		public static string[] braces(string[] values)
		{
			string[] returnVals = new string[values.Length];

			// Loop through the strings given
			for (int i = 0; i < values.Length; i++)
			{
				returnVals[i] = "YES";

				string temp = values[i]; // Take a string from the array
				bool done = false; // Used to exit an invalid string (Example: non-bracket char encountered)

				// Tack open brackets
				List<char> openBraces = new List<char>();

				// Loop through the string to check for valid braces
				for (int j = 0; j < temp.Length; j++)
				{
					// Edge case: If there are more than temp.Length / 2 open braces, then the current string cannot be valid
					if (openBraces.Count > (temp.Length / 2))
					{
						returnVals[i] = "NO";
						break;
					}

					switch (temp[j])
					{
						case '(':
						case '[':
						case '{':
							openBraces.Add(temp[j]);
							break;
						case ')': // One improvment could be to merge all the cases for closed brackets like above, but time contraints kept me from that
							if (openBraces.Count == 0)
							{
								returnVals[i] = "NO";
								done = true;
								break;
							}
							else if (openBraces.Last() == '(')
							{
								openBraces.RemoveAt(openBraces.Count - 1);
							}
							else
							{
								returnVals[i] = "NO";
								done = true;
								break;
							}
							break;
						case ']':
							if (openBraces.Count == 0)
							{
								returnVals[i] = "NO";
								done = true;
								break;
							}
							else if (openBraces.Last() == '[')
							{
								openBraces.RemoveAt(openBraces.Count - 1);
							}
							else
							{
								returnVals[i] = "NO";
								done = true;
								break;
							}
							break;
						case '}':
							if (openBraces.Count == 0)
							{
								returnVals[i] = "NO";
								done = true;
								break;
							}
							else if (openBraces.Last() == '{')
							{
								openBraces.RemoveAt(openBraces.Count - 1);
							}
							else
							{
								returnVals[i] = "NO";
								done = true;
								break;
							}
							break;
						default:
							returnVals[i] = "";
							done = true;
							break;
					}

					if (done)
					{
						break;
					}
				}
			}

			return returnVals;
		}

		public static void finalPrice(List<int> prices)
		{
			string noDiscountIndices = "";
			int total = 0;

			for (int i = 0; i < prices.Count; i++)
			{
				bool totalAdded = false;
				int price = prices[i];

				// Catch edge cases for negatives/0/greater than max value
				if (price <= 0 || price > 1000000 || prices.Count > 100000)
				{
					noDiscountIndices = "";
					total = 0;
					break;
				}

				// Big O Worse Case = O(n^2) - yikes
				for (int j = i + 1; j < prices.Count; j++)
				{
					if (price >= prices[j])
					{
						total += price - prices[j];
						totalAdded = true;
						break; // Stop the loop to save some cycles
					}
				}

				if (totalAdded)
				{
					continue;
				}
				else
				{
					total += price;
					noDiscountIndices += (i + " ");
				}
			}

			Console.WriteLine(total);
			Console.WriteLine(noDiscountIndices.TrimEnd());
		}
	}
}
