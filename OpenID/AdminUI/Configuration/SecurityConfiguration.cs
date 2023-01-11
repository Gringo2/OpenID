using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpsPolicy;
using System;
using System.Collections.Generic;

namespace OpenID.AdminUI.Configuration
{
	public class SecurityConfiguration
	{
		/// <summary>
		/// Use the developer exception page instead of the Identity error handler.
		/// </summary>
		public bool UseDeveloperExceptionPage { get; set; } = false;

		/// <summary>
		/// Enable HSTS in responses.
		/// </summary>
		public bool UseHsts { get; set; } = true;
	}
}
