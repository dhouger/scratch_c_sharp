using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace scratch_c_sharp
{
	class EncodingValidation
	{
		/// <summary>
		/// List of all text encoding formats available in .NET
		/// </summary>
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

		private string DEFAULT_SOURCE_PATH = Environment.CurrentDirectory;
		private string DEFAULT_SOURCE_FILENAME = "output.markdown";
		private string DEFAULT_DEST_PATH = Environment.CurrentDirectory;
		private string DEFAULT_DEST_FILENAME(string newFilename, Encoding destEncoding)
		{
			string output = String.IsNullOrEmpty(newFilename) ? "output.markdown" : newFilename;
			if (destEncoding == null)
				output = output.Insert(output.IndexOf(new FileInfo(output).Extension), "_UNKNOWN");
			else output = output.Insert(output.IndexOf(new FileInfo(output).Extension), "_" + destEncoding.EncodingName.Replace(' ', '-'));
			return output;
		}

		#region Constructors
		/// <summary>
		/// Constructor that allows specification on text encoding using the encoding codepage id
		/// </summary>
		/// <param name="sourceEncoding">Source file's text encoding codepage value</param>
		/// <param name="destEncoding">Destination file's text encoding codepage value</param>
		/// <param name="sourcePath">Source file folder path</param>
		/// <param name="sourceFilename">Source file name</param>
		/// <param name="destPath">Destination file folder</param>
		/// <param name="destFilename">Destination file name</param>
		public EncodingValidation(int sourceEncoding, int destEncoding, string sourcePath, string sourceFilename, string destPath, string destFilename)
		{
			Encoding src = Encoding.GetEncoding(sourceEncoding);
			Encoding dest = Encoding.GetEncoding(destEncoding);

			Initialize(src, dest, sourcePath, sourceFilename, destPath, destFilename);
		}

		/// <summary>
		/// Constructor that allows for direct specification on source and destination encoding
		/// </summary>
		public EncodingValidation(Encoding sourceEncoding, Encoding destEncoding, string sourcePath, string sourceFilename, string destPath, string destFilename)
		{
			Initialize(sourceEncoding, destEncoding, sourcePath, sourceFilename, destPath, destFilename);
		}

		/// <summary>
		/// Constructor that uses default encoding
		/// </summary>
		public EncodingValidation(string sourcePath, string sourceFilename, string destPath, string destFilename)
		{
			Encoding src = GetFileEncoding(Path.Combine(sourcePath, sourceFilename));
			Encoding dest = Encoding.UTF8;

			Initialize(src, dest, sourcePath, sourceFilename, destPath, destFilename);
		}

		/// <summary>
		/// Constructor that allows specification of encoding types and parses full file paths for the source and destination
		/// </summary>
		/// <param name="source">Expects full file path with extension: "C:\Example\sample.txt"</param>
		/// <param name="destination">Expects full file path with extension: "C:\Example\sample.txt"</param>
		public EncodingValidation(Encoding sourceEncoding, Encoding destEncoding, string source, string destination)
		{
			// Split full paths into folder path and file name
			// Source
			int lastSlashIndex = source.LastIndexOf('\\') - 1;
			string sourceFilename = source.Substring(lastSlashIndex, source.Length - 1 - lastSlashIndex);

			string sourcePath = source.Substring(0, lastSlashIndex);

			// Destination
			lastSlashIndex = destination.LastIndexOf('\\') - 1;
			string destFilename = destination.Substring(lastSlashIndex, destination.Length - 1 - lastSlashIndex);

			string destPath = destination.Substring(0, lastSlashIndex);

			Initialize(sourceEncoding, destEncoding, sourcePath, sourceFilename, destPath, destFilename);
		}

		/// <summary>
		/// Constructor that uses default encoding values and parses full file paths for the source and destination
		/// </summary>
		public EncodingValidation(string source, string destination)
		{
			// Split full paths into folder path and file name
			// Source
			int lastSlashIndex = source.LastIndexOf('\\') - 1;
			string sourceFilename = source.Substring(lastSlashIndex, source.Length - 1 - lastSlashIndex);

			string sourcePath = source.Substring(0, lastSlashIndex);

			// Destination
			lastSlashIndex = destination.LastIndexOf('\\') - 1;
			string destFilename = destination.Substring(lastSlashIndex, destination.Length - 1 - lastSlashIndex);

			string destPath = destination.Substring(0, lastSlashIndex);

			Encoding src = GetFileEncoding(source);
			Encoding dest = Encoding.UTF8;

			Initialize(src, dest, sourcePath, sourceFilename, destPath, destFilename);
		}

		/// <summary>
		/// Empty constructor, used to initialize the class without specifying values
		/// </summary>
		public EncodingValidation()
		{

		}
		#endregion

		#region Public Functions
		/// <summary>
		/// Start checking encoding for a specified file
		/// </summary>
		/// <returns>Success of the function</returns>
		public bool Initialize(Encoding sourceEncoding, Encoding destEncoding, string sourcePath, string sourceFilename, string destPath, string destFilename)
		{
			try
			{
				if (!FileIsValidUTF8(Path.Combine(sourcePath, sourceFilename)))
				{
					if (String.IsNullOrEmpty(sourcePath) ||
						String.IsNullOrEmpty(sourceFilename) ||
						String.IsNullOrEmpty(destPath) ||
						String.IsNullOrEmpty(destFilename))
					{
						LogMessage(ConsoleColor.Yellow, null, "Encoding Stopped.  Cannot encode with missing values, set values first!");
					}
					else
					{

						// Get text from file
						string text = ReadTextFromFile(Path.Combine(sourcePath, sourceFilename), sourceEncoding);

						if (!String.IsNullOrEmpty(text))
						{
							// Convert the file to a byte array
							byte[] conversion = ConvertTextEncoding(text, sourceEncoding, destEncoding);

							if (conversion.Length > 1)
							{
								// Write byte array to file
								SaveBytesToFile(conversion, destPath, destFilename, destEncoding);
							}
						}
					}
				}

				return true;
			}
			catch (Exception e)
			{
				LogError(e);
				return false;
			}
		}

		/// <summary>
		/// Validates a file's text for UTF-8 validity
		/// </summary>
		/// <param name="filepath">The full path to the file to validate</param>
		/// <returns>bool</returns>
		public static bool FileIsValidUTF8(string filepath)
		{
			try
			{
				byte[] b = File.ReadAllBytes(filepath);

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
				//LogError(e); TODO
				return true; // Defaulting to 'true' to prevent text encoding from continuing to process
			}

			return true;
		}

		/// <summary>
		/// Takes a filepath and returns the encoding of the text.  This is not accurate, but for the purposes of converting to a particular text encoding, using the desired destination encode should work
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns>Encoding</returns>
		public static Encoding GetFileEncoding(string filePath)
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
				//LogError(e); TODO
				return null;
			}
		}
		#endregion

		#region Private Functions
		/// <summary>
		/// Takes a text string, a source encoding, and a destination encoding and converts the text into a byte array of the destination encoding
		/// </summary>
		/// <param name="message">The string to convert</param>
		/// <param name="src">The source text encoding</param>
		/// <param name="dest">The destination text encoding</param>
		/// <returns>byte[]</returns>
		private byte[] ConvertTextEncoding(string message, Encoding src, Encoding dest)
		{
			try
			{
				LogMessage(ConsoleColor.White, null, "Encoding String...");
				byte[] srcBytes = src.GetBytes(message);
				byte[] destBytes = Encoding.Convert(src, dest, srcBytes);
				LogMessage(ConsoleColor.Green, null, "Done!\r\n");

				return destBytes;
			}
			catch (Exception e)
			{
				LogError(e);
				return new byte[] { 0 };
			}
		}

		/// <summary>
		/// Takes a file path and source encoding and returns the text from the file in the specified encoding
		/// </summary>
		/// <param name="path">The file path to pull read from</param>
		/// <param name="srcEncoding">The text encoding of the file</param>
		/// <returns></returns>
		private string ReadTextFromFile(string path, Encoding srcEncoding)
		{
			if (!File.Exists(path))
			{
				LogMessage(ConsoleColor.Red, null, "Error! File does not exist at: '{0}'", path);
				return String.Empty;
			}

			string value = String.Empty;

			LogMessage(ConsoleColor.White, null, "Reading file from: '{0}'", path);
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

						Console.Write(String.Format("{0} {1}%", bar + space, Math.Round(percent, MidpointRounding.AwayFromZero).ToString()));
						Console.SetCursorPosition(0, Console.CursorTop);
						bytes[i] = (byte)stream.ReadByte();
					}

					//byte[] outBytes = Encoding.Convert(Encoding.Default, srcEncoding, bytes.ToArray<byte>());
					value = srcEncoding.GetString(bytes);
				}
				catch (Exception e)
				{
					LogError(e);
				}
			}

			return value;
		}

		/// <summary>
		/// Saves byte array to the specified filepath with the specified text encoding
		/// </summary>
		/// <param name="message">Bytes to encode</param>
		/// <param name="path">Folder path to use</param>
		/// <param name="filename">File name to use</param>
		/// <param name="destEncoding">Encoding to use</param>
		private void SaveBytesToFile(byte[] message, string path, string filename, Encoding destEncoding)
		{
			// Check path
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
				LogMessage(ConsoleColor.Yellow, null, "Created new directory at: '{0}'\r\n", path);
			}

			if (GrantAccess(path))
			{
				string fullpath = Path.Combine(path, filename);
				LogMessage(ConsoleColor.Yellow, null, "Checking file: '{0}'", fullpath);

				// Delete the file if it exists
				if (File.Exists(fullpath))
				{
					File.Delete(fullpath);
					LogMessage(ConsoleColor.Yellow, null, "Found file at: '{0}'\r\nDeleted file...\r\n", fullpath);
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
									LogMessage(ConsoleColor.Red, null, "Error! Write stream failed to validate properly!");
								}
							}
							catch (Exception e)
							{
								LogError(e, "An Error Occurred During Validation:\r\n\r\nMessage:\r\n{0}\r\n\r\nStack Trace:\r\n{1}\r\n");
							}
						}
						LogMessage(ConsoleColor.Green, null, "Validation Complete!");
						LogMessage(ConsoleColor.Green, null, "Press Enter to continue...");
						Console.ReadLine();
					}
				}
				catch (Exception e)
				{
					LogError(e);
				}
			}
		}

		/// <summary>
		/// Checks and grants access to the specified folder path
		/// </summary>
		/// <param name="path">Path to check and grant access to</param>
		/// <returns>Function success</returns>
		private bool GrantAccess(string path)
		{
			DirectoryInfo di = new DirectoryInfo(path);
			var ds = di.GetAccessControl();

			try
			{
				DirectorySecurity security = Directory.GetAccessControl(path);
				LogMessage(ConsoleColor.Green, null, "Console has access to: '{0}'", path);
			}
			catch (Exception)
			{
				try
				{
					LogMessage(ConsoleColor.Yellow, null, "Console requesting access to: '{0}'", path);
					ds.AddAccessRule(new FileSystemAccessRule(
						new SecurityIdentifier(WellKnownSidType.SelfSid, null),
						FileSystemRights.FullControl,
						InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
						PropagationFlags.NoPropagateInherit,
						AccessControlType.Allow));
					di.SetAccessControl(ds);
					LogMessage(ConsoleColor.Green, null, "Done!", null);
				}
				catch (Exception ex)
				{
					LogError(ex, "Error requesting access to: '{0}'");
					return false;
				}
			}

			return true;
		}

		// Text logging functions that will need to be re-written in Halcyon
		private void LogMessage(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string message, params string[] args)
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

		private void LogError(Exception e)
		{
			LogMessage(ConsoleColor.Red, null, "An Error Occurred:\r\n\r\nMessage:\r\n{0}\r\n\r\nStack Trace:\r\n{1}\r\n", e.Message, e.StackTrace);
		}

		private void LogError(Exception e, string message)
		{
			string m = message;
			if (String.IsNullOrEmpty(message))
				m = "An Error Occurred:\r\n\r\nMessage:\r\n{0}\r\n\r\nStack Trace:\r\n{1}\r\n";

			LogMessage(ConsoleColor.Red, null, m, e.Message, e.StackTrace);
			LogMessage(ConsoleColor.Green, null, "Press Enter to Return to Menu...");
			//Console.ReadLine();
		}
		#endregion
	}
}
