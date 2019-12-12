using System.Net;

namespace Spider.Models
{
	public class ValidationResult
	{
		public bool Result { get; set; }

		public string ErrorMessage { get; set; }

        public HttpStatusCode StatusCode { get; set; }
	}
}
