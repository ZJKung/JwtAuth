﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
	public class User
	{
		public Guid Id{ get; set; }

		public string Username{ get; set; }

		public string Password { get; set; }

		public RefreshToken RefreshToken { get; set; }
	}
}
