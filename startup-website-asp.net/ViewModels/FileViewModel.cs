using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Models
{
	public class FileViewModel
	{
		[Required(ErrorMessage = "Please select file.")]
		[Display(Name = "Browse File")]
		public HttpPostedFileBase[] files { get; set; }
	}
}