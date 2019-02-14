using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPricer;
using Validation;
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
			CONVERT_TEXT_ENCODING,
			TEST,
			GENERATE_UNICODE_FILES,
			CONVERT_TEXT_USING_CLASS,
			CLEAR_CONSOLE,
			EXIT
		}

		public static Dictionary<int, string> TextEncodings = new Dictionary<int, string>()
		{
			{ 708,"  ASMO-708:                Arabic (ASMO 708)" },
			{ 950,"  big5:                    Chinese Traditional (Big5)" },
			{ 21025,"cp1025:                  IBM EBCDIC (Cyrillic Serbian-Bulgarian)" },
			{ 866,"  cp866:                   Cyrillic (DOS)" },
			{ 875,"  cp875:                   IBM EBCDIC (Greek Modern)" },
			{ 50221,"csISO2022JP:             Japanese (JIS-Allow 1 byte Kana)" },
			{ 720,"  DOS-720:                 Arabic (DOS)" },
			{ 862,"  DOS-862:                 Hebrew (DOS)" },
			{ 51936,"EUC-CN:                  Chinese Simplified (EUC)" },
			{ 20932,"EUC-JP:                  Japanese (JIS 0208-1990 and 0212-1990)" },
			{ 51932,"euc-jp:                  Japanese (EUC)" },
			{ 51949,"euc-kr:                  Korean (EUC)" },
			{ 54936,"GB18030:                 Chinese Simplified (GB18030)" },
			{ 936,"  gb2312:                  Chinese Simplified (GB2312)" },
			{ 52936,"hz-gb-2312:              Chinese Simplified (HZ)" },
			{ 858,"  IBM00858:                OEM Multilingual Latin I" },
			{ 20924,"IBM00924:                IBM Latin-1" },
			{ 1047," IBM01047:                IBM Latin-1" },
			{ 1140," IBM01140:                IBM EBCDIC (US-Canada-Euro)" },
			{ 1141," IBM01141:                IBM EBCDIC (Germany-Euro)" },
			{ 1142," IBM01142:                IBM EBCDIC (Denmark-Norway-Euro)" },
			{ 1143," IBM01143:                IBM EBCDIC (Finland-Sweden-Euro)" },
			{ 1144," IBM01144:                IBM EBCDIC (Italy-Euro)" },
			{ 1145," IBM01145:                IBM EBCDIC (Spain-Euro)" },
			{ 1146," IBM01146:                IBM EBCDIC (UK-Euro)" },
			{ 1147," IBM01147:                IBM EBCDIC (France-Euro)" },
			{ 1148," IBM01148:                IBM EBCDIC (International-Euro)" },
			{ 1149," IBM01149:                IBM EBCDIC (Icelandic-Euro)" },
			{ 37,"   IBM037:                  IBM EBCDIC (US-Canada)" },
			{ 1026," IBM1026:                 IBM EBCDIC (Turkish Latin-5)" },
			{ 20273,"IBM273:                  IBM EBCDIC (Germany)" },
			{ 20277,"IBM277:                  IBM EBCDIC (Denmark-Norway)" },
			{ 20278,"IBM278:                  IBM EBCDIC (Finland-Sweden)" },
			{ 20280,"IBM280:                  IBM EBCDIC (Italy)" },
			{ 20284,"IBM284:                  IBM EBCDIC (Spain)" },
			{ 20285,"IBM285:                  IBM EBCDIC (UK)" },
			{ 20290,"IBM290:                  IBM EBCDIC (Japanese katakana)" },
			{ 20297,"IBM297:                  IBM EBCDIC (France)" },
			{ 20420,"IBM420:                  IBM EBCDIC (Arabic)" },
			{ 20423,"IBM423:                  IBM EBCDIC (Greek)" },
			{ 20424,"IBM424:                  IBM EBCDIC (Hebrew)" },
			{ 437,"  IBM437:                  OEM United States" },
			{ 500,"  IBM500:                  IBM EBCDIC (International)" },
			{ 737,"  ibm737:                  Greek (DOS)" },
			{ 775,"  ibm775:                  Baltic (DOS)" },
			{ 850,"  ibm850:                  Western European (DOS)" },
			{ 852,"  ibm852:                  Central European (DOS)" },
			{ 855,"  IBM855:                  OEM Cyrillic" },
			{ 857,"  ibm857:                  Turkish (DOS)" },
			{ 860,"  IBM860:                  Portuguese (DOS)" },
			{ 861,"  ibm861:                  Icelandic (DOS)" },
			{ 863,"  IBM863:                  French Canadian (DOS)" },
			{ 864,"  IBM864:                  Arabic (864)" },
			{ 865,"  IBM865:                  Nordic (DOS)" },
			{ 869,"  ibm869:                  Greek, Modern (DOS)" },
			{ 870,"  IBM870:                  IBM EBCDIC (Multilingual Latin-2)" },
			{ 20871,"IBM871:                  IBM EBCDIC (Icelandic)" },
			{ 20880,"IBM880:                  IBM EBCDIC (Cyrillic Russian)" },
			{ 20905,"IBM905:                  IBM EBCDIC (Turkish)" },
			{ 20838,"IBM-Thai:                IBM EBCDIC (Thai)" },
			{ 50220,"iso-2022-jp:             Japanese (JIS)" },
			{ 50222,"iso-2022-jp:             Japanese (JIS-Allow 1 byte Kana - SO/SI)" },
			{ 50225,"iso-2022-kr:             Korean (ISO)" },
			{ 28591,"iso-8859-1:              Western European (ISO)" },
			{ 28603,"iso-8859-13:             Estonian (ISO)" },
			{ 28605,"iso-8859-15:             Latin 9 (ISO)" },
			{ 28592,"iso-8859-2:              Central European (ISO)" },
			{ 28593,"iso-8859-3:              Latin 3 (ISO)" },
			{ 28594,"iso-8859-4:              Baltic (ISO)" },
			{ 28595,"iso-8859-5:              Cyrillic (ISO)" },
			{ 28596,"iso-8859-6:              Arabic (ISO)" },
			{ 28597,"iso-8859-7:              Greek (ISO)" },
			{ 28598,"iso-8859-8:              Hebrew (ISO-Visual)" },
			{ 38598,"iso-8859-8-i:            Hebrew (ISO-Logical)" },
			{ 28599,"iso-8859-9:              Turkish (ISO)" },
			{ 1361," Johab:                   Korean (Johab)" },
			{ 20866,"koi8-r:                  Cyrillic (KOI8-R)" },
			{ 21866,"koi8-u:                  Cyrillic (KOI8-U)" },
			{ 949,"  ks_c_5601-1987:          Korean" },
			{ 10000,"macintosh:               Western European (Mac)" },
			{ 932,"  shift_jis:               Japanese (Shift-JIS)" },
			{ 20127,"us-ascii:                US-ASCII" },
			{ 1200," utf-16:                  Unicode" },
			{ 1201," utf-16BE:                Unicode (Big-Endian)" },
			{ 12000,"utf-32:                  Unicode (UTF-32)" },
			{ 12001,"utf-32BE:                Unicode (UTF-32 Big-Endian)" },
			{ 65000,"utf-7:                   Unicode (UTF-7)" },
			{ 65001,"utf-8:                   Unicode (UTF-8)" },
			{ 1250," windows-1250:            Central European (Windows)" },
			{ 1251," windows-1251:            Cyrillic (Windows)" },
			{ 1252," Windows-1252:            Western European (Windows)" },
			{ 1253," windows-1253:            Greek (Windows)" },
			{ 1254," windows-1254:            Turkish (Windows)" },
			{ 1255," windows-1255:            Hebrew (Windows)" },
			{ 1256," windows-1256:            Arabic (Windows)" },
			{ 1257," windows-1257:            Baltic (Windows)" },
			{ 1258," windows-1258:            Vietnamese (Windows)" },
			{ 874,"  windows-874:             Thai (Windows)" },
			{ 20000,"x-Chinese-CNS:           Chinese Traditional (CNS)" },
			{ 20002,"x-Chinese-Eten:          Chinese Traditional (Eten)" },
			{ 20001,"x-cp20001:               TCA Taiwan" },
			{ 20003,"x-cp20003:               IBM5550 Taiwan" },
			{ 20004,"x-cp20004:               TeleText Taiwan" },
			{ 20005,"x-cp20005:               Wang Taiwan" },
			{ 20261,"x-cp20261:               T.61" },
			{ 20269,"x-cp20269:               ISO-6937" },
			{ 20936,"x-cp20936:               Chinese Simplified (GB2312-80)" },
			{ 20949,"x-cp20949:               Korean Wansung" },
			{ 50227,"x-cp50227:               Chinese Simplified (ISO-2022)" },
			{ 20833,"x-EBCDIC-KoreanExtended: IBM EBCDIC (Korean Extended)" },
			{ 29001,"x-Europa:                Europa" },
			{ 20105,"x-IA5:                   Western European (IA5)" },
			{ 20106,"x-IA5-German:            German (IA5)" },
			{ 20108,"x-IA5-Norwegian:         Norwegian (IA5)" },
			{ 20107,"x-IA5-Swedish:           Swedish (IA5)" },
			{ 57006,"x-iscii-as:              ISCII Assamese" },
			{ 57003,"x-iscii-be:              ISCII Bengali" },
			{ 57002,"x-iscii-de:              ISCII Devanagari" },
			{ 57010,"x-iscii-gu:              ISCII Gujarati" },
			{ 57008,"x-iscii-ka:              ISCII Kannada" },
			{ 57009,"x-iscii-ma:              ISCII Malayalam" },
			{ 57007,"x-iscii-or:              ISCII Oriya" },
			{ 57011,"x-iscii-pa:              ISCII Punjabi" },
			{ 57004,"x-iscii-ta:              ISCII Tamil" },
			{ 57005,"x-iscii-te:              ISCII Telugu" },
			{ 10004,"x-mac-arabic:            Arabic (Mac)" },
			{ 10029,"x-mac-ce:                Central European (Mac)" },
			{ 10008,"x-mac-chinesesimp:       Chinese Simplified (Mac)" },
			{ 10002,"x-mac-chinesetrad:       Chinese Traditional (Mac)" },
			{ 10082,"x-mac-croatian:          Croatian (Mac)" },
			{ 10007,"x-mac-cyrillic:          Cyrillic (Mac)" },
			{ 10006,"x-mac-greek:             Greek (Mac)" },
			{ 10005,"x-mac-hebrew:            Hebrew (Mac)" },
			{ 10079,"x-mac-icelandic:         Icelandic (Mac)" },
			{ 10001,"x-mac-japanese:          Japanese (Mac)" },
			{ 10003,"x-mac-korean:            Korean (Mac)" },
			{ 10010,"x-mac-romanian:          Romanian (Mac)" },
			{ 10021,"x-mac-thai:              Thai (Mac)" },
			{ 10081,"x-mac-turkish:           Turkish (Mac)" },
			{ 10017,"x-mac-ukrainian:         Ukrainian (Mac)" }
		};

		static bool FileIsValidUnicode(string filepath)
		{
			string text;

			try
			{
				byte[] b = File.ReadAllBytes(filepath);

				text = Encoding.UTF8.GetString(b);

				for (int i = 0; i < b.Length - 4; i++)
				{
					if (b[i] >= 0xF0 && b[i] <= 0xF4 && b[i + 1] >= 0x80 && b[i + 1] < 0xC0 && b[i + 2] >= 0x80 && b[i + 2] < 0xC0 && b[i + 3] >= 0x80 && b[i + 3] < 0xC0) { i += 3; continue; }
					if (b[i] >= 0xE0 && b[i] <= 0xF0 && b[i + 1] >= 0x80 && b[i + 1] < 0xC0 && b[i + 2] >= 0x80 && b[i + 2] < 0xC0) { i += 2; continue; }
					if (b[i] >= 0xC2 && b[i] <= 0xDF && b[i + 1] >= 0x80 && b[i + 1] < 0xC0) { i += 1; continue; }
					if (b[i] <= 0x7f) { continue; }
					return false;
				}
			}
			catch (Exception e)
			{
				WriteErrorToConsole(e);
				return true;
			}

			return true;
		}

		static Encoding GetFileEncoding(string filePath)
		{
			try
			{
				Encoding encoding;

				using (StreamReader sr = new StreamReader(filePath, true))
				{
					if (sr.Peek() >= 0)
						sr.Read();

					encoding = sr.CurrentEncoding;
				}

				return encoding;
			}
			catch (Exception e)
			{
				WriteErrorToConsole(e);
				return null;
			}
		}

		static void Main(string[] args)
		{
			//int spacing = 24;
			//int leading_spaces = 5;
			//var encodings = Encoding.GetEncodings();
			//List<string> output = new List<string>();
			//foreach (var encoding in encodings)
			//{
			//	string tmp_spaces = new string(' ', spacing - encoding.Name.Length);
			//	string tmp_spaces2 = new string(' ', leading_spaces - encoding.CodePage.ToString().Length);
			//	//Console.WriteLine(String.Format("{1}, // {0}", encoding.DisplayName, encoding.Name.ToUpper().Replace('-','_')));
			//	//output.Add(String.Format("{{ \"{1}\",\"{2}\" }}, // {0}", encoding.DisplayName, encoding.Name.ToUpper().Replace('-', '_'), encoding.CodePage));
			//	//output.Add(String.Format("{0} = {1}, // {2}", encoding.Name.ToUpper().Replace('-', '_'), encoding.CodePage, encoding.DisplayName));
			//	output.Add(String.Format("{2}	{{ {1},\"{4}{2}:{3}{0}\" }},", encoding.DisplayName, encoding.CodePage, encoding.Name, tmp_spaces, tmp_spaces2));
			//}

			//string[] o = output.ToArray();
			//Array.Sort(o);
			//foreach (var s in o)
			//{
			//	Console.WriteLine(s.Substring(s.IndexOf('{'), s.Length - s.IndexOf('{')));
			//}

			//Console.ReadLine();

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
								finalPrice(new List<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1000001 });
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
							case (int)Menu.CONVERT_TEXT_ENCODING:
								ConvertFile();
								break;
							case (int)Menu.TEST:
								WriteToConsole(ConsoleColor.Green, null, "Type in a file path & name...");
								string text = Console.ReadLine();
								WriteToConsole(ConsoleColor.Green, null, "The string is a valid UTF-8 string = {0}", new string[] { FileIsValidUnicode(text).ToString() });
								break;
							case (int)Menu.GENERATE_UNICODE_FILES:
								int tmpsel = -1;
								while (tmpsel != 2)
								{
									WriteMenu(new string[] {
									"0. Use Default Settings\r\n",
									"1. Input Custom Settings\r\n",
									"2. Return to Main Menu"
								}, "Generate Unicode Files Menu\r\n", clearConsole);
									selection = Console.ReadLine();
									Int32.TryParse(selection, out tmpsel);
									switch (tmpsel)
									{
										case 0:
										default:
											WriteToConsole(ConsoleColor.Green, null, "Creating Test File...");
											EncodingValidation.CreateUnicodeTestFiles();
											WriteToConsole(ConsoleColor.Green, null, "Done!");
											tmpsel = 2;
											break;
										case 1:
											int tmpsel2 = -1;
											int numFiles = 1;
											int unicodeRange = 109384;

											while (tmpsel2 != 3)
											{
												WriteMenu(new string[]
												{
													String.Format("0. Input Number of Files To Create.  Currently: {0}\r\n", numFiles),
													String.Format("1. Input the Range of Unicode Characters to Create.  Currently: {0}\r\n", unicodeRange),
													(numFiles > 1 ? "2. Create Files" : "2. Create File\r\n"),
													"3. Return to File Creation Menu\r\n"
												}, "Select File Generation Options\r\n", clearConsole);
												selection = Console.ReadLine();
												Int32.TryParse(selection, out tmpsel2);
												switch (tmpsel2)
												{
													case 0:
														selection = Console.ReadLine();
														Int32.TryParse(selection, out numFiles);
														break;
													case 1:
														selection = Console.ReadLine();
														Int32.TryParse(selection, out unicodeRange);
														break;
													case 2:
														WriteToConsole(ConsoleColor.Green, null, "Creating Test Files...");
														EncodingValidation.CreateUnicodeTestFiles();
														WriteToConsole(ConsoleColor.Green, null, "Done!");
														tmpsel2 = 3;
														tmpsel = 2;
														break;
													case 3:
													default:
														break;
												}
											}
											break;
										case 2:
											break;
									}
								}
								break;
							case (int)Menu.CONVERT_TEXT_USING_CLASS:
								WriteToConsole(ConsoleColor.Green, null, "Running UTF-8 Validation & Conversion on Test Files...");
								WriteToConsole(null, null, "Input a file path");
								string input = Console.ReadLine();
								input = input.Trim('"');
								WriteToConsole(null, null, "Checking for UTF-8 Validity...");
								if (!EncodingValidation.FileIsValidUTF8(input))
								{
									WriteToConsole(ConsoleColor.Red, null, "File is not valid UTF-8");
									WriteToConsole(null, null, "Encoding file to UTF-8");

									string dest = input.Insert(input.LastIndexOf(".") - 1, "UTF-8");

									new EncodingValidation(input, dest);

									WriteToConsole(ConsoleColor.Green, null, "Done!");
								}
								else
									WriteToConsole(ConsoleColor.Green, null, "File is a valid UTF-8 file");
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

		static string DEFAULT_DEST_FILENAME(string newFilename, Encoding destEncoding)
		{
			string output = String.IsNullOrEmpty(newFilename) ? "output.markdown" : newFilename;
			if (destEncoding == null)
				output = output.Insert(output.IndexOf(new FileInfo(output).Extension), "_UNKNOWN");
			else output = output.Insert(output.IndexOf(new FileInfo(output).Extension), "_" + destEncoding.EncodingName.Replace(' ', '-'));
			return output;
		}

		static void ConvertFile(bool clearConsole = true)
		{
			// Encoding Statics
			string DEFAULT_SOURCE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			string DEFAULT_SOURCE_FILENAME = "output.markdown";
			string DEFAULT_DEST_PATH = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

			bool useFileSourceEncoding = false;

			Encoding sourceEncoding = null;
			Encoding destEncoding = null;
			string filename = "";
			string path = "";
			string fullPath = "";
			string newFilename = "";
			string newPath = "";
			string selection;

			string sourceEncodingHeadline = "Select Source Encoding:\r\n";
			string destEncodingHeadline = "Select Destination Encoding:\r\n";
			string sourcePathHeadline = "Select a Source Folder Path:\r\n";
			string destPathHeadline = "Select a Destination Folder Path:\r\n";
			string sourceFileHeadline = "Select a Source File Name:\r\n";
			string destFileHeadline = "Select a Destination File Name:\r\n";


			string[] sourceEncodingMenu = new string[]
			{
				"0. Use Source Text Encoding\r\n",
				"1. Choose Specific Text Encoding\r\n"
			};
			string[] destEncodingMenu = new string[]
			{
				"0. Use Default Text Encoding (UTF-8)\r\n",
				"1. Choose Specific Text Encoding\r\n"
			};
			string[] fileMenu = new string[]
			{
				String.Format("0. Use Default File Name\r\n"),
				"1. Input Custom File Name\r\n"
			};
			string[] pathMenu = new string[]
			{
				String.Format("0. Use Default Path\r\n"),
				"1. Input Custom Folder Path\r\n"
			};

			bool goToMainMenu = false;

			while (!goToMainMenu)
			{
				int selectionIndex = -1;
				string[] encodingMainMenu = new string[]
				{
					sourceEncoding == null ? "0. Select Source Encoding\r\n" : String.Format("0. Reset Source Encoding: Source = '{0}'\r\n", sourceEncoding.EncodingName),
					destEncoding == null ? "1. Select Destination Encoding\r\n" : String.Format("1. Reset Destination Encoding: Dest = '{0}'\r\n", destEncoding.EncodingName),
					String.IsNullOrEmpty(path) ? "2. Select a Source Folder Path\r\n" : String.Format("2. Reset Source Folder Path: Source = '{0}'\r\n", path),
					String.IsNullOrEmpty(filename) ? "3. Select a Source File Name\r\n" : String.Format("3. Reset Source File Name: Source = '{0}'\r\n", filename),
					String.IsNullOrEmpty(newPath) ? "4. Select a Destination Folder Path\r\n" : String.Format("4. Reset Destination Folder Path: Dest = '{0}'\r\n", newPath),
					String.IsNullOrEmpty(newFilename) ? "5. Select a Destination File Name\r\n" : String.Format("5. Reset Destination File Name: Dest = '{0}'\r\n", newFilename),
					"6. Start Encoding\r\n"
				};

				WriteTextEncodingMenu(encodingMainMenu, "Encoding Main Menu\r\n", clearConsole);
				selection = Console.ReadLine();

				Int32.TryParse(selection, out selectionIndex);

				switch (selectionIndex)
				{
					case 0: // Source Encoding
						WriteTextEncodingMenu(sourceEncodingMenu, sourceEncodingHeadline, clearConsole);
						selection = Console.ReadLine();

						int tmp = -1;
						Int32.TryParse(selection, out tmp);

						switch (tmp)
						{
							case 0:
							default:
								if (!String.IsNullOrEmpty(path) && !String.IsNullOrEmpty(filename))
									sourceEncoding = GetFileEncoding(Path.Combine(path, filename));
								else
									useFileSourceEncoding = true;
								break;
							case 1:
								useFileSourceEncoding = false;
								string[] encodings = new string[TextEncodings.Count];
								int i = 0;

								foreach (var o in TextEncodings.AsEnumerable())
								{
									encodings[i++] = String.Format("{0}. {1}\r\n", o.Key, o.Value);
								}

								WriteMenu(encodings, "Encoding List:\r\n", clearConsole);
								selection = Console.ReadLine();

								int enc = -1;
								if (Int32.TryParse(selection, out enc))
								{
									try
									{
										sourceEncoding = Encoding.GetEncoding(enc);
									}
									catch (Exception e)
									{
										WriteErrorToConsole(e);
									}
								}
								else
								{
									WriteToConsole(ConsoleColor.Red, null, "Error: Failed to successfully find a text encoder with the code: {0}", selection);
								}
								break;
						}
						break;
					case 1: // Destination Encoding
						WriteTextEncodingMenu(destEncodingMenu, destEncodingHeadline, clearConsole);
						selection = Console.ReadLine();

						int tmp2 = -1;
						Int32.TryParse(selection, out tmp2);

						switch (tmp2)
						{
							case 0:
							default:
								destEncoding = Encoding.UTF8;
								break;
							case 1:
								string[] encodings = new string[TextEncodings.Count];
								int i = 0;

								foreach (var o in TextEncodings.AsEnumerable())
								{
									encodings[i++] = String.Format("{0}. {1}\r\n", o.Key, o.Value);
								}

								WriteMenu(encodings, "Encoding List:\r\n", clearConsole);
								selection = Console.ReadLine();

								int enc = -1;
								if (Int32.TryParse(selection, out enc))
								{
									try
									{
										destEncoding = Encoding.GetEncoding(enc);
									}
									catch (Exception e)
									{
										WriteErrorToConsole(e);
									}
								}
								else
								{
									WriteToConsole(ConsoleColor.Red, null, "Error: Failed to successfully find a text encoder with the code: {0}", selection);
								}
								break;
						}
						break;
					case 2: // Source Folder Path
						WriteTextEncodingMenu(pathMenu, sourcePathHeadline, clearConsole);
						selection = Console.ReadLine();

						int tmp3 = -1;
						Int32.TryParse(selection, out tmp3);

						switch (tmp3)
						{
							case 0:
							default:
								WriteToConsole(null, null, "Using Default Path:");
								path = DEFAULT_SOURCE_PATH;
								WriteToConsole(ConsoleColor.Green, null, "'{0}'", path);
								break;
							case 1:
								WriteToConsole(null, null, "Type in a folder path...");
								path = Console.ReadLine();

								if (String.IsNullOrEmpty(path))
								{
									path = DEFAULT_SOURCE_PATH;
									WriteToConsole(ConsoleColor.Green, null, "'{0}'", path);
								}
								else if (!String.IsNullOrEmpty(new FileInfo(path).Extension))
								{
									int lastSlashIndex = path.LastIndexOf('\\');
									filename = path.Substring(lastSlashIndex + 1, path.Length - 1 - lastSlashIndex);

									path = path.Substring(0, lastSlashIndex);
								}
								break;
						}

						if (!String.IsNullOrEmpty(path) && !String.IsNullOrEmpty(filename))
							sourceEncoding = GetFileEncoding(Path.Combine(path, filename));

						if (!String.IsNullOrEmpty(path) && !String.IsNullOrEmpty(filename))
							fullPath = Path.Combine(path, filename);
						break;
					case 3: // Source File Name
						WriteTextEncodingMenu(fileMenu, sourceFileHeadline, clearConsole);
						selection = Console.ReadLine();

						int tmp4 = -1;
						Int32.TryParse(selection, out tmp4);

						switch (tmp4)
						{
							case 0:
							default:
								WriteToConsole(null, null, "Using Default File Name:");
								filename = DEFAULT_SOURCE_FILENAME;
								WriteToConsole(ConsoleColor.Green, null, "'{0}'", filename);
								break;
							case 1:
								WriteToConsole(null, null, "Type in a file name...");
								filename = Console.ReadLine();
								FileInfo fi = new FileInfo(filename);

								if (String.IsNullOrEmpty(filename))
								{
									filename = DEFAULT_SOURCE_FILENAME;
									WriteToConsole(ConsoleColor.Green, null, "'{0}'", filename);
								}
								else if (fi.DirectoryName != Environment.CurrentDirectory)
								{
									path = fi.DirectoryName;
									filename = fi.Name;
								}
								break;
						}

						if (!String.IsNullOrEmpty(path) && !String.IsNullOrEmpty(filename))
							sourceEncoding = GetFileEncoding(Path.Combine(path, filename));

						if (!String.IsNullOrEmpty(path) && !String.IsNullOrEmpty(filename))
							fullPath = Path.Combine(path, filename);
						break;
					case 4: // Destination Folder Path
						WriteTextEncodingMenu(pathMenu, destPathHeadline, clearConsole);
						selection = Console.ReadLine();

						int tmp5 = -1;
						Int32.TryParse(selection, out tmp5);

						switch (tmp5)
						{
							case 0:
							default:
								WriteToConsole(null, null, "Using Default Path:");
								newPath = DEFAULT_DEST_PATH;
								WriteToConsole(ConsoleColor.Green, null, "'{0}'", newPath);
								break;
							case 1:
								WriteToConsole(null, null, "Type in a folder path...");
								newPath = Console.ReadLine();

								if (String.IsNullOrEmpty(newPath))
								{
									newPath = DEFAULT_DEST_PATH;
									WriteToConsole(ConsoleColor.Green, null, "'{0}'", newPath);
								}
								else if (!String.IsNullOrEmpty(new FileInfo(newPath).Extension))
								{
									int lastSlashIndex = newPath.LastIndexOf('\\');
									newFilename = newPath.Substring(lastSlashIndex + 1, newPath.Length - 1 - lastSlashIndex);

									newPath = newPath.Substring(0, lastSlashIndex);
								}
								break;
						}
						break;
					case 5: // Destination File Name
						WriteTextEncodingMenu(fileMenu, destFileHeadline, clearConsole);
						selection = Console.ReadLine();

						if (!String.IsNullOrEmpty(newFilename))
							newFilename = String.Empty;

						int tmp6 = -1;
						Int32.TryParse(selection, out tmp6);

						switch (tmp6)
						{
							case 0:
							default:
								WriteToConsole(null, null, "Using Default File Name:");
								newFilename = DEFAULT_DEST_FILENAME(filename, destEncoding);
								WriteToConsole(ConsoleColor.Green, null, "'{0}'", newFilename);
								break;
							case 1:
								WriteToConsole(null, null, "Type in a file name...");
								newFilename = Console.ReadLine();
								FileInfo fi = new FileInfo(newFilename);

								if (String.IsNullOrEmpty(newFilename))
								{
									newFilename = DEFAULT_DEST_FILENAME(filename, destEncoding);
									WriteToConsole(ConsoleColor.Green, null, "'{0}'", newFilename);
								}
								else if (fi.DirectoryName != Environment.CurrentDirectory)
								{
									newPath = fi.DirectoryName;
									newFilename = fi.Name;
								}
								break;
						}
						break;
					case 6: // Start Encoding
						if (sourceEncoding == null || 
							destEncoding == null || 
							String.IsNullOrEmpty(path) || 
							String.IsNullOrEmpty(filename) || 
							String.IsNullOrEmpty(newPath) || 
							String.IsNullOrEmpty(newFilename))
						{
							WriteToConsole(ConsoleColor.Yellow, null, "Encoding Stopped.  Cannot encode with missing values, set values first!");
							WriteToConsole(ConsoleColor.Green, null, "Press Enter to Return to Menu...");
							Console.ReadLine();
							continue;
						}
						else
						{
							// Get text from file
							string text = ReadTextFromFile(fullPath, sourceEncoding);

							if (!String.IsNullOrEmpty(text))
							{
								// Convert the file to a byte array
								byte[] conversion = ConvertTextEncoding(text, sourceEncoding, destEncoding);

								if (conversion.Length > 1)
								{
									// Write byte array to file
									SaveBytesToFile(conversion, newPath, newFilename, destEncoding);
								}
							}
						}
						break;
					default: goToMainMenu = true; continue;
				}
			}
		}

		static byte[] ConvertTextEncoding(string message, Encoding src, Encoding dest)
		{
			try
			{
				WriteToConsole(ConsoleColor.White, null, "Encoding String...");
				byte[] srcBytes = src.GetBytes(message);
				byte[] destBytes = Encoding.Convert(src, dest, srcBytes);
				WriteToConsole(ConsoleColor.Green, null, "Done!\r\n");

				return destBytes;
			}
			catch (Exception e)
			{
				WriteErrorToConsole(e);
				return new byte[] { 0 };
			}
		}

		static string ReadTextFromFile(string path, Encoding srcEncoding)
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

					char block = '■';
					string bar = String.Empty;
					string space = String.Empty;
					float percent = 0;

					Console.ForegroundColor = ConsoleColor.Green;

					for (int i = 0; i < stream.Length; i++)
					{ 
						percent = ((float)i / (float)bytes.Length) * 100;
						bar = new string(block, (int)(Math.Round(percent / 7, MidpointRounding.AwayFromZero)));
						space = new string(' ', 14 - (int)(Math.Round(percent / 7, MidpointRounding.AwayFromZero)));
									
						Console.Write(String.Format("{0} {1}%",bar + space, Math.Round(percent, MidpointRounding.AwayFromZero).ToString()));
						Console.SetCursorPosition(0, Console.CursorTop);
						bytes[i] = (byte)stream.ReadByte();
					}

					//byte[] outBytes = Encoding.Convert(Encoding.Default, srcEncoding, bytes.ToArray<byte>());
					value = srcEncoding.GetString(bytes);
				}
				catch (Exception e)
				{
					WriteErrorToConsole(e);
				}
			}

			return value;
		}

		static void SaveBytesToFile(byte[] message, string path, string filename, Encoding destEncoding)
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
					using (BinaryWriter writer = new BinaryWriter(stream, destEncoding))
					{
						// Write message to the file
						//for (int i = 0; i < message.Length; i++)
						//{
						//	stream.WriteByte(message[i]);
						//}
						writer.Write(message);

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
						WriteToConsole(ConsoleColor.Green, null, "Validation Complete!");
						WriteToConsole(ConsoleColor.Green, null, "Press Enter to continue...");
						Console.ReadLine();
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
			WriteToConsole(ConsoleColor.Green, null, "Press Enter to Return to Menu...");
			Console.ReadLine();
		}

		static void WriteMenu(bool clearConsole = true)
		{
			WriteMenu(
				new string[] {
				"0.  Linked List\r\n",
				"1.  Remove Characters\r\n",
				"2.  Reverse Words\r\n",
				"3.  Rotate Image (Multidementional Array)\r\n",
				"4.  Validate Braces\r\n",
				"5.  Final Price\r\n",
				"6.  Car Pricer\r\n",
				"7.  Convert Text Encoding\r\n",
				"8.  Check a file is valid UTF-8\r\n",
				"9.  Generate Unicode Test Files\r\n",
				"10. Convert Text Encoding with Class\r\n",
				"\r\nApplication Settings:\r\n",
				"11.  Toggle Clear Console\r\n",
				"12. Exit"
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

		static void WriteTextEncodingMenu(string[] menu, string headline, bool clearConsole = true)
		{
			string[] m = new string[menu.Length + 2];
			Array.Copy(menu, m, menu.Length);

			m[m.Length - 2] = "\r\nSettings:\r\n";
			m[m.Length - 1] = String.Format("{0}. Return To Main Menu", menu.Length);

			WriteMenu(m,
			headline,
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
