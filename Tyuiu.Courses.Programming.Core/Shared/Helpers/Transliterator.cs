namespace Tyuiu.Courses.Programming.Core.Shared.Helpers
{
	public static class Transliterator
	{
		public static string CyrillicToLatin(string cyrillicText)
		{
			var cyrillicToLatinMapping = new Dictionary<string, string>
		{
			{"а", "a"}, {"б", "b"}, {"в", "v"}, {"г", "g"}, {"д", "d"}, {"е", "e"},
			{"ё", "e"}, {"ж", "zh"}, {"з", "z"}, {"и", "i"}, {"й", "i"}, {"к", "k"},
			{"л", "l"}, {"м", "m"}, {"н", "n"}, {"о", "o"}, {"п", "p"}, {"р", "r"},
			{"с", "s"}, {"т", "t"}, {"у", "u"}, {"ф", "f"}, {"х", "kh"}, {"ц", "ts"},
			{"ч", "ch"}, {"ш", "sh"}, {"щ", "shch"}, {"ь", ""}, {"ы", "y"}, {"ъ", ""},
			{"э", "e"}, {"ю", "yu"}, {"я", "ya"},
			{"А", "A"}, {"Б", "B"}, {"В", "V"}, {"Г", "G"}, {"Д", "D"}, {"Е", "E"},
			{"Ё", "E"}, {"Ж", "Zh"}, {"З", "Z"}, {"И", "I"}, {"Й", "I"}, {"К", "K"},
			{"Л", "L"}, {"М", "M"}, {"Н", "N"}, {"О", "O"}, {"П", "P"}, {"Р", "R"},
			{"С", "S"}, {"Т", "T"}, {"У", "U"}, {"Ф", "F"}, {"Х", "Kh"}, {"Ц", "Ts"},
			{"Ч", "Ch"}, {"Ш", "Sh"}, {"Щ", "Shch"}, {"Ь", ""}, {"Ы", "Y"}, {"Ъ", ""},
			{"Э", "E"}, {"Ю", "Yu"}, {"Я", "Ya"}
		};

			return string.Join("", cyrillicText.ToCharArray().Select(c =>
			{
				var chr = c.ToString();
				return cyrillicToLatinMapping.ContainsKey(chr) ? cyrillicToLatinMapping[chr] : chr;
			}));
		}
	}
}
