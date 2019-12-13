namespace SeoSpider.AgiltyTest
{
	public static class LongExtensions
	{
		public static string UserTimeElapsed(this long elapsedMs)
		{
			var userMessage = string.Empty;
			var countDown = elapsedMs;

			while (countDown != 0)
			{
				// First split in hours (if we can)
				if (countDown >= (1000 * 60 * 60))
				{
					var decimalValue = (decimal)countDown / (decimal)(1000 * 60 * 60);
					userMessage += (int)decimalValue + "h ";
					countDown = countDown - (long)((1000 * 60 * 60) * (int)decimalValue);
				}
				else if (countDown >= (1000 * 60))
				{
					var decimalValue = (decimal)countDown / (decimal)(1000 * 60);
					userMessage += (int)decimalValue + "m ";
					countDown = countDown - (long)((1000 * 60) * (int)decimalValue);
				}
				else if (countDown >= (1000))
				{
					var decimalValue = (decimal)countDown / (decimal)1000;
					userMessage += (int)decimalValue + "s ";
					countDown = countDown - (long)(1000 * (int)decimalValue);
				}
				else if (countDown < 1000)
				{
					userMessage += countDown + "ms ";
					countDown = 0;
				}
			}

			return userMessage;
		}

	}
}
